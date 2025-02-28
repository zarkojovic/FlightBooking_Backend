using GETFlightApp.Application.DTO.User;
using GETFlightApp.Application.UseCases.Commands.User;
using GETFlightApp.Core;
using GETFlightApp.DataAccess;
using GETFlightApp.DTO;
using GETFlightApp.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GETFlightApp.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : Controller
{
    private readonly AspContext _context;
    private readonly UseCaseHandler _useCaseHandler;
    private readonly JwtTokenCreator _jwtTokenCreator;
    public UserController([FromServices] AspContext context, UseCaseHandler useCaseHandler, JwtTokenCreator jwtTokenCreator)
    {
        _context = context;
        _useCaseHandler = useCaseHandler;
        _jwtTokenCreator = jwtTokenCreator;
    }

    [HttpPost]
    public IActionResult Create([FromBody]RegisterUserDTO dto, [FromServices] IRegisterUserCommand command)
    {
        _useCaseHandler.HandleCommand(command, dto);
        return Created();
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] AuthRequest request)
    {
        string token = _jwtTokenCreator.Create(request.Email, request.Password);

        return Ok(new AuthResponse { Token = token });
    }

}
