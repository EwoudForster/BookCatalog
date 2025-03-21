namespace BookCatalog.DataLayer.Logging
{
    public interface IGeneralLogger
    {
        void Error(Exception ex);
        void Log<T>(string message, T item) where T:IEntity;
        void Log(string message);
    }
}
