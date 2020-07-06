namespace MicroservicesPOC.Shared.Common.Models
{
    using System;
    using System.Collections.Generic;

    public class PolicyDTO
    {
        public Guid Number { get; set; }

        public string ProductCode { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public string PolicyHolder { get; set; }

        public decimal TotalPremum { get; set; }

        public ICollection<string> Covers { get; set; }
    }
}
