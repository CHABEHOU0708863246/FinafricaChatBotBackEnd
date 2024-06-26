using LeilAFinafrica.Domain.Models;

namespace LeilAFinafrica.Services
{
    public class InsurancePackService
    {
        // Constantes pour les calculs de prime et de facteur d'ajustement
        private const decimal BASE_PREMIUM = 500;
        private const decimal BONUS_PER_POINT = 50;
        private const decimal RISK_ADJUSTMENT_FACTOR = 0.05m;

        public List<InsurancePack> GetInsurancePacks(int totalBonusPoints, RiskCategory riskCategory)
        {
            var packs = new List<InsurancePack>
        {
            new InsurancePack
            {
                Name = "Pack DALAL",
                Price3Months = CalculatePremium(3, totalBonusPoints, riskCategory),
                Price6Months = CalculatePremium(6, totalBonusPoints, riskCategory),
                Price12Months = CalculatePremium(12, totalBonusPoints, riskCategory),
                IncludedCoverages = new List<string> { "Responsabilité civile", "Défense et recours", "Assistance 24/7" }
            },
            new InsurancePack
            {
                Name = "Pack SOUTOUREU",
                Price3Months = CalculatePremium(3, totalBonusPoints, riskCategory),
                Price6Months = CalculatePremium(6, totalBonusPoints, riskCategory),
                Price12Months = CalculatePremium(12, totalBonusPoints, riskCategory),
                IncludedCoverages = new List<string> { "Responsabilité civile", "Défense et recours", "Assistance 24/7", "Bris de glace" }
            }
        };

            return packs;
        }

        private decimal CalculatePremium(int durationMonths, int totalBonusPoints, RiskCategory riskCategory)
        {
            decimal adjustedBasePremium = BASE_PREMIUM + (totalBonusPoints * BONUS_PER_POINT);
            decimal riskAdjustment = adjustedBasePremium * RiskAdjustmentFactor(riskCategory);
            decimal totalPremium = adjustedBasePremium + riskAdjustment;

            // Ajuster le totalPremium en fonction de la durée
            switch (durationMonths)
            {
                case 3:
                    return totalPremium; // Prix pour 3 mois
                case 6:
                    return totalPremium * 2; // Prix pour 6 mois (double le prix pour 3 mois)
                case 12:
                    return totalPremium * 4; // Prix pour 12 mois (quadruple le prix pour 3 mois)
                default:
                    throw new ArgumentException("Invalid duration", nameof(durationMonths));
            }
        }


        private decimal RiskAdjustmentFactor(RiskCategory riskCategory)
        {
            switch (riskCategory)
            {
                case RiskCategory.LowRisk:
                    return 1; // Aucune augmentation
                case RiskCategory.MediumRisk:
                    return 1.1m; // 10% d'augmentation
                case RiskCategory.HighRisk:
                    return 1.2m; // 20% d'augmentation
                default:
                    throw new ArgumentException("Invalid risk category", nameof(riskCategory));
            }
        }


        public enum RiskCategory
        {
            LowRisk,
            MediumRisk,
            HighRisk
        }
    }
}
