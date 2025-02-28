using FluentValidation;
using GETFlightApp.Application.DTO.Flight;
using GETFlightApp.Application.UseCases.Commands.Flight;
using GETFlightApp.DataAccess;
using GETFlightApp.Implementation.Validation.Flight;

namespace GETFlightApp.Implementation.UseCases.Commands.Flight;

public class EfCreateFlightCommand : ICreateFlightCommand
{
    private readonly AspContext _context;
    private readonly CreateFlightValidator _validator;

    public EfCreateFlightCommand(AspContext context, CreateFlightValidator validator)
    {
        _context = context;
        _validator = validator;
    }

    public int Id => 2;

    public string Name => "Flight.CreateFlight";

    public void Execute(CreateFlightDTO data)
    {
        _validator.ValidateAndThrow(data);


        var flight = new Domain.Entities.Flight
        {
            Seats = data.Seats,
            Layovers = data.Layovers,
            DepartureDate = data.DepartureDate,
            DepartureId = data.DepartureId,
            DestinationId = data.DestinationId,
        };

        _context.Flights.Add(flight);

        _context.SaveChanges();
    }
}
