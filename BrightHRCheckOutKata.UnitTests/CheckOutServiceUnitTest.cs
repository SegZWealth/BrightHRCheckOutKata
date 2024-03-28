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
                new UnitPriceDTO { Sku = 'C', Price = 20 },
                 new UnitPriceDTO { Sku = 'D', Price = 15 }
            };
            var specialPrices = new List<SpecialPriceDTO> { };
            var checkout = new CheckOutService(unitPrices, specialPrices);

            // Act
            checkout.Scan("A");
            checkout.Scan("B");
            checkout.Scan("C");
            checkout.Scan("D");

            // Assert
            Assert.Equal(115, checkout.GetTotalPrice()); // A=50, B=30, C=20, D=15
        }

        [Fact]
        public void Scan_MultipleItems_WithDuplicatedItems_CalculatesTotalPriceCorrectly() 
        {
            // Arrange
            var unitPrices = new List<UnitPriceDTO>
            {
                new UnitPriceDTO { Sku = 'A', Price = 50 },
                new UnitPriceDTO { Sku = 'B', Price = 30 },
                new UnitPriceDTO { Sku = 'C', Price = 20 },
                new UnitPriceDTO { Sku = 'D', Price = 15 }
            };
            var specialPrices = new List<SpecialPriceDTO>
            {
                new SpecialPriceDTO { Sku = 'A', Quantity = 3, Price = 130 },
                new SpecialPriceDTO { Sku = 'B', Quantity = 2, Price = 45 }
            };
            var checkout = new CheckOutService(unitPrices, specialPrices);

            // Act
            checkout.Scan("A");
            checkout.Scan("B");
            checkout.Scan("A");
            checkout.Scan("C");
            checkout.Scan("D");
            checkout.Scan("B");
            checkout.Scan("A");


            // Assert
            Assert.Equal(210, checkout.GetTotalPrice()); // A=130, B=45, C=20, D=15
        }

        [Fact]
        public void Scan_MultipleItems_WithQuantityOfDuplicatedItems_ExceedingSpecialPriceQuantity_CalculatesTotalPriceCorrectly() 
        {
            // Arrange
            var unitPrices = new List<UnitPriceDTO>
            {
                new UnitPriceDTO { Sku = 'A', Price = 50 },
                new UnitPriceDTO { Sku = 'B', Price = 30 },
                new UnitPriceDTO { Sku = 'C', Price = 20 },
                new UnitPriceDTO { Sku = 'D', Price = 15 }
            };
            var specialPrices = new List<SpecialPriceDTO>
            {
                new SpecialPriceDTO { Sku = 'A', Quantity = 3, Price = 130 },
                new SpecialPriceDTO { Sku = 'B', Quantity = 2, Price = 45 }
            };
            var checkout = new CheckOutService(unitPrices, specialPrices);

            // Act
            checkout.Scan("A");
            checkout.Scan("B");
            checkout.Scan("A");
            checkout.Scan("C");
            checkout.Scan("D");
            checkout.Scan("B");
            checkout.Scan("A");
            checkout.Scan("B");
            checkout.Scan("A");

            // Assert
            Assert.Equal(290, checkout.GetTotalPrice()); // A=130, A=50, B=45, B=30, C=20, D=15
        }


    }
}