
namespace GETFlightApp.Application.DTO.Reservation;

public class UserReservationDTO : PagedSearch
{
    public int Id { get; set; }
    public int SeatsReserved { get; set; }
    public string FlightDeparture { get; set; }
    public string FlightDestination { get; set; }
    public DateTime DepartureDate { get; set; }
    public DateTime ReservationDate { get; set; }
    public string Status { get; set; }
}
