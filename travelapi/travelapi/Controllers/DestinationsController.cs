using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using travelapi.Domain.Models;
using travelapi.Domain.Dto;
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
        public async Task<ActionResult<IEnumerable<DestinationDto>>> GetDestinations()
        {
            var destinations = await _context.Destinations.ToListAsync();
            var destinationDtos = new List<DestinationDto>();

            foreach (var destination in destinations)
            {
                var destinationDto = new DestinationDto
                {
                    IdDestination = destination.IdDestination,
                    Name = destination.Name,
                    Description = destination.Description,
                };

                destinationDtos.Add(destinationDto);
            }

            return Ok(destinationDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DestinationDto>> GetDestinationById(int id)
        {
            var destination = await _context.Destinations.FindAsync(id);

            if (destination == null)
            {
                return NotFound();
            }

            var destinationDto = new DestinationDto
            {
                IdDestination = destination.IdDestination,
                Name = destination.Name,
                Description = destination.Description,
            };

            return destinationDto;
        }

        [HttpPost]
        public async Task<ActionResult<DestinationDto>> PostDestination(DestinationDto destinationDto)
        {
            var destination = new Destination
            {
                Name = destinationDto.Name,
                Description = destinationDto.Description,
            };

            _context.Destinations.Add(destination);
            await _context.SaveChangesAsync();

            destinationDto.IdDestination = destination.IdDestination;

            return CreatedAtAction("GetDestinationById", new { id = destinationDto.IdDestination }, destinationDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDestination(int id, DestinationDto destinationDto)
        {
            if (id != destinationDto.IdDestination)
            {
                return BadRequest();
            }

            var destination = await _context.Destinations.FindAsync(id);

            if (destination == null)
            {
                return NotFound();
            }

            destination.Name = destinationDto.Name;
            destination.Description = destinationDto.Description;

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
