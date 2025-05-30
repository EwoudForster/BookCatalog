using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Channels;

namespace BookCatalog.DAL.Logging;

public class FileLoggerProvider : ILoggerProvider
{
    private readonly Channel<LoggingEntry> _logChannel;

    public FileLoggerProvider(Channel<LoggingEntry> logChannel)
    {
        _logChannel = logChannel;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new FileLogger(_logChannel, categoryName);
    }

    public void Dispose() { }
}
