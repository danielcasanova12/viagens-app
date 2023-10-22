using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using travelapi.Domain.Models;
using travelapi.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace travelapi.Controllers
{
    [Route("api/local")] // Define uma rota base para o controller
    [ApiController]
    public class LocalController : ControllerBase
    {
        private readonly TravelContext _context;

        public LocalController(TravelContext context)
        {
            _context = context;
        }

        // Rota personalizada para listar todos os locais
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Local>>> GetAllLocals()
        {
            return await _context.Locations.ToListAsync();
    }

    // Rota personalizada para obter um local por ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Local>> GetLocal(int id)
    {
        var local = await _context.Locations.FindAsync(id);

        if (local == null)
        {
            return NotFound();
        }

        return local;
    }

    // Rota personalizada para criar um novo local
    [HttpPost]
    public async Task<ActionResult<Local>> CreateLocal(Local local)
    {
        _context.Locations.Add(local);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetLocal", new { id = local.IdLocal }, local);
    }

    // Rota personalizada para atualizar um local por ID
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLocal(int id, Local local)
    {
        if (id != local.IdLocal)
        {
            return BadRequest();
        }

        _context.Entry(local).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!LocalExists(id))
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

    // Rota personalizada para excluir um local por ID
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLocal(int id)
    {
        var local = await _context.Locations.FindAsync(id);
        if (local == null)
        {
            return NotFound();
        }

        _context.Locations.Remove(local);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool LocalExists(int id)
    {
        return _context.Locations.Any(e => e.IdLocal == id);
    }
}
}
