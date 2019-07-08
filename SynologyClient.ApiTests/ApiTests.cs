using FluentAssertions;
using NUnit.Framework;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web.Configuration;

namespace SynologyClient.ApiTests
{
    [TestFixture]
    public class ApiTests
    {
        [SetUp]
        public void Setup()
        {
            if (_session == null)
            {
                _session = new SynologySession(new AppSettingsClientConfig());
                _session.Login();
                _api = new SynologyApi(_session);
            }
        }

        [TearDown]
        public void TearDown()
        {
            if (_session.loggedInTime < DateTime.UtcNow.AddMinutes(-5))
            {
                _session.LogOut();
                _session = null;
            }
        }

        public ApiTests()
        {
            var executingAssembly = new FileInfo(Assembly.GetExecutingAssembly().FullName);
            _localTestFolderNoSlash = executingAssembly.DirectoryName + "\\TestFiles";
            _localTestImage = _localTestFolderNoSlash + "\\image\\synologybox.jpg";
            _md5TestImage = _localTestFolderNoSlash + "\\image\\md5test.jpg";
            _synoTestFolderNoSlash = WebConfigurationManager.AppSettings.Get("Syno.TestFolder");
            if (string.IsNullOrWhiteSpace(_synoTestFolderNoSlash))
                throw new ConfigurationErrorsException("No Syno.TestFolder in app config found or value is empty");

            _session = new SynologySession(new AppSettingsClientConfig());
            _session.Login();
            _api = new SynologyApi(_session);
        }

        public ApiTests(SynologySession session)
        {
        }

        private readonly string _synoTestFolderNoSlash;
        private static string _localTestFolderNoSlash;
        private readonly string _localTestImage;
        private readonly string _md5TestImage;
        private SynologySession _session;
        private SynologyApi _api;

        public ApiTests(string synoTestFolderNoSlash)
        {
            _synoTestFolderNoSlash = synoTestFolderNoSlash;
        }

        [Test]
        public void BackgroundTask()
        {
            var list = _api.GetBackgroundTasks(0, 0, SynologyApi.BackgroundTaskSortBy.finished,
                SynologyApi.SortDirection.asc);
            list.success.Should().BeTrue();
        }

        [Test]
        public void CheckPermission()
        {
            var check = _api.CheckWritePermission("/Data");
            Assert.Pass();
        }

