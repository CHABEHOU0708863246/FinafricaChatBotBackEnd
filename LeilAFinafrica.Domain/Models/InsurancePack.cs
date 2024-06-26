namespace LeilAFinafrica.Domain.Models
{
    public class InsurancePack
    {
        public string Name { get; set; }
        public decimal Price3Months { get; set; }
        public decimal Price6Months { get; set; }
        public decimal Price12Months { get; set; }
        public List<string> IncludedCoverages { get; set; }
    }
}
