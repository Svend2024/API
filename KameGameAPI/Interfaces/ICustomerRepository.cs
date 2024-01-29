using KameGameAPI.Models;

namespace KameGameAPI.Interfaces
{
    public interface ICustomerRepository 
    {
        Task<Customer> LoginCustomerRepository(string username, string password);
    }
}
