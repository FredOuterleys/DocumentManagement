using DocumentManagement_API.Models;
using DocumentManagement_API.Services;
using Microsoft.AspNetCore.Mvc;
 
namespace DocumentManagement_API.Controllers
{
    [ApiController]
    [Route("api/v1/CSDSDocuments")]
    public class CSDSDocumentController : ControllerBase
    {
        private readonly ILogger<CSDSDocumentController> _logger;

        public CSDSDocumentController(ILogger<CSDSDocumentController> logger)
        {
            _logger = logger;
        }

        [HttpPost("Add")]
        public IActionResult Post([FromServices] ICSDSDocumentService service, CSDSDocumentEntity document, bool doUpsert = false)
        {
            service.AddDocument(document, doUpsert);
            _logger.LogInformation("Added Document with Upsert '{upsert}': {contents}", doUpsert, document.ToString());

            return Ok();
        }

        [HttpPut("Update")]
        public IActionResult Put([FromServices] ICSDSDocumentService service, CSDSDocumentEntity document)
        {
            service.UpdateDocument(document);
            _logger.LogInformation("Updated Document: {contents}", document.ToString());

            return Ok();
        }

        [HttpGet("Query")]

        [ProducesResponseType<CSDSDocumentEntity[]>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Query([FromServices] ICSDSDocumentService service, string? propertyId, string? caseNumber)
        {
            var documents = await service.GetDocuments(propertyId, caseNumber);
            _logger.LogInformation("Queried Documents for Property Id '{propertyId}' and Case Number '{caseNumber}', returned {docCount} documents."
                , propertyId, caseNumber, documents != null ? documents.Count : 0);

            return Ok(documents);
        }

        [HttpDelete("Delete")]
        public IActionResult Delete([FromServices] ICSDSDocumentService service, CSDSDocumentEntity document)
        {
            service.DeleteDocument(document);
            _logger.LogInformation("Deleted Document: {contents}", document.ToString());

            return Ok();
        }

        [HttpGet("Download")]

        [ProducesResponseType<string>(StatusCodes.Status200OK)]
        public IActionResult Download([FromServices] ICSDSDocumentService service, string propertyId, string caseNumber, string filename)
        {
            var file = service.DownloadDocument(propertyId, caseNumber, filename);
            _logger.LogInformation("Downloaded document: '{filename}' for Property Id '{propertyId} and Case Number '{caseNumber}' with length of: {fileLength} bytes"
                , filename, propertyId, caseNumber, file != null ? file.Length : 0);

            return Ok(file);
        }
    }
}
