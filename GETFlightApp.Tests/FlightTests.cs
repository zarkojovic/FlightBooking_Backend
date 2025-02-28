using GETFlightApp.Application.DTO.Flight;
using GETFlightApp.DataAccess;
using GETFlightApp.Implementation.UseCases.Commands.Flight;
using GETFlightApp.Implementation.Validation.Flight;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using FluentValidation;

public class EfCreateFlightCommandTests
{
    private readonly AspContext _context;
    private readonly CreateFlightValidator _validator;
    private readonly EfCreateFlightCommand _command;

    public EfCreateFlightCommandTests()
    {
        var options = new DbContextOptionsBuilder<AspContext>()
            .UseSqlServer("Data Source=ZARKO\\SQLEXPRESS;Initial Catalog=GET_FlightApp;Integrated Security=True;Trust Server Certificate=True")
            .Options;

        _context = new AspContext(options);
        _validator = new CreateFlightValidator(_context);
        _command = new EfCreateFlightCommand(_context, _validator);
    }

    [Fact]
    public void Execute_Should_Create_Flight_When_Valid_Data_Is_Provided()
    {
        var validFlight = new CreateFlightDTO
        {
            Seats = 30,
            Layovers = 1,
            DepartureDate = DateTime.UtcNow.AddDays(5),
            DepartureId = 1,
            DestinationId = 3
        };

        _command.Execute(validFlight);

        var flight = _context.Flights
            .Where(f => f.DepartureId == 1 && f.DestinationId == 3)
            .OrderByDescending(f => f.CreatedAt)
            .FirstOrDefault();

        flight.Should().NotBeNull();
        flight.Seats.Should().Be(30);
        flight.Layovers.Should().Be(1);
    }

    [Fact]
    public void Execute_Should_Throw_Exception_When_Required_Fields_Are_Missing()
    {
        var invalidFlight = new CreateFlightDTO
        {
            Seats = 30,
            Layovers = 1,
            DepartureDate = DateTime.UtcNow.AddDays(5),
            DepartureId = 0, // Missing Departure
            DestinationId = 3
        };

        Action action = () => _command.Execute(invalidFlight);

        action.Should().Throw<ValidationException>().WithMessage("*DepartureId*");
    }

    [Fact]
    public void Execute_Should_Throw_Exception_When_Departure_And_Destination_Are_The_Same()
    {
        var invalidFlight = new CreateFlightDTO
        {
            Seats = 20,
            Layovers = 2,
            DepartureDate = DateTime.UtcNow.AddDays(10),
            DepartureId = 2,
            DestinationId = 2 // Departure and Destination can't be the same
        };

        Action action = () => _command.Execute(invalidFlight);

        action.Should().Throw<ValidationException>().WithMessage("*Departure and Destination must be different*");
    }

    [Fact]
    public void Execute_Should_Throw_Exception_When_Departure_Date_Is_In_The_Past()
    {
        var invalidFlight = new CreateFlightDTO
        {
            Seats = 50,
            Layovers = 0,
            DepartureDate = DateTime.UtcNow.AddDays(-1), // Past Date
            DepartureId = 1,
            DestinationId = 4
        };

        Action action = () => _command.Execute(invalidFlight);

        action.Should().Throw<ValidationException>().WithMessage("*Departure date must be in the future*");
    }
}
