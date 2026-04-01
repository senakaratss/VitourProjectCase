using AutoMapper;
using MongoDB.Driver;
using VitourProjectCase.Dtos.GalleryDtos;
using VitourProjectCase.Entities;
using VitourProjectCase.Settings;

namespace VitourProjectCase.Services.GalleryServices
{
    public class GalleryService : IGalleryService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Gallery> _galleryCollection;

        public GalleryService(IMapper mapper,IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _galleryCollection = database.GetCollection<Gallery>(_databaseSettings.GalleryCollectionName);
            _mapper = mapper;
        }

        public async Task CreateImageAsync(CreateGalleryDto createGalleryDto)
        {
            var value = _mapper.Map<Gallery>(createGalleryDto);
            await _galleryCollection.InsertOneAsync(value);
        }

        public async Task DeleteImageAsync(string id)
        {
            await _galleryCollection.DeleteOneAsync(x => x.GalleryId == id);
        }

        public async Task<List<ResultGalleryDto>> GetAllImagesAsync()
        {
            var values = await _galleryCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultGalleryDto>>(values);
        }

        public async Task<List<ResultGalleryByTourIdDto>> GetAllImagesByTourIdAsync(string id)
        {
            var values = await _galleryCollection.Find(x => x.TourId == id).ToListAsync();
            return _mapper.Map<List<ResultGalleryByTourIdDto>>(values);
        }

        public async Task<GetGalleryByIdDto> GetImageByIdAsync(string id)
        {
            var value = await _galleryCollection.Find(x => x.GalleryId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetGalleryByIdDto>(value);
        }

        public async Task UpdateImageAsync(UpdateGalleryDto updateGalleryDto)
        {
            var value=_mapper.Map<Gallery>(updateGalleryDto);
            await _galleryCollection.FindOneAndReplaceAsync(x => x.GalleryId == updateGalleryDto.GalleryId, value);
        }
    }
}
