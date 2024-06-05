using DocumentManagement_API.Data;
using DocumentManagement_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DocumentManagement_API.Repositories
{
    public class CSDSDocumentRepository(ApplicationDbContext context) : ICSDSDocumentRepository
    {
        private readonly ApplicationDbContext _context = context;

        public bool DocumentExists(CSDSDocumentEntity document) =>
            _context.Find<CSDSDocumentEntity>(document.PropertyId, document.CaseNumber, document.Filename) != null;

        public void AddDocument(CSDSDocumentEntity document, bool doUpsert)
        {
            if (doUpsert && DocumentExists(document))
            {
                _context.ChangeTracker.Clear();
                _context.Update(document);
            }
            else
                _context.Add(document);

            _context.SaveChanges();
        }

        public void UpdateDocument(CSDSDocumentEntity document)
        {
            _context.ChangeTracker.Clear();
            _context.Update(document);
            _context.SaveChanges();
        }

        public void DeleteDocument(CSDSDocumentEntity document)
        {
            _context.Remove(document);
            _context.SaveChanges();
        }

        private async Task<List<CSDSDocumentEntity>?> GetDocumentsByPropertyWildcard(string propertyId)
        {
            if (!string.IsNullOrEmpty(propertyId))
                return await _context.CSDSDocument.Where(c => c.PropertyId.StartsWith(propertyId)).ToListAsync();

            return await _context.CSDSDocument.ToListAsync();
        }

        private async Task<List<CSDSDocumentEntity>?> GetDocumentsByCaseNrWildcard(string caseNr)
        {
            if (!string.IsNullOrEmpty(caseNr))
                return await _context.CSDSDocument.Where(c => c.CaseNumber.StartsWith(caseNr)).ToListAsync();

            return await _context.CSDSDocument.ToListAsync();
        }

        private async Task<List<CSDSDocumentEntity>?> GetDocumentsByPropertyIdAndCaseNrWildcard(string propertyId, string caseNr, bool propertyIdWildcard, bool caseNrWildcard)
        {
            if (propertyIdWildcard && !caseNrWildcard)
                return await _context.CSDSDocument.Where(c => c.PropertyId.StartsWith(propertyId) && c.CaseNumber == caseNr).ToListAsync();

            if (!propertyIdWildcard && caseNrWildcard)
                return await _context.CSDSDocument.Where(c => c.PropertyId == propertyId && c.CaseNumber.StartsWith(caseNr)).ToListAsync();

            return await _context.CSDSDocument.Where(c => c.PropertyId.StartsWith(propertyId) && c.CaseNumber.StartsWith(caseNr)).ToListAsync();
        }

        public async Task<List<CSDSDocumentEntity>?> GetDocumentsWithWildcard(string? propertyId, string? caseNr)
        {
            if (string.IsNullOrEmpty(propertyId) && string.IsNullOrEmpty(caseNr)) return null;

            bool propertyHaswildcard = propertyId != null && propertyId.Contains('*');
            bool caseNrHaswildcard = caseNr != null && caseNr.Contains('*');

            propertyId = propertyId?.Replace("*", "");
            caseNr = caseNr?.Replace("*", "");

            if (caseNr != null && propertyId == null)
                return await GetDocumentsByCaseNrWildcard(caseNr);

            if (caseNr == null && propertyId != null)
                return await GetDocumentsByPropertyWildcard(propertyId);

            return await GetDocumentsByPropertyIdAndCaseNrWildcard(propertyId, caseNr, propertyHaswildcard, caseNrHaswildcard);
        }

        public async Task<List<CSDSDocumentEntity>?> GetDocuments(string? propertyId, string? caseNr)
        {
            if (string.IsNullOrEmpty(propertyId) && string.IsNullOrEmpty(caseNr)) return null;

            if (!string.IsNullOrEmpty(caseNr) && string.IsNullOrEmpty(propertyId))
                return await _context.CSDSDocument.Where(c => c.CaseNumber == caseNr).ToListAsync();

            if (string.IsNullOrEmpty(caseNr) && !string.IsNullOrEmpty(propertyId))
                return await _context.CSDSDocument.Where(c => c.PropertyId == propertyId).ToListAsync();

            return await _context.CSDSDocument.Where(c => c.PropertyId == propertyId && c.CaseNumber == caseNr).ToListAsync();
        }
    }
}

