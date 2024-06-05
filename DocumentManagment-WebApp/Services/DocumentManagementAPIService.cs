using DocumentManagement_API.Models;
using DocumentManagment_WebApp.Repositories;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace DocumentManagment_WebApp.Services
{
    public class DocumentManagementAPIService : IDocumentManagementAPIService
    {
        const string WWWROOT = ".//wwwroot";

        private readonly DocumentManagementAPIRepository _repository;
        private readonly IConfiguration _config;

        public DocumentManagementAPIService(DocumentManagementAPIRepository repository, IConfiguration config)
        {
            ArgumentNullException.ThrowIfNull(repository, nameof(repository));
            ArgumentNullException.ThrowIfNull(config, nameof(config));

            _repository = repository;
            _config = config;
        }

        public string GetTempPath(CSDSDocumentEntity document, bool useWWWRoot = true, bool includeFilename = false)
        {
            ArgumentNullException.ThrowIfNull(document, nameof(document));

            var subFolder = _config["RootFolders:TempFolder"] ?? string.Empty;

            return Path.Combine(
                useWWWRoot ? WWWROOT : string.Empty,
                subFolder,
                document.PropertyId,
                document.CaseNumber,
                includeFilename ? Path.GetFileName(document.Filename) : string.Empty);
        }

        public async Task<CSDSDocumentEntity[]?> QueryDocuments(string? propertyID, string? caseNr) =>
            await _repository.QueryDocuments(propertyID, caseNr);

        private static FileStream GetFileStream(string filename) => File.OpenRead(filename);

        private async Task<string> GetFileContentsAsBase64(IBrowserFile file)
        {
            using var stream = file.OpenReadStream(_config.GetValue<long>("Documents:MaxSize"));
            using var ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            return Convert.ToBase64String(ms.ToArray());
        }

        public DotNetStreamReference GetStreamReference(CSDSDocumentEntity document)
        {
            var fileStream = GetFileStream(GetTempPath(document, includeFilename: true));

            return new DotNetStreamReference(stream: fileStream);
        }

        public async Task DeleteDocument(CSDSDocumentEntity document) =>
            await _repository.DeleteDocument(document);

        public async Task AddDocuments(string propertyId, string caseNr, IBrowserFile[] filenames)
        {
            foreach (var file in filenames)
            {
                await _repository.AddDocuments(
                   new CSDSDocumentEntity
                   {
                       PropertyId = propertyId,
                       CaseNumber = caseNr,
                       Filename = Path.GetFileName(file.Name),
                       Contents = await GetFileContentsAsBase64(file)
                   });
            }
        }

        public void DeleteViewedFile(CSDSDocumentEntity document)
        {
            var filename = Path.Combine(GetTempPath(document), document.Filename);

            if (!string.IsNullOrEmpty(filename) && File.Exists(filename))
                File.Delete(filename);
        }

        public async Task<bool> RetrieveDocumentForViewing(CSDSDocumentEntity document)
        {
            var file = await _repository.DownloadDocument(document);

            if (file != null)
            {
                var path = GetTempPath(document);

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                File.WriteAllBytes(Path.Combine(path, Path.GetFileName(document.Filename)), file);

                return true;
            }

            return false;
        }
    }
}
