using AllBirds.DTOs.SizeDTOs;

namespace AllBirds.Application.Services.SizeServices
{
    public interface ISizeService
    {
        public Task<CUSizeDTO> CreateAsync(CUSizeDTO cUSizeDTO);
        public Task<CUSizeDTO> UpdateAsync(CUSizeDTO cUSizeDTO);
        public Task<CUSizeDTO> SoftDeleteAsync(int sizeId);
        public Task<CUSizeDTO> HardDeleteAsync(int sizeId);
        public Task<List<CUSizeDTO>> GetAllAsync();
        public Task<List<CUSizeDTO>> GetAllWithDeletedAsync();
        public Task<GetSizeDTO> GetByIdAsync(int sizeId);
    }
}
