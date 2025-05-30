using Microsoft.Extensions.DependencyInjection;
using System.Threading.Channels;
using Microsoft.Extensions.Hosting;
using BookCatalog.DAL.FileStorage;

namespace BookCatalog.DAL.Logging;

public class FileLogQueueService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly Channel<LoggingEntry> _logChannel;

    public FileLogQueueService(IServiceScopeFactory scopeFactory, Channel<LoggingEntry> logChannel)
    {
        _scopeFactory = scopeFactory;
        _logChannel = logChannel;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var log in _logChannel.Reader.ReadAllAsync(stoppingToken))
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var repo = scope.ServiceProvider.GetRequiredService<IFileRepository<LoggingEntry>>();
                await repo.Add(log);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[LogQueue Error] {ex.Message}");
            }
        }
    }
}

