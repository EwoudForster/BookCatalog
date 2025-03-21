namespace BookCatalog.DataLayer.FileStorage.Formatting
{

    // This interface is implemented in any formatter we create
    public interface ISerialize<T> where T : class
    {
        public List<T> DeSerializer(string value);
        public string Serializer(List<T> items);
    }
}
