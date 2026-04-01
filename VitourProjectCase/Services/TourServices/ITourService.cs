using VitourProjectCase.Dtos.TourDtos;

namespace VitourProjectCase.Services.TourServices
{
    public interface ITourService
    {
        Task<List<ResultTourDto>> GetAllTourAsync();
        Task<List<ResultTourDto>> GetAllTourWithCategoryAsync();
        Task CreateTourAsync(CreateTourDto createTourDto);
        Task UpdateTourAsync(UpdateTourDto updateTourDto);
        Task DeleteTourAsync(string id);
        Task<GetTourByIdDto> GetTourByIdAsync(string id);
        Task<List<TourCardDto>> GetTourCardListAsync();
        Task<TourDetailDto> GetTourDetailAsync(string id);
        Task<List<ResultTourDto>> GetLast3TourAsync();
        Task<(List<TourCardDto> Tours,int TotalCount)> GetPagedToursAsync(int page,int pageSize);
    }
}
