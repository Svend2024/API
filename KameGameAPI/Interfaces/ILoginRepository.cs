using KameGameAPI.Models;

namespace KameGameAPI.Interfaces
{
    public interface ILoginRepository 
    {
        Task<object> LoginActionRepository(string username, string password);
    }
}
