using System.Text.Json.Serialization;

namespace GETFlightApp.Application.DTO.Reservation;

public class SearchReservationDTO : PagedSearch
{
    [JsonIgnore]
    public int UserId { get; set; }
}
