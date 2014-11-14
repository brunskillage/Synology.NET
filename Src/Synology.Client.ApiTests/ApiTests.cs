using System;
using FluentAssertions;
using NUnit.Framework;

namespace SynologyClient.ApiTests
{
    [TestFixture]
    public class ApiTests
    {
        private SynologySession _session;
        private SynologyApi _api;

        [SetUp]
        public void setup()
        {
            _session = new SynologySession(new SynologyClientConfig());
            _session.Login();
            _api = new SynologyApi(new SynologyClientConfig(), _session);
        }

        [TearDown]
        public void TearDown()
        {
            if (_session != null)
                _session.LogOut();
        }

        // [Test]
        public void SynoFilestationInfo()
        {
            var res = _api.SynoFilestationInfo();
            res.success.Should().BeTrue(); 
        }

        //[Test]
        public void FileStationListShare()
        {
            var res = _api.SynoFileStationListShare();
            res.success.Should().BeTrue();

            var res2 = _api.SynoFileStationListShare(
                0,
                0,
                SynologyApi.sort_by.name,
                SynologyApi.sort_direction.asc,false,
                new SynologyApi.FileListAddtionalOptions
                {
                    size = true, 
                    time = true, 
                    real_path = true
                });
            
            res2.success.Should().BeTrue();
            
            int atime = (int) (res2.data["shares"][0]["additional"]["time"]["atime"]);
            atime.Should().BeGreaterThan(0);
            
            string real_path = res2.data["shares"][0]["additional"]["real_path"];
            real_path.Should().NotBeNullOrEmpty();
        }

        //[Test]
        public void FileStationList()
        {
            var res = _api.SynoFileStationList("/Data"); 
            res.success.Should().BeTrue();

            var res2 = _api.SynoFileStationList("/Data", 0,10, SynologyApi.sort_by.name, SynologyApi.sort_direction.asc, "*", SynologyApi.filetype.all, null, new SynologyApi.FileListAddtionalOptions
            {
                real_path = true,
                time = true
            });

            res2.success.Should().BeTrue();

            int atime = (int)(res2.data["files"][0]["additional"]["time"]["atime"]);
            atime.Should().BeGreaterThan(0);

            string real_path = res2.data["files"][0]["path"];
            real_path.Should().NotBeNullOrEmpty();
        }

        //[Test]
        public void FileStationGetInfo()
        {
            var res = _api.SynoFileStationListGetInfo(new [] {"/Data"}, 
                new SynologyApi.FileGetInfoAddtionalOptions{real_path = true, size = true, owner = true, time = true} );
            res.success.Should().BeTrue();
        }

        //[Test]
        public void FileStationSearchStart()
        {
            var res = _api.SynoFileStationSearchStart("/Data");

            res.success.Should().BeTrue();

            string taskid = res.data["taskid"];
            taskid.Should().NotBeNullOrEmpty();

            bool finished = false;
            for (int i = 0; i < 10; i++)
            {
                var list = _api.SynoFileStationSearchList(taskid);
                list.success.Should().BeTrue();
                finished = (bool)list.data["finished"];
                if (finished)
                    ((int) list.data["finished"]).Should().BeGreaterThan(0);
                System.Threading.Thread.Sleep(3);
            }


            _api.SynoFileStationSearchStop(taskid).success.Should().BeTrue();
            _api.SynoFileStationSearchClean(taskid).success.Should().BeTrue();
        }

        // [Test]
        public void SynoFileStationVirtualFolderList()
        {
            var res = _api.SynoFileStationVirtualFolderList();
            res.success.Should().BeTrue(); 
        }

       // [Test]
        public void SynoFileStationFavoriteList()
        {
            var res = _api.SynoFileStationFavoriteList();
            res.success.Should().BeTrue();
        }

        //[Test]
        public void SynoFileStationFavoriteDelete()
        {
            var res = _api.SynoFileStationFavoriteDelete("/Data");
            res.success.Should().BeTrue();
        }

        //[Test]
        public void SynoFileStationFavoriteAdd()
        {
            try {
                var res = _api.SynoFileStationFavoriteAdd("/Data", "DataFavorite");
                res.success.Should().BeTrue();
            }
            catch (Exception e) {
                e.Message.Should().Be("A folder path of favorite folder is already added to user’s favorites");
            }
        }


        //[Test]
        public void SynoFileStationFavoriteClearBroken()
        {
            var res = _api.SynoFileStationFavoriteClearBroken("/Data", "DataFavorite");
            res.success.Should().BeTrue();
        }

        //[Test]
        public void SynoFileStationFavoriteEdit()
        {
            var res = _api.SynoFileStationFavoriteEdit("/Data", "DataFavorite");
            res.success.Should().BeTrue();
        }

        //[Test]
        public void SynoFileStationFavoriteReplaceAll()
        {
            var res = _api.SynoFileStationFavoriteReplaceAll("/Data", "DataFavorite");
            res.success.Should().BeTrue();
        }

        [Test]
        public void SynoFileStationThumbGet()
        {
            byte[] res = _api.SynoFileStationThumbGet("/image/");
            res.Length.Should().BeGreaterThan(1);
        }
    }
}