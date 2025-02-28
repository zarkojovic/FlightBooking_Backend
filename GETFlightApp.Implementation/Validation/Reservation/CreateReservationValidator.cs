using FluentValidation;
using GETFlightApp.Application.DTO.Reservation;
using GETFlightApp.DataAccess;

namespace GETFlightApp.Implementation.Validation.Reservation;

public class CreateReservationValidator : AbstractValidator<CreateReservationDTO>
{
    private readonly AspContext _context;
    public CreateReservationValidator(AspContext asp)
    {
        _context = asp;

        RuleFor(x => x.FlightId)
            .NotEmpty()
            .WithMessage("Flight id is required.")
            .Must(x => _context.Flights.Any(f => f.Id == x))
            .WithMessage("Flight with id of {PropertyValue} does not exist.")
            .Must(x => _context.Flights.Any(f => f.Id == x && f.StatusId == 1))
            .WithMessage("Flight with id of {PropertyValue} is not available for reservation.")
            .Must(x => IsDepartureDateValid(x))
            .WithMessage("Flight with id of {PropertyValue} cannot be reserved as the departure date is less than 3 days away.");

        RuleFor(x => x.SeatsReserved)
            .NotEmpty()
            .WithMessage("Number of seats reserved is required.")
            .GreaterThan(0)
            .WithMessage("Number of seats reserved must be greater than 0.")
            .LessThanOrEqualTo(5)
            .WithMessage("Number of seats reserved must be less than or equal to 5.")
            .Must((dto, seatsReserved) => AreSeatsAvailable(dto.FlightId, seatsReserved))
            .WithMessage("Not enough seats available for reservation.");
    }

    private bool IsDepartureDateValid(int flightId)
    {
        var flight = _context.Flights.FirstOrDefault(f => f.Id == flightId);
        if (flight == null) return false;

        return (flight.DepartureDate - DateTime.Now).TotalDays >= 3;
    }

    private bool AreSeatsAvailable(int flightId, int seatsReserved)
    {
        var flight = _context.Flights.FirstOrDefault(f => f.Id == flightId);
        if (flight == null) return false;

        var reservedSeats = flight.Reservations.Sum(r => r.SeatsReserved);
        return flight.Seats >= reservedSeats + seatsReserved;
    }
}
