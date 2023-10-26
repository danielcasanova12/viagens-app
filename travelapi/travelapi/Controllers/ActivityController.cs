using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using travelapi.Application.Interfaces;
using travelapi.Domain.Dto;
using travelapi.Domain.Models;
using travelapi.Infrastructure;
using travelapi.Utils;

namespace travelapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly TravelContext _context;
        private readonly IMapper _mapper;
        private readonly IActivityServices _travelServices;

        public ActivityController(TravelContext context, IMapper mapper, IActivityServices travelServices)
        {
            _context = context;
            _mapper = mapper;
            _travelServices = travelServices;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActivityDto>>> GetActivities()
        {
            var activities = await _context.Activities.ToListAsync();
            var activityDtos = _mapper.Map<List<ActivityDto>>(activities);
            return activityDtos;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ActivityDto>> GetActivity(int id)
        {
            try
            {
                var result = await _travelServices.BuscarActyvityPorId(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex.Failin();
            }
            //var activity = await _context.Activities.FindAsync(id);

            //if (activity == null)
            //{
            //    return NotFound();
            //}

            //var activityDto = _mapper.Map<ActivityDto>(activity);
            //return activityDto;
        }

        [HttpPost]
        public async Task<ActionResult<ActivityDto>> PostActivity(ActivityDto activityDto)
        {
            var activity = _mapper.Map<Activity>(activityDto);
            _context.Activities.Add(activity);
            await _context.SaveChangesAsync();

            var createdActivityDto = _mapper.Map<ActivityDto>(activity);
            return CreatedAtAction("GetActivity", new { id = createdActivityDto.IdActivity }, createdActivityDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivity(int id, ActivityDto activityDto)
        {
            if (id != activityDto.IdActivity)
            {
                return BadRequest();
            }

            var existingActivity = await _context.Activities.FindAsync(id);

            if (existingActivity == null)
            {
                return NotFound();
            }

            _mapper.Map(activityDto, existingActivity);
            _context.Entry(existingActivity).State = EntityState.Modified;

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
