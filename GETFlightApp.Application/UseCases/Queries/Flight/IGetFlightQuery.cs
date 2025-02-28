using GETFlightApp.Application.DTO;
using GETFlightApp.Application.DTO.Flight;

namespace GETFlightApp.Application.UseCases.Queries.Flight;

public interface IGetFlightQuery : IQuery<PagedResponse<FlightDTO>, FlightSearchDTO>
{
}
