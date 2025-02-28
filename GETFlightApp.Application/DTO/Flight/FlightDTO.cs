namespace GETFlightApp.Application.DTO.Flight;

public class FlightDTO
{
    public int Id { get; set; }
    public int Seats { get; set; }
    public int Layovers { get; set; }
    public int SeatsLeft { get; set; }
    public string Departure { get; set; }
    public string Destination { get; set; }
    public DateTime DepartureDate { get; set; }
    public string Status { get; set; }
}