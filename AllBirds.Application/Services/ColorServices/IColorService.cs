using AllBirds.DTOs.ColorDTOs;

namespace AllBirds.Application.Services.ColorServices
{
    public interface IColorService
    {
        public Task<CUColorDTO> CreateAsync(CUColorDTO CUColorDTO);
        public Task<CUColorDTO> UpdateAsync(CUColorDTO CUColorDTO);
        public Task<CUColorDTO> SoftDeleteAsync(int sizeId);
        public Task<CUColorDTO> HardDeleteAsync(int sizeId);
        public Task<List<CUColorDTO>> GetAllAsync();
        public Task<List<CUColorDTO>> GetAllWithDeletedAsync();
        public Task<GetColorDTO> GetByIdAsync(int sizeId);
    }
}
