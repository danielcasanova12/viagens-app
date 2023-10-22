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
    public class DestinationsController : ControllerBase
    {
        private readonly TravelContext _context;

        public DestinationsController(TravelContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Destination>>> GetDestinations()
        {
            var destinations = await _context.Destinations.ToListAsync();
            return Ok(destinations);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Destination>> GetDestinationById(int id)
    {
        var destination = await _context.Destinations.FindAsync(id);

        if (destination == null)
        {
            return NotFound();
        }

        return destination;
    }

    [HttpPost]
    public async Task<ActionResult<Destination>> PostDestination(Destination destination)
    {
        _context.Destinations.Add(destination);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetDestinationById", new { id = destination.IdDestination }, destination);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutDestination(int id, Destination destination)
    {
        if (id != destination.IdDestination)
        {
            return BadRequest();
        }

        _context.Entry(destination).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!DestinationExists(id))
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
    public async Task<IActionResult> DeleteDestination(int id)
    {
        var destination = await _context.Destinations.FindAsync(id);

        if (destination == null)
        {
            return NotFound();
        }

        _context.Destinations.Remove(destination);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool DestinationExists(int id)
    {
        return _context.Destinations.Any(e => e.IdDestination == id);
    }
}
}
