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
    public class TouristAttractionsController : ControllerBase
    {
        private readonly TravelContext _context;

        public TouristAttractionsController(TravelContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TouristAttraction>>> GetTouristAttractions()
        {
            var touristAttractions = await _context.TouristAttractions.ToListAsync();
            return Ok(touristAttractions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TouristAttraction>> GetTouristAttractionById(int id)
    {
        var touristAttraction = await _context.TouristAttractions.FindAsync(id);

        if (touristAttraction == null)
        {
            return NotFound();
        }

        return touristAttraction;
    }

    [HttpPost]
    public async Task<ActionResult<TouristAttraction>> PostTouristAttraction(TouristAttraction touristAttraction)
    {
        _context.TouristAttractions.Add(touristAttraction);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetTouristAttractionById", new { id = touristAttraction.IdAttraction }, touristAttraction);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutTouristAttraction(int id, TouristAttraction touristAttraction)
    {
        if (id != touristAttraction.IdAttraction)
        {
            return BadRequest();
        }

        _context.Entry(touristAttraction).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TouristAttractionExists(id))
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
    public async Task<IActionResult> DeleteTouristAttraction(int id)
    {
        var touristAttraction = await _context.TouristAttractions.FindAsync(id);

        if (touristAttraction == null)
        {
            return NotFound();
        }

        _context.TouristAttractions.Remove(touristAttraction);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TouristAttractionExists(int id)
    {
        return _context.TouristAttractions.Any(e => e.IdAttraction == id);
    }
}
}
