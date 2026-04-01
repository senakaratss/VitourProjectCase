using AutoMapper;
using MongoDB.Driver;
using VitourProjectCase.Dtos.CategoryDtos;
using VitourProjectCase.Entities;
using VitourProjectCase.Settings;

namespace VitourProjectCase.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMongoCollection<Tour> _tourCollection;

        public CategoryService(IMapper mapper,IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);
            _tourCollection = database.GetCollection<Tour>(_databaseSettings.TourCollectionName);
            _mapper = mapper;
        }

        public async Task BulkDeleteAsync(List<string> ids)
        {
            var filter = Builders<Category>.Filter.In(x => x.CategoryId, ids);
            await _categoryCollection.DeleteManyAsync(filter);
        }

        public async Task CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            var value = _mapper.Map<Category>(createCategoryDto);
            await _categoryCollection.InsertOneAsync(value);
        }

        public async Task DeleteCategoryAsync(string id)
        {
            await _categoryCollection.DeleteOneAsync(x=>x.CategoryId==id);
        }

        public async Task<List<ResultCategoryDto>> GetAllCategoryAsync()
        {
            var values = await _categoryCollection.Find(x => true).ToListAsync();
            var tours = await _tourCollection.Find(x => true).ToListAsync();

            var tourCounts = tours.GroupBy(x => x.CategoryId).ToDictionary(t => t.Key, t => t.Count());

            var mapped= _mapper.Map<List<ResultCategoryDto>>(values);

            foreach(var item in mapped)
            {
                item.TourCount = tourCounts.ContainsKey(item.CategoryId) ? tourCounts[item.CategoryId] : 0;
            }
            return mapped;
        }

        public async Task<GetCategoryByIdDto> GetCategoryByIdAsync(string id)
        {
            var value = await _categoryCollection.Find(x => x.CategoryId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetCategoryByIdDto>(value);
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
        {
            var value = _mapper.Map<Category>(updateCategoryDto);
            await _categoryCollection.FindOneAndReplaceAsync(x => x.CategoryId == updateCategoryDto.CategoryId, value);
        }
    }
}
