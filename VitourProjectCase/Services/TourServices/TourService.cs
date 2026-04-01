using AutoMapper;
using MongoDB.Driver;
using VitourProjectCase.Dtos.TourDtos;
using VitourProjectCase.Entities;
using VitourProjectCase.Settings;

namespace VitourProjectCase.Services.TourServices
{
    public class TourService : ITourService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Tour> _tourCollection;
        private readonly IMongoCollection<Review> _reviewCollection;
        private readonly IMongoCollection<Gallery> _galleryCollection;
        private readonly IMongoCollection<Category> _categoryCollection;

        public TourService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _tourCollection = database.GetCollection<Tour>(_databaseSettings.TourCollectionName);
            _reviewCollection = database.GetCollection<Review>(_databaseSettings.ReviewCollectionName);
            _galleryCollection = database.GetCollection<Gallery>(_databaseSettings.GalleryCollectionName);
            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task CreateTourAsync(CreateTourDto createTourDto)
        {
            var value = _mapper.Map<Tour>(createTourDto);
            await _tourCollection.InsertOneAsync(value);
        }

        public async Task DeleteTourAsync(string id)
        {
            await _tourCollection.DeleteOneAsync(x => x.TourId == id);
        }

        public async Task<List<ResultTourDto>> GetAllTourAsync()
        {
            var values = await _tourCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultTourDto>>(values);
        }

        public async Task<List<TourCardDto>> GetTourCardListAsync()
        {

            var tours = await _tourCollection.Find(x => true).ToListAsync();
            var tourDtos = _mapper.Map<List<TourCardDto>>(tours);

            foreach (var tour in tourDtos)
            {
                var reviews = await _reviewCollection.Find(x => x.TourId == tour.TourId).ToListAsync();
                var galleries = await _galleryCollection.Find(x => x.TourId == tour.TourId).ToListAsync();
                var category = await _categoryCollection.Find(x => x.CategoryId == tour.CategoryId).FirstOrDefaultAsync();
                tour.CategoryName = category?.CategoryName;
                tour.GalleryCount = galleries.Count;
                tour.ReviewCount = reviews.Count;
                tour.ReviewScore = reviews.Count == 0 ? 0 : reviews.Average(x => x.Score);
            }
            return tourDtos;
        }
        public async Task<(List<TourCardDto> Tours, int TotalCount)> GetPagedToursAsync(int page, int pageSize)
        {
            var totalCount = (int)await _tourCollection.CountDocumentsAsync(x => true);

            var tours = await _tourCollection.Find(x => true).Skip((page - 1) * pageSize).Limit(pageSize).ToListAsync();
            var tourDtos = _mapper.Map<List<TourCardDto>>(tours);

            foreach (var tour in tourDtos)
            {
                var review = await _reviewCollection.Find(x => x.TourId == tour.TourId).ToListAsync();
                var galleries = await _galleryCollection.Find(x => x.TourId == tour.TourId).ToListAsync();
                var category = await _categoryCollection.Find(x => x.CategoryId == tour.CategoryId).FirstOrDefaultAsync();
                tour.CategoryName = category?.CategoryName;
                tour.GalleryCount = galleries.Count;
                tour.ReviewCount = review.Count;
                tour.ReviewScore = review.Count == 0 ? 0 : review.Average(x => x.Score);
            }

            return (tourDtos, totalCount);
        }
        public async Task<GetTourByIdDto> GetTourByIdAsync(string id)
        {
            var value = await _tourCollection.Find(x => x.TourId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetTourByIdDto>(value);
        }

        public async Task UpdateTourAsync(UpdateTourDto updateTourDto)
        {
            var value = _mapper.Map<Tour>(updateTourDto);
            await _tourCollection.FindOneAndReplaceAsync(x => x.TourId == updateTourDto.TourId, value);
        }

        public async Task<TourDetailDto> GetTourDetailAsync(string id)
        {
            var tour = await _tourCollection.Find(x => x.TourId == id).FirstOrDefaultAsync();
            var tourDetailDto = _mapper.Map<TourDetailDto>(tour);

            var reviews = await _reviewCollection.Find(x => x.TourId == id).ToListAsync();

            tourDetailDto.ReviewCount = reviews.Count;
            tourDetailDto.ReviewScore = reviews.Count == 0 ? 0 : reviews.Average(x => x.Score);

            return tourDetailDto;
        }

        public async Task<List<ResultTourDto>> GetLast3TourAsync()
        {
            var values = await _tourCollection.Find(x => true).SortByDescending(x => x.CreatedDate).Limit(3).ToListAsync();
            return _mapper.Map<List<ResultTourDto>>(values);
        }

        public async Task<List<ResultTourDto>> GetAllTourWithCategoryAsync()
        {
            var tours = await _tourCollection.Find(x => true).ToListAsync();
            var categories = await _categoryCollection.Find(x => true).ToListAsync();

            var mappedTours = _mapper.Map<List<ResultTourDto>>(tours);
            foreach(var tour in mappedTours)
            {
                tour.CategoryName = categories.Where(x => x.CategoryId == tour.CategoryId).Select(c => c.CategoryName).FirstOrDefault();
            }
            return mappedTours;
        }
    }
}
