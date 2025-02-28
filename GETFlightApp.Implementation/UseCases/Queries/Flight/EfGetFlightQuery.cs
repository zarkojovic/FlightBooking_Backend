using FluentValidation;
using GETFlightApp.Application.DTO;
using GETFlightApp.Application.DTO.Flight;
using GETFlightApp.Application.UseCases.Queries.Flight;
using GETFlightApp.DataAccess;
using GETFlightApp.Implementation.Validation.Flight;

namespace GETFlightApp.Implementation.UseCases.Queries.Flight;

public class EfGetFlightQuery : IGetFlightQuery
{
    private readonly AspContext _context;
    private readonly FlightSearchValidator _validator;

    public EfGetFlightQuery(AspContext context, FlightSearchValidator validator)
    {
        _context = context;
        _validator = validator;
    }

    public int Id => 3;
    public string Name => "Flight.GetFlights";

    public PagedResponse<FlightDTO> Execute(FlightSearchDTO search)
    {
        _validator.ValidateAndThrow(search);

        var query = _context.Flights.Where(x => 
            x.StatusId == 1 &&
            x.DepartureDate > DateTime.Now
            ).AsQueryable();

        if (search.FlightId.HasValue)
        {
            query = query.Where(x => x.Id == search.FlightId);
        }

        if (search.DepartureId.HasValue)
        {
            query = query.Where(x => x.DepartureId == search.DepartureId);
        }

        if (search.DestinationId.HasValue)
        {
            query = query.Where(x => x.DestinationId == search.DestinationId);
        }

        if (search.Layovers.HasValue)
        {
            query = query.Where(x => (bool)search.Layovers ? x.Layovers > 0 : x.Layovers == 0);
        }

        int totalCount = query.Count();

        int perPage = search.PerPage.HasValue ? (int)Math.Abs((double)search.PerPage) : 10;
        int page = search.Page.HasValue ? (int)Math.Abs((double)search.Page) : 1;
        int skip = perPage * (page - 1);

        query = query.Skip(skip).Take(perPage);

        return new PagedResponse<FlightDTO>
        {
            Data = query.Select(x => new FlightDTO
            {
                Id = x.Id,
                Seats = x.Seats,
                Layovers = x.Layovers,
                SeatsLeft = x.Seats - x.Reservations.Sum(r => r.SeatsReserved),
                Departure = x.Departure.Name,
                Destination = x.Destination.Name,
                DepartureDate = x.DepartureDate,
                Status = x.Status.Name
            }).ToList(),
            CurrentPage = page,
            PerPage = perPage,
            TotalCount = totalCount
        };
    }
}
