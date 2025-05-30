namespace BookCatalog.DAL.Logging
{
    public interface IGeneralLogger
    {
        void Error(Exception ex, string message);
        void Log<T>(string message, T item) where T:IEntity;
        void Log(string message);
    }
}
