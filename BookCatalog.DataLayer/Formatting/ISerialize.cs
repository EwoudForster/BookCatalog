namespace BookCatalog.DataLayer.Formatting
{
    public interface ISerialize<T> where T : class
    {
        public List<T> DeSerializer(string value);
        public string Serializer(List<T> items);
    }
}
