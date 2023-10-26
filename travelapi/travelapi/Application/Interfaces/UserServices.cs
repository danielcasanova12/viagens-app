using Microsoft.EntityFrameworkCore;
using travelapi.Application.Interfaces;
using travelapi.Domain.Models;
using travelapi.Infrastructure;
using travelapi.Utils;

namespace travelapi.Application.Services
{
    public class UserServices : IUserServices
    {
        private readonly TravelContext _context;

        public UserServices(TravelContext context)
        {
            _context = context;
        }
        public async Task<List<User>> BuscarUserPagiandos(int pageNumber, int pageSize)
        {
            try
            {
                 
                int startIndex = (pageNumber - 1) * pageSize;        // Consulta o número total de usuários no banco de dados.
                int totalUsers = await _context.Users.CountAsync();  // Consulta os usuários para a página atual.
                var users = await _context.Users
                    .Skip(startIndex)
                    .Take(pageSize)
                    .ToListAsync();
                return await Task.FromResult(users);

            }
            catch (Exception ex)
            {
                throw ex.Failin();
            }
        }
        public async Task<User> BuscarUserPorId(int id)
        {
            try
            {
                var data = _context.Users.Where((a) => a.IdUser == id).Single();
                return await Task.FromResult(data);

            }
            catch (Exception ex)
            {
                throw ex.Failin();
            }
        }
    }
}
