using GETFlightApp.Application.DTO;
using GETFlightApp.Application.DTO.Reservation;

namespace GETFlightApp.Application.UseCases.Queries.Reservation;

public interface IGetUserReservationQuery : IQuery<PagedResponse<UserReservationDTO>, SearchReservationDTO>
{
    PagedResponse<UserReservationDTO> Execute(SearchReservationDTO search);
}   