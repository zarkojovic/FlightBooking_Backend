using GETFlightApp.Domain.Primitives;

namespace GETFlightApp.Domain.Entities;

public class City : Entity
{
    public string Name { get; set; }

    public virtual ICollection<Flight> Departures { get; set; } = new HashSet<Flight>();
    public virtual ICollection<Flight> Destinations { get; set; } = new HashSet<Flight>();
}
