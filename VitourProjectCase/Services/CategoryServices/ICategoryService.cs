using VitourProjectCase.Dtos.CategoryDtos;

namespace VitourProjectCase.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<List<ResultCategoryDto>> GetAllCategoryAsync();
        Task CreateCategoryAsync(CreateCategoryDto createCategoryDto);
        Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto);

        Task DeleteCategoryAsync(string id);
        Task BulkDeleteAsync(List<string> ids);
        Task<GetCategoryByIdDto> GetCategoryByIdAsync(string id);
    }
}
