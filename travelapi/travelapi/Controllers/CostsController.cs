using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using travelapi.Domain.Models;
using travelapi.Infrastructure;

namespace travelapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CostsController : ControllerBase
    {
        private readonly TravelContext _context;

        public CostsController(TravelContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cost>>> GetCosts()
        {
            var costs = await _context.Costs.ToListAsync();
            return Ok(costs);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Cost>> GetCostById(int id)
    {
        var cost = await _context.Costs.FindAsync(id);

        if (cost == null)
        {
            return NotFound();
        }

        return cost;
    }

    [HttpPost]
    public async Task<ActionResult<Cost>> PostCost(Cost cost)
    {
        _context.Costs.Add(cost);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetCostById", new { id = cost.IdCosts }, cost);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCost(int id, Cost cost)
    {
        if (id != cost.IdCosts)
        {
            return BadRequest();
        }

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
