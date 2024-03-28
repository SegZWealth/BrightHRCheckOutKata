namespace BrightHRCheckOutKata.Core.Services
{
    public interface ICheckoutService
    {
        void Scan(string item);
        int GetTotalPrice();
    }
}
