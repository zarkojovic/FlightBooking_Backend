using GETFlightApp.Application;
using GETFlightApp.DataAccess;

namespace GETFlightApp.Core;

public class ConsoleExceptionLogger : IExceptionLogger
{
    public Guid Log(Exception ex, IApplicationActor actor)
    {
        var id = Guid.NewGuid();
        Console.WriteLine(ex.Message + " ID: " + id);

        return id;
    }
}
