using FluentValidation;
using GETFlightApp.Application.DTO.Flight;
using GETFlightApp.DataAccess;

namespace GETFlightApp.Implementation.Validation.Flight;

public class CreateFlightValidator : AbstractValidator<CreateFlightDTO>
{
    private readonly AspContext _context;
    public CreateFlightValidator(AspContext asp)
    {
        _context = asp;

        RuleFor(x => x.Seats)
            .NotEmpty()
            .WithMessage("Number of seats is required.")
            .GreaterThan(10)
            .WithMessage("Number of seats must be greater than 10.")
            .LessThan(50)
            .WithMessage("Number of seats must be less than 50.");

        RuleFor(x => x.Layovers)
            .GreaterThan(-1)
            .WithMessage("Number of layovers must be greater than -1.")
            .LessThan(5)
            .WithMessage("Number of layovers must be less than 5.");

        RuleFor(x => x.DepartureDate)
            .NotEmpty()
            .WithMessage("Departure date is required.")
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("Departure date must be in the future.");

        RuleFor(x => x.DepartureId)
            .NotEmpty()
            .WithMessage("Departure location is required.")
            .Must(departureId => _context.Cities.Any(l => l.Id == departureId))
            .WithMessage("Departure location does not exist.");

        RuleFor(x => x.DestinationId)
            .NotEmpty()
            .WithMessage("Destination location is required.")
            .NotEqual(x => x.DepartureId)
            .WithMessage("Destination location must be different from departure location.")
            .Must(destinationId => _context.Cities.Any(l => l.Id == destinationId))
            .WithMessage("Destination location does not exist.");

    }
}
