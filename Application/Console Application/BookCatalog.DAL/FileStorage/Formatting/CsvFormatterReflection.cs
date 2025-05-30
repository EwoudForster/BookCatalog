using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace BookCatalog.DAL.FileStorage.Formatting
{
    public class CsvFormatterReflection<T> where T : new()
    {

        public CsvFormatterReflection()
        {
        }
        // Serialize to a string
        public string Serialize(List<T> objects)
        {
            StringBuilder stringBuilder = new StringBuilder();
            var properties = typeof(T).GetProperties();

            // CSV column names
            stringBuilder.AppendLine(string.Join(",", properties.Select(p => p.Name)));

            // Create the rows for each object in the list
            foreach (var obj in objects)
            {
                var values = properties.Select(p => p.GetValue(obj)?.ToString() ?? string.Empty);
                stringBuilder.AppendLine(string.Join(",", values));
            }

            return stringBuilder.ToString();
        }

        // Deserialize CSV to a list of objects
        // Deserialize CSV to a list of objects
        public List<T> DeSerialize(string csv)
        {
            var lines = csv.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var properties = typeof(T).GetProperties();
            var list = new List<T>();

            // Skip the header line (first line)
            for (int i = 1; i < lines.Length; i++)
            {
                var values = lines[i].Split(',');

                var obj = new T();
                for (int j = 0; j < properties.Length; j++)
                {
                    if (j < values.Length)
                    {
                        var value = values[j];

                        // Handle Guid properties separately
                        if (properties[j].PropertyType == typeof(Guid))
                        {
                            if (Guid.TryParse(value, out Guid parsedGuid))
                            {
                                properties[j].SetValue(obj, parsedGuid);
                            }
                            else
                            {
                                // Handle invalid Guid format
                                throw new InvalidCastException($"Invalid GUID format: {value} for property {properties[j].Name}");
                            }
                        }
                        // Handle DateTimeOffset properties separately
                        else if (properties[j].PropertyType == typeof(DateTimeOffset))
                        {
                            if (DateTimeOffset.TryParse(value, out DateTimeOffset parsedDateTimeOffset))
                            {
                                properties[j].SetValue(obj, parsedDateTimeOffset);
                            }
                            else
                            {
                                // Handle invalid DateTimeOffset format
                                throw new InvalidCastException($"Invalid DateTimeOffset format: {value} for property {properties[j].Name}");
                            }
                        }
                        else
                        {
                            try
                            {
                                // Attempt to convert the value to the correct property type
                                properties[j].SetValue(obj, Convert.ChangeType(value, properties[j].PropertyType));
                            }
                            catch (Exception ex)
                            {
                                // Handle other conversion exceptions gracefully
                                throw new InvalidCastException($"Failed to convert value '{value}' to type {properties[j].PropertyType} for property {properties[j].Name}", ex);
                            }
                        }
                    }
                    else
                    {
                        // If there are missing values in the CSV, handle default value assignment (optional)
                        properties[j].SetValue(obj, GetDefaultValue(properties[j].PropertyType));
                    }
                }
                list.Add(obj);
            }

            return list;
        }


        private object GetDefaultValue(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }
    }
}
