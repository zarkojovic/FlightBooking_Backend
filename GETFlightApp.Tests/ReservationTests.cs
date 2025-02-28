using FluentAssertions;
using FluentValidation;
using GETFlightApp.Application.DTO.Reservation;
using GETFlightApp.DataAccess;
using GETFlightApp.Implementation.UseCases.Commands.Reservation;
using GETFlightApp.Implementation.Validation.Reservation;
using GETFlightApp.Implementation.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Moq;

public class EfCreateReservationCommandTests
{
    private readonly AspContext _context;
    private readonly CreateReservationValidator _validator;
    private readonly EfCreateReservationCommand _command;
    private readonly Mock<IHubContext<ReservationHub>> _hubContextMock;

    public EfCreateReservationCommandTests()
    {
        var options = new DbContextOptionsBuilder<AspContext>()
            .UseSqlServer("Data Source=ZARKO\\SQLEXPRESS;Initial Catalog=GET_FlightApp;Integrated Security=True;Trust Server Certificate=True")
            .Options;

        _context = new AspContext(options);
        _validator = new CreateReservationValidator(_context);
        _hubContextMock = new Mock<IHubContext<ReservationHub>>();
        _command = new EfCreateReservationCommand(_validator, _context, _hubContextMock.Object);
    }

    [Fact]
    public void Execute_Should_Create_Reservation_When_Valid_Data_Is_Provided()
    {
        var validReservation = new CreateReservationDTO
        {
            SeatsReserved = 2,
            FlightId = 1,
            UserId = 3
        };

        _command.Execute(validReservation);

        var reservation = _context.Reservations
            .Where(r => r.FlightId == 1 && r.UserId == 3)
            .OrderByDescending(r => r.CreatedAt)
            .FirstOrDefault();

        reservation.Should().NotBeNull();
        reservation.SeatsReserved.Should().Be(2);
        reservation.StatusId.Should().Be(2); // Default status
    }

    [Fact]
    public void Execute_Should_Throw_Exception_When_Required_Fields_Are_Missing()
    {
        var invalidReservation = new CreateReservationDTO
        {
            SeatsReserved = 2,
            FlightId = 0, // Missing Flight ID
            UserId = 3
        };

        Action action = () => _command.Execute(invalidReservation);

        action.Should().Throw<ValidationException>().WithMessage("*FlightId*");
    }

    [Fact]
    public void Execute_Should_Throw_Exception_When_SeatsReserved_Exceeds_Available()
    {
        var invalidReservation = new CreateReservationDTO
        {
            SeatsReserved = 9999, // Too many seats
            FlightId = 1,
            UserId = 3
        };

        Action action = () => _command.Execute(invalidReservation);

        action.Should().Throw<ValidationException>().WithMessage("*Not enough seats available*");
    }

    [Fact]
    public void Execute_Should_Throw_Exception_When_FlightId_Is_Invalid()
    {
        var invalidReservation = new CreateReservationDTO
        {
            SeatsReserved = 1,
            FlightId = 9999, // Non-existent flight
            UserId = 3
        };

        Action action = () => _command.Execute(invalidReservation);

        action.Should().Throw<ValidationException>().WithMessage("*Flight does not exist*");
    }

    [Fact]
    public void Execute_Should_Throw_Exception_When_UserId_Is_Invalid()
    {
        var invalidReservation = new CreateReservationDTO
        {
            SeatsReserved = 1,
            FlightId = 1,
            UserId = 9999 // Non-existent user
        };

        Action action = () => _command.Execute(invalidReservation);

        action.Should().Throw<ValidationException>().WithMessage("*User does not exist*");
    }
}
