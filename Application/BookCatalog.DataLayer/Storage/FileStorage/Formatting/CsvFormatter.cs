using ServiceStack.Text;

namespace BookCatalog.DataLayer.FileStorage.Formatting
{
    // This class is responsible for serializing and deserializing data to and from a CSV file
    // it is generic and has only a constraint of class
    public class CsvFormatter<T> : ISerialize<T> where T : class
    {

        // This class is responsible for serializing and deserializing data to and from a CSV file
        public string Serializer(List<T> items)
        {
            // we use the CSVserializer from the namespace ServiceStack.Text to serialize the data
            // very easy
            return CsvSerializer.SerializeToCsv(items);
        }

        public List<T> DeSerializer(string value)
        {
            // we deserialize the data into a list of generic objects of any type of class
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
