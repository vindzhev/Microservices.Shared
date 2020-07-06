namespace MicroservicesPOC.Shared.Common.Models
{
    using System.Collections.Generic;

    public class CalculatePriceResult
    {
        public decimal TotalPrice { get; set; }

        public IDictionary<string, decimal> CoverPrices { get; set; }
    }
}
