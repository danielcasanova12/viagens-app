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
    public class RestaurantsController : ControllerBase
    {
        private readonly TravelContext _context;

        public RestaurantsController(TravelContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurants()
        {
            var restaurants = await _context.Restaurants.ToListAsync();
            return Ok(restaurants);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Restaurant>> GetRestaurantById(int id)
    {
        var restaurant = await _context.Restaurants.FindAsync(id);

        if (restaurant == null)
        {
            return NotFound();
        }

        return restaurant;
    }

    [HttpPost]
    public async Task<ActionResult<Restaurant>> PostRestaurant(Restaurant restaurant)
    {
        _context.Restaurants.Add(restaurant);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetRestaurantById", new { id = restaurant.IdRestaurant }, restaurant);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutRestaurant(int id, Restaurant restaurant)
    {
        if (id != restaurant.IdRestaurant)
        {
            return BadRequest();
        }

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
