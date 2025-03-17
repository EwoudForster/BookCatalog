namespace BookCatalog.DataLayer.Models.Generics
{
    public interface IEntity
    {
        Guid Id { get; set; }
        DateTimeOffset CreationDate { get; set; }
        string Log(string message);
    }
}
