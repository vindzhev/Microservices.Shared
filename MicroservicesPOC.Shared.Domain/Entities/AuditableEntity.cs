namespace MicroservicesPOC.Shared.Domain
{
    using System;

    using MicroservicesPOC.Shared.Domain.Interfaces;

    public class AuditableEntity : IAuditableEntity
    {
        public Guid CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public Guid ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
