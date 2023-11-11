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
                    return await _context.Users.ToListAsync();
                }
                else
                {
                    int startIndex = (pageNumber.Value - 1) * pageSize.Value;

                    int totalUsers = await _context.Users.CountAsync();
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
        public async Task<User> EditarUser(int id, User user)
        {
            try
            {
                var existingUser = await _context.Users.FindAsync(id);

                if (existingUser == null)
                {
                    return null;
                }
                _context.Entry(existingUser).CurrentValues.SetValues(user);
                _context.SaveChanges();
                return user;

            }
            catch (Exception ex)
            {
                throw ex.Failin();
            }
        }
        public async Task<User> DeletarUser(int id)
        {
            if(id == null)
            {
                return null;
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return null;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return user;
        }
        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.IdUser == id);
        }
        public bool BuscaLogin(string? email, string? senha)
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == email && u.Password == senha);
            if (user == null)
            {
                return false;
            }
            return true;
        }
        public User ValidaLogin(string email, string senha)
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == email && u.Password == senha);

            return user;
        }
    }
}
