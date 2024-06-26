using LeilAFinafrica.Domain.DTOs;
using LeilAFinafrica.Domain.Models;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace LeilAFinafrica.Services.VehicleQuoteServices
{
    public class VehicleQuoteService : IVehicleQuoteService
    {
        private VehicleDTOs _vehicle;
        private decimal _premium;
        private List<InsurancePack> _insurancePacks;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public VehicleQuoteService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _httpClient.BaseAddress = new Uri(_configuration["ApiBaseUrl"]);
            _insurancePacks = new List<InsurancePack>();
        }

        #region Simulation Initialization

        public async Task<string> StartInsuranceSimulation()
        {
            return "Bonjour ! Bienvenue sur Finafrica. Je suis Leila de Finafrica, votre assistant virtuel pour toutes vos questions d'assurance. Comment puis-je vous aider aujourd'hui ?";
        }

        public async Task<string> GenerateContentUsingGeminiStartInsuranceSimulation()
        {
            var content = new
            {
                parts = new[]
                 {
                    new
                    {
                        text = "je souhaite faire la simulation de la tarification de mon vehicule"
                    }
                }
            };

            var jsonContent = JsonSerializer.Serialize(content);

            try
            {
                var response = await _httpClient.PostAsync("https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash-latest:generateContent?key=AIzaSyA_0h5quYmO8FpweK5YJ8GWc_EndbMWNFY",
                    new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                return responseContent;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Une erreur s'est produite lors de l'appel à l'API Gemini : {ex.Message}");
            }
        }

        private class ContentGenerationResponse
        {
            public List<Candidate> candidates { get; set; }
        }

        private class Candidate
        {
            public Content content { get; set; }
        }

        private class Content
        {
            public List<Part> parts { get; set; }
        }

        private class Part
        {
            public string text { get; set; }
        }

        #endregion

        #region Vehicle Information Collection

        public async Task<string> CollectVehicleInformation(VehicleDTOs vehicleInput)
        {
            _vehicle = vehicleInput;
            return "Merci pour ces informations.";
        }

        #endregion

        #region Insurance Premium Calculation

        public async Task<decimal> CalculateBaseTariff(VehicleDTOs vehicle)
        {
            if (vehicle == null)
            {
                throw new ArgumentNullException(nameof(vehicle), "Les informations du véhicule ne sont pas disponibles.");
            }

            if (string.IsNullOrEmpty(vehicle.Category) || string.IsNullOrEmpty(vehicle.Usage) || vehicle.FiscalPower == 0)
            {
                throw new ArgumentException("Les informations du véhicule sont incomplètes.");
            }

            decimal baseTariff = 10000m + (vehicle.FiscalPower * 1000m) + (vehicle.SeatNumber * 500m);

            if (vehicle.Usage == "location")
            {
                baseTariff += 20000m;
            }

            if (vehicle.Genre == "tourisme")
            {
                baseTariff += 15000m;
            }

            return baseTariff;
        }

        public async Task<decimal> AdjustTariffForMarketValue(decimal baseTariff, decimal marketValue, decimal newValue)
        {
            await Task.Delay(100); // Simulation de traitement asynchrone

            decimal factor = marketValue switch
            {
                < 5000000m => 1.0m,
                < 10000000m => 1.2m,
                _ => 1.5m
            };

            // Ajout de la logique pour ajuster le facteur en fonction de la valeur de newValue
            if (newValue > 15000000m && newValue <= 30000000m)
            {
                factor *= 1.1m; // Augmente le facteur de 10% si newValue est dans cette plage
            }
            else if (newValue > 30000000m)
            {
                factor *= 1.2m; // Augmente le facteur de 20% si newValue dépasse 30 millions
            }

            return baseTariff * factor;
        }

        public async Task<decimal> AddGuaranteeCosts(decimal adjustedTariff, List<Guarantee> chosenGuarantees)
        {
            await Task.Delay(100); // Simulation de traitement asynchrone

            var guaranteeCosts = new Dictionary<string, decimal>
            {
                { "Défense et Recours", 10000m },
                { "Bris de Glace", 15000m },
                { "Vol", 20000m },
                { "Incendie", 18000m },
                { "Dommages Tous Accidents", 25000m },
                { "Perte Totale", 30000m },
                { "Inondation", 22000m },
                { "Responsabilité Civile", 12000m },
                { "Assistance 24/7", 17000m },
                { "Protection Juridique", 16000m },
            };

            decimal totalGuaranteeCost = chosenGuarantees
                .Where(g => guaranteeCosts.ContainsKey(g.Name))
                .Sum(g => guaranteeCosts[g.Name]);

            return adjustedTariff + totalGuaranteeCost;
        }

        public async Task<decimal> ApplyDurationCoefficient(decimal tariffWithGuarantees, decimal duration)
        {
            await Task.Delay(100); // Simulation de traitement asynchrone

            var durationCoefficients = new Dictionary<decimal, decimal>
            {
                { 3m, 0.25m },
                { 6m, 0.5m },
                { 12m, 1.0m }
            };

            if (durationCoefficients.ContainsKey(duration))
            {
                return tariffWithGuarantees * durationCoefficients[duration];
            }

            throw new ArgumentException("Durée du contrat invalide.");
        }

        public async Task<decimal> CalculateInsurancePremium(VehicleDTOs vehicle)
        {
            try
            {
                decimal baseTariff = await Task.Run(() => CalculateBaseTariff(vehicle));
                decimal adjustedTariff = await Task.Run(() => AdjustTariffForMarketValue(baseTariff, vehicle.MarketValue, vehicle.NewValue));

                // Liste des garanties choisies par l'utilisateur
                List<Guarantee> chosenGuarantees = new List<Guarantee>
                {
                    new Guarantee { Name = "Responsabilité Civile" },
                    new Guarantee { Name = "Assistance 24/7" },
                    new Guarantee { Name = "Vol" },
                    new Guarantee { Name = "Incendie" },
                    new Guarantee { Name = "Dommages Tous Accidents" },
                    new Guarantee { Name = "Perte Totale" },
                    new Guarantee { Name = "Inondation" }
                };

                decimal tariffWithGuarantees = await Task.Run(() => AddGuaranteeCosts(adjustedTariff, chosenGuarantees));
                _premium = await Task.Run(() => ApplyDurationCoefficient(tariffWithGuarantees, vehicle.ContractDuration));
                return _premium;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erreur lors du calcul de la prime d'assurance : {ex.Message}");
            }
        }

        public void DisplayInsurancePremium(decimal premium)
        {
            Console.WriteLine($"Voici la prime d'assurance pour votre véhicule : {premium} FCFA.");
        }

        #endregion

        #region Content Generation
        public async Task GenerateContentUsingGeminiAsync()
        {
            var content = new
            {
                parts = new[]
                {
                    new
                    {
                        text = "Contenu généré par l'API Gemini."
                    }
                },
                role = "model",
                finishReason = "STOP",
                index = 0,
                safetyRatings = new[]
                {
                    new
                    {
                        category = "HARM_CATEGORY_SEXUALLY_EXPLICIT",
                        probability = "NEGLIGIBLE"
                    },
                    new
                    {
                        category = "HARM_CATEGORY_HATE_SPEECH",
                        probability = "NEGLIGIBLE"
                    },
                    new
                    {
                        category = "HARM_CATEGORY_HARASSMENT",
                        probability = "NEGLIGIBLE"
                    },
                    new
                    {
                        category = "HARM_CATEGORY_DANGEROUS_CONTENT",
                        probability = "NEGLIGIBLE"
                    }
                }
            };

            var jsonContent = JsonSerializer.Serialize(content);

            try
            {
                var apiUrl = $"{_httpClient.BaseAddress}";

                var response = await _httpClient.PostAsync(apiUrl, new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseContent);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Une erreur s'est produite lors de l'appel à l'API Gemini : {ex.Message}");
            }
        }

        #endregion

        #region Insurance Pack Display and Selection

        public async Task<List<InsurancePack>> ShowInsurancePacks(List<InsurancePack> packs)
        {
            _insurancePacks = packs;
            return _insurancePacks;
        }

        public async Task<InsurancePack> ChooseInsurancePack(string packName)
        {
            var chosenPack = _insurancePacks.FirstOrDefault(p => p.Name.Equals(packName, StringComparison.OrdinalIgnoreCase));

            if (chosenPack == null)
            {
                throw new ArgumentException("Pack d'assurance non trouvé.");
            }

            return chosenPack;
        }

        #endregion

        #region Payment Processing

        public async Task<string> ProcessPayment(decimal amount)
        {
            // Implémentez la logique de paiement ici, par exemple en utilisant une API de paiement tiers
            return $"Le montant à payer est de : {amount} FCFA. Procédez au paiement.";
        }

        #endregion
    }
}