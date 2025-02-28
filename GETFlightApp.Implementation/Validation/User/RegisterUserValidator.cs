using FluentValidation;
using GETFlightApp.Application.DTO.User;
using GETFlightApp.DataAccess;

namespace GETFlightApp.Implementation.Validation.User;

public class RegisterUserValidator : AbstractValidator<RegisterUserDTO>
{
    private readonly AspContext _aspContext;

    public RegisterUserValidator(AspContext aspContext)
    {
        _aspContext = aspContext;


        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("First name is required.")
            .MaximumLength(50)
            .WithMessage("First name can't be longer than 50 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Last name is required.")
            .MaximumLength(50)
            .WithMessage("Last name can't be longer than 50 characters.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .MaximumLength(50)
            .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")
            .WithMessage("Email is not in the correct format.")
            .Must(email => !aspContext.Users.Any(u => u.Email == email))
            .WithMessage("Email is already taken.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .MinimumLength(8)
            .WithMessage("Password must be at least 8 characters long.")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$")
            .WithMessage("Password must contain at least one uppercase letter, one lowercase letter and one number.");

        RuleFor(x => x.RoleId)
            .NotEmpty()
            .WithMessage("Role is required.")
            .Must(roleId => aspContext.Roles.Any(r => r.Id == roleId))
            .WithMessage("Role does not exist.");



    }
}
