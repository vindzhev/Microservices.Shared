namespace MicroservicesPOC.Shared.Domain
{
    public abstract class Entity<TKey>
    {
        public virtual TKey Id { get; protected set; }
    }
}
