using BookCatalog.DAL.Models;

namespace BookCatalog.DAL.Logging;

public class LoggingEntry : EntityBase
{
    public string LogLevel { get; set; }
    public string Category { get; set; }
    public string Message { get; set; }
    public string? Exception { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.Now;

}
