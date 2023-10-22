using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using travelapi.Domain.Models;
using travelapi.Infrastructure;

namespace travelapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly TravelContext _context;

        public ActivityController(TravelContext context)
        {
            _context = context;
        }

        // GET: api/Activity
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Activity>>> GetActivities()
        {
            return await _context.Activities.ToListAsync();
    }

    // GET: api/Activity/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Activity>> GetActivity(int id)
    {
        var activity = await _context.Activities.FindAsync(id);

        if (activity == null)
        {
            return NotFound();
        }

        return activity;
    }

    // POST: api/Activity
    [HttpPost]
    public async Task<ActionResult<Activity>> PostActivity(Activity activity)
    {
        _context.Activities.Add(activity);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetActivity", new { id = activity.IdActivity }, activity);
    }

    // PUT: api/Activity/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutActivity(int id, Activity activity)
    {
        if (id != activity.IdActivity)
        {
            return BadRequest();
        }

        _context.Entry(activity).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ActivityExists(id))
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

    // DELETE: api/Activity/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActivity(int id)
    {
        var activity = await _context.Activities.FindAsync(id);
        if (activity == null)
        {
            return NotFound();
        }

        _context.Activities.Remove(activity);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ActivityExists(int id)
    {
        return _context.Activities.Any(e => e.IdActivity == id);
    }
}
}
