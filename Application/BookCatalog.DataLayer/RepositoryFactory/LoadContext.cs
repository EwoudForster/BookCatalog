using System.Reflection;
using System.Runtime.Loader;

namespace BookCatalog.DataLayer.RepositoryFactory
{
        public class LoadContext : AssemblyLoadContext
        {
            private AssemblyDependencyResolver _resolver;

            public LoadContext(string Location)
            {
                _resolver = new AssemblyDependencyResolver(Location);
            }

            protected override Assembly? Load(AssemblyName assemblyName)
            {
                string? assemblyPath = _resolver.ResolveAssemblyToPath(assemblyName);
                if (assemblyPath != null)
                {
                    return LoadFromAssemblyPath(assemblyPath);
                }

                return null;
            }

            protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
            {
                string? libraryPath = _resolver.ResolveUnmanagedDllToPath(unmanagedDllName);

                if (libraryPath != null)
                {
                    return LoadUnmanagedDllFromPath(libraryPath);
                }

                return IntPtr.Zero;
            }
    }
}