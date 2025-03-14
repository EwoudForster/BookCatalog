using ServiceStack.Text;

namespace BookCatalog.DataLayer.Formatting
{
    public class CsvFormatter<T> : ISerialize<T> where T : class
    {
        public string Serializer(List<T> items)
        {
            return CsvSerializer.SerializeToCsv(items);
        }

        public List<T> DeSerializer(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                var formatted = CsvSerializer.DeserializeFromString<List<T>>(value);
                if (formatted != null)
                {
                    return formatted;
                }
            }
            return new List<T>();
        }
    }
}
