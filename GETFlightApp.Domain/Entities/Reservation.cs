using GETFlightApp.Domain.Primitives;

namespace GETFlightApp.Domain.Entities;

public class Reservation : Entity
{
    public int SeatsReserved { get; set; }
    public int UserId { get; set; }
    public int FlightId { get; set; }
    public  int StatusId { get; set; }
    public virtual Flight Flight { get; set; }
    public virtual User User { get; set; }
    public virtual Status Status { get; set; }
}
