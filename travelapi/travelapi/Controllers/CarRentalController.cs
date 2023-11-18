using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using travelapi.Domain.Models;
using travelapi.Infrastructure;

namespace travelapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarRentalController : Controller
    {
        private readonly TravelContext _context; // Substitua YourDbContext pelo nome do seu DbContext

        public CarRentalController(TravelContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarRental>>> GetCarRentals()
        {
            return await _context.CarRentals.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<CarRental>> PostCarRental(CarRental carRental)
        {
            _context.CarRentals.Add(carRental);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // ...
    }
}