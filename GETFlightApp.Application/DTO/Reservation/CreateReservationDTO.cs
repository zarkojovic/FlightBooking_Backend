namespace GETFlightApp.Application.DTO.Reservation;

public class CreateReservationDTO
{
    public int UserId { get; set; }
    public int SeatsReserved { get; set; }
    public int FlightId { get; set; }
}
