using KameGameAPI.DTO;

namespace KameGameAPI.Interfaces
{
    public interface ICustomerService
    {
        Task<LoginResponse> LoginCustomerService(string username, string password);
    }
}
