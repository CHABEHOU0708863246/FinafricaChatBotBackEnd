using System.ComponentModel.DataAnnotations;

namespace LeilAFinafrica.Domain.DTOs
{
    public class InsuranceCalculationRequest
    {
        [Required]
        public VehicleDTOs Vehicle { get; set; }
    }
}
