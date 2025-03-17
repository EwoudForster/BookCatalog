using System.Text.Json;

namespace BookCatalog.DataLayer.Formatting
{
    public class JsonFormatter<T> : ISerialize<T> where T : class
    {
        private JsonSerializerOptions _serializerOptions;
        public JsonFormatter()
        {
            _serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public string Serializer(List<T> items) => JsonSerializer.Serialize<List<T>>(items, _serializerOptions);

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
