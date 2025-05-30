namespace BookCatalog.DAL
{
    // The base class of any entities created that will be using the repository
    public abstract class EntityBase : IEntity
    {
        // guid for easy identification of the entity
        public Guid Id { get; set; }

        // Date of creation and last updated date for logging purposes
        public DateTimeOffset CreationDate { get; set; }= DateTimeOffset.Now;
        public DateTimeOffset LastUpdated { get; set; } = DateTimeOffset.Now;

        public override string ToString() => $"Id: {Id.ToString()}\nDate of creation: {CreationDate}";
    }
}
