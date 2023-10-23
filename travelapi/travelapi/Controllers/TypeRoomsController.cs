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
    public class TypeRoomsController : ControllerBase
    {
        private readonly TravelContext _context;
        private readonly IMapper _mapper;

        public TypeRoomsController(TravelContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeRoomDto>>> GetTypeRooms()
        {
            var typeRooms = await _context.TypeRooms.ToListAsync();
            var typeRoomDtos = _mapper.Map<List<TypeRoomDto>>(typeRooms);
            return Ok(typeRoomDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TypeRoomDto>> GetTypeRoomById(int id)
        {
            var typeRoom = await _context.TypeRooms.FindAsync(id);

            if (typeRoom == null)
            {
                return NotFound();
            }

            var typeRoomDto = _mapper.Map<TypeRoomDto>(typeRoom);
            return typeRoomDto;
        }

        [HttpPost]
        public async Task<ActionResult<TypeRoomDto>> PostTypeRoom(TypeRoomDto typeRoomDto)
        {
            var typeRoom = _mapper.Map<TypeRoom>(typeRoomDto);
            _context.TypeRooms.Add(typeRoom);
            await _context.SaveChangesAsync();

            var createdDto = _mapper.Map<TypeRoomDto>(typeRoom);

            return CreatedAtAction("GetTypeRoomById", new { id = createdDto.IdTypeRoom }, createdDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypeRoom(int id, TypeRoomDto typeRoomDto)
        {
            if (id != typeRoomDto.IdTypeRoom)
            {
                return BadRequest();
            }

            var typeRoom = _mapper.Map<TypeRoom>(typeRoomDto);
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
