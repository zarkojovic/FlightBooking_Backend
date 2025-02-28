using FluentValidation;
using GETFlightApp.Application.UseCases.Commands.Reservation;
using GETFlightApp.DataAccess;
using GETFlightApp.Implementation.Hubs;
using GETFlightApp.Implementation.Validation.Reservation;
using Microsoft.AspNetCore.SignalR;

namespace GETFlightApp.Implementation.UseCases.Commands.Reservation;

public class EfApproveReservationCommand : IApproveReservationCommand
{
    private readonly AspContext _aspContext;
    private readonly ApproveReservationValidator _approveReservationValidator;
    private readonly IHubContext<ReservationHub> _hubContext;


    public EfApproveReservationCommand(AspContext aspContext, ApproveReservationValidator approveReservationValidator, IHubContext<ReservationHub> hubContext)
    {
        _aspContext = aspContext;
        _approveReservationValidator = approveReservationValidator;
        _hubContext = hubContext;
    }

    public int Id => 6;
    public string Name => "Reservation.ApproveReservation";

    public async void Execute(int data)
    {
        _approveReservationValidator.ValidateAndThrow(data);

        var reservation = _aspContext.Reservations.FirstOrDefault(r => r.Id == data);

        reservation.StatusId = 3;

        _aspContext.SaveChanges();

        // Notify the user via SignalR
        await _hubContext.Clients.All
            .SendAsync("ReservationStatusUpdated", reservation.Id, reservation.StatusId);


    }
}
