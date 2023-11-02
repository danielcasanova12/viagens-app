using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using travelapi.Domain.Dto;
using travelapi.Domain.Models;
using travelapi.Infrastructure;
using AutoMapper;

[Route("api/[controller]")]
[ApiController]
public class HotelsController : ControllerBase
{
    private readonly TravelContext _context;
    private readonly IMapper _mapper;

    public HotelsController(TravelContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<HotelDto>>> GetHotels()
    {
        var hotels = await _context.Hotels
            .Include(h => h.Location)
            .Include(h => h.Images)
            .ToListAsync();

        var hotelDtos = _mapper.Map<List<HotelDto>>(hotels);
        return Ok(hotelDtos);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<HotelDto>> GetHotelById(int id)
    {
        var hotel = await _context.Hotels.FindAsync(id);

        if (hotel == null)
        {
            return NotFound();
        }

        var hotelDto = _mapper.Map<HotelDto>(hotel);
        return hotelDto;
    }

    [HttpPost]
    public async Task<ActionResult<HotelDto>> PostHotel(HotelDto hotelDto)
    {
        if (hotelDto.Images != null && hotelDto.Images.Any())
        {
            // Verifique se as imagens existem no banco de dados.
            foreach (var imageDto in hotelDto.Images)
            {
                if (imageDto.Id == null)
                {
                    // A imagem não tem um ID, portanto, crie-a.
                    var image = _mapper.Map<HotelImage>(imageDto);
                    _context.HotelImages.Add(image);
                }
                else
                {
                    // Verifique se a imagem com o ID existe no banco de dados.
                    var existingImage = await _context.HotelImages.FindAsync(imageDto.Id);
                    if (existingImage == null)
                    {
                        // A imagem com o ID fornecido não existe, então crie-a.
                        var image = _mapper.Map<HotelImage>(imageDto);
                        _context.HotelImages.Add(image);
                    }
                }
            }
        }

        var hotel = _mapper.Map<Hotel>(hotelDto);
        _context.Hotels.Add(hotel);
        await _context.SaveChangesAsync();

        var createdDto = _mapper.Map<HotelDto>(hotel);
        return CreatedAtAction("GetHotelById", new { id = createdDto.IdHotel }, createdDto);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> PutHotel(int id, HotelDto hotelDto)
    {
        if (id != hotelDto.IdHotel)
        {
            return BadRequest();
        }

        var hotel = _mapper.Map<Hotel>(hotelDto);
        _context.Entry(hotel).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!HotelExists(id))
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
    public async Task<IActionResult> DeleteHotel(int id)
    {
        var hotel = await _context.Hotels.FindAsync(id);
        if (hotel == null)
        {
            return NotFound();
        }

        _context.Hotels.Remove(hotel);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool HotelExists(int id)
    {
        return _context.Hotels.Any(e => e.IdHotel == id);
    }
}
