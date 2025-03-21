using BookCatalog.DataLayer.FileStorage.Formatting;
using System.Reflection;

namespace BookCatalog.DataLayer.FileStorage.Filesystems
{
    // This class is responsible for reading and writing data to a file, it is generic so can be used by any class that implements IEntity
    public class FileSystem<T> : IFileSystem<T> where T : class, IEntity
    {
        // The file path to the file that will be read and written to
        protected readonly string _filePath;

        // The serializer that will be used to serialize and deserialize the data (also generic so it can be any filetype)
        protected readonly ISerialize<T> _serializer;

        // the file type is not created in this class, it is given as a parameter
        // dependency injection is used to inject the serializer, this is so that the class can be used with any type of file
        // this is the Single Responsibility Principle, we don't want this class to be responsible for creating the file type
        public FileSystem(string fileName, ISerialize<T> serializer)
        {
            _serializer = serializer;

            // the file path is created using the file name
            _filePath = FilePath(fileName);
        }

        // This method is used to read the data from the file
        public IEnumerable<T> Read()
        {
            if (File.Exists(_filePath))
            {
                // if the file exists, the data is read from the file
                string output = File.ReadAllText(_filePath);
                return _serializer.DeSerializer(output);
            }

            // if the file does not exist, an empty list is returned but most importantly the file is created
            CreateFile();
            return Enumerable.Empty<T>();
        }

        // This method is used to save the data to the file
        public void Save(IEnumerable<T> input)
        {
            // the data is serialized and then written to the file
            string formatted = _serializer.Serializer((List<T>)input);
            File.WriteAllText(_filePath, formatted);
        }

        public void CreateFile()
        {
            // This is the function to create a file, used in the read method
            File.Create(_filePath);
        }

        // This method is used to get the file path of the file that will be read and written to
        private string FilePath(string fileName)
        {
            if (fileName == null)
            {
                // if the file name is null, a default file name is used
                fileName = "defaultFileName.json";
            }

            // we use reflection to get the directory of the executing assembly
            string executableDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            // we then combine the directory with the file name to get the full path
            return Path.Combine(Path.GetFullPath(executableDirectory), fileName);
        }
    }
}
