using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.IO;
using System.Reflection;
using System.Threading;
using SynologyClient.Response;

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

        //[Test]
        //public void FileStationInfoGetInfo()
        //{
        //    InfoGetInfoResponse res = _api.SynoFilestationInfoGetInfo();
        //    res.success.Should().BeTrue();
        //}        


        //[Test]
        //public void FileStationListListShare()
        //{
        //    ListListShareResponse resp = _api.SynoFileStationList_ListShare(
        //        0,
        //        0,
        //        SynologyApi.SortBy.name,
        //        SynologyApi.SortDirection.asc, false,
        //        new SynologyApi.FileListAddtionalOptions
        //        {
        //            size = true,
        //            time = true,
        //            real_path = true,
        //            mount_point_type = true,
        //            owner = true,
        //            perm = true
        //        });

        //    resp.success.Should().BeTrue();
        //    resp.Data.shares.Count().Should().BeGreaterOrEqualTo(1);
        //}

        //[Test]
        //public void FileStationList()
        //{
        //    ListListResponse resp = _api.SynoFileStationList_List("/Data", 0, 10, SynologyApi.SortBy.name,
        //        SynologyApi.SortDirection.asc, "*", SynologyApi.FileTypeFilter.all, null,
        //        new SynologyApi.FileListAddtionalOptions
        //        {
        //            real_path = true,
        //            time = true,mount_point_type = true,owner = true,perm = true,size = true,volume_status = true
        //        });

        //    resp.success.Should().BeTrue();
        //}

        [Test]
        public void FileStationGetInfo()
        {
            ListGetInfoResponse resp = _api.SynoFileStationList_GetInfo(new[] { "/Data" },
                new SynologyApi.FileGetInfoAddtionalOptions { real_path = true, size = true, owner = true, time = true });
            resp.success.Should().BeTrue();
        }

        //[Test]
        //public void FileStationSearchStart()
        //{
        //    BaseSynologyResponse res = _api.SynoFileStationSearchStart("/Data");

        //    res.success.Should().BeTrue();

        //    string taskid = res.data["taskid"];
        //    taskid.Should().NotBeNullOrEmpty();

        //    bool finished = false;
        //    for (int i = 0; i < 10; i++)
        //    {
        //        BaseSynologyResponse list = _api.SynoFileStationSearchList(taskid);
        //        list.success.Should().BeTrue();
        //        finished = (bool)list.data["finished"];
        //        if (finished)
        //        {
        //            ((int)list.data["finished"]).Should().BeGreaterThan(0);
        //            break;
        //        }

        //        Thread.Sleep(2000);
        //    }

        //    _api.SynoFileStationSearchStop(taskid).success.Should().BeTrue();
        //    _api.SynoFileStationSearchClean(taskid).success.Should().BeTrue();
        //}

        //[Test]
        //public void SynoFileStationCheckPermission()
        //{
        //    try
        //    {
        //        BaseSynologyResponse res = _api.SynoFileStationCheckPermission("/Data");
        //        res.success.Should().BeTrue();
        //    }
        //    catch (Exception)
        //    {
        //        Assert.Pass();
        //    }
        //}

        //[Test]
        //public void SynoFileStationCompress()
        //{
        //    _api.SynoFileStationUpload(new FileInfo(_localTestImage), _synoTestFolderNoSlash + "/test_compress");
        //    BaseSynologyResponse res =
        //        _api.SynoFileStationCompressStart(_synoTestFolderNoSlash + "/test_compress",
        //            _synoTestFolderNoSlash + "/test_compress.zip");
        //    res.success.Should().BeTrue();

        //    string taskid = res.data["taskid"];
        //    taskid.Should().NotBeNullOrEmpty();

        //    for (int i = 0; i < 10; i++)
        //    {
        //        Thread.Sleep(2000);
        //        BaseSynologyResponse status = _api.SynoFileStationCompressStatus(taskid);
        //        status.success.Should().BeTrue();
        //        var finished = (bool)status.data["finished"];
        //        if (finished)
        //        {
        //            ((bool)status.data["finished"]).Should().BeTrue();
        //            break;
        //        }
        //    }

        //    BaseSynologyResponse stop = _api.SynoFileStationCompressStop(taskid);
        //    stop.success.Should().BeTrue();
        //}

        //[Test]
        //public void SynoFileStationCopyMove()
        //{
        //    _api.SynoFileStationCreateFolder(_synoTestFolderNoSlash, "test");
        //    BaseSynologyResponse res = _api.SynoFileStationCopyMoveStart(_synoTestFolderNoSlash + "/synologybox.jpg",
        //        _synoTestFolderNoSlash + "/test");
        //    res.success.Should().BeTrue();

        //    string taskid = res.data["taskid"];
        //    taskid.Should().NotBeNullOrEmpty();

        //    for (int i = 0; i < 10; i++)
        //    {
        //        Thread.Sleep(2000);
        //        BaseSynologyResponse status = _api.SynoFileStationCopyMoveStatus(taskid);
        //        status.success.Should().BeTrue();
        //        var finished = (bool)status.data["finished"];
        //        if (finished)
        //        {
        //            ((bool)status.data["finished"]).Should().BeTrue();
        //            break;
        //        }
        //    }

        //    BaseSynologyResponse stop = _api.SynoFileStationCopyMoveStop(taskid);
        //    stop.success.Should().BeTrue();
        //}

        //[Test]
        //public void SynoFileStationCreateFolder()
        //{
        //    BaseSynologyResponse create = _api.SynoFileStationCreateFolder(_synoTestFolderNoSlash, "newfolder");
        //    create.success.Should().BeTrue();
        //}

        //[Test]
        //public void SynoFileStationDelete()
        //{
        //    _api.SynoFileStationUpload(new FileInfo(_localTestImage), _synoTestFolderNoSlash + "/test_upload");
        //    BaseSynologyResponse res =
        //        _api.SynoFileStationDeleteStart(_synoTestFolderNoSlash + "/test_upload/synologybox.jpg");
        //    res.success.Should().BeTrue();

        //    string taskid = res.data["taskid"];
        //    taskid.Should().NotBeNullOrEmpty();

        //    for (int i = 0; i < 10; i++)
        //    {
        //        Thread.Sleep(2000);
        //        BaseSynologyResponse status = _api.SynoFileStationDeleteStatus(taskid);
        //        status.success.Should().BeTrue();
        //        var finished = (bool)status.data["finished"];
        //        if (finished)
        //        {
        //            ((bool)status.data["finished"]).Should().BeTrue();
        //            break;
        //        }
        //    }

        //    BaseSynologyResponse stop = _api.SynoFileStationDeleteStop(taskid);
        //    stop.success.Should().BeTrue();

        //    _api.SynoFileStationUpload(new FileInfo(_localTestImage), _synoTestFolderNoSlash + "/test_upload");
        //    BaseSynologyResponse deleteSync =
        //        _api.SynoFileStationDeleteSync(_synoTestFolderNoSlash + "/test_upload/synologybox.jpg");
        //    deleteSync.success.Should().BeTrue();
        //}

        //[Test]
        //public void SynoFileStationDirSize()
        //{
        //    BaseSynologyResponse res = _api.SynoFileStationDirsizeStart("/photo");

        //    res.success.Should().BeTrue();

        //    string taskid = res.data["taskid"];
        //    taskid.Should().NotBeNullOrEmpty();

        //    for (int i = 0; i < 10; i++)
        //    {
        //        Thread.Sleep(2000);
        //        BaseSynologyResponse list = _api.SynoFileStationDirsizeStatus(taskid);
        //        list.success.Should().BeTrue();
        //        var finished = (bool)list.data["finished"];
        //        if (finished)
        //        {
        //            ((bool)list.data["finished"]).Should().BeTrue();
        //            break;
        //        }
        //    }

        //    _api.SynoFileStationDirsizeStatus(taskid).success.Should().BeTrue();
        //}

        //[Test]
        //public void SynoFileStationExtract()
        //{
        //    BaseSynologyResponse res =
        //        _api.SynoFileStationExtractStart(_synoTestFolderNoSlash + "/test_compress.zip",
        //            _synoTestFolderNoSlash + "/test_extract");
        //    res.success.Should().BeTrue();

        //    string taskid = res.data["taskid"];
        //    taskid.Should().NotBeNullOrEmpty();

        //    for (int i = 0; i < 10; i++)
        //    {
        //        Thread.Sleep(2000);
        //        BaseSynologyResponse status = _api.SynoFileStationExtractStatus(taskid);
        //        status.success.Should().BeTrue();
        //        var finished = (bool)status.data["finished"];
        //        if (finished)
        //        {
        //            ((bool)status.data["finished"]).Should().BeTrue();
        //            break;
        //        }
        //    }

        //    BaseSynologyResponse stop = _api.SynoFileStationExtractStop(taskid);
        //    stop.success.Should().BeTrue();

        //    BaseSynologyResponse list = _api.SynoFileStationExtractList(_synoTestFolderNoSlash + "/test_compress.zip");
        //    list.success.Should().BeTrue();
        //}

        //[Test]
        //public void SynoFileStationFavoriteAdd()
        //{
        //    try
        //    {
        //        BaseSynologyResponse res = _api.SynoFileStationFavoriteAdd("/Data", "DataFavorite");
        //        res.success.Should().BeTrue();
        //    }
        //    catch (Exception e)
        //    {
        //        e.Message.Should().Be("A folder path of favorite folder is already added to user’s favorites");
        //    }
        //}

        //[Test]
        //public void SynoFileStationFavoriteClearBroken()
        //{
        //    BaseSynologyResponse res = _api.SynoFileStationFavoriteClearBroken("/Data", "DataFavorite");
        //    res.success.Should().BeTrue();
        //}

        //[Test]
        //public void SynoFileStationFavoriteDelete()
        //{
        //    BaseSynologyResponse res = _api.SynoFileStationFavoriteDelete("/Data");
        //    res.success.Should().BeTrue();
        //}

        //[Test]
        //public void SynoFileStationFavoriteEdit()
        //{
        //    BaseSynologyResponse res = _api.SynoFileStationFavoriteEdit("/Data", "DataFavorite");
        //    res.success.Should().BeTrue();
        //}

        //[Test]
        //public void SynoFileStationFavoriteList()
        //{
        //    BaseSynologyResponse res = _api.SynoFileStationFavoriteList();
        //    res.success.Should().BeTrue();
        //}

        //[Test]
        //public void SynoFileStationFavoriteReplaceAll()
        //{
        //    BaseSynologyResponse res = _api.SynoFileStationFavoriteReplaceAll("/Data", "DataFavorite");
        //    res.success.Should().BeTrue();
        //}

        //[Test]
        //public void SynoFileStationMd5()
        //{
        //    BaseSynologyResponse res = _api.SynoFileStationMd5Start("/homes/allanb/20130524059.jpg");

        //    res.success.Should().BeTrue();

        //    string taskid = res.data["taskid"];
        //    taskid.Should().NotBeNullOrEmpty();

        //    for (int i = 0; i < 10; i++)
        //    {
        //        Thread.Sleep(2000);
        //        BaseSynologyResponse list = _api.SynoFileStationMd5Status(taskid);
        //        list.success.Should().BeTrue();
        //        var finished = (bool)list.data["finished"];
        //        if (finished)
        //        {
        //            ((bool)list.data["finished"]).Should().BeTrue();
        //            break;
        //        }
        //    }

        //    _api.SynoFileStationDirsizeStop(taskid).success.Should().BeTrue();
        //}

        //[Test]
        //public void SynoFileStationRename()
        //{
        //    try
        //    {
        //        BaseSynologyResponse renamed = _api.SynoFileStationRename(_synoTestFolderNoSlash + "/newfolder",
        //            "newfolder_renamed");
        //        renamed.success.Should().BeTrue();
        //    }
        //    catch (Exception e)
        //    {
        //        e.Message.Should().Be("Failed to rename it. More information in <errors> object");
        //    }
        //}

        //[Test]
        //public void SynoFileStationSharing()
        //{
        //    BaseSynologyResponse create = _api.SynoFileStationSharingCreate(_synoTestFolderNoSlash);
        //    dynamic id = create.data["links"][0]["id"];
        //    create.success.Should().BeTrue();

        //    BaseSynologyResponse info = _api.SynoFileStationSharingGetInfo(id);
        //    info.success.Should().BeTrue();

        //    BaseSynologyResponse edit = _api.SynoFileStationSharingEdit(id);
        //    edit.success.Should().BeTrue();

        //    _api.SynoFileStationSharingDelete(id);

        //    _api.SynoFileStationSharingClearInvalid(id);
        //}

        //[Test]
        //public void SynoFileStationThumbGet()
        //{
        //    byte[] res = _api.SynoFileStationThumbGet("/homes/allanb/20130524059.jpg");
        //    //File.WriteAllBytes("c:\\test\\20130524059.jpg", res);
        //    res.Length.Should().BeGreaterThan(1);
        //}

        //[Test]
        //public void SynoFileStationUpload_Download()
        //{
        //    var local = new FileInfo(_localTestImage);
        //    string dest = _synoTestFolderNoSlash;
        //    string downloadedName = _localTestFolderNoSlash + "\\dload_" + local.Name;

        //    if (System.IO.File.Exists(downloadedName))
        //        System.IO.File.Delete(downloadedName);

        //    BaseSynologyResponse res = _api.SynoFileStationUpload(new FileInfo(_localTestImage), dest, true, true);
        //    res.success.Should().BeTrue();

        //    Thread.Sleep(3000);

        //    byte[] file = _api.SynoFileStationDownload(dest + "/" + local.Name);
        //    file.Length.Should().Be((int)local.Length);
        //}

        //[Test]
        //public void SynoFileStationVirtualFolderList()
        //{
        //    BaseSynologyResponse res = _api.SynoFileStationVirtualFolderList();
        //    res.success.Should().BeTrue();
        //}

        //[Test]
        //public void SynoFilestationGetInfo()
        //{
        //    BaseSynologyResponse res = _api.SynoFilestationGetInfo();
        //    res.success.Should().BeTrue();
        //}
    }
}