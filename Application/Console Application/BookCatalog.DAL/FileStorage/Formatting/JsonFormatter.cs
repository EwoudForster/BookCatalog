using System.Text.Json;

namespace BookCatalog.DAL.FileStorage.Formatting
{

    // This class is responsible for serializing and deserializing data to and from a JSON file
    public class JsonFormatter<T> : ISerialize<T> where T : class
    {
        // We create a variable wherein we will store the options for the JsonSerializer
        private JsonSerializerOptions _serializerOptions;
        public JsonFormatter()
        {
            // We set the options for the JsonSerializer to ignore the case of the properties
            _serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        // This method is used to serialize the data to a JSON string, we use an expression-bodied member because it is a one line method
        public string Serializer(List<T> items) => JsonSerializer.Serialize(items, _serializerOptions);

        // This method is used to deserialize the data from a JSON string
        public List<T> DeSerializer(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {

                var formatted = JsonSerializer.Deserialize<List<T>>(value, _serializerOptions);
                if (formatted != null)
                {
                    return formatted;
                }
            }
            return new List<T>();
        }
    }
}
