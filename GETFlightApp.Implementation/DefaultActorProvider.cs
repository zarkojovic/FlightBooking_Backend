using GETFlightApp.Application;

namespace GETFlightApp.Implementation;

public class DefaultActorProvider : IApplicationActorProvider
{
    public IApplicationActor GetActor()
    {
        return new Actor
        {
            Id = 0,
            FirstName = "Anonymous",
        };
    }
}
