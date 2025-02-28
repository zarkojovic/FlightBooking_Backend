using FluentValidation;
using GETFlightApp.Application.DTO.Reservation;
using GETFlightApp.Application.UseCases.Commands.Reservation;
using GETFlightApp.DataAccess;
using GETFlightApp.Implementation.Hubs;
using GETFlightApp.Implementation.Validation.Reservation;
using Microsoft.AspNetCore.SignalR;

namespace GETFlightApp.Implementation.UseCases.Commands.Reservation;

public class EfCreateReservationCommand : ICreateReservationCommand
{
    private readonly CreateReservationValidator _createReservationValidator;
    private readonly AspContext _aspContext;
    private readonly IHubContext<ReservationHub> _hubContext;

    public EfCreateReservationCommand(CreateReservationValidator createReservationValidator, AspContext aspContext, IHubContext<ReservationHub> hubContext)
    {
        _createReservationValidator = createReservationValidator;
        _aspContext = aspContext;
        _hubContext = hubContext;
    }

    public int Id => 5;
    public string Name => "Reservation.CreateReservation";

    public async void Execute(CreateReservationDTO data)
    {
        _createReservationValidator.ValidateAndThrow(data);

        var reservation = new Domain.Entities.Reservation
        {
            SeatsReserved = data.SeatsReserved,
            FlightId = data.FlightId,
            StatusId = 2,
            UserId = data.UserId
        };

        _aspContext.Reservations.Add(reservation);

        _aspContext.SaveChanges();

        // Notify agents via SignalR
        await _hubContext.Clients.All.SendAsync("NewReservationCreated", 
            reservation.Id, reservation.UserId, reservation.FlightId, reservation.SeatsReserved);
    }
}
