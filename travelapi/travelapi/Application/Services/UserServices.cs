using Microsoft.EntityFrameworkCore;
using travelapi.Application.Interfaces;
using travelapi.Domain.Dto;
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
        public async Task<List<User>> BuscarUserPagiandos(int? pageNumber, int? pageSize)
        {
            try
            {
                if (pageNumber == null || pageSize == null)
                {
                    // Se pageNumber ou pageSize forem nulos, traga todos os dados do banco.
                    return await _context.Users.ToListAsync();
                }
                else
                {
                    int startIndex = (pageNumber.Value - 1) * pageSize.Value;

                    // Consulta o número total de usuários no banco de dados.
                    int totalUsers = await _context.Users.CountAsync();

                    // Consulta os usuários para a página atual.
                    var users = await _context.Users
                        .Skip(startIndex)
                        .Take(pageSize.Value)
                        .ToListAsync();

                    return users;
                }
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
        public async Task<User> CriarUser(User user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                throw ex.Failin();
            }
        }
        public async Task<User> EditarUser()
        {
            try
            {

            }catch (Exception ex) {
                throw ex.Failin();
            }
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.IdUser == id);
        }
    }
}
