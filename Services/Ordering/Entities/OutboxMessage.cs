namespace Ordering.Entities
{
    public class OutboxMessage : EntityBase
    {
        public string Type { get; set; }
        public string Content { get; set; }
        public string CorrelationId { get; set; }
        public DateTime OccurredOn { get; set; }
        public DateTime? ProcessedOn { get; set; }
        public bool isProcessed => ProcessedOn.HasValue;
        public string? Error { get; set; }
    }
}
