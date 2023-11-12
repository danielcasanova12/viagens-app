using travelapi.Domain.Models;

namespace travelapi.Application.Interfaces
{
    public interface IUserServices
    {
        Task<List<User>> BuscarUserPagiandos(int? pageNumber, int? pageSize);
        Task<User> BuscarUserPorId(int id);
        Task<User> CriarUser(User user);
        Task<User> EditarUser(int id, User user);
        Task<User> DeletarUser(int id);
        User ValidaLogin(string email, string senha);
        bool BuscaLogin(string email, string senha);
        string GenerateJwtToken(string email);
    }
}
