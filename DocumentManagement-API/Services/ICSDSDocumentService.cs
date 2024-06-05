using DocumentManagement_API.Models;

namespace DocumentManagement_API.Services
{
    public interface ICSDSDocumentService
    {
        void AddDocument(CSDSDocumentEntity document, bool doUpsert = false);
        Task<List<CSDSDocumentEntity>?> GetDocuments(string? propertyId, string? caseNumber);
        void UpdateDocument(CSDSDocumentEntity document);
        void DeleteDocument(CSDSDocumentEntity document);
        string? DownloadDocument(string propertyId, string caseNumber, string filename);
    }
}