using DocumentManagement_API.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace DocumentManagment_WebApp.Services
{
    public interface IDocumentManagementAPIService
    {
        Task<CSDSDocumentEntity[]?> QueryDocuments(string? propertyID, string? caseNr);
        Task AddDocuments(string propertyId, string caseNr, IBrowserFile[] filenames);
        Task DeleteDocument(CSDSDocumentEntity document);
        Task<bool> RetrieveDocumentForViewing(CSDSDocumentEntity document);
        string GetTempPath(CSDSDocumentEntity document, bool useWWWRoot = true, bool includeFilename = true);
        DotNetStreamReference GetStreamReference(CSDSDocumentEntity document);
        void DeleteViewedFile(CSDSDocumentEntity document);
    }
}