using BrightHRCheckOutKata.Core.DTO;
using BrightHRCheckOutKata.Core.Services;

namespace BrightHRCheckOutKata.Infrastructure.Services
{
    public class CheckOutService : ICheckoutService
    {
        private readonly List<UnitPriceDTO> _unitPrices = new(); // List to store unit prices
        private readonly List<SpecialPriceDTO> _specialPrices = new(); // List to store special prices

        private List<char> _scannedItems = new(); // List to store scanned items

        public CheckOutService(List<UnitPriceDTO> unitPrices, List<SpecialPriceDTO> specialPrices) 
        {
            _unitPrices = unitPrices ?? throw new ArgumentNullException(nameof(unitPrices));
            _specialPrices = specialPrices ?? throw new ArgumentNullException(nameof(specialPrices));
        }
        public int GetTotalPrice()
        {
            int totalPrice = 0;

            foreach (var sku in _scannedItems.Distinct())
            {
                int itemCount = _scannedItems.Count(item => item == sku);
                var unitPrice = _unitPrices.Find(up => up.Sku == sku).Price;
                var specialPrice = _specialPrices.Find(sp => sp.Sku == sku);

                if (specialPrice != null && itemCount >= specialPrice.Quantity)
                {
                    int specialPriceQuantity = specialPrice.Quantity;
                    int specialPriceValue = specialPrice.Price;
                    int regularPrice = unitPrice * (itemCount % specialPriceQuantity);
                    int specialPriceCount = itemCount / specialPriceQuantity;
                    totalPrice += specialPriceCount * specialPriceValue + regularPrice;
                }
                else
                {
                    totalPrice += unitPrice * itemCount;
                }
            }

            return totalPrice;
        }

        public void Scan(string item)
        {
            if (string.IsNullOrEmpty(item) || item.Length != 1 || !_unitPrices.Any(up => up.Sku == item[0]))
            {
                throw new ArgumentException("Invalid item.");
            }

            _scannedItems.Add(item[0]);
        }
    }
}
