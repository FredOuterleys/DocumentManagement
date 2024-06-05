using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DocumentManagement_API.Models
{
    public class CSDSDocumentEntity
    {
        [JsonPropertyName("case_Number")]
        public string CaseNumber { get; set; } = string.Empty;
        [JsonPropertyName("property_Id")]
        public string PropertyId { get; set; } = string.Empty;
        [JsonPropertyName("filename")]
        public string Filename {  get; set; } = string.Empty;
        [NotMapped]
        [JsonPropertyName("contents")]
        public string Contents { get; set; } = string.Empty;

        public override string ToString()
        {
            return Environment.NewLine +
                   $"Property Id: {PropertyId}{Environment.NewLine}" +
                   $"Case Number: {CaseNumber}{Environment.NewLine}" +
                   $"Filename: {Filename}{Environment.NewLine}" +
                   $"Content Size: {Contents.Length}";
        }
    }
}
