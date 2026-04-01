namespace VitourProjectCase.Services.ReservationServices
{
    public interface IReservationValidator
    {
        Task CheckCapacityAsync(string tourId, int requestedPersonCount);
    }
}
