using FluentValidation;
using GETFlightApp.Application.DTO.Flight;
using GETFlightApp.DataAccess;

namespace GETFlightApp.Implementation.Validation.Flight;

public class FlightSearchValidator : AbstractValidator<FlightSearchDTO>
{
    private readonly AspContext _context;
    public FlightSearchValidator(AspContext asp)
    {
        _context = asp;

        When(x => x.FlightId.HasValue, () =>
        {
            RuleFor(x => x.FlightId.Value)
                .Must(ctx => _context.Flights.Any(f => f.Id == ctx))
                .WithMessage("Flight with id of {PropertyValue} does not exist.");
        });

        When(x => x.DepartureId.HasValue, () =>
        {
            RuleFor(x => x.DepartureId.Value)
                .Must(ctx => _context.Cities.Any(c => c.Id == ctx))
                .WithMessage("Departure city with id of {PropertyValue} does not exist.");
        });

        When(x => x.DestinationId.HasValue, () =>
        {
            RuleFor(x => x.DestinationId.Value)
                .Must(ctx => _context.Cities.Any(c => c.Id == ctx))
                .WithMessage("Destination city with id of {PropertyValue} does not exist.")
                .Must((dto, ctx) => dto.DepartureId != ctx)
                .WithMessage("Departure and destination cities must be different.");
        });
    }
}
