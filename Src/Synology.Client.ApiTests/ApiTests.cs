using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using FluentAssertions;
using NUnit.Framework;

namespace SynologyClient.ApiTests
{
    [TestFixture]
    public class ApiTests
    {
        [SetUp]
        public void setup()
        {
            _session = new SynologySession(new SynologyClientConfig());
            _session.Login();
            _api = new SynologyApi(_session);
        }

        [TearDown]
        public void TearDown()
        {
            if (_session != null)
                _session.LogOut();
        }

        public ApiTests()
        {
            var executingAssembly = new FileInfo(Assembly.GetExecutingAssembly().FullName);
            _localTestFolderNoSlash = executingAssembly.DirectoryName + "\\TestFiles";
            _localTestImage = _localTestFolderNoSlash + "\\image\\synologybox.jpg";
        }

        private readonly string _synoTestFolderNoSlash = "/public/apitest";
        private static string _localTestFolderNoSlash;
        private readonly string _localTestImage;

        private SynologySession _session;
        private SynologyApi _api;

        public ApiTests(string synoTestFolderNoSlash)
        {
            _synoTestFolderNoSlash = synoTestFolderNoSlash;
        }

        [Test]
        public void FileStationInfo()
        {
            InfoGetInfoResponse res = _api.GetDiskstationInfo();
            res.success.Should().BeTrue();
        }


        [Test]
        public void FileStationList()
        {
            ListGetInfoResponse getinfo = _api.GetFileSystemInfo(new[] { "/Data" },
                new SynologyApi.FileGetInfoAddtionalOptions
                {
                    real_path = true,
                    size = true,
                    owner = true,
                    time = true,
                    mount_point_type = true,
                    perm = true,
                    type = true
                });
            getinfo.success.Should().BeTrue();


            FileSystemListResponse fileSystemList = _api.GetFileSystemEntries("/Data", 0, 10, SynologyApi.SortBy.name,
                SynologyApi.SortDirection.asc, "*", SynologyApi.FileTypeFilter.all, null,
                new SynologyApi.FileListAddtionalOptions
                {
                    real_path = true,
                    time = true,
                    mount_point_type = true,
                    owner = true,
                    perm = true,
                    size = true,
                    volume_status = true
                });

            fileSystemList.success.Should().BeTrue();

            ListListShareResponse listShare = _api.GetShares(0, 0, SynologyApi.SortBy.name,
                SynologyApi.SortDirection.asc, false, new SynologyApi.FileListAddtionalOptions
                {
                    size = true,
                    time = true,
                    real_path = true,
                    mount_point_type = true,
                    owner = true,
                    perm = true
                });

            listShare.success.Should().BeTrue();
            listShare.Data.shares.Count().Should().BeGreaterOrEqualTo(1);
        }

        [Test]
        public void FileStationSearch()
        {
            SearchStartResponse start = _api.SearchStart(_synoTestFolderNoSlash);

            start.success.Should().BeTrue();

            string taskid = start.Data.taskid;
            taskid.Should().NotBeNullOrEmpty();

            for (int i = 0; i < 10; i++)
            {
                SearchListResponse list = _api.Searches(taskid, 0, 100, SynologyApi.SortBy.name,
                    SynologyApi.SortDirection.asc, new[] { "*" }, SynologyApi.FileTypeFilter.all,
                    new SynologyApi.FileSearchListAddtionalOptions
                    {
                        owner = true,
                        perm = true,
                        real_path = true,
                        size = true,
                        time = true,
                        type = true
                    });
                list.success.Should().BeTrue();

                if (list.Data.finished)
                    break;

                Thread.Sleep(2000);
            }

            _api.SearchStop(taskid).success.Should().BeTrue();
            _api.SearchClean(taskid).success.Should().BeTrue();
        }

        [Test]
        public void FileStationVirtualFolder()
        {
            VirtualFolderListResponse list = _api.VirtualFolderList(SynologyApi.FileSystemType.cifs, 0, 0,
                SynologyApi.SortBy.name, SynologyApi.SortDirection.asc,
                new SynologyApi.VirtualFolderListAddtionalOptions
                {
                    owner = true,
                    perm = true,
                    real_path = true,
                    size = true,
                    time = true,
                    volume_status = true
                });

            list.success.Should().BeTrue();
        }

