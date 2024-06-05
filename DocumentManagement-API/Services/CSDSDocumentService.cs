using DocumentManagement_API.Models;
using DocumentManagement_API.Repositories;
using System.IO;

namespace DocumentManagement_API.Services
{
    public class CSDSDocumentService(ICSDSDocumentRepository repository, IConfiguration config) : ICSDSDocumentService
    {
        private readonly ICSDSDocumentRepository _repository = repository;
        private readonly IConfiguration _config = config;

        private string GetDocumentFolder(CSDSDocumentEntity document) =>
            $"{_config["RootFolders:DocumentFolder"]}/{document.PropertyId}/{document.CaseNumber}";

        private string GetDocumentFolder(string propertyId, string caseNumber) =>
            GetDocumentFolder(new CSDSDocumentEntity { PropertyId = propertyId, CaseNumber = caseNumber });

        private static void SaveDocument(string contents, string path, string filename)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            File.WriteAllBytes(Path.Combine(path, filename), Convert.FromBase64String(contents));
        }

        private static bool ContainsWildCards(string? propertyId, string? caseNumber)
        {
            return propertyId != null && propertyId.Contains('*') ||
                   caseNumber != null && caseNumber.Contains('*');
        }

        public void AddDocument(CSDSDocumentEntity document, bool doUpsert = false)
        {
            _repository.AddDocument(document, doUpsert);

            SaveDocument(document.Contents, GetDocumentFolder(document), Path.GetFileName(document.Filename));
        }

        public void UpdateDocument(CSDSDocumentEntity document)
        {
            _repository.UpdateDocument(document);

            SaveDocument(document.Contents, GetDocumentFolder(document), Path.GetFileName(document.Filename));
        }

        public void DeleteDocument(CSDSDocumentEntity document)
        {
            _repository.DeleteDocument(document);
            File.Delete(Path.Combine(GetDocumentFolder(document), Path.GetFileName(document.Filename)));
        }

        public async Task<List<CSDSDocumentEntity>?> GetDocuments(string? propertyId, string? caseNumber)
        {
            if (ContainsWildCards(propertyId, caseNumber))
                return await _repository.GetDocumentsWithWildcard(propertyId, caseNumber);

            return await _repository.GetDocuments(propertyId, caseNumber);
        }

        public string? DownloadDocument(string propertyId, string caseNumber, string filename)
        {
            var path = GetDocumentFolder(propertyId, caseNumber);

            return Convert.ToBase64String(File.ReadAllBytes(Path.Combine(path, Path.GetFileName(filename))));
        }
    }
}
