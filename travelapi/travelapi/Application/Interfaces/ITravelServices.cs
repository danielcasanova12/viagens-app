using travelapi.Domain.Models;
using travelapi.Infrastructure;
using System.Threading.Tasks;

namespace travelapi.Application.Interfaces
{
    public interface ITravelServices
    {
        Task<Activity> BuscarActyvityById(int id);
    }
}