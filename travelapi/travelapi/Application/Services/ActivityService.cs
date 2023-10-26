using travelapi.Application.Interfaces;
using travelapi.Domain.Models;
using travelapi.Infrastructure;
using travelapi.Utils;

namespace travelapi.Application.Services
{
    public class ActivityServices : IActivityServices
    {
        private readonly TravelContext _context;

        public ActivityServices(TravelContext context)
        {
            _context = context;
        }



        public async Task<Activity> BuscarActyvityPorId(int id)
        {
            try
            {
                var data = _context.Activities.Where((a) => a.IdActivity == id).Single();
                return await Task.FromResult(data);

            }
            catch (Exception ex)
            {
                throw ex.Failin();
            }
        }
    }
}
