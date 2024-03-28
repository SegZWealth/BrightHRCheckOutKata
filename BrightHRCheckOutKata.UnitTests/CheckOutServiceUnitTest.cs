using BrightHRCheckOutKata.Core.Domain;
using BrightHRCheckOutKata.Core.DTO;
using BrightHRCheckOutKata.Infrastructure.Services;

namespace BrightHRCheckOutKata.UnitTests
{
    public class CheckOutServiceUnitTest 
    {
        [Fact]
        public void Scan_AddsItemToCheckout()
        {
            // Arrange
            var unitPrices = new List<UnitPriceDTO> { new UnitPriceDTO { Sku = 'A', Price = 50 } };
            var specialPrices = new List<SpecialPriceDTO> { };
            var checkout = new CheckOutService(unitPrices, specialPrices);

            // Act
            checkout.Scan("A");

            // Assert
            Assert.Equal(50, checkout.GetTotalPrice());
        }

        [Fact]
        public void Scan_MultipleItems_CalculatesTotalPriceCorrectly()
        {
            // Arrange
            var unitPrices = new List<UnitPriceDTO>
            {
                new UnitPriceDTO { Sku = 'A', Price = 50 },
                new UnitPriceDTO { Sku = 'B', Price = 30 },
                new UnitPriceDTO { Sku = 'C', Price = 20 }
            };
            var specialPrices = new List<SpecialPriceDTO> { };
            var checkout = new CheckOutService(unitPrices, specialPrices);

            // Act
            checkout.Scan("A");
            checkout.Scan("B");
            checkout.Scan("C");

            // Assert
            Assert.Equal(100, checkout.GetTotalPrice()); // A=50, B=30, C=20
        }

    }
}