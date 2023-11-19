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
        public async Task<ActionResult<CarCount>> GetCarRentals(int? pageNumber, int? pageSize, string? searchValue)
        {
            var query = _context.CarRentals.AsQueryable();

            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(c => c.Company.Contains(searchValue) || c.Model.Contains(searchValue));
            }

            var totalCount = await query.CountAsync();

            if (pageNumber.HasValue && pageSize.HasValue)
            {
                query = query.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

            var cars = await query.ToListAsync();

            var result = new CarCount
            {
                Carro = cars,
                ContAllCars = totalCount
            };

            return result;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<CarRental>> GetCarRental(int id)
        {
            var carRental = await _context.CarRentals.FindAsync(id);

            if (carRental == null)
            {
                return NotFound();
            }

            return carRental;
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