
using Microsoft.AspNetCore.SignalR;

namespace GETFlightApp.Implementation.Hubs;

public class ReservationHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        await Clients.All.SendAsync("UserConnected", Context.ConnectionId);
    }
    public async Task NotifyNewReservation(int reservationId, int userId, int flightId, int seatsReserved)
    {
        await Clients.All.SendAsync("NewReservationCreated", reservationId, userId, flightId, seatsReserved);
    }

    public async Task NotifyReservationStatusUpdated(int reservationId, int statusId)
    {
        await Clients.All.SendAsync("ReservationStatusUpdated", reservationId, statusId);
    }
}
