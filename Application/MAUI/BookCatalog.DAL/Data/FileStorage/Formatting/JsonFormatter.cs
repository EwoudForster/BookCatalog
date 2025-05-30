using BookCatalog.DAL.Models;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using BookCatalog.DAL.Logging;

namespace BookCatalog.DAL.FileStorage;


// This class is responsible for serializing and deserializing data to and from a JSON file
public class JsonFormatter<T> : ISerialize<T> where T : IEntity
{
    // We create a variable wherein we will store the options for the JsonSerializer
    private JsonSerializerOptions _serializerOptions;
    protected readonly ILogger<JsonFormatter<T>> _logger;

    public JsonFormatter(ILogger<JsonFormatter<T>> logger)
    {
        // We set the options for the JsonSerializer to ignore the case of the properties
        _serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        _logger = logger ?? throw new ArgumentNullException(nameof(logger), LoggingStrings.ErrorCreatingRepository("Logger"));

    }

    // This method is used to serialize the data to a JSON string, we use an expression-bodied member because it is a one line method
    public async Task Serializer(List<T> items, Stream stream)
    {
        await JsonSerializer.SerializeAsync(stream, items, _serializerOptions);
        await stream.FlushAsync();

    }

    // This method is used to deserialize the data from a JSON string
    public async Task<List<T>> DeSerializer(Stream stream)
    {
        try
        {
            if (stream.Length == 0)
            {
                return new List<T>(); // Return an empty list if the stream is empty
            }

            var formatted = await JsonSerializer.DeserializeAsync<List<T>>(stream, _serializerOptions);

            if (formatted != null)
            {
                return formatted;
            }

            return new List<T>(); // Return an empty list if deserialization fails
        }
        catch (JsonException ex)
        {
            // Log or handle the exception as needed
            // You can also throw a custom exception or return an empty list if required
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("JSON deserialization failed"));
            return new List<T>(); // Return an empty list on error
        }
    }
}


