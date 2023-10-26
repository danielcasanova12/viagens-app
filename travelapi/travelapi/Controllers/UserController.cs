using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using travelapi.Domain.Dto;
using travelapi.Domain.Models;
using travelapi.Infrastructure;
using AutoMapper;
using travelapi.Application.Services;
using travelapi.Application.Interfaces;

[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly TravelContext _context;
    private readonly IMapper _mapper;
    private readonly IUserServices _userServices;
    public UserController(TravelContext context, IMapper mapper, IUserServices userServices)
    {
        _context = context;
        _mapper = mapper;
        _userServices = userServices;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers(int pageNumber, int pageSize)
    {
        var results = await _userServices.BuscarUserPagiandos(pageNumber, pageSize);
        var userDtos = _mapper.Map<List<UserDto>>(results);
        return Ok(userDtos);
    }
    

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        var userDto = _mapper.Map<UserDto>(user);
        return userDto;
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> PostUser(UserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var createdDto = _mapper.Map<UserDto>(user);

        return CreatedAtAction("GetUser", new { id = createdDto.IdUser }, createdDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(int id, UserDto userDto)
    {
        if (id != userDto.IdUser)
        {
            return BadRequest();
        }

        var user = _mapper.Map<User>(userDto);
        _context.Entry(user).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(id))
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
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool UserExists(int id)
    {
        return _context.Users.Any(e => e.IdUser == id);
    }
}
