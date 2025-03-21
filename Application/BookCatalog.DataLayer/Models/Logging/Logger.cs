using System.Reflection;

namespace BookCatalog.DataLayer.Logging
{
    public class Logger : IGeneralLogger
    {
        public void Error(Exception ex) { 
        
            Console.WriteLine(ex.Message);
        }

        // using reflection we display the properties of the instance of a class implementing IEntity
        public void Log<T>(string message, T item) where T : IEntity
        {
            Console.WriteLine(message);

            // get the type of the specific item
            Type type = item.GetType();

            // get the interfaces implemented in this specifc type
            Type[] interfaces = type.GetInterfaces();

            // get the specific interface
            Type interfaceType = typeof(IEntity);

            // getting the properties of this interface (id, creation and update date)
            PropertyInfo[] interfaceProperties = interfaceType.GetProperties();

            // show this data seperate
            foreach (PropertyInfo interfaceProperty in interfaceProperties) 
            {
                var value = interfaceProperty.GetValue(item);
                Console.WriteLine($"{interfaceProperty.Name}: {value}");
            }

            // get the properties of the specific class
            PropertyInfo[] classProperties = type.GetProperties();

            // Use LINQ to filter the specific prorties of the class and not the implemented interface
            var classOnlyProperties = classProperties
            .Where(p => !interfaceProperties.Any(ip => ip.Name == p.Name))
            .ToArray();

            // show the properties that are in this class (with indentation because it looks nicer)
            foreach (PropertyInfo info in classOnlyProperties)
            {
                var value = info.GetValue(item);
                Console.WriteLine($"\t{info.Name}: {value}");
            }
        }

        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
