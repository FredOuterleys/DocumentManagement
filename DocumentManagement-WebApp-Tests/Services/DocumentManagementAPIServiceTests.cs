using Microsoft.Extensions.DependencyInjection;
using DocumentManagment_WebApp.Repositories;
using RestSharp;
using Moq;
using Microsoft.Extensions.Configuration;
using DocumentManagement_API.Models;

namespace DocumentManagment_WebApp.Services.Tests
{
    [TestClass()]
    public class DocumentManagementAPIServiceTests
    {
        private readonly DocumentManagementAPIService service;

        public DocumentManagementAPIServiceTests()
        {
            var inMemorySettings = new Dictionary<string, string> {
                {"RootFolders:TempFolder", "TempFiles"} };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var services = new ServiceCollection();
            var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);

            services.AddTransient(provider => new DocumentManagementAPIRepository(mockRestClient.Object));
            services.AddTransient(provider => new DocumentManagementAPIService(
                new DocumentManagementAPIRepository(mockRestClient.Object),
                configuration));

            var serviceProvider = services.BuildServiceProvider();

            service = serviceProvider.GetService<DocumentManagementAPIService>();
        }

        [TestMethod()]
        public void GetTempPathWithWWWRootAndFilenameTest()
        {
            var document = new CSDSDocumentEntity
            {
                PropertyId = "1234",
                CaseNumber = "ABCD",
                Filename = "TestFile.txt"
            };

            var res = service.GetTempPath(document, useWWWRoot: true, includeFilename: true);

            Assert.AreEqual(@".//wwwroot\TempFiles\1234\ABCD\TestFile.txt", @$"{res}");
        }

        [TestMethod()]
        public void GetTempPathWithWWWRootAndWithoutFilenameTest()
        {
            var document = new CSDSDocumentEntity
            {
                PropertyId = "1234",
                CaseNumber = "ABCD",
                Filename = "TestFile.txt"
            };

            var res = service.GetTempPath(document, useWWWRoot: true, includeFilename: false);

            Assert.AreEqual(@".//wwwroot\TempFiles\1234\ABCD", @$"{res}");
        }

        [TestMethod()]
        public void GetTempPathWithoutWWWRootAndWitFilenameTest()
        {
            var document = new CSDSDocumentEntity
            {
                PropertyId = "1234",
                CaseNumber = "ABCD",
                Filename = "TestFile.txt"
            };

            var res = service.GetTempPath(document, useWWWRoot: false, includeFilename: true);

            Assert.AreEqual(@"TempFiles\1234\ABCD\TestFile.txt", @$"{res}");
        }

        [TestMethod()]
        public void GetTempPathWithoutWWWRootAndWithoutFilenameTest()
        {
            var document = new CSDSDocumentEntity
            {
                PropertyId = "1234",
                CaseNumber = "ABCD",
                Filename = "TestFile.txt"
            };

            var res = service.GetTempPath(document, useWWWRoot: false, includeFilename: false);

            Assert.AreEqual(@"TempFiles\1234\ABCD", @$"{res}");
        }
    }
}