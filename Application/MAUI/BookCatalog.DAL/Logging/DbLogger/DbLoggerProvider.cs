using Microsoft.Extensions.Logging;
using BookCatalog.DAL.Models;
using BookCatalog.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Channels;
using BookCatalog.DAL.Logging;

namespace BookCatalog.DAL.Logging;

public class DbLoggerProvider : ILoggerProvider
{
    private readonly Channel<LoggingEntry> _logChannel;

    public DbLoggerProvider(Channel<LoggingEntry> logChannel)
    {
        _logChannel = logChannel;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new DbLogger(_logChannel, categoryName);
    }

    public void Dispose() { }
}
