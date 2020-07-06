namespace MicroservicesPOC.Shared.Messaging.Events
{
    using System;
    
    using MediatR;

    using MicroservicesPOC.Shared.Common.Models;

    public class PolicyCreatedEvent : INotification
    {
        public Guid PolicyNumber { get; set; }

        public string ProductCode { get; set; }

        public DateTime PolicyFrom { get; set; }

        public DateTime PolicyTo { get; set; }

        public PersonDTO PolicyHolder { get; set; }

        public decimal TotalPremium { get; set; }
    }
}
