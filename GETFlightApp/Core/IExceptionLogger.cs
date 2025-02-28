using GETFlightApp.Application;

namespace GETFlightApp.Core;

public interface IExceptionLogger
{
    Guid Log(Exception ex, IApplicationActor actor);
}
