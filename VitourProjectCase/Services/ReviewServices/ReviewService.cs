using AutoMapper;
using MongoDB.Driver;
using VitourProjectCase.Dtos.ReviewDtos;
using VitourProjectCase.Entities;
using VitourProjectCase.Settings;

namespace VitourProjectCase.Services.ReviewServices
{
    public class ReviewService : IReviewService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Review> _reviewCollection;
        public ReviewService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _reviewCollection = database.GetCollection<Review>(_databaseSettings.ReviewCollectionName);
            _mapper = mapper;
        }

        public async Task CreateReviewAsync(CreateReviewDto createReviewDto)
        {
            var value = _mapper.Map<Review>(createReviewDto);
            value.Score = (value.GuideScore + value.TransportationScore + value.AccommodationScore + value.ValueForMoneyScore) / 4.0;
            value.ReviewDate = DateTime.UtcNow;

            await _reviewCollection.InsertOneAsync(value);
        }

        public async Task DeleteReviewAsync(string id)
        {
            await _reviewCollection.DeleteOneAsync(x => x.ReviewId == id);
        }

        public async Task<List<ResultReviewDto>> GetAllReviewAsync()
        {
            var values = await _reviewCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultReviewDto>>(values);
        }

        public async Task<List<ResultReviewByTourIdDto>> GetAllReviewsByTourIdAsync(string id)
        {
            var value = await _reviewCollection.Find(x => x.TourId == id).ToListAsync();
            return _mapper.Map<List<ResultReviewByTourIdDto>>(value);
        }

        public async Task<GetReviewByIdDto> GetReviewByIdAsync(string id)
        {
            var value = await _reviewCollection.Find(x => x.ReviewId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetReviewByIdDto>(value);
        }

        public async Task<ReviewSummaryDto> GetReviewSummaryByTourIdAsync(string tourId)
        {
            var values = await _reviewCollection.Find(x => x.TourId == tourId).ToListAsync();

            double GetAverage(Func<Review, double> selector)
                => values.Any() ? values.Average(selector) : 0;

            return new ReviewSummaryDto
            {
                AverageScore = GetAverage(x => x.Score),
                GuideScore = GetAverage(x => x.GuideScore),
                TransportationScore = GetAverage(x => x.TransportationScore),
                AccommodationScore = GetAverage(x => x.AccommodationScore),
                ValueForMoneyScore = GetAverage(x => x.ValueForMoneyScore)
            };
        }

        public async Task UpdateReviewAsync(UpdateReviewDto updateReviewDto)
        {
            var value = _mapper.Map<Review>(updateReviewDto);
            await _reviewCollection.FindOneAndReplaceAsync(x => x.ReviewId == updateReviewDto.ReviewId, value);
        }
    }
}
