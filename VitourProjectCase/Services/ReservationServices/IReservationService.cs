using VitourProjectCase.Dtos.ReservationDtos;

namespace VitourProjectCase.Services.ReservationServices
{
    public interface IReservationService
    {
        Task<List<ResultReservationDto>> GetAllReservationAsync();
        Task<List<ResultReservationByTourIdDto>> GetReservationsByTourIdAsync(string id);
        Task<string> CreateReservationAsync(CreateReservationDto createReservationDto);
        Task UpdateReservationAsync(UpdateReservationDto updateReservationDto);
        Task DeleteReservationAsync(string id);
        Task<GetReservationByIdDto> GetReservationByIdAsync(string id);
        Task<GetReservationByIdDto> GetReservationByCodeAsync(string code);
    }
}
