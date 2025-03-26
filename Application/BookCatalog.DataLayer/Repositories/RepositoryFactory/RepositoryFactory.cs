using Microsoft.Extensions.Configuration;
using System.Reflection;
using BookCatalog.DataLayer.Logging;
using BookCatalog.DataLayer.Repositories;
using BookCatalog.DataLayer.FileStorage.Formatting;
using BookCatalog.DataLayer.FileStorage.Filesystems;

namespace BookCatalog.DataLayer.Repositories.RepositoryFactory
{
    public class RepositoryFactory<T> where T : class, IEntity, new ()
    {
        private IConfiguration Configuration;
        private IGeneralLogger logger;
            
        public RepositoryFactory()
        {
            Configuration =  new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .Build(); ;
        }
        private ISerialize<T>? formatter;
        private IFileSystem<T>? fileSystem;
        private IRepository<T>? repository;
        
        public IRepository<T> GetDataSystem()
        {
            LoadContext loadContextFormatter;
            AssemblyName assemblyNameFormatter;
            GetFormatter(out loadContextFormatter, out assemblyNameFormatter);

            LoadContext loadContextFileSystem;
            AssemblyName assemblyNameFileSystem;
            GetFileSystem(out loadContextFileSystem, out assemblyNameFileSystem);

            LoadContext loadContextRepository;
            AssemblyName assemblyNameRepository;
            GetRepository(out loadContextRepository, out assemblyNameRepository);

            return repository;
        }

        private void GetRepository(out LoadContext loadContext, out AssemblyName assemblyName)
        {
            // Check Configuration
            string? repositoryAssemblyName = Configuration["Repository:RepositoryAssembly"];
            string repositoryLocation = AppDomain.CurrentDomain.BaseDirectory
                                        + repositoryAssemblyName;
            Console.WriteLine(repositoryAssemblyName);
            Console.WriteLine(repositoryLocation);


            // Load the assembly
            loadContext = new LoadContext(repositoryLocation);
            Console.WriteLine($"loadcontext: {loadContext.ToString()}");

            assemblyName = new AssemblyName(Path.GetFileNameWithoutExtension(repositoryAssemblyName));
            Console.WriteLine($"assemblyName: {assemblyName.ToString()}\n");


            Assembly repositoryAssembly = loadContext.LoadFromAssemblyName(assemblyName);
            Console.WriteLine($"repositoryassembly: {repositoryAssembly.ToString()}\n");



            // Look for the type
            string? repositoryTypeName = Configuration["Repository:RepositoryType"];
            Type repositoryType = repositoryAssembly.ExportedTypes
                                .First(t => t.FullName == repositoryTypeName);
            Console.WriteLine($"repositoryname: {repositoryTypeName.ToString()}\n");
            Console.WriteLine($"Repositorytype: {repositoryType.ToString()}\n");



            // Create the data reader
            repository = (T)Activator.CreateInstance(repositoryType, formatter) as IRepository<T>;
            if (repository is null)
            {
                throw new InvalidOperationException(
                    $"Unable to create instance of {repositoryType} as IRepository<T> with parameter {formatter}");
            }
        }

