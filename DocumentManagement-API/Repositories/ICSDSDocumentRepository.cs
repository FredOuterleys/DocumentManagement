using DocumentManagement_API.Models;

namespace DocumentManagement_API.Repositories
{
    public interface ICSDSDocumentRepository
    {
        void AddDocument(CSDSDocumentEntity CSDSDocument, bool doUpsert);
        Task<List<CSDSDocumentEntity>?> GetDocuments(string? propertyId, string? caseNr);
        Task<List<CSDSDocumentEntity>?> GetDocumentsWithWildcard(string? propertyId, string? caseNr);
        void UpdateDocument(CSDSDocumentEntity CSDSDocument);
        void DeleteDocument(CSDSDocumentEntity CSDSDocument);
    }
}