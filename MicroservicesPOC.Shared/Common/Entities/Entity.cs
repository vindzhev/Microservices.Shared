namespace MicroservicesPOC.Shared.Common.Entities
{
    public abstract class Entity<TKey>
    {
        public virtual TKey Id { get; protected set; }
    }
}
