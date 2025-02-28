using FluentValidation;
using GETFlightApp.DataAccess;

namespace GETFlightApp.Implementation.Validation.Reservation;

public class ApproveReservationValidator : AbstractValidator<int>
{
    private readonly AspContext _aspContext;

    public ApproveReservationValidator(AspContext aspContext)
    {
        _aspContext = aspContext ?? throw new ArgumentNullException(nameof(aspContext));

        RuleFor(id => id)
            .NotEmpty()
            .WithMessage("You must provide an ID!")
            .Must(ReservationExists)
            .WithMessage("Reservation doesn't exist with provided ID!")
            .Must(ReservationIsPending)
            .WithMessage("Unable to approve this reservation!");
    }

    private bool ReservationExists(int id)
    {
        return _aspContext.Reservations.Any(x => x.Id == id);
    }

    private bool ReservationIsPending(int id)
    {
        return _aspContext.Reservations.Any(x => x.Id == id && x.StatusId == 2);
    }
}
