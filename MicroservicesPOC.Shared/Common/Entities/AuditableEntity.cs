namespace MicroservicesPOC.Shared.Common.Entities
{
    using System;

    using MicroservicesPOC.Shared.Common.Entities.Interfaces;

    public class AuditableEntity : IAuditableEntity
    {
        public Guid CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public Guid ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
