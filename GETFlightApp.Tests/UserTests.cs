using FluentAssertions;
using FluentValidation;
using GETFlightApp.Application.DTO.User;
using GETFlightApp.DataAccess;
using GETFlightApp.Implementation.UseCases.Commands.User;
using GETFlightApp.Implementation.Validation.User;
using Microsoft.EntityFrameworkCore;

public class EfRegisteredUserCommandTests
{
    private readonly AspContext _context;
    private readonly RegisterUserValidator _validator;
    private readonly EfRegisteredUserCommand _command;

    public EfRegisteredUserCommandTests()
    {
        var options = new DbContextOptionsBuilder<AspContext>()
            .UseSqlServer("Data Source=ZARKO\\SQLEXPRESS;Initial Catalog=GET_FlightApp;Integrated Security=True;Trust Server Certificate=True")
            .Options;

        _context = new AspContext(options);
        _validator = new RegisterUserValidator(_context);
        _command = new EfRegisteredUserCommand(_context, _validator);
    }

    [Fact]
    public void Execute_Should_Create_User_When_Valid_Data_Is_Provided()
    {
        var validUser = new RegisterUserDTO
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Password = "StrongPassword123!",
            RoleId = 2
        };

        _command.Execute(validUser);

        var user = _context.Users
            .Where(u => u.Email == "john.doe@example.com")
            .OrderByDescending(u => u.CreatedAt)
            .FirstOrDefault();

        user.Should().NotBeNull();
        user.FirstName.Should().Be("John");
        user.LastName.Should().Be("Doe");
        user.RoleId.Should().Be(2);
    }

    [Fact]
    public void Execute_Should_Throw_Exception_When_Required_Fields_Are_Missing()
    {
        var invalidUser = new RegisterUserDTO
        {
            FirstName = "",
            LastName = "",
            Email = "test@example.com",
            Password = "StrongPassword123!",
            RoleId = 2
        };

        Action action = () => _command.Execute(invalidUser);

        action.Should().Throw<ValidationException>().WithMessage("*FirstName*").And
              .Message.Should().Contain("*LastName*");
    }

    [Fact]
    public void Execute_Should_Throw_Exception_When_Email_Format_Is_Invalid()
    {
        var invalidUser = new RegisterUserDTO
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "invalid-email",
            Password = "StrongPassword123!",
            RoleId = 2
        };

        Action action = () => _command.Execute(invalidUser);

        action.Should().Throw<ValidationException>().WithMessage("*Email*");
    }

    [Fact]
    public void Execute_Should_Throw_Exception_When_Email_Already_Exists()
    {
        var existingUser = new RegisterUserDTO
        {
            FirstName = "Jane",
            LastName = "Doe",
            Email = "existing@example.com",
            Password = "StrongPassword123!",
            RoleId = 2
        };

        _command.Execute(existingUser);

        var duplicateUser = new RegisterUserDTO
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "existing@example.com",
            Password = "AnotherPassword123!",
            RoleId = 2
        };

        Action action = () => _command.Execute(duplicateUser);

        action.Should().Throw<ValidationException>().WithMessage("*Email already exists*");
    }

    [Fact]
    public void Execute_Should_Throw_Exception_When_RoleId_Is_Invalid()
    {
        var invalidUser = new RegisterUserDTO
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Password = "StrongPassword123!",
            RoleId = 9999 // Non-existent role
        };

        Action action = () => _command.Execute(invalidUser);

        action.Should().Throw<ValidationException>().WithMessage("*Role does not exist*");
    }
}
