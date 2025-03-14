namespace BookCatalog.DataLayer.Filesystems
{
    public interface IFileSystem<T> where T : IEntity
    {
        public void CreateFile();
        public IEnumerable<T> Read();
        public void Save(IEnumerable<T> list);
    }
}
