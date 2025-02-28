using FluentValidation;
using GETFlightApp.Application.UseCases.Commands.Flight;
using GETFlightApp.DataAccess;
using GETFlightApp.Implementation.Validation.Flight;

namespace GETFlightApp.Implementation.UseCases.Commands.Flight;

public class EfCancelFlightCommand : ICancelFlightCommand
{
    private readonly AspContext _aspContext;
    private readonly CancelFlightValidator _validator;
    public EfCancelFlightCommand(AspContext aspContext, CancelFlightValidator validator)
    {
        _aspContext = aspContext;
        _validator = validator;
    }

    public int Id => 4;

    public string Name => "Flight.CancelFlight";

    public void Execute(int id)
    {
        _validator.ValidateAndThrow(id);

        var flight = _aspContext.Flights.Find(id);
        flight.StatusId = 5;

        var reservations = _aspContext.Reservations.Where(r => r.FlightId == id).ToList();
        foreach (var reservation in reservations)
        {
            reservation.StatusId = 5;
        }

        _aspContext.SaveChanges();
    }
}
