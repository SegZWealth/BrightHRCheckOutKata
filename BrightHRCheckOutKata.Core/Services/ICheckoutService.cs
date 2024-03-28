using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightHRCheckOutKata.Core.Services
{
    public interface ICheckoutService
    {
        void Scan(string item);
        int GetTotalPrice();
    }
}
