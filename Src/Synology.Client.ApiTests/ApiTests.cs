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
        public void Setup()
        {
            if (_session == null) {
                _session = new SynologySession(new SynologyClientConfig());
                _session.Login();
                _api = new SynologyApi(_session);
            }
        }

        [TearDown]
        public void TearDown()
        {
            if (_session.loggedInTime < DateTime.UtcNow.AddMinutes(-5)) {
                _session.LogOut();
                _session = null;
            }
        }

        public ApiTests()
        {
            var executingAssembly = new FileInfo(Assembly.GetExecutingAssembly().FullName);
            _localTestFolderNoSlash = executingAssembly.DirectoryName + "\\TestFiles";
            _localTestImage = _localTestFolderNoSlash + "\\image\\synologybox.jpg";
            _session = new SynologySession(new SynologyClientConfig());
            _session.Login();
            _api = new SynologyApi(_session);
        }

        public ApiTests(SynologySession session) {}

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
        public void BackgroundTask()
        {
            GetBackgroundTasksResponse list = _api.GetBackgroundTasks(0, 0, SynologyApi.BackgroundTaskSortBy.finished,
                SynologyApi.SortDirection.asc);
            list.success.Should().BeTrue();
        }

        [Test]
        public void CheckPermission()
        {
            RawSynologyResponse check = _api.CheckWritePermission("/Data");
            Assert.Pass();
        }

        [Test]
        public void Compress()
        {
            _api.Upload(new FileInfo(_localTestImage), _synoTestFolderNoSlash + "/test_compress");

            CompressAsyncResposne start = _api.CompressAsync(_synoTestFolderNoSlash + "/test_compress",
                _synoTestFolderNoSlash + "/test_compress.zip", SynologyApi.CompressionLevel.moderate,
                SynologyApi.CompressionMode.add, SynologyApi.CompressionFormat.formatZip);

            start.success.Should().BeTrue();

            start.Data.taskid.Should().NotBeNullOrEmpty();

            for (int i = 0; i < 10; i++) {
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
        public void CopyMove()
        {
            _api.AddFolder(_synoTestFolderNoSlash, "test");
            CopyMoveAsyncResponse @async = _api.CopyMoveAsync(_synoTestFolderNoSlash + "/synologybox.jpg",
                _synoTestFolderNoSlash + "/test");
            async.success.Should().BeTrue();

            async.Data.taskid.Should().NotBeNullOrEmpty();

            for (int i = 0; i < 10; i++) {
                Thread.Sleep(2000);
                CopyMoveStatusResponse status = _api.CopyMoveStatus(async.Data.taskid);
                status.success.Should().BeTrue();
                if (status.Data.finished)
                    break;
            }

            BaseSynologyResponse stop = _api.CopyMoveStop(async.Data.taskid);
            stop.success.Should().BeTrue();
        }

        [Test]
        public void CreateFolder()
        {
            BaseSynologyResponse create = _api.AddFolder(_synoTestFolderNoSlash, "newfolder");
            create.success.Should().BeTrue();
        }

        [Test]
        public void Delete()
        {
            _api.Upload(new FileInfo(_localTestImage), _synoTestFolderNoSlash + "/test_upload");
            DeleteAsyncResponse @async = _api.DeleteAsync(_synoTestFolderNoSlash + "/test_upload/synologybox.jpg");
            async.success.Should().BeTrue();

            async.Data.taskid.Should().NotBeNullOrEmpty();

            for (int i = 0; i < 10; i++) {
                DeleteStatusResponse status = _api.DeleteStatus(async.Data.taskid);
                status.success.Should().BeTrue();
                if (status.Data.finished)
                    break;

                Thread.Sleep(2000);
            }

            DeleteStopResponse stop = _api.DeleteStop(async.Data.taskid);
            stop.success.Should().BeTrue();

            _api.Upload(new FileInfo(_localTestImage), _synoTestFolderNoSlash + "/test_upload");
            DeleteResponse delete = _api.Delete(_synoTestFolderNoSlash + "/test_upload/synologybox.jpg");
            delete.success.Should().BeTrue();
        }

        [Test]
        public void DirSize()
        {
            GetDirectorySizeAsyncResponse @async = _api.GetDirectorySizeAsync(_synoTestFolderNoSlash);

            async.success.Should().BeTrue();
            async.Data.taskid.Should().NotBeNullOrEmpty();

            for (int i = 0; i < 10; i++) {
                DirSizeStatusResponse status = _api.GetDirectorySizeStatus(async.Data.taskid);
                status.success.Should().BeTrue();
                if (status.Data.finished)
                    break;

                Thread.Sleep(2000);
            }

            _api.GetDirectorySizeStatus(async.Data.taskid).success.Should().BeTrue();
        }

        [Test]
        public void Extract()
        {
            ExtractAsyncResponse @async = _api.ExtractAsync(_synoTestFolderNoSlash + "/test_compress.zip",
                _synoTestFolderNoSlash + "/test_extract");
            async.success.Should().BeTrue();

            async.Data.taskid.Should().NotBeNullOrEmpty();

            for (int i = 0; i < 10; i++) {
                ExtractStatusResponse status = _api.ExtractStatus(async.Data.taskid);
                status.success.Should().BeTrue();
                if (status.Data.finished)
                    break;

                Thread.Sleep(2000);
            }

            ExtractStopResponse stop = _api.ExtractStop(async.Data.taskid);
            stop.success.Should().BeTrue();

            ExtractListResponse list = _api.ExtractListFiles(_synoTestFolderNoSlash + "/test_compress.zip");
            list.success.Should().BeTrue();
        }

        [Test]
        public void Favorite()
        {
            FavoriteListResponse list = _api.FavoriteList(0, 0, SynologyApi.StatusFilter.all,
                new SynologyApi.FileStationFavoriteAddtionalOptions {
                    mount_point_type = true,
                    owner = true,
                    perm = true,
                    real_path = true,
                    size = true,
                    time = true
                });

            list.success.Should().BeTrue();

            string testFavoritesDest = _synoTestFolderNoSlash + "/fav_test";
            string testFavoritesName = _synoTestFolderNoSlash + "/fav_test";

            DeleteFavoriteResponse delete = _api.DeleteFavorite(testFavoritesDest);
            delete.success.Should().BeTrue();

            AddFavoriteResponse add = _api.AddFavorite(testFavoritesDest, testFavoritesName);
            add.success.Should().BeTrue();

            EditFavoriteResponse edit = _api.EditFavorite(testFavoritesDest, testFavoritesName);
            edit.success.Should().BeTrue();

            ReplaceFavoriteResponse replace = _api.ReplaceFavorite(testFavoritesDest, testFavoritesName);
            replace.success.Should().BeTrue();

            ClearBrokenFavoritesResponse clear = _api.ClearBrokenFavorites(testFavoritesDest, testFavoritesName);
            clear.success.Should().BeTrue();
        }

        [Test]
        public void FileSystemInfo()
        {
            GetFileSystemInfoResponse getinfo = _api.GetFileSystemInfo(new[] { _synoTestFolderNoSlash },
                new SynologyApi.FileGetInfoAddtionalOptions {
                    real_path = true,
                    size = true,
                    owner = true,
                    time = true,
                    mount_point_type = true,
                    perm = true,
                    type = true
                });
            getinfo.success.Should().BeTrue();

            GetFileSystemEntriesResponse getFileSystemEntries = _api.GetFileSystemEntries(_synoTestFolderNoSlash, 0, 10,
                SynologyApi.SortBy.name, SynologyApi.SortDirection.asc, "*", SynologyApi.FileTypeFilter.all, null,
                new SynologyApi.FileListAddtionalOptions {
                    real_path = true,
                    time = true,
                    mount_point_type = true,
                    owner = true,
                    perm = true,
                    size = true,
                    volume_status = true
                });

            getFileSystemEntries.success.Should().BeTrue();

            GetSharesResponse listShare = _api.GetShares(0, 0, SynologyApi.SortBy.name, SynologyApi.SortDirection.asc,
                false, new SynologyApi.FileListAddtionalOptions {
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
        public void GetDiskstationInfo()
        {
            GetDiskstationInfoResponse res = _api.GetDiskstationInfo();
            res.success.Should().BeTrue();
        }

        [Test]
        public void Md5()
        {
            GetFileMd5AsyncResponse @async = _api.GetFileMd5Async("/homes/allanb/20130524059.jpg");

            async.success.Should().BeTrue();

            async.Data.taskid.Should().NotBeNullOrEmpty();

            for (int i = 0; i < 10; i++) {
                GetFileMd5StatusResponse status = _api.GetFileMd5Status(async.Data.taskid);
                status.success.Should().BeTrue();
                if (status.Data.finished)
                    break;

                Thread.Sleep(2000);
            }

            _api.GetDirectorySizeStop(async.Data.taskid).success.Should().BeTrue();
        }

        [Test]
        public void Rename()
        {
            DeleteResponse delete = _api.Delete(_synoTestFolderNoSlash + "/newfolder_renamed");

            RenameResponse renamed = _api.FileSystemRename(_synoTestFolderNoSlash + "/newfolder", "newfolder_renamed");
            renamed.success.Should().BeTrue();
        }

        [Test]
        public void Search()
        {
            SearchStartResponse start = _api.SearchStart(_synoTestFolderNoSlash);

            start.success.Should().BeTrue();

            string taskid = start.Data.taskid;
            taskid.Should().NotBeNullOrEmpty();

            for (int i = 0; i < 10; i++) {
                SearchesResponse list = _api.Searches(taskid, 0, 100, SynologyApi.SortBy.name,
                    SynologyApi.SortDirection.asc, new[] { "*" }, SynologyApi.FileTypeFilter.all,
                    new SynologyApi.FileSearchListAddtionalOptions {
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
        public void Sharing()
        {
            AddShareResponse create = _api.AddShare(_synoTestFolderNoSlash);
            create.success.Should().BeTrue();

            GetSharingInfoResponse info = _api.GetSharingInfo(create.Data.links.First().id);
            info.success.Should().BeTrue();

            EditShareResponse editShare = _api.EditShare(create.Data.links.First().id);
            editShare.success.Should().BeTrue();

            _api.DeleteShare(create.Data.links.First().id);

            _api.ClearInvalidShares(create.Data.links.First().id);
        }

        [Test]
        public void Upload_GetThumb_Download()
        {
            var local = new FileInfo(_localTestImage);
            string downloadedName = _localTestFolderNoSlash + "\\dload_" + local.Name;

            if (System.IO.File.Exists(downloadedName))
                System.IO.File.Delete(downloadedName);

            RawSynologyResponse res = _api.Upload(new FileInfo(_localTestImage), _synoTestFolderNoSlash, true, true);
            res.success.Should().BeTrue();

            byte[] thumb = _api.GetThumb(_synoTestFolderNoSlash + "/" + local.Name);
            thumb.Length.Should().BeGreaterThan(1);

            Thread.Sleep(3000);

            byte[] file = _api.Download(_synoTestFolderNoSlash + "/" + local.Name);
            file.Length.Should().Be((int) local.Length);
        }

        [Test]
        public void VirtualFolder()
        {
            GetVirtualFoldersResponse list = _api.GetVirtualFolders(SynologyApi.FileSystemType.cifs, 0, 0,
                SynologyApi.SortBy.name, SynologyApi.SortDirection.asc,
                new SynologyApi.VirtualFolderListAddtionalOptions {
                    owner = true,
                    perm = true,
                    real_path = true,
                    size = true,
                    time = true,
                    volume_status = true
                });

            list.success.Should().BeTrue();
        }
    }
}
