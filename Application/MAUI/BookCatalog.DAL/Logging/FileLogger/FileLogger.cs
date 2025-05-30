using Microsoft.Extensions.Logging;
using System.Threading.Channels;

namespace BookCatalog.DAL.Logging;
public class FileLogger : ILogger
{
    private readonly Channel<LoggingEntry> _logChannel;
    private readonly string _category;

    public FileLogger(Channel<LoggingEntry> logChannel, string categoryName)
    {
        _logChannel = logChannel;
        _category = categoryName;
    }

    public IDisposable BeginScope<TState>(TState state) => null;
    public bool IsEnabled(LogLevel logLevel) => logLevel != LogLevel.None;



    public void Log<TState>(LogLevel logLevel, EventId eventId,
        TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        if (!IsEnabled(logLevel)) return;
        if (formatter == null) throw new ArgumentNullException(nameof(formatter));
        if (_category.StartsWith("Microsoft.EntityFrameworkCore"))
            return;
        var entry = new LoggingEntry
        {
            LogLevel = logLevel.ToString(),
            Message = formatter(state, exception),
            Category = _category,
            Timestamp = DateTime.Now,
            Exception = exception?.ToString()
        };

        // Non-blocking enqueue
        _logChannel.Writer.TryWrite(entry);
    }
}
