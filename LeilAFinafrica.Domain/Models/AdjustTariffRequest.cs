namespace LeilAFinafrica.Domain.Models
{
    public class AdjustTariffRequest
    {
        public decimal BaseTariff { get; set; }
        public decimal MarketValue { get; set; }
        public decimal NewValue { get; set; }
    }

}
