using LeilAFinafrica.Domain.DTOs;
using LeilAFinafrica.Domain.Models;
using LeilAFinafrica.Services;
using LeilAFinafrica.Services.VehicleQuoteServices;
using Microsoft.AspNetCore.Mvc;

namespace LeilAFinafrica.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FleetQuoteController : ControllerBase
    {
        private readonly IVehicleQuoteService _quoteService;
        private readonly InsurancePackService _packService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FleetQuoteController(IVehicleQuoteService quoteService, InsurancePackService packService, IHttpContextAccessor httpContextAccessor)
        {
            _quoteService = quoteService;
            _packService = packService;
            _httpContextAccessor = httpContextAccessor;
        }


        /// <summary>
        /// Démarre la simulation d'assurance.
        /// </summary>
        /// <returns>Message de confirmation</returns>
        [HttpPost("start-simulation")]
        public IActionResult StartSimulation()
        {
            try
            {
                _quoteService.GenerateContentUsingGeminiAsync();
                return Ok("Simulation démarrée avec succès.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur lors du démarrage de la simulation : {ex.Message}");
            }
        }



        /// <summary>
        /// Collecte les informations du véhicule et les stocke dans la session.
        /// </summary>
        /// <param name="vehicleInput">Données du véhicule</param>
        /// <returns>Message de confirmation</returns>
        [HttpPost("collect-vehicle-info")]
        public async Task<ActionResult<string>> CollectVehicleInformation([FromBody] VehicleDTOs vehicleInput)
        {
            if (vehicleInput == null)
            {
                return BadRequest("Les informations du véhicule sont requises.");
            }

            HttpContext.Session.SetObject("VehicleInfo", vehicleInput); // Sauvegarde des informations dans la session
            return Ok("Merci pour ces informations.");
        }



        /// <summary>
        /// Calcule le tarif de base à partir des informations du véhicule stockées dans la session.
        /// </summary>
        /// <returns>Tarif de base calculé</returns>
        [HttpGet("calculate-base-tariff")]
        public async Task<ActionResult<decimal>> CalculateBaseTariff()
        {
            var vehicleInfo = HttpContext.Session.GetObject<VehicleDTOs>("VehicleInfo");

            if (vehicleInfo == null)
            {
                return StatusCode(500, "Erreur lors du calcul du tarif de base : Les informations du véhicule ne sont pas disponibles.");
            }

            try
            {
                decimal baseTariff = await _quoteService.CalculateBaseTariff(vehicleInfo); // Passez les informations du véhicule
                return Ok(baseTariff);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur lors du calcul du tarif de base : {ex.Message}");
            }
        }


        /// <summary>
        /// Ajuste le tarif en fonction de la valeur de marché.
        /// </summary>
        /// <param name="request">Détails de la demande d'ajustement du tarif</param>
        /// <returns>Tarif ajusté</returns>
        [HttpPost("adjust-tariff")]
        public async Task<ActionResult<decimal>> AdjustTariff(AdjustTariffRequest request)
        {
            try
            {
                var adjustedTariff = await _quoteService.AdjustTariffForMarketValue(request.BaseTariff, request.MarketValue, request.NewValue);
                return Ok(adjustedTariff);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur lors de l'ajustement du tarif : {ex.Message}");
            }
        }


        /// <summary>
        /// Ajoute les coûts des garanties au tarif ajusté.
        /// </summary>
        /// <param name="request">Détails de la demande d'ajout des coûts des garanties</param>
        /// <returns>Coût total avec garanties</returns>
        [HttpPost("add-guarantee-costs")]
        public async Task<ActionResult<decimal>> AddGuaranteeCosts(AddGuaranteeCostsRequest request)
        {
            try
            {
                var totalCost = await _quoteService.AddGuaranteeCosts(request.AdjustedTariff, request.ChosenGuarantees);
                return Ok(totalCost);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur lors de l'ajout des coûts des garanties : {ex.Message}");
            }
        }


        /// <summary>
        /// Applique un coefficient de durée au tarif avec garanties.
        /// </summary>
        /// <param name="request">Détails de la demande d'application du coefficient de durée</param>
        /// <returns>Prime d'assurance calculée</returns>
        [HttpPost("apply-duration-coefficient")]
        public async Task<ActionResult<decimal>> ApplyDurationCoefficient(ApplyDurationCoefficientRequest request)
        {
            try
            {
                var premium = await _quoteService.ApplyDurationCoefficient(request.TariffWithGuarantees, request.Duration);
                return Ok(premium);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur lors de l'application du coefficient de durée : {ex.Message}");
            }
        }



        /// <summary>
        /// Calcule la prime d'assurance finale pour le véhicule.
        /// </summary>
        /// <param name="vehicle">Détails du véhicule</param>
        /// <returns>Prime d'assurance calculée</returns>
        [HttpPost("calculate-insurance-premium")]
        public async Task<ActionResult<decimal>> CalculateInsurancePremium([FromBody] VehicleDTOs vehicle)
        {
            try
            {
                if (vehicle == null)
                {
                    return BadRequest("Les informations du véhicule sont requises.");
                }

                decimal premium = await _quoteService.CalculateInsurancePremium(vehicle);
                return Ok(premium);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur lors du calcul de la prime d'assurance : {ex.Message}");
            }
        }






        /// <summary>
        /// Affiche la prime d'assurance calculée.
        /// </summary>
        /// <param name="premium">Prime d'assurance à afficher</param>
        /// <returns>Message de confirmation</returns>
        [HttpPost("display-insurance-premium")]
        public ActionResult DisplayInsurancePremium(decimal premium)
        {
            try
            {
                _quoteService.DisplayInsurancePremium(premium);
                return Ok("Prime d'assurance affichée avec succès.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur lors de l'affichage de la prime d'assurance : {ex.Message}");
            }
        }


        /// <summary>
        /// Génère du contenu en utilisant Gemini.
        /// </summary>
        /// <returns>Message de confirmation</returns>
        [HttpPost("generate-content-using-gemini")]
        public ActionResult GenerateContentUsingGemini()
        {
            try
            {
                _quoteService.GenerateContentUsingGeminiAsync();
                return Ok("Contenu généré avec succès.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur lors de la génération de contenu avec Gemini : {ex.Message}");
            }
        }


        /// <summary>
        /// Affiche les packs d'assurance disponibles en fonction des points de bonus et de la catégorie de risque.
        /// </summary>
        /// <param name="totalBonusPoints">Points de bonus totaux</param>
        /// <param name="riskCategory">Catégorie de risque</param>
        /// <returns>Liste des packs d'assurance disponibles</returns>
        [HttpGet("show-insurance-packs")]
        public ActionResult<List<InsurancePack>> ShowInsurancePacks(int totalBonusPoints, InsurancePackService.RiskCategory riskCategory)
        {
            try
            {
                var packs = _packService.GetInsurancePacks(totalBonusPoints, riskCategory);
                return Ok(packs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur lors de l'affichage des packs d'assurance : {ex.Message}");
            }
        }



        /// <summary>
        /// Choisit un pack d'assurance par son nom.
        /// </summary>
        /// <param name="packName">Nom du pack d'assurance</param>
        /// <returns>Pack d'assurance choisi</returns>
        [HttpPost("choose-insurance-pack")]
        public async Task<ActionResult<InsurancePack>> ChooseInsurancePack(string packName)
        {
            try
            {
                var chosenPack = await _quoteService.ChooseInsurancePack(packName);
                return Ok(chosenPack);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur lors du choix du pack d'assurance : {ex.Message}");
            }
        }


        /// <summary>
        /// Traite le paiement d'un montant donné.
        /// </summary>
        /// <param name="amount">Montant à payer</param>
        /// <returns>Message de confirmation</returns>
        [HttpPost("process-payment")]
        public async Task<ActionResult<string>> ProcessPayment(decimal amount)
        {
            try
            {
                var paymentResponse = await _quoteService.ProcessPayment(amount);
                return Ok(paymentResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur lors du traitement du paiement : {ex.Message}");
            }
        }
    }
}