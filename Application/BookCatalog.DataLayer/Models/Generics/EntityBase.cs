namespace BookCatalog.DataLayer.Models.Generics
{
    public class EntityBase : IEntity
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreationDate { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset UpdateDate { get; set; } = DateTimeOffset.Now;
        public override string ToString() => $"Id: {Id.ToString()}\nDate of creation: {CreationDate}";
        public virtual string Log(string message)
        {
            return $"{message}:\n{ToString()}" ;
        }
    }
}
