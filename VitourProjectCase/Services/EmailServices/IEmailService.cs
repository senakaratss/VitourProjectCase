using VitourProjectCase.Dtos.ReservationDtos;

namespace VitourProjectCase.Services.EmailServices
{
    public interface IEmailService
    {
        Task SendConfirmReservationEmailAsync(CreateReservationDto createReservationDto);
    }
}
