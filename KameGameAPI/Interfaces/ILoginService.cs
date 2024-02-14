using KameGameAPI.DTO;

namespace KameGameAPI.Interfaces
{
    public interface ILoginService
    {
        Task<LoginResponse> LoginActionService(string username, string password);
    }
}
