using Newtonsoft.Json;
using GETFlightApp.Application;

namespace GETFlightApp.Implementation.Logging.UseCases;

public class ConsoleUseCaseLogger : IUseCaseLogger
{
    public void Log(UseCaseLog log)
    {
        DateTime date = DateTime.UtcNow;
        string username = log.Username;
        string useCase = log.UseCaseName;
        string useCaseData = JsonConvert.SerializeObject(log.UseCaseData);

        Console.WriteLine($"Date: {date.ToLongDateString()} {date.ToLongTimeString()}, User: {username}, UseCase: {useCase}, Data: {useCaseData}");

    }
}
