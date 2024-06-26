namespace LeilAFinafrica.Domain.Models
{
    public class InsuranceOffer
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public List<string> Coverages { get; set; } = new List<string>();
    }
}
