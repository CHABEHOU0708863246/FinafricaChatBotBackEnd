using LeilAFinafrica.Domain.DTOs;
using LeilAFinafrica.Domain.Models;

namespace LeilAFinafrica.Services.VehicleQuoteServices
{
    public interface IVehicleQuoteService
    {
        /// <summary>
        /// Démarre la simulation de tarification d'assurance.
        /// Cette méthode peut initialiser les variables ou préparer le contexte de la simulation.
        /// </summary>
        Task<string> StartInsuranceSimulation();

        /// <summary>
        /// Collecte les informations sur le véhicule.
        /// </summary>
        /// <param name="vehicleInput">Les détails du véhicule à utiliser pour la simulation de tarification.</param>
        Task<string> CollectVehicleInformation(VehicleDTOs vehicleInput);

        /// <summary>
        /// Calcule le tarif de base basé sur les informations du véhicule.
        /// </summary>
        /// <returns>Le tarif de base calculé.</returns>
        Task<decimal> CalculateBaseTariff(VehicleDTOs vehicle);

        /// <summary>
        /// Ajuste le tarif de base en fonction de la valeur vénale du véhicule.
        /// </summary>
        /// <param name="baseTariff">Le tarif de base calculé.</param>
        /// <param name="marketValue">La valeur vénale du véhicule.</param>
        /// <returns>Le tarif ajusté en fonction de la valeur vénale.</returns>
        Task<decimal> AdjustTariffForMarketValue(decimal baseTariff, decimal marketValue, decimal newValue);

        /// <summary>
        /// Ajoute les coûts des garanties au tarif ajusté.
        /// </summary>
        /// <param name="adjustedTariff">Le tarif ajusté après prise en compte de la valeur vénale.</param>
        /// <param name="chosenGuarantees">Les garanties choisies par l'utilisateur.</param>
        /// <returns>Le tarif avec les coûts des garanties ajoutées.</returns>
        Task<decimal> AddGuaranteeCosts(decimal adjustedTariff, List<Guarantee> chosenGuarantees);

        /// <summary>
        /// Applique le coefficient de durée au tarif final.
        /// </summary>
        /// <param name="tariffWithGuarantees">Le tarif après ajout des coûts des garanties.</param>
        /// <param name="durationCoefficient">Le coefficient de durée basé sur la durée du contrat.</param>
        /// <returns>La prime d'assurance finale.</returns>
        Task<decimal> ApplyDurationCoefficient(decimal tariffWithGuarantees, decimal duration);

        /// <summary>
        /// Calcule la prime d'assurance basée sur les informations du véhicule collectées.
        /// </summary>
        /// <returns>La prime d'assurance calculée.</returns>
        Task<decimal> CalculateInsurancePremium(VehicleDTOs vehicle);

        /// <summary>
        /// Affiche la prime d'assurance calculée.
        /// </summary>
        /// <param name="premium">La prime d'assurance à afficher.</param>
        void DisplayInsurancePremium(decimal premium);

        /// <summary>
        /// Génère du contenu en utilisant l'API Gemini.
        /// Cette méthode peut être utilisée pour des interactions dynamiques avec les utilisateurs.
        /// </summary>
        Task GenerateContentUsingGeminiAsync();

        /// <summary>
        /// Affiche les packs d'assurance disponibles.
        /// </summary>
        /// <param name="packs">La liste des packs d'assurance disponibles à afficher.</param>
        Task<List<InsurancePack>> ShowInsurancePacks(List<InsurancePack> packs);

        /// <summary>
        /// Permet à l'utilisateur de choisir un pack d'assurance parmi ceux affichés.
        /// </summary>
        /// <returns>Le pack d'assurance choisi par l'utilisateur.</returns>
        Task<InsurancePack> ChooseInsurancePack(string packName);

        /// <summary>
        /// Traite le paiement pour un montant spécifié.
        /// </summary>
        /// <param name="amount">Le montant à payer.</param>
        Task<string> ProcessPayment(decimal amount);
    }
}
