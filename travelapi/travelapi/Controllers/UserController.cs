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
using Microsoft.Extensions.ObjectPool;

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
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers(int? pageNumber, int? pageSize)
    {
        var results = await _userServices.BuscarUserPagiandos(pageNumber, pageSize);
        var userDtos = _mapper.Map<List<UserDto>>(results);
        return Ok(userDtos);
    }
    [HttpGet("login")]
    public async Task<ActionResult<AuthToken>> Login(string email, string password)
    {
        var verifica = _userServices.BuscaLogin(email, password);

        if (verifica == true)
        {
            var user = _userServices.ValidaLogin(email, password);
            var token = _userServices.GenerateJwtToken(user.Username);
            var authToken = new AuthUser
            {
                accessToken = token,
                user = user, // Adicione esta linha
            };

            return Ok(authToken);
        }
        else
        {
            return BadRequest("Email ou Senha Incorretos");
        }

    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUsersById(int id)
    {
        var user = await _userServices.BuscarUserPorId(id);

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
        try
        {
            var user = _mapper.Map<User>(userDto);
            var resul = await _userServices.CriarUser(user);

            var createdDto = _mapper.Map<UserDto>(user);

            return CreatedAtAction("GetUsersById", new { id = createdDto.IdUser }, createdDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(int id, UserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        if (id != userDto.IdUser)
        {
            return BadRequest("Id do user != do id");
        }
        try
        {
            var result = await _userServices.EditarUser(id, user);
            if (result != null)
            {
                return Ok(result); 
            }
            else
            {
                return NotFound(); 
            }
        }
        catch (Exception ex)
        {  
            return BadRequest(ex.Message); 
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        try
        {

            var result = await _userServices.DeletarUser(id);
            if (result == null)
            {
                return BadRequest();
            }

            return NoContent();
        }
        catch(Exception ex) { }
        {
            return BadRequest($"Unable to delete user {id}");
        }
    }


}
