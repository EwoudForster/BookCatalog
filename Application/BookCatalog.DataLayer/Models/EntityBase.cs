namespace BookCatalog.DataLayer
{
    public abstract class EntityBase : IEntity
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreationDate { get; set; }= DateTimeOffset.Now;
        public DateTimeOffset LastUpdated { get; set; } = DateTimeOffset.Now;

        public override string ToString() => $"Id: {Id.ToString()}\nDate of creation: {CreationDate}";
    }
}
