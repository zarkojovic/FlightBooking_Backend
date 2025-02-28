using GETFlightApp.Application.DTO;
using GETFlightApp.Application.DTO.Flight;
using GETFlightApp.Application.UseCases.Commands.Flight;
using GETFlightApp.Application.UseCases.Commands.User;
using GETFlightApp.Application.UseCases.Queries.Flight;
using GETFlightApp.Implementation;
using Microsoft.AspNetCore.Mvc;

namespace GETFlightApp.Controllers;

[ApiController]
[Route("[controller]")]
public class FlightController : Controller
{
    private readonly UseCaseHandler _useCaseHandler;

    public FlightController(UseCaseHandler useCaseHandler)
    {
        _useCaseHandler = useCaseHandler;
    }

    [HttpPost]
    public IActionResult Create([FromServices]ICreateFlightCommand command, [FromBody]CreateFlightDTO dto)
    {
        _useCaseHandler.HandleCommand(command, dto);
        return Created();
    }

    [HttpGet]
    public IActionResult Get([FromServices] IGetFlightQuery query, [FromQuery] FlightSearchDTO search)
    {
        return Ok(_useCaseHandler.HandleQuery(query,search));
    }

    //[HttpGet("{id}")]
    //public IActionResult Find([FromServices] IFindFlightQuery query, [FromRoute] int id)
    //{
    //    return Ok(_useCaseHandler.HandleQuery(query,id));
    //}

    [HttpDelete("{id}")]
    public IActionResult Delete([FromServices] ICancelFlightCommand command, [FromRoute] int id)
    {
        _useCaseHandler.HandleCommand(command, id);
        return NoContent();
    }
}
