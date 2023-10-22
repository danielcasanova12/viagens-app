using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using travelapi.Domain.Models;
using travelapi.Domain.Dto;
using travelapi.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;

[Route("api/local")] // Define uma rota base para o controller
[ApiController]
public class LocalController : ControllerBase
{
    private readonly TravelContext _context;
    private readonly IMapper _mapper;

    public LocalController(TravelContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // Rota personalizada para listar todos os locais
    [HttpGet]
    public async Task<ActionResult<IEnumerable<LocalDto>>> GetAllLocals()
    {
        var locals = await _context.Locations.ToListAsync();
        var localDtos = _mapper.Map<List<LocalDto>>(locals);
        return Ok(localDtos);
    }

    // Rota personalizada para obter um local por ID
    [HttpGet("{id}")]
    public async Task<ActionResult<LocalDto>> GetLocal(int id)
    {
        var local = await _context.Locations.FindAsync(id);

        if (local == null)
        {
            return NotFound();
        }

        var localDto = _mapper.Map<LocalDto>(local);
        return localDto;
    }

    // Rota personalizada para criar um novo local
    [HttpPost]
    public async Task<ActionResult<LocalDto>> CreateLocal(LocalDto localDto)
    {
        var local = _mapper.Map<Local>(localDto);
        _context.Locations.Add(local);
        await _context.SaveChangesAsync();

        var createdDto = _mapper.Map<LocalDto>(local);

        return CreatedAtAction("GetLocal", new { id = createdDto.IdLocal }, createdDto);
    }

    // Rota personalizada para atualizar um local por ID
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLocal(int id, LocalDto localDto)
    {
        if (id != localDto.IdLocal)
        {
            return BadRequest();
        }

        var local = _mapper.Map<Local>(localDto);
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
