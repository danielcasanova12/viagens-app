using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using travelapi.Domain.Models;
using travelapi.Infrastructure;

namespace travelapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly TravelContext _context;

        public HotelsController(TravelContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotels()
        {
            var hotels = await _context.Hotels.ToListAsync();
            return Ok(hotels);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Hotel>> GetHotelById(int id)
    {
        var hotel = await _context.Hotels.FindAsync(id);

        if (hotel == null)
        {
            return NotFound();
        }

        return hotel;
    }

    [HttpPost]
    public async Task<ActionResult<Hotel>> PostHotel(Hotel hotel)
    {
        _context.Hotels.Add(hotel);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetHotelById", new { id = hotel.IdHotel }, hotel);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutHotel(int id, Hotel hotel)
    {
        if (id != hotel.IdHotel)
        {
            return BadRequest();
        }

        _context.Entry(hotel).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!HotelExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHotel(int id)
    {
        var hotel = await _context.Hotels.FindAsync(id);

        if (hotel == null)
        {
            return NotFound();
        }

        _context.Hotels.Remove(hotel);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool HotelExists(int id)
    {
        return _context.Hotels.Any(e => e.IdHotel == id);
    }
}
}
