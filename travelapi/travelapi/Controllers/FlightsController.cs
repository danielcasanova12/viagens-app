using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using travelapi.Domain.DTOs;
using travelapi.Domain.Models;
using travelapi.Infrastructure;

namespace travelapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlightsController : Controller
    {
        
            private readonly TravelContext _context; 

            public FlightsController(TravelContext context)
            {
                _context = context;
            }

        [HttpGet]
        public async Task<ActionResult<FlightCount>> GetFlights(int? pageNumber, int? pageSize, string? searchValue)
        {
            var query = _context.Flights
                .Include(f => f.DepartureLocation)
                .Include(f => f.ArrivalLocation)
                .AsQueryable();
            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(f => f.Airline.Contains(searchValue));
            }

            var totalCount = await query.CountAsync();

            if (pageNumber.HasValue && pageSize.HasValue)
            {
                query = query.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

            var flights = await query.ToListAsync();

            var result = new FlightCount
            {
                Flighti = flights,
                ContAllFlight = totalCount
            };

            return result;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Flight>> GetFlight(int id)
        {
            var flight = await _context.Flights
                .Include(f => f.DepartureLocation)
                .Include(f => f.ArrivalLocation)
                .FirstOrDefaultAsync(f => f.IdFlight == id);

            if (flight == null)
            {
                return NotFound();
            }

            return flight;
        }

        [HttpPost]
            public async Task<ActionResult<Flight>> PostFlight(Flight flight)
            {
                _context.Flights.Add(flight);
                await _context.SaveChangesAsync();

                return Ok();
            
            }
    }
}
