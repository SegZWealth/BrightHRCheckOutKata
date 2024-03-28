using BrightHRCheckOutKata.Core.Domain;
using BrightHRCheckOutKata.Core.DTO;
using BrightHRCheckOutKata.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightHRCheckOutKata.Infrastructure.Services
{
    public class CheckOutService : ICheckoutService
    {
        private List<UnitPriceDTO> _unitPrices = new List<UnitPriceDTO>(); // List to store unit prices
        private List<SpecialPriceDTO> _specialPrices = new List<SpecialPriceDTO>(); // List to store special prices

        private List<char> _scannedItems = new List<char>(); // List to store scanned items

        public CheckOutService(List<UnitPriceDTO> unitPrices, List<SpecialPriceDTO> specialPrices) 
        {
            _unitPrices = unitPrices ?? throw new ArgumentNullException(nameof(unitPrices));
            _specialPrices = specialPrices ?? throw new ArgumentNullException(nameof(specialPrices));
        }
        public int GetTotalPrice()
        {
            throw new NotImplementedException();
        }

        public void Scan(string item)
        {
            throw new NotImplementedException();
        }
    }
}
