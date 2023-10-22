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
    public class RestaurantsController : ControllerBase
    {
        private readonly TravelContext _context;
        private readonly IMapper _mapper;

        public RestaurantsController(TravelContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetRestaurants()
        {
            var restaurants = await _context.Restaurants.ToListAsync();
            var restaurantDtos = _mapper.Map<List<RestaurantDto>>(restaurants);
            return Ok(restaurantDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantDto>> GetRestaurantById(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);

            if (restaurant == null)
            {
                return NotFound();
            }

            var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);
            return restaurantDto;
        }

        [HttpPost]
        public async Task<ActionResult<RestaurantDto>> PostRestaurant(RestaurantDto restaurantDto)
        {
            var restaurant = _mapper.Map<Restaurant>(restaurantDto);
            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();

            var createdDto = _mapper.Map<RestaurantDto>(restaurant);

            return CreatedAtAction("GetRestaurantById", new { id = createdDto.IdRestaurant }, createdDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestaurant(int id, RestaurantDto restaurantDto)
        {
            if (id != restaurantDto.IdRestaurant)
            {
                return BadRequest();
            }

            var restaurant = _mapper.Map<Restaurant>(restaurantDto);
            _context.Entry(restaurant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestaurantExists(id))
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
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);

            if (restaurant == null)
            {
                return NotFound();
            }

            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RestaurantExists(int id)
        {
            return _context.Restaurants.Any(e => e.IdRestaurant == id);
        }
    }
}
