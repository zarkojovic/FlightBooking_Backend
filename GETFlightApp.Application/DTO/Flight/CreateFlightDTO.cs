namespace GETFlightApp.Application.DTO.Flight;

public class CreateFlightDTO
{
    public int Layovers { get; set; }
    public int Seats { get; set; }
    public DateTime DepartureDate { get; set; }
    public int DepartureId { get; set; }
    public int DestinationId { get; set; }
}
