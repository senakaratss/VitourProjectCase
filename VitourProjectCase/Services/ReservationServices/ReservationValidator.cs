
using MongoDB.Driver;
using VitourProjectCase.Entities;
using VitourProjectCase.Exceptions.ReservationExceptions;
using VitourProjectCase.Settings;

namespace VitourProjectCase.Services.ReservationServices
{
    public class ReservationValidator : IReservationValidator
    {
        private readonly IMongoCollection<Reservation> _reservationCollection;
        private readonly IMongoCollection<Tour> _tourCollection;
        public ReservationValidator(IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);

            _reservationCollection = database.GetCollection<Reservation>(_databaseSettings.ReservationCollectionName);
            _tourCollection = database.GetCollection<Tour>(_databaseSettings.TourCollectionName);
        }
        public async Task CheckCapacityAsync(string tourId, int requestedPersonCount)
        {
            var tour = await _tourCollection.Find(x => x.TourId == tourId).FirstOrDefaultAsync();
            if (tour == null)
            {
                throw new Exception("Tour bulunamadı");
            }
            var reservations = await _reservationCollection.Find(x => x.TourId == tourId).ToListAsync();
            var totalReserved = reservations.Sum(x => x.PersonCount);
            if (totalReserved + requestedPersonCount > tour.Capacity)
            {
                throw new CapacityExceededException("Bu tur için yeterli kapasite yok");
            }
        }
    }
}
