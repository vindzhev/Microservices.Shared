namespace MicroservicesPOC.Shared.Common.Models
{
    using System;

    using System.Collections.Generic;
    
    public class CalculationData
    {
        public CalculationData() { }

        public CalculationData(string productCode, DateTimeOffset policyFrom, DateTimeOffset policyTo, ICollection<string> selectedCovers, ICollection<QuestionAnswerDTO> answers)
        {
            this.ProductCode = productCode;
            this.PolicyFrom = policyFrom;
            this.PolicyTo = policyTo;
            this.SelectedCovers = selectedCovers;
            this.Answers = answers;
        }

        public string ProductCode { get; set; }

        public DateTimeOffset PolicyFrom { get; set; }

        public DateTimeOffset PolicyTo { get; set; }

        public ICollection<string> SelectedCovers { get; set; }

        public ICollection<QuestionAnswerDTO> Answers { get; set; }
    }
}
