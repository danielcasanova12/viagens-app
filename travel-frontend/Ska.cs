using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class CostsController : ControllerBase
    {
        private readonly TravelContext _context;
        private readonly IMapper _mapper;

        public CostsController(TravelContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CostDto>>> GetCosts()
        {
            var costs = await _context.Costs.ToListAsync();
        var costDtos = _mapper.Map<List<CostDto>>(costs);
            return Ok(costDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CostDto>> GetCostById(int id)
    {
        var cost = await _context.Costs.FindAsync(id);

        if (cost == null)
        {
            return NotFound();
        }

        var costDto = _mapper.Map<CostDto>(cost);
        return Ok(costDto);
    }

    [HttpPost]
    public async Task<ActionResult<CostDto>> PostCost(CostDto costDto)
    {
        var cost = _mapper.Map<Cost>(costDto);
        _context.Costs.Add(cost);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetCostById", new { id = cost.IdCosts }, _mapper.Map<CostDto>(cost));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCost(int id, CostDto costDto)
    {
        if (id != costDto.IdCosts)
        {
            return BadRequest();
        }

        var cost = _mapper.Map<Cost>(costDto);
        _context.Entry(cost).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CostExists(id))
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
    public async Task<IActionResult> DeleteCost(int id)
    {
        var cost = await _context.Costs.FindAsync(id);

        if (cost == null)
        {
            return NotFound();
        }

        _context.Costs.Remove(cost);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CostExists(int id)
    {
        return _context.Costs.Any(e => e.IdCosts == id);
    }
}
}
