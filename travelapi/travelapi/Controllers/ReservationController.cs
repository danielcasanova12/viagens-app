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
    public class ReservationController : ControllerBase
    {
        private readonly TravelContext _context;
        private readonly IMapper _mapper;

        public ReservationController(TravelContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetReservations()
        {
            var reservations = await _context.Reservations.ToListAsync();
            var reservationDtos = _mapper.Map<List<ReservationDto>>(reservations);
            return Ok(reservationDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationDto>> GetReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);

            if (reservation == null)
            {
                return NotFound();
            }

            var reservationDto = _mapper.Map<ReservationDto>(reservation);
            return reservationDto;
        }

        [HttpPost]
        public async Task<ActionResult<ReservationDto>> PostReservation(ReservationDto reservationDto)
        {
            var reservation = _mapper.Map<Reservation>(reservationDto);
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            var createdDto = _mapper.Map<ReservationDto>(reservation);

            return CreatedAtAction("GetReservation", new { id = createdDto.IdReservation }, createdDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservation(int id, ReservationDto reservationDto)
        {
            if (id != reservationDto.IdReservation)
            {
                return BadRequest();
            }

            var reservation = _mapper.Map<Reservation>(reservationDto);
            _context.Entry(reservation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(id))
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
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);

            if (reservation == null)
            {
                return NotFound();
            }

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.IdReservation == id);
        }
    }
}
