﻿using Microsoft.AspNetCore.Mvc;
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
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetReservationsByUserId(int userId)
        {
            var reservations = await _context.Reservations.Where(r => r.UserId == userId).ToListAsync();

            if (reservations == null || !reservations.Any())
            {
                return NotFound();
            }

            var reservationDtos = _mapper.Map<IEnumerable<ReservationDto>>(reservations);
            var reservationCount = await _context.Reservations.CountAsync(r => r.UserId == userId);

            var reservationAll = new ReservationsAll
            {
                Reservations = reservations,
                TotalReservations = reservationCount
            };
            return Ok(reservationAll);
        }
        [HttpPost]
        public async Task<ActionResult<ReservationDto>> PostReservation(ReservationDto reservationDto)
        {
            var reservation = _mapper.Map<Reservation>(reservationDto);

            if (reservationDto.ReservedHotel.IdHotel != null)
            {
                // Se o idHotel estiver presente, busque o hotel correspondente no banco de dados
                var hotel = _context.Hotels.Find(reservationDto.ReservedHotel.IdHotel);
                if (hotel != null)
                {
                    // Se o hotel for encontrado, use-o para a reserva
                    reservation.ReservedHotel = hotel;
                }
                else
                {
                    // Se o hotel não for encontrado, retorne um erro
                    return NotFound("Hotel não encontrado");
                }
            }
            else
            {
                // Se o idHotel não estiver presente, use os dados fornecidos para criar um novo hotel
                var hotel = _mapper.Map<Hotel>(reservationDto.ReservedHotel);
                _context.Hotels.Add(hotel);
                await _context.SaveChangesAsync();
                reservation.ReservedHotel = hotel;
            }

            Console.WriteLine(reservation.ToString());
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
