using GETFlightApp.Application;
using GETFlightApp.Application.DTO.Reservation;
using GETFlightApp.Application.UseCases.Commands.Reservation;
using GETFlightApp.Application.UseCases.Queries.Reservation;
using GETFlightApp.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GETFlightApp.Controllers;

[ApiController]
[Route("[controller]")]
public class ReservationController : Controller
{

    private readonly UseCaseHandler _useCaseHandler;
    private readonly IApplicationActorProvider _applicationActorProvider;
    public ReservationController(UseCaseHandler useCaseHandler, IApplicationActorProvider applicationActorProvider)
    {
        _useCaseHandler = useCaseHandler;
        _applicationActorProvider = applicationActorProvider;
    }


    [HttpPost]
    public IActionResult Create([FromBody] CreateReservationDTO dto, [FromServices] ICreateReservationCommand command)
    {
        dto.UserId = _applicationActorProvider.GetActor().Id;
        _useCaseHandler.HandleCommand(command, dto);
        return Created();
    }

    [HttpPatch("{id}/approve")]
    public IActionResult Approve(int id, [FromServices] IApproveReservationCommand command)
    {
        _useCaseHandler.HandleCommand(command,id);
        return NoContent();
    }

    [HttpGet]
    public IActionResult UserReservation([FromServices] IGetUserReservationQuery query, [FromQuery]SearchReservationDTO dto)
    {
        dto.UserId = _applicationActorProvider.GetActor().Id;
        return Ok(_useCaseHandler.HandleQuery(query, dto));
    }
}
