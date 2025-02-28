using GETFlightApp.Domain.Primitives;

namespace GETFlightApp.Domain.Entities;

public class Status : Entity
{
    public string Name { get; set; }

    public virtual ICollection<Flight> Flights { get; set; } = new HashSet<Flight>();
    public virtual ICollection<Reservation> Reservations { get; set; } = new HashSet<Reservation>();
}
