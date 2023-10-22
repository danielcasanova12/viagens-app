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
    public class TypeRoomsController : ControllerBase
    {
        private readonly TravelContext _context;

        public TypeRoomsController(TravelContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeRoom>>> GetTypeRooms()
        {
            var typeRooms = await _context.TypeRooms.ToListAsync();
            return Ok(typeRooms);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TypeRoom>> GetTypeRoomById(int id)
    {
        var typeRoom = await _context.TypeRooms.FindAsync(id);

        if (typeRoom == null)
        {
            return NotFound();
        }

        return typeRoom;
    }

    [HttpPost]
    public async Task<ActionResult<TypeRoom>> PostTypeRoom(TypeRoom typeRoom)
    {
        _context.TypeRooms.Add(typeRoom);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetTypeRoomById", new { id = typeRoom.IdTypeRoom }, typeRoom);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutTypeRoom(int id, TypeRoom typeRoom)
    {
        if (id != typeRoom.IdTypeRoom)
        {
            return BadRequest();
        }

        _context.Entry(typeRoom).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TypeRoomExists(id))
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
    public async Task<IActionResult> DeleteTypeRoom(int id)
    {
        var typeRoom = await _context.TypeRooms.FindAsync(id);

        if (typeRoom == null)
        {
            return NotFound();
        }

        _context.TypeRooms.Remove(typeRoom);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TypeRoomExists(int id)
    {
        return _context.TypeRooms.Any(e => e.IdTypeRoom == id);
    }
}
}
