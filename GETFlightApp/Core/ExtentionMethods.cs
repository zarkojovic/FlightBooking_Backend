using GETFlightApp.Application.UseCases.Commands.Flight;
using GETFlightApp.Application.UseCases.Commands.Reservation;
using GETFlightApp.Application.UseCases.Commands.User;
using GETFlightApp.Application.UseCases.Queries.Flight;
using GETFlightApp.Application.UseCases.Queries.Reservation;
using GETFlightApp.Implementation.UseCases.Commands.Flight;
using GETFlightApp.Implementation.UseCases.Commands.Reservation;
using GETFlightApp.Implementation.UseCases.Commands.User;
using GETFlightApp.Implementation.UseCases.Queries.Flight;
using GETFlightApp.Implementation.UseCases.Queries.Reservation;
using GETFlightApp.Implementation.Validation.Flight;
using GETFlightApp.Implementation.Validation.Reservation;
using GETFlightApp.Implementation.Validation.User;
using System.IdentityModel.Tokens.Jwt;

namespace GETFlightApp.Core;

public static class ExtentionMethods
{
    public static void AddUseCases(this IServiceCollection services)
    {
        services.AddTransient<IRegisterUserCommand, EfRegisteredUserCommand>();
        services.AddTransient<RegisterUserValidator>();
        services.AddTransient<ICreateFlightCommand, EfCreateFlightCommand>();
        services.AddTransient<CreateFlightValidator>();
        services.AddTransient<IGetFlightQuery, EfGetFlightQuery>();
        services.AddTransient<FlightSearchValidator>();
        services.AddTransient<ICancelFlightCommand, EfCancelFlightCommand>();
        services.AddTransient<CancelFlightValidator>();
        services.AddTransient<ICreateFlightCommand, EfCreateFlightCommand>();
        services.AddTransient<CreateFlightValidator>();
        services.AddTransient<ICreateReservationCommand, EfCreateReservationCommand>();
        services.AddTransient<CreateReservationValidator>();
        services.AddTransient<IApproveReservationCommand, EfApproveReservationCommand>();
        services.AddTransient<ApproveReservationValidator>();
        services.AddTransient<IGetUserReservationQuery, EfGetUserReservationQuery>();
    }
    public static Guid? GetTokenId(this HttpRequest request)
    {
        if (request == null || !request.Headers.ContainsKey("Authorization"))
        {
            return null;
        }

        string authHeader = request.Headers["Authorization"].ToString();

        if (authHeader.Split("Bearer ").Length != 2)
        {
            return null;
        }

        string token = authHeader.Split("Bearer ")[1];

        var handler = new JwtSecurityTokenHandler();

        var tokenObj = handler.ReadJwtToken(token);

        var claims = tokenObj.Claims;

        var claim = claims.First(x => x.Type == "jti").Value;

        var tokenGuid = Guid.Parse(claim);

        return tokenGuid;
    }

}
