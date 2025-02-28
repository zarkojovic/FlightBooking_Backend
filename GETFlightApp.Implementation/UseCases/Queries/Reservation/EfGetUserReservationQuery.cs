using GETFlightApp.Application.DTO;
using GETFlightApp.Application.DTO.Reservation;
using GETFlightApp.Application.UseCases.Queries.Reservation;
using GETFlightApp.DataAccess;
using GETFlightApp.Implementation.Validation.Flight;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace GETFlightApp.Implementation.UseCases.Queries.Reservation;

public class EfGetUserReservationQuery : IGetUserReservationQuery
{
    private readonly AspContext _context;
    private readonly CreateFlightValidator _validator;

    public EfGetUserReservationQuery(AspContext context, CreateFlightValidator validator)
    {
        _context = context;
        _validator = validator;
    }

    public int Id => 7;

    public string Name => "Reservation.GetUserReservation";

    public PagedResponse<UserReservationDTO> Execute(SearchReservationDTO search)
    {
        var user = _context.Users.Find(search.UserId);
        var query = user.Role.Name == "Agent"
            ? _context.Reservations.AsQueryable()
            : user.Reservations.AsQueryable();

        int totalCount = query.Count();

        int perPage = search.PerPage.HasValue ? (int)Math.Abs((double)search.PerPage) : 10;
        int page = search.Page.HasValue ? (int)Math.Abs((double)search.Page) : 1;
        int skip = perPage * (page - 1);

        query = query.Skip(skip).Take(perPage);

        return new PagedResponse<UserReservationDTO>
        {
            Data = query.Select(r => new UserReservationDTO
            {
                Id = r.Id,
                SeatsReserved = r.SeatsReserved,
                FlightDeparture = r.Flight.Departure.Name,
                FlightDestination = r.Flight.Destination.Name,
                DepartureDate = r.Flight.DepartureDate,
                Status = r.Status.Name,
                ReservationDate = r.CreatedAt,
            }).ToList(),
            CurrentPage = page,
            PerPage = perPage,
            TotalCount = totalCount
        };
    }

}
