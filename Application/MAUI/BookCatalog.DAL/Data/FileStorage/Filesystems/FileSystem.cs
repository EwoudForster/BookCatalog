using BookCatalog.DAL.Logging;
using BookCatalog.DAL.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace BookCatalog.DAL.FileStorage;

// This class is responsible for reading and writing data to a file, it is generic so can be used by any class that implements IEntity
public class FileSystem<T> : IFileSystem<T> where T : IEntity
{
    // The file path to the file that will be read and written to
    protected string _filePath;

    // The serializer that will be used to serialize and deserialize the data (also generic so it can be any filetype)
    protected readonly ISerialize<T> _serializer;
    protected readonly ILogger<FileSystem<T>> _logger;


    // the file type is not created in this class, it is given as a parameter
    // dependency injection is used to inject the serializer, this is so that the class can be used with any type of file
    // this is the Single Responsibility Principle, we don't want this class to be responsible for creating the file type
    public FileSystem(IConfiguration configuration, ISerialize<T> serializer, ILogger<FileSystem<T>> logger)
    {
        try
        {
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer), LoggingStrings.ErrorCreatingRepository("Serializer"));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), LoggingStrings.ErrorCreatingRepository("Logger"));

            // the file path is created using the file name
            if (string.IsNullOrWhiteSpace(_filePath))
                _filePath = FilePath(configuration.GetValue<string>($"FileSettings:{typeof(T).Name}")) ?? throw new ArgumentNullException(LoggingStrings.ErrorCreatingRepository(serializer.GetType().Name)) ;
        }
        catch (Exception)
        {
            throw;
        }

    }

    public async Task<IEnumerable<T>> Read()
    {
        try
        {
            if (File.Exists(_filePath))
            {
                using FileStream stream = new FileStream(_filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, useAsync: true);
                return await _serializer.DeSerializer(stream);
            }

            // File does not exist, return an empty list and create the file
            return Enumerable.Empty<T>();
        }
        catch (FileNotFoundException ex)
        {
            // Specific exception handling if needed, you can log it or take action
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("File not found"));
            return Enumerable.Empty<T>();
        }
        catch (UnauthorizedAccessException ex)
        {
            // Handle access issues
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("Access denied"));
            return Enumerable.Empty<T>();
        }
        catch (Exception ex)
        {
            // Generic exception handler
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("An error occurred"));
            throw;
        }
    }

    public async Task Save(IEnumerable<T> input)
    {
        try
        {
            using FileStream stream = new FileStream(_filePath, FileMode.Open, FileAccess.Write, FileShare.ReadWrite, 4096, useAsync: true);
            await _serializer.Serializer(input.ToList(), stream);
        }
        catch (IOException ex)
        {
            // Handle any I/O specific issues
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("I/O error while saving data"));
        }
        catch (UnauthorizedAccessException ex)
        {
            // Handle access issues
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("Access denied while saving data"));
        }
        catch (Exception ex)
        {
            // Generic exception handler
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("An error occurred while saving data"));
            throw;
        }
    }


    // This method is used to get the file path of the file that will be read and written to
    public string FilePath(string? fileName)
    {
        _filePath = fileName;
        if (fileName == null)
        {
            // if the file name is null, a default file name is used
            _filePath = "logs.json";
        }

        // we use reflection to get the directory of the executing assembly
        string executableDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;

        // we then combine the directory with the file name to get the full path
        var path = Path.Combine(Path.GetFullPath(executableDirectory), _filePath);
        if (!File.Exists(path))
        {
            using (File.Create(path)) { }
        }
        return path;

    }
}
