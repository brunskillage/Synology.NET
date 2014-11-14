//using FluentAssertions;
//using Moq;
//using Ninject;
//using Ninject.MockingKernel.Moq;
//using NUnit.Framework;
//using RestSharp;
//using System;
//using System.IO;

//namespace SynologyClient.Tests
//{
//    [TestFixture]
//    public class Upload
//    {
//        private readonly DirectoryInfo _localTestFilesBasePathNoSlash =
//            new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "\\Synology\\TestFiles");

//        [Test]
//        public void IngestionProcessor()
//        {
//            var kernel = new MoqMockingKernel();
//            Mock<IRestClient> client = kernel.GetMock<IRestClient>();

//            client.Setup(c => c.Execute<SynologyResponse>(It.IsAny<IRestRequest>()))
//                .Returns(new RestResponse<SynologyResponse>
//                {
//                    Data = new SynologyResponse { success = true },
//                    Content = "TEST"
//                }).Verifiable();

//            kernel.GetMock<ISynologySession>().SetupGet(s => s.sid).Returns("1234");
//            var api = kernel.Get<SynologyApi>();
//            string testfile = _localTestFilesBasePathNoSlash + "\\webpage.html";
//            var args = new SynologyUploadArgs(new FileInfo(testfile), "/uploads/test");

//            SynologyResponse res = api.Upload(args);

//            res.success.Should().BeTrue();

//            client.Verify(c => c.Execute<SynologyResponse>(It.IsAny<IRestRequest>()), Times.Once());
//            client.VerifyAll();
//        }

//        [Test]
//        public void TestPathBuilder()
//        {
//            var kernel = new MoqMockingKernel();
//            var proc =
//                new InjestionProcessor(new IngestionArgs
//                {
//                    CmdArgs = new CmdArgs { IngestionDirectory = _localTestFilesBasePathNoSlash.FullName, SynoDirectory = "/ingestion/client" },
//                    Session = new SynologySession(new SynologyClientConfig()) { sid = "xyz1234567890" }
//                });
//            string testfile = _localTestFilesBasePathNoSlash + "\\Apples\\Braeburn.txt";

//            var res = proc.BuildDestFolderPath(new FileInfo(testfile));

//            res.Should().Be(string.Format("/ingestion/client/{0}_xyz12345/Apples", Environment.MachineName));
//        }
//    }
//}