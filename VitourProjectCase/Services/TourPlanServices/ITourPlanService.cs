using VitourProjectCase.Dtos.TourPlanDtos;

namespace VitourProjectCase.Services.TourPlanServices
{
    public interface ITourPlanService
    {
        Task<List<ResultTourPlanDto>> GetAllTourPlanAsync();
        Task CreateTourPlanAsync(CreateTourPlanDto createTourPlanDto);
        Task UpdateTourPlanAsync(UpdateTourPlanDto updateTourPlanDto);
        Task DeleteTourPlanAsync(string id);
        Task<GetTourPlanByIdDto> GetTourPlanByIdAsync(string id);
        Task<List<ResultTourPlanByTourIdDto>> GetAllTourPlanByTourIdAsync(string id);
    }
}
