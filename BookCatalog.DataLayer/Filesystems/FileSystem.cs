using BookCatalog.DataLayer.Formatting;
using System.Reflection;

namespace BookCatalog.DataLayer.Filesystems
{
    public class FileSystem<T> : IFileSystem<T> where T : class, IEntity
    {
        protected readonly string _filePath;
        protected readonly ISerialize<T> _serializer;
        public FileSystem(string fileName, ISerialize<T> serializer)
        {
            _serializer = serializer;
            _filePath = FilePath(fileName);
        }

        public IEnumerable<T> Read()
        {
            if (File.Exists(_filePath))
            {
                string output = File.ReadAllText(_filePath);
                return _serializer.DeSerializer(output);
            }
            CreateFile();
            return Enumerable.Empty<T>();
        }

        public void Save(IEnumerable<T> input)
        {
            string formatted = _serializer.Serializer((List<T>)input);
            File.WriteAllText(_filePath, formatted);
        }

        public void CreateFile()
        {
            File.Create(_filePath);
        }

        private string FilePath(string fileName)
        {
            if (fileName == null)
            {
                fileName = "defaultFileName.json";
            }
            string executableDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return Path.Combine(Path.GetFullPath(Path.Combine(executableDirectory, @"..\..\..\..")), fileName);
        }
    }
}
