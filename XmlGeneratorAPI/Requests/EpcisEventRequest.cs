using XmlGeneratorAPI.Enums;

namespace XmlGeneratorAPI.Requests
{
    public class EpcisEventRequest
    {
        // Core event attributes
        public DateTime EventTime { get; set; } 
        public BizStep BizStep { get; set; }
        public string? ReadPoint { get; set; }
        public string? BizLocation { get; set; }

        // Identifiers
        public string? ParentID { get; set; }   // SSCC in aggregation

        // Source / Destination
        public string? SourceType { get; set; }
        public string? DestinationType { get; set; }

        // Product / Lot details
        public string? LotNumber { get; set; }
        public DateOnly? ItemExpirationDate { get; set; }

        // Correction / Exception handling
        public DateTime? DeclarationTime { get; set; } 
        public string? Reason { get; set; }
        public string? CorrectiveEventID { get; set; }
        public int? Quantity { get; set; }
        public string? UnitOfMeasure { get; set; }
        public string? EPCClass { get; set; }


        public Guid SGITNCsvFileID { get; set; }

    }
}