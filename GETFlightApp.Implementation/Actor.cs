using GETFlightApp.Application;

namespace GETFlightApp.Implementation;


public class Actor : IApplicationActor
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    public IEnumerable<int> AllowedUseCases { get; set; }

}

public class UnauthorizedActor : IApplicationActor
{
    public int Id => 0;

    public string FirstName => "unauthorized";

    public string LastName => "unauthorized";

    public string Email => "unauthorized";
    public IEnumerable<int> AllowedUseCases => new List<int> { 1,2,3,4,5,6,7,8 };
}
