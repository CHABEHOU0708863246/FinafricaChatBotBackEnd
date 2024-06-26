namespace LeilAFinafrica.Domain.DTOs
{
    public class VehicleDTOs
    {

        public string Category { get; set; }
        public string Usage { get; set; }
        public string Genre { get; set; }
        public int FiscalPower { get; set; }
        public string EnergyType { get; set; }
        public int SeatNumber { get; set; }
        public decimal NewValue { get; set; }
        public decimal MarketValue { get; set; }
        public bool HasTrailer { get; set; }
        public DateTime FirstRegistrationDate { get; set; }
        public int ContractDuration { get; set; }
    }
}
