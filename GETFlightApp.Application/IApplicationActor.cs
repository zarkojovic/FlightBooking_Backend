namespace GETFlightApp.Application;

public interface IApplicationActor
{
    int Id { get; }
    string FirstName { get; }
    string LastName { get; }
    string Email { get; }
    IEnumerable<int> AllowedUseCases { get; }
}