        private void GetFileSystem(out LoadContext loadContext, out AssemblyName assemblyName)
        {
            // Check Configuration
            string? fileName = Configuration["FileSystem:FileName"];
            string? fileSystemAssemblyName = Configuration["FileSystem:FileSystemAssembly"];
            string fileSystemLocation = AppDomain.CurrentDomain.BaseDirectory + fileSystemAssemblyName;
            Console.WriteLine(fileSystemAssemblyName);
            Console.WriteLine(fileSystemLocation);

            // Load the assembly
            loadContext = new LoadContext(fileSystemLocation);
            Console.WriteLine($"loadcontext: {loadContext.ToString()}");

            assemblyName = new AssemblyName(Path.GetFileNameWithoutExtension(fileSystemAssemblyName));
            Console.WriteLine($"assemblyName: {assemblyName.ToString()}\n");

            Assembly fileSystemAssembly = loadContext.LoadFromAssemblyName(assemblyName);
            Console.WriteLine($"fileSystemassembly: {fileSystemAssembly.ToString()}\n");

            // Look for the type
            string? fileSystemTypeName = Configuration["FileSystem:FileSystemType"];
            Type fileSystemType = fileSystemAssembly.ExportedTypes.First(t => t.FullName == fileSystemTypeName);
            Console.WriteLine($"fileSystemname: {fileSystemTypeName.ToString()}\n");
            Console.WriteLine($"Repositorytype: {fileSystemType.ToString()}\n");

            // Verify type constraints
            Console.WriteLine($"Type constraints for {fileSystemType.Name}:");
            foreach (var constraint in fileSystemType.GetGenericArguments()[0].GetGenericParameterConstraints())
            {
                Console.WriteLine($" - {constraint}");
            }
            Console.Write(formatter.ToString());

            // Ensure formatter is initialized
            if (formatter == null)
            {
                throw new InvalidOperationException("Formatter is not initialized.");
            }

            // Create the data reader
            if (fileSystemType.IsGenericType)
            {
                Type genericFileSystemType = fileSystemType.MakeGenericType(typeof(T));
                Console.WriteLine($"Loaded formatter type: {genericFileSystemType.FullName}");

                // Create the data reader instance
                var fileSystem = Activator.CreateInstance(genericFileSystemType, new object[] { fileName, formatter });

                // Do something with the formatter...
                Console.WriteLine($"Created formatter instance: {fileSystem.GetType()}");
            }
            else
            {
                throw new InvalidOperationException($"{fileSystemType.Name} is not a generic type.");
            }
        }

        private void GetFormatter(out LoadContext loadContext, out AssemblyName assemblyName)
        {
            // Check Configuration
            string? formatterAssemblyName = Configuration["Formatter:FormatterAssembly"];
            string formatterLocation = AppDomain.CurrentDomain.BaseDirectory + formatterAssemblyName;
            Console.WriteLine(formatterAssemblyName);
            Console.WriteLine(formatterLocation);

            // Load the assembly
            loadContext = new LoadContext(formatterLocation);
            Console.WriteLine($"loadcontext: {loadContext.ToString()}");

            assemblyName = new AssemblyName(Path.GetFileNameWithoutExtension(formatterAssemblyName));
            Console.WriteLine($"assemblyName: {assemblyName.ToString()}\n");

            Assembly formatterAssembly = loadContext.LoadFromAssemblyName(assemblyName);
            Console.WriteLine($"formatterassembly: {formatterAssembly.ToString()}\n");

            // Look for the type
            string? formatterTypeName = Configuration["Formatter:FormatterType"];
            Type formatterType = formatterAssembly.ExportedTypes.First(t => t.FullName == formatterTypeName);
            Console.WriteLine($"formattername: {formatterTypeName.ToString()}\n");
            Console.WriteLine($"formattertype: {formatterType.ToString()}\n");

            // Create the data reader
            if (formatterType.IsGenericType)
            {
                Type genericFormatterType = formatterType.MakeGenericType(typeof(T));
                Console.WriteLine($"Loaded formatter type: {genericFormatterType.FullName}");

                // Create the data reader instance
                formatter = Activator.CreateInstance(genericFormatterType) as ISerialize<T>;

                // Do something with the formatter...
                Console.WriteLine($"Created formatter instance: {formatter.GetType()}");
                formatter = Activator.CreateInstance(genericFormatterType) as ISerialize<T>;
                if (formatter == null)
                {
                    throw new InvalidOperationException($"Failed to create an instance of {genericFormatterType.FullName}");
                }
            }
            else
            {
                throw new InvalidOperationException($"{formatterType.Name} is not a generic type.");
            }
        }
    }
}
