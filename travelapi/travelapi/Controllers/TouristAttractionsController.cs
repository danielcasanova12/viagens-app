using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using travelapi.Domain.Dto;
using travelapi.Domain.Models;
using travelapi.Infrastructure;
using AutoMapper;

namespace travelapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TouristAttractionsController : ControllerBase
    {
        private readonly TravelContext _context;
        private readonly IMapper _mapper;

        public TouristAttractionsController(TravelContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TouristAttractionDto>>> GetTouristAttractions()
        {
            var touristAttractions = await _context.TouristAttractions.ToListAsync();
            var touristAttractionDtos = _mapper.Map<List<TouristAttractionDto>>(touristAttractions);
            return Ok(touristAttractionDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TouristAttractionDto>> GetTouristAttractionById(int id)
        {
            var touristAttraction = await _context.TouristAttractions.FindAsync(id);

            if (touristAttraction == null)
            {
                return NotFound();
            }

            var touristAttractionDto = _mapper.Map<TouristAttractionDto>(touristAttraction);
            return touristAttractionDto;
        }

        [HttpPost]
        public async Task<ActionResult<TouristAttractionDto>> PostTouristAttraction(TouristAttractionDto touristAttractionDto)
        {
            var touristAttraction = _mapper.Map<TouristAttraction>(touristAttractionDto);
            _context.TouristAttractions.Add(touristAttraction);
            await _context.SaveChangesAsync();

            var createdDto = _mapper.Map<TouristAttractionDto>(touristAttraction);

            return CreatedAtAction("GetTouristAttractionById", new { id = createdDto.IdAttraction }, createdDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTouristAttraction(int id, TouristAttractionDto touristAttractionDto)
        {
            if (id != touristAttractionDto.IdAttraction)
            {
                return BadRequest();
            }

            var touristAttraction = _mapper.Map<TouristAttraction>(touristAttractionDto);
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
