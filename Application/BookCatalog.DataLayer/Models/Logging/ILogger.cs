namespace BookCatalog.DataLayer.Logging
{
    public interface ILogger
    {
        void Error(Exception ex);
        void Log<T>(string message, T item) where T:IEntity;
        void Log(string message);
    }
}