        [Test]
        public void Compress()
        {
            _api.Upload(new FileInfo(_localTestImage), _synoTestFolderNoSlash + "/test_compress");

            var start = _api.CompressAsync(_synoTestFolderNoSlash + "/test_compress",
                _synoTestFolderNoSlash + "/test_compress.zip", SynologyApi.CompressionLevel.moderate,
                SynologyApi.CompressionMode.add, SynologyApi.CompressionFormat.formatZip);

            start.success.Should().BeTrue();

            start.Data.taskid.Should().NotBeNullOrEmpty();

            for (var i = 0; i < 10; i++)
            {
                var status = _api.CompressStatus(start.Data.taskid);
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
            _api.Delete(_synoTestFolderNoSlash + "/synologybox.jpg", false);
            _api.AddFolder(_synoTestFolderNoSlash, "test");
            var @async = _api.CopyMoveAsync(_synoTestFolderNoSlash + "/synologybox.jpg",
                _synoTestFolderNoSlash + "/test");
            async.success.Should().BeTrue();

            async.Data.taskid.Should().NotBeNullOrEmpty();

            for (var i = 0; i < 10; i++)
            {
                Thread.Sleep(2000);
                var status = _api.CopyMoveStatus(async.Data.taskid);
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
            _api.AddFolder(_synoTestFolderNoSlash, "test_upload");
            _api.Upload(new FileInfo(_localTestImage), _synoTestFolderNoSlash + "/test_upload");
            var @async = _api.DeleteAsync(_synoTestFolderNoSlash + "/test_upload/synologybox.jpg");
            async.success.Should().BeTrue();

            async.Data.taskid.Should().NotBeNullOrEmpty();

            for (var i = 0; i < 10; i++)
            {
                var status = _api.DeleteStatus(async.Data.taskid);
                status.success.Should().BeTrue();
                if (status.Data.finished)
                    break;

                Thread.Sleep(2000);
            }

            var stop = _api.DeleteStop(async.Data.taskid);
            stop.success.Should().BeTrue();

            _api.Upload(new FileInfo(_localTestImage), _synoTestFolderNoSlash + "/test_upload");
            var delete = _api.Delete(_synoTestFolderNoSlash + "/test_upload/synologybox.jpg");
            delete.success.Should().BeTrue();
        }

        [Ignore("401 error unknown file error?")]
        public void DeleteAsync()
        {
            _api.AddFolder(_synoTestFolderNoSlash, "test_upload");
            _api.Upload(new FileInfo(_localTestImage), _synoTestFolderNoSlash + "/test_upload");
            var @async = _api.DeleteAsync(_synoTestFolderNoSlash + "/test_upload/synologybox.jpg");
            async.success.Should().BeTrue();

            async.Data.taskid.Should().NotBeNullOrEmpty();

            for (var i = 0; i < 10; i++)
            {
                var status = _api.DeleteStatus(async.Data.taskid);
                status.success.Should().BeTrue();
                if (status.Data.finished)
                    break;

                Thread.Sleep(2000);
            }

            var stop = _api.DeleteStop(async.Data.taskid);
            stop.success.Should().BeTrue();
        }

        [Test]
        public void DirSize()
        {
            var @async = _api.GetDirectorySizeAsync(_synoTestFolderNoSlash);

            async.success.Should().BeTrue();
            async.Data.taskid.Should().NotBeNullOrEmpty();

            DirSizeStatusResponse status = null;
            for (var i = 0; i < 10; i++)
            {
                status = _api.GetDirectorySizeStatus(async.Data.taskid);
                status.success.Should().BeTrue();
                if (status.Data.finished)
                    break;

                Thread.Sleep(2000);
            }

            status.success.Should().BeTrue();
            status.Data.finished.Should().BeTrue();
        }

        [Test]
        public void Extract()
        {
            var @async = _api.ExtractAsync(_synoTestFolderNoSlash + "/test_compress.zip",
                _synoTestFolderNoSlash + "/test_extract");
            async.success.Should().BeTrue();

            async.Data.taskid.Should().NotBeNullOrEmpty();

            for (var i = 0; i < 10; i++)
            {
                var status = _api.ExtractStatus(async.Data.taskid);
                status.success.Should().BeTrue();
                if (status.Data.finished)
                    break;

                Thread.Sleep(2000);
            }

            var stop = _api.ExtractStop(async.Data.taskid);
            stop.success.Should().BeTrue();

            var list = _api.ExtractListFiles(_synoTestFolderNoSlash + "/test_compress.zip");
            list.success.Should().BeTrue();
        }

        [Test]
        public void Favorite()
        {
            var list = _api.FavoriteList(0, 0, SynologyApi.StatusFilter.all,
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

            var add = _api.AddFavorite(testFavoritesDest, testFavoritesName);
            add.success.Should().BeTrue();

            var delete = _api.DeleteFavorite(testFavoritesDest);
            delete.success.Should().BeTrue();

            var edit = _api.EditFavorite(testFavoritesDest, testFavoritesName);
            edit.success.Should().BeTrue();

            var replace = _api.ReplaceFavorite(testFavoritesDest, testFavoritesName);
            replace.success.Should().BeTrue();

            var clear = _api.ClearBrokenFavorites(testFavoritesDest, testFavoritesName);
            clear.success.Should().BeTrue();
        }

        [Test]
        public void FileSystemInfo()
        {
            var getinfo = _api.GetFileSystemInfo(new[] { _synoTestFolderNoSlash },
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

            var getFileSystemEntries = _api.GetFileSystemEntries(_synoTestFolderNoSlash, 0, 10,
                SynologyApi.SortBy.name, SynologyApi.SortDirection.asc, "*", SynologyApi.FileTypeFilter.all, null,
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

            getFileSystemEntries.success.Should().BeTrue();

            var listShare = _api.GetShares(0, 0, SynologyApi.SortBy.name, SynologyApi.SortDirection.asc,
                false, new SynologyApi.FileListAddtionalOptions
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
        public void GetDiskstationInfo()
        {
            var res = _api.GetDiskstationInfo();
            res.success.Should().BeTrue();
        }

        [Test]
        public void Md5()
        {
            _api.Upload(new FileInfo(_md5TestImage), _synoTestFolderNoSlash, true, true);

            var md5 = _api.GetFileMd5Async(_synoTestFolderNoSlash + "/md5test.jpg");

            md5.success.Should().BeTrue();

            md5.Data.taskid.Should().NotBeNullOrEmpty();

            GetFileMd5StatusResponse status = null;

            for (var i = 0; i < 10; i++)
            {
                status = _api.GetFileMd5Status(md5.Data.taskid);
                status.success.Should().BeTrue();
                if (status.Data.finished)
                    break;

                Thread.Sleep(2000);
            }

            status.success.Should().BeTrue();
            status.Data.finished.Should().BeTrue();
        }

        [Test]
        public void Rename()
        {
            var delete = _api.Delete(_synoTestFolderNoSlash + "/newfolder_renamed");
            var delete2 = _api.Delete(_synoTestFolderNoSlash + "/newfolder");
            _api.AddFolder(_synoTestFolderNoSlash, "newfolder");

            var renamed = _api.FileSystemRename(_synoTestFolderNoSlash + "/newfolder", "newfolder_renamed");
            renamed.success.Should().BeTrue();
        }

        [Test]
        public void Search()
        {
            var start = _api.SearchStartAsync(_synoTestFolderNoSlash);

            start.success.Should().BeTrue();

            var taskid = start.Data.taskid;
            taskid.Should().NotBeNullOrEmpty();

            for (var i = 0; i < 10; i++)
            {
                var list = _api.SearchStatus(taskid, 0, 100, SynologyApi.SortBy.name,
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
        public void Sharing()
        {
            var create = _api.AddShare(_synoTestFolderNoSlash);
            create.success.Should().BeTrue();

            var info = _api.GetSharingInfo(create.Data.links.First().id);
            info.success.Should().BeTrue();

            var editShare = _api.EditShare(create.Data.links.First().id);
            editShare.success.Should().BeTrue();

            _api.DeleteShare(create.Data.links.First().id);

            _api.ClearInvalidShares(create.Data.links.First().id);
        }

        [Test]
        public void Upload_GetThumb_Download()
        {
            var local = new FileInfo(_localTestImage);
            var downloadedName = _localTestFolderNoSlash + "\\dload_" + local.Name;

            if (System.IO.File.Exists(downloadedName))
                System.IO.File.Delete(downloadedName);

            var res = _api.Upload(new FileInfo(_localTestImage), _synoTestFolderNoSlash, true, true);
            res.success.Should().BeTrue();

            var thumb = _api.GetThumb(_synoTestFolderNoSlash + "/" + local.Name);
            thumb.Length.Should().BeGreaterThan(1);

            Thread.Sleep(3000);

            var file = _api.Download(_synoTestFolderNoSlash + "/" + local.Name);
            file.Length.Should().Be((int)local.Length);
        }

        [Test]
        public void VirtualFolder()
        {
            var list = _api.GetVirtualFolders(SynologyApi.FileSystemType.cifs, 0, 0,
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
    }
}