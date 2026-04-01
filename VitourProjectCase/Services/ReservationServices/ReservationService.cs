using AutoMapper;
using MongoDB.Driver;
using VitourProjectCase.Dtos.ReservationDtos;
using VitourProjectCase.Entities;
using VitourProjectCase.Services.EmailServices;
using VitourProjectCase.Settings;

namespace VitourProjectCase.Services.ReservationServices
{
    public class ReservationService : IReservationService
    {
        private readonly IMongoCollection<Reservation> _reservationCollection;
        private readonly IMongoCollection<Tour> _tourCollection;
        private readonly IMapper _mapper;
        private readonly IReservationValidator _reservationValidator;
        private readonly IEmailService _emailService;

        public ReservationService(IMapper mapper, IDatabaseSettings _databaseSettings, IReservationValidator reservationValidator, IEmailService emailService)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _reservationCollection = database.GetCollection<Reservation>(_databaseSettings.ReservationCollectionName);
            _tourCollection = database.GetCollection<Tour>(_databaseSettings.TourCollectionName);
            _mapper = mapper;
            _reservationValidator = reservationValidator;
            _emailService = emailService;
        }

        public async Task<string> CreateReservationAsync(CreateReservationDto createReservationDto)
        {
            await _reservationValidator.CheckCapacityAsync(createReservationDto.TourId, createReservationDto.PersonCount);

            var tour = await _tourCollection.Find(x => x.TourId == createReservationDto.TourId).FirstOrDefaultAsync();

            createReservationDto.TotalPrice = tour.Price * createReservationDto.PersonCount;
            createReservationDto.ReservationDate= DateTime.UtcNow;
            createReservationDto.ReservationCode=Guid.NewGuid().ToString().Substring(0, 8).ToUpper();

            var value = _mapper.Map<Reservation>(createReservationDto);
            await _reservationCollection.InsertOneAsync(value);

            await _emailService.SendConfirmReservationEmailAsync(createReservationDto);

            return createReservationDto.ReservationCode;
        }

        public async Task DeleteReservationAsync(string id)
        {
            await _reservationCollection.DeleteOneAsync(x => x.ReservationId == id);
        }

        public async Task<List<ResultReservationDto>> GetAllReservationAsync()
        {
            var values = await _reservationCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultReservationDto>>(values);
        }

        public async Task<GetReservationByIdDto> GetReservationByCodeAsync(string code)
        {
            var value = await _reservationCollection.Find(x => x.ReservationCode == code).FirstOrDefaultAsync();
            return _mapper.Map<GetReservationByIdDto>(value);
        }

        public async Task<GetReservationByIdDto> GetReservationByIdAsync(string id)
        {
            var value = await _reservationCollection.Find(x => x.ReservationId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetReservationByIdDto>(value);
        }

        public async Task<List<ResultReservationByTourIdDto>> GetReservationsByTourIdAsync(string id)
        {
            var values=await _reservationCollection.Find(x=>x.TourId==id).ToListAsync();
            return _mapper.Map<List<ResultReservationByTourIdDto>>(values);
        }

        public async Task UpdateReservationAsync(UpdateReservationDto updateReservationDto)
        {
            var value = _mapper.Map<Reservation>(updateReservationDto);
            await _reservationCollection.FindOneAndReplaceAsync(x => x.ReservationId == updateReservationDto.ReservationId, value);
        }
    }
}
