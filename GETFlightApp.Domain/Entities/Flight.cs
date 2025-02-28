using GETFlightApp.Domain.Primitives;

namespace GETFlightApp.Domain.Entities;

public class Flight : Entity
{
    public int Layovers { get; set; }
    public int Seats { get; set; }
    public DateTime DepartureDate { get; set; }
    public int DepartureId { get; set; }
    public int DestinationId { get; set; }
    public int StatusId { get; set; }
    public virtual Status Status { get; set; }
    public virtual City Departure { get; set; }
    public virtual City Destination { get; set; }
    public virtual ICollection<Reservation> Reservations { get; set; } = new HashSet<Reservation>();
}
