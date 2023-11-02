using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using travelapi.Domain.Dto;
using travelapi.Domain.Models;
using travelapi.Infrastructure;

namespace travelapi.Controllers
{
    [Route("api/hotelimages")]
    [ApiController]
    public class HotelImageController : ControllerBase
    {
        private readonly TravelContext _context; // Injete o contexto do banco de dados no controlador.

        public HotelImageController(TravelContext context)
        {
            _context = context;
        }

        // POST: api/hotelimages
        [HttpPost]
        public async Task<IActionResult> PostHotelImage([FromBody] HotelImageDto hotelImageDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Verifique se o hotel com o ID fornecido existe.
                    var hotel = _context.Hotels.FirstOrDefault(h => h.IdHotel == hotelImageDto.HotelId);

                    if (hotel == null)
                    {
                        return NotFound($"Hotel with ID {hotelImageDto.HotelId} not found");
                    }

                    // Mapeie o HotelImageDto para o modelo HotelImage.
                    var hotelImage = new HotelImage
                    {
                        HotelId = hotelImageDto.HotelId,
                        ImageUrl = hotelImageDto.ImageUrl
                    };

                    _context.HotelImages.Add(hotelImage);
                    await _context.SaveChangesAsync();

                    return Ok();

                }
                catch (Exception ex)
                {
                    // Lide com erros de validação ou exceções e retorne uma resposta apropriada.
                    return BadRequest(ex.Message);
                }
            }

            return BadRequest(ModelState);
        }



    }
}