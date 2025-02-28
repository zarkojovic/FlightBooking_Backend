namespace GETFlightApp.Application.DTO.Flight;

public class FlightSearchDTO : PagedSearch
{
    public int? FlightId { get; set; }
    public int? DepartureId { get; set; }
    public int? DestinationId { get; set; }
    public bool? Layovers { get; set; }
}