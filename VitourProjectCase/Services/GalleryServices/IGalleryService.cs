using VitourProjectCase.Dtos.GalleryDtos;

namespace VitourProjectCase.Services.GalleryServices
{
    public interface IGalleryService
    {
        Task<List<ResultGalleryDto>> GetAllImagesAsync();
        Task<List<ResultGalleryByTourIdDto>> GetAllImagesByTourIdAsync(string id);
        Task<GetGalleryByIdDto> GetImageByIdAsync(string id);
        Task CreateImageAsync(CreateGalleryDto createGalleryDto);
        Task UpdateImageAsync(UpdateGalleryDto updateGalleryDto);
        Task DeleteImageAsync(string id);
    }
}
