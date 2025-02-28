using GETFlightApp.Application.UseCases;
using GETFlightApp.Application;
using System.Diagnostics;

namespace GETFlightApp.Implementation;

public class UseCaseHandler
{

    private readonly IApplicationActor _actor;
    private readonly IUseCaseLogger _logger;
    public UseCaseHandler(IApplicationActor actor, IUseCaseLogger logger)
    {
        _actor = actor;
        _logger = logger;
    }
    public void HandleCommand<TData>(ICommand<TData> command, TData data)
    {
        HandleCrossCuttingConcerns(command, data);

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        command.Execute(data);

        stopwatch.Stop();

        Console.WriteLine($"UseCase: {command.Name}, {stopwatch.ElapsedMilliseconds} ms");
    }
    
    public TResult HandleQuery<TResult, TSearch>(Application.UseCases.IQuery<TResult, TSearch> query, TSearch search)
        where TResult : class

    {
        HandleCrossCuttingConcerns(query, search);
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        var result = query.Execute(search);

        stopwatch.Stop();

        Console.WriteLine($"UseCase: {query.Name}, {stopwatch.ElapsedMilliseconds} ms");

        return result;
    }

    private void HandleCrossCuttingConcerns(IUseCase useCase, object data)
    {
        //Autorizacija
        if (!_actor.AllowedUseCases.Contains(useCase.Id))
        {
            throw new UnauthorizedAccessException();
        }

        var log = new UseCaseLog
        {
            UseCaseData = data,
            UseCaseName = useCase.Name,
            Username = _actor.Email,
        };

        _logger.Log(log);
    }
}
