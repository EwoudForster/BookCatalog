namespace BookCatalog.DAL.Storage.FileStorage.Filesystems
{
    // This interface is used to define the methods that are used to read and write data to a file
    public interface IFileSystem<T> where T : IEntity
    {
        public IEnumerable<T> Read();
        public void Save(IEnumerable<T> list);
    }
}
