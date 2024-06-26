using LeilAFinafrica.Domain.DTOs;
using LeilAFinafrica.Domain.Models;
using LeilAFinafrica.Services.VehicleQuoteServices;
using Moq;

namespace LeilAFinafrica.ServicesTest
{
    [TestClass]
    public class VehicleQuoteServiceTests
    {
        private Mock<HttpClient> _httpClientMock;
        private VehicleQuoteService _vehicleQuoteService;

        [TestInitialize]
        public void Setup()
        {
            _httpClientMock = new Mock<HttpClient>();
            /*_vehicleQuoteService = new VehicleQuoteService(_httpClientMock.Object);*/
        }

        [TestMethod]
        public async Task StartInsuranceSimulation_ShouldReturnWelcomeMessage()
        {
            // Act
            var result = await _vehicleQuoteService.StartInsuranceSimulation();

            // Assert
            Assert.AreEqual("Bonjour ! Bienvenue sur Finafrica. Je suis Leila de Finafrica, votre assistant virtuel pour toutes vos questions d'assurance. Comment puis-je vous aider aujourd'hui ?", result);
        }

        [TestMethod]
        public async Task CollectVehicleInformation_ShouldReturnAcknowledgementMessage()
        {
            // Arrange
            var vehicleInput = new VehicleDTOs
            {
                Category = "Privée",
                Genre = "Corolla",
                EnergyType = "Diesel"
            };

            // Act
            var result = await _vehicleQuoteService.CollectVehicleInformation(vehicleInput);

            // Assert
            Assert.AreEqual("Merci pour ces informations.", result);
            Assert.AreEqual(vehicleInput, _vehicleQuoteService.GetType().GetField("_vehicle", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(_vehicleQuoteService));
        }

        [TestMethod]
        public async Task AdjustTariffForMarketValue_WithValidParameters_ReturnsAdjustedTariff()
        {
            // Arrange
            var baseTariff = 50000m;
            var marketValue = 6000000m;
            var newValue = 20000000m;

            // Act
            var adjustedTariff = await _vehicleQuoteService.AdjustTariffForMarketValue(baseTariff, marketValue, newValue);

            // Assert
            Assert.IsTrue(adjustedTariff > baseTariff);
        }


        [TestMethod]
        public void GenerateContentUsingGemini_ReturnsGeneratedContent()
        {
            // Arrange
            var expectedContent = "Contenu généré par l'API Gemini.";
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            _vehicleQuoteService.GenerateContentUsingGeminiAsync();
            var generatedContent = consoleOutput.ToString().Trim();

            // Assert
            Assert.AreEqual(expectedContent, generatedContent);
        }


        [TestMethod]
        public async Task ShowInsurancePacks_ReturnsCorrectPacks()
        {
            // Arrange
            var expectedPacks = new List<InsurancePack>
            {
                new InsurancePack { Name = "Pack 1" },
                new InsurancePack { Name = "Pack 2" },
                new InsurancePack { Name = "Pack 3" }
            };

            // Act
            var result = await _vehicleQuoteService.ShowInsurancePacks(expectedPacks);

            // Assert
            CollectionAssert.AreEqual(expectedPacks, result);
        }

        [TestMethod]
        public async Task ChooseInsurancePack_ReturnsCorrectPack()
        {
            // Arrange
            var packs = new List<InsurancePack>
            {
                new InsurancePack { Name = "Pack 1" },
                new InsurancePack { Name = "Pack 2" },
                new InsurancePack { Name = "Pack 3" }
            };

            var packName = "Pack 2";
            var expectedPack = packs.FirstOrDefault(p => p.Name.Equals(packName, StringComparison.OrdinalIgnoreCase));

            // Act
            var result = await _vehicleQuoteService.ChooseInsurancePack(packName);

            // Assert
            Assert.AreEqual(expectedPack, result);
        }

        [TestMethod]
        public async Task ProcessPayment_ReturnsPaymentMessage()
        {
            // Arrange
            decimal amount = 1000m;
            string expectedMessage = $"Le montant à payer est de : {amount} FCFA. Procédez au paiement.";

            // Act
            var result = await _vehicleQuoteService.ProcessPayment(amount);

            // Assert
            Assert.AreEqual(expectedMessage, result);
        }
    }
}
