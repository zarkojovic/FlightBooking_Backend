using FluentValidation;
using GETFlightApp.Application.DTO.User;
using GETFlightApp.Application.UseCases.Commands.User;
using GETFlightApp.DataAccess;
using GETFlightApp.Implementation.Validation.User;

namespace GETFlightApp.Implementation.UseCases.Commands.User;

public class EfRegisteredUserCommand : IRegisterUserCommand
{
    private readonly AspContext _context;
    private readonly RegisterUserValidator _validator;

    public EfRegisteredUserCommand(AspContext context, RegisterUserValidator validator)
    {
        _context = context;
        _validator = validator;
    }

    public int Id => 1;

    public string Name => "User.RegisterUser";

    public void Execute(RegisterUserDTO data)
    {
        _validator.ValidateAndThrow(data);

        var user = new Domain.Entities.User
        {
            FirstName = data.FirstName,
            LastName = data.LastName,
            Email = data.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(data.Password),
            RoleId = data.RoleId
        };

        _context.Users.Add(user);

        _context.SaveChanges();
    }
}
