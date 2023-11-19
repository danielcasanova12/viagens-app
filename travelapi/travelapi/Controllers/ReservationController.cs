using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using travelapi.Domain.Dto;
using travelapi.Domain.Models;
using travelapi.Infrastructure;
using AutoMapper;
using System.Runtime.ConstrainedExecution;

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
            var reservations = await _context.Reservations
                .Include(r => r.ReservedHotel) // Inclua os detalhes do hotel
                    .ThenInclude(h => h.Location) // Inclua os detalhes da localização do hotel
                .Include(r => r.ReservedHotel.Images) // Inclua as imagens do hotel
                .Include(r => r.CarRentals) // Inclua os detalhes do carro alugado
                .Include(r => r.Flights) // Inclua os detalhes do voo
                .Where(r => r.UserId == userId)
                .ToListAsync();

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
            if(reservationDto.CarRentals != null)
            {
                await Console.Out.WriteLineAsync("12");
            }
            var reservation = _mapper.Map<Reservation>(reservationDto);
            if(reservation.CarRentals != null)
            {
                await Console.Out.WriteLineAsync("2");
            }
            // Lida com o hotel
            if (reservationDto.ReservedHotel != null)
            {
                if (reservationDto.ReservedHotel.IdHotel != null)
                {
                    var hotel = _context.Hotels.Find(reservationDto.ReservedHotel.IdHotel);
                    if (hotel != null)
                    {
                        reservation.ReservedHotel = hotel;
                    }
                    else
                    {
                        return NotFound("Hotel não encontrado");
                    }
                }
                else
                {
                    var hotel = _mapper.Map<Hotel>(reservationDto.ReservedHotel);
                    _context.Hotels.Add(hotel);
                    await _context.SaveChangesAsync();
                    reservation.ReservedHotel = hotel;
                }
            }

            // Lida com o carro
            if (reservationDto.CarRentals != null)
            {
                if (reservationDto.CarRentals.IdCarRental != null)
                {
                    var car = _context.CarRentals.Find(reservationDto.CarRentals.IdCarRental);
                    if (car != null)
                    {
                        reservation.CarRentals = car;
                    }
                    else
                    {
                        return NotFound("Carro não encontrado");
                    }
                }
                else
                {
                    var car = _mapper.Map<CarRental>(reservationDto.CarRentals);
                    _context.CarRentals.Add(car);
                    await _context.SaveChangesAsync();
                    reservation.CarRentals = car;
                }
            }

            // Lida com o voo
            if (reservationDto.Flights != null)
            {
                if (reservationDto.Flights.IdFlight != null)
                {
                    var flight = _context.Flights.Find(reservationDto.Flights.IdFlight);
                    if (flight != null)
                    {
                        reservation.Flights = flight;
                    }
                    else
                    {
                        return NotFound("Voo não encontrado");
                    }
                }
                else
                {
                    var flight = _mapper.Map<Flight>(reservationDto.Flights);
                    _context.Flights.Add(flight);
                    await _context.SaveChangesAsync();
                    reservation.Flights = flight;
                }
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
