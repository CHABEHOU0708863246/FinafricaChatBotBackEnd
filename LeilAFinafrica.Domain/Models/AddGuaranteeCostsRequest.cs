namespace LeilAFinafrica.Domain.Models
{
    public class AddGuaranteeCostsRequest
    {
        public decimal AdjustedTariff { get; set; }
        public List<Guarantee> ChosenGuarantees { get; set; }
    }

}
