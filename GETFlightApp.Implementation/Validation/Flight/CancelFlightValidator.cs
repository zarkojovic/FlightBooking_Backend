using FluentValidation;
using GETFlightApp.DataAccess;

namespace GETFlightApp.Implementation.Validation.Flight;

public class CancelFlightValidator : AbstractValidator<int>
{
    private readonly AspContext _aspContext;
    public CancelFlightValidator(AspContext aspContext)
    {
        _aspContext = aspContext;

        RuleFor(x => x)
            .Must(ctx => _aspContext.Flights.Any(x => x.Id == ctx))
            .WithMessage("Flight with id of {PropertyValue} does not exist.")
            .Must(ctx => _aspContext.Flights.Any(x => x.Id == ctx && x.StatusId != 5))
            .WithMessage("Flight with id of {PropertyValue} is already canceled.")
            .Must(ctx => _aspContext.Flights.Any(x => x.Id == ctx && x.DepartureDate > DateTime.Now))
            .WithMessage("Flight with id of {PropertyValue} has a departure date in the past.");
    }
}
