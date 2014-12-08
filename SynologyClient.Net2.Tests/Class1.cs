using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace SynologyClient.Net2.Tests
{
    [TestFixture]
    public class Class1
    {
        private SynologySession _session;
        private SynologyApi _api;
        private readonly string _synoTestFolderNoSlash;

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

        [Test]
        public void FileSystemInfo()
        {
            GetFileSystemInfoResponse getinfo = _api.GetFileSystemInfo(new[] { _synoTestFolderNoSlash },
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
            Assert.IsTrue(getinfo.success);

            GetFileSystemEntriesResponse getFileSystemEntries = _api.GetFileSystemEntries(_synoTestFolderNoSlash, 0, 10,
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

            Assert.IsTrue(getFileSystemEntries.success);

            GetSharesResponse listShare = _api.GetShares(0, 0, SynologyApi.SortBy.name, SynologyApi.SortDirection.asc,
                false, new SynologyApi.FileListAddtionalOptions
                {
                    size = true,
                    time = true,
                    real_path = true,
                    mount_point_type = true,
                    owner = true,
                    perm = true
                });

            Assert.IsTrue(listShare.success);
            Assert.IsTrue(listShare.Data.shares.Count > 0);
        }
    }
}
