using travelapi.Domain.Models;

namespace travelapi.Application.Interfaces
{
    public interface IUserServices
    {
        Task<List<User>> BuscarUserPagiandos(int? pageNumber, int? pageSize);
        Task<User> BuscarUserPorId(int id);
        Task<User> CriarUser(User user);
    }
}
