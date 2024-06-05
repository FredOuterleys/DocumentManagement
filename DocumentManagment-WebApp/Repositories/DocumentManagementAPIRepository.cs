using DocumentManagement_API.Models;
using RestSharp;
using System.Text.Json;

namespace DocumentManagment_WebApp.Repositories
{
    public class DocumentManagementAPIRepository
    {
        private readonly IRestClient _restClient;

        public DocumentManagementAPIRepository(IRestClient restClient)
        {
            ArgumentNullException.ThrowIfNull(restClient, nameof(restClient));

            _restClient = restClient;
        }

        public async Task<CSDSDocumentEntity[]?> QueryDocuments(string? propertyID, string? caseNr)
        {
            var request = new RestRequest("Query", Method.Get);
            request.AddQueryParameter("propertyId", propertyID);
            request.AddQueryParameter("caseNumber", caseNr);

            return await _restClient.GetAsync<CSDSDocumentEntity[]>(request);
        }

        public async Task AddDocuments(CSDSDocumentEntity document)
        {
            var request = new RestRequest("Add", Method.Post);
            request.AddBody(JsonSerializer.Serialize(document));
            request.AddQueryParameter("doUpsert", true);

            _ = await _restClient.PostAsync(request);
        }

        public async Task DeleteDocument(CSDSDocumentEntity document)
        {
            var request = new RestRequest("Delete", Method.Delete);
            request.AddBody(JsonSerializer.Serialize(document));

            _ = await _restClient.DeleteAsync(request);
        }

        public async Task<byte[]?> DownloadDocument(CSDSDocumentEntity document)
        {
            var request = new RestRequest("Download", Method.Get);
            request.AddQueryParameter("propertyId", document.PropertyId);
            request.AddQueryParameter("caseNumber", document.CaseNumber);
            request.AddQueryParameter("filename", document.Filename);

            var base64Str = await _restClient.GetAsync<string>(request);
            
            if (!string.IsNullOrEmpty(base64Str))
                return Convert.FromBase64String(base64Str);

            return null;
        }
    }
}