        [Test]
        public void FileStationFavorite()
        {
            FavoriteListResponse list = _api.FavoriteList(0, 0, SynologyApi.StatusFilter.all,
                new SynologyApi.FileStationFavoriteAddtionalOptions
                {
                    mount_point_type = true,
                    owner = true,
                    perm = true,
                    real_path = true,
                    size = true,
                    time = true
                });

            list.success.Should().BeTrue();

            var testFavoritesDest = _synoTestFolderNoSlash + "/fav_test";
            var testFavoritesName = _synoTestFolderNoSlash + "/fav_test";

            FavoriteDeleteResponse delete = _api.FavoriteDelete(testFavoritesDest);
            delete.success.Should().BeTrue();

            FavoriteAddResponse add = _api.FavoriteAdd(testFavoritesDest, testFavoritesName);
            add.success.Should().BeTrue();

            FavoriteEditResponse edit = _api.FavoriteEdit(testFavoritesDest, testFavoritesName);
            edit.success.Should().BeTrue();

            FavoritReplaceAllResponse replace = _api.FavoriteReplaceAll(testFavoritesDest, testFavoritesName);
            replace.success.Should().BeTrue();

            FavoriteClearBrokenResponse clear = _api.FavoriteClearBroken(testFavoritesDest, testFavoritesName);
            clear.success.Should().BeTrue();
        }



        [Test]
        public void SynoFileStationCompress()
        {
            _api.Upload(new FileInfo(_localTestImage), _synoTestFolderNoSlash + "/test_compress");

            CompressStartResponse start =
                _api.CompressStart(_synoTestFolderNoSlash + "/test_compress",
                    _synoTestFolderNoSlash + "/test_compress.zip", 
                    SynologyApi.CompressionLevel.moderate, SynologyApi.CompressionMode.add, 
                    SynologyApi.CompressionFormat.formatZip);

            start.success.Should().BeTrue();

            start.Data.taskid.Should().NotBeNullOrEmpty();

            for (int i = 0; i < 10; i++)
            {
                CompressStatusResponse status = _api.CompressStatus(start.Data.taskid);
                status.success.Should().BeTrue();

                if (status.Data.finished)
                    break;

                Thread.Sleep(2000);
            }

            BaseSynologyResponse stop = _api.CompressStop(start.Data.taskid);
            stop.success.Should().BeTrue();
        }


        [Test]
        public void SynoFileStationThumbGet()
        {
            byte[] thumb = _api.GetThumb("/homes/allanb/20130524059.jpg");
            //File.WriteAllBytes("c:\\test\\20130524059.jpg", res);
            thumb.Length.Should().BeGreaterThan(1);
            
        }

        [Test]
        public void SynoFileStationDirSize()
        {
            DirSizeStartResponse start = _api.GetDirectorySizeStart("/photo");

            start.success.Should().BeTrue();
            start.Data.taskid.Should().NotBeNullOrEmpty();

            for (int i = 0; i < 10; i++)
            {
                
                DirSizeStatusResponse status = _api.GetDirectorySizeStatus(start.Data.taskid);
                status.success.Should().BeTrue();
                if (status.Data.finished)
                    break;

                Thread.Sleep(2000);
            }

            _api.GetDirectorySizeStatus(start.Data.taskid).success.Should().BeTrue();
        }

        [Test]
        public void SynoFileStationMd5()
        {
            Md5StartResponse start = _api.GetFileMd5Start("/homes/allanb/20130524059.jpg");

            start.success.Should().BeTrue();

            start.Data.taskid.Should().NotBeNullOrEmpty();

            for (int i = 0; i < 10; i++)
            {
                Md5StatusResponse status = _api.GetFileMd5Status(start.Data.taskid);
                status.success.Should().BeTrue();
                if (status.Data.finished)
                    break;
                
                Thread.Sleep(2000);
            }

            _api.GetDirectorySizeStop(start.Data.taskid).success.Should().BeTrue();
        }

        [Test]
        public void FileStationCheckPermission()
        {
            RawSynologyResponse check = _api.CheckWritePermission("/Data");
            Assert.Pass();
        }

        [Test]
        public void SynoFileStationUpload_Download()
        {
            var local = new FileInfo(_localTestImage);
            string dest = _synoTestFolderNoSlash;
            string downloadedName = _localTestFolderNoSlash + "\\dload_" + local.Name;

            if (System.IO.File.Exists(downloadedName))
                System.IO.File.Delete(downloadedName);

            RawSynologyResponse res = _api.Upload(new FileInfo(_localTestImage), dest, true, true);
            res.success.Should().BeTrue();

            Thread.Sleep(3000);

            byte[] file = _api.Download(dest + "/" + local.Name);
            file.Length.Should().Be((int)local.Length);
        }

        [Test]
        public void SynoFileStationSharing()
        {
            SharingCreateResponse create = _api.CreateShare(_synoTestFolderNoSlash);
            create.success.Should().BeTrue();

            SharingGetInfoResponse info = _api.GetSharingInfo(create.Data.links.First().id);
            info.success.Should().BeTrue();

            SharingEditResponse edit = _api.EditShare(create.Data.links.First().id);
            edit.success.Should().BeTrue();

            _api.DeleteShare(create.Data.links.First().id);

            _api.ClearInvalidShares(create.Data.links.First().id);
        }

        [Test]
        public void SynoFileStationCreateFolder()
        {
            BaseSynologyResponse create = _api.CreateFolder(_synoTestFolderNoSlash, "newfolder");
            create.success.Should().BeTrue();
        }

        [Test]
        public void SynoFileStationRename()
        {
            var delete = _api.Delete(_synoTestFolderNoSlash + "/newfolder_renamed");

            RenameResponse renamed = _api.FIleSystemRename(_synoTestFolderNoSlash + "/newfolder",
                "newfolder_renamed");
            renamed.success.Should().BeTrue();
        }

        [Test]
        public void SynoFileStationCopyMove()
        {
            _api.CreateFolder(_synoTestFolderNoSlash, "test");
            CopyMoveStartResponse start = _api.CopyMoveStart(_synoTestFolderNoSlash + "/synologybox.jpg",
                _synoTestFolderNoSlash + "/test");
            start.success.Should().BeTrue();

            start.Data.taskid.Should().NotBeNullOrEmpty();

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(2000);
                CopyMoveStatusResponse status = _api.CopyMoveStatus(start.Data.taskid);
                status.success.Should().BeTrue();
                if (status.Data.finished)
                    break;
            }

            BaseSynologyResponse stop = _api.CopyMoveStop(start.Data.taskid);
            stop.success.Should().BeTrue();
        }

        [Test]
        public void SynoFileStationDelete()
        {
            _api.Upload(new FileInfo(_localTestImage), _synoTestFolderNoSlash + "/test_upload");
            DeleteStartResponse start =
                _api.DeleteStart(_synoTestFolderNoSlash + "/test_upload/synologybox.jpg");
            start.success.Should().BeTrue();

            start.Data.taskid.Should().NotBeNullOrEmpty();

            for (int i = 0; i < 10; i++)
            {

                DeleteStatusResponse status = _api.DeleteStatus(start.Data.taskid);
                status.success.Should().BeTrue();
                if (status.Data.finished)
                    break;

                Thread.Sleep(2000);
            }

            DeleteStopResponse stop = _api.DeleteStop(start.Data.taskid);
            stop.success.Should().BeTrue();

            _api.Upload(new FileInfo(_localTestImage), _synoTestFolderNoSlash + "/test_upload");
            DeleteSyncResponse deleteSync =
                _api.Delete(_synoTestFolderNoSlash + "/test_upload/synologybox.jpg");
            deleteSync.success.Should().BeTrue();
        }

        [Test]
        public void SynoFileStationExtract()
        {
            ExtractStartResponse start =
                _api.ExtractStart(_synoTestFolderNoSlash + "/test_compress.zip",
                    _synoTestFolderNoSlash + "/test_extract");
            start.success.Should().BeTrue();

            start.Data.taskid.Should().NotBeNullOrEmpty();

            for (int i = 0; i < 10; i++)
            {
                ExtractStatusResponse status = _api.ExtractStatus(start.Data.taskid);
                status.success.Should().BeTrue();
                if (status.Data.finished)
                    break;

                Thread.Sleep(2000);
            }

            ExtractStopResponse stop = _api.ExtractStop(start.Data.taskid);
            stop.success.Should().BeTrue();

            ExtractListResponse list = _api.ExtractListFiles(_synoTestFolderNoSlash + "/test_compress.zip");
            list.success.Should().BeTrue();
        }

        [Test]
        public void BackgroundTask()
        {

            BackgroundTaskListResponse list = _api.GetBackgroundTasks(0, 0,
                SynologyApi.BackgroundTaskSortBy.finished, SynologyApi.SortDirection.asc);
            list.success.Should().BeTrue();
        }
    }
}
