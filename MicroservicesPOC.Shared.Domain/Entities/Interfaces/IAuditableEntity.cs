namespace MicroservicesPOC.Shared.Domain.Interfaces
{
    using System;

    public interface IAuditableEntity
    {
        public Guid CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public Guid ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
