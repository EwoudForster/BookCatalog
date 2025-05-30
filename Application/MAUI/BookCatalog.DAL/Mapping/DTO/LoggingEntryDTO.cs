namespace BookCatalog.DAL.DTO;

public class LoggingEntryDTO
{
    public DateTime CreatedAt { get; set; }
    public Guid Id { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string LogLevel { get; set; }
    public string Category { get; set; }
    public string Message { get; set; }
    public string? Exception { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.Now;
}