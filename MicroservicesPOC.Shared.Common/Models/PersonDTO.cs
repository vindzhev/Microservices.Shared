namespace MicroservicesPOC.Shared.Common.Models
{
    public class PersonDTO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public string TaxId { get; set; }

        public override string ToString() => $"{this.FirstName} {this.LastName}";
    }
}
