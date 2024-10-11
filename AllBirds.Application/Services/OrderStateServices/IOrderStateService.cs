using AllBirds.DTOs.OrderStateDTOs;

namespace AllBirds.Application.Services.OrderStateServices
{
    public interface IOrderStateService
    {
        public Task<CUOrderStateDTO> CreateAsync(CUOrderStateDTO CUOrderStateDTO);
        public Task<CUOrderStateDTO> UpdateAsync(CUOrderStateDTO CUOrderStateDTO);
        public Task<CUOrderStateDTO> SoftDeleteAsync(int sizeId);
        public Task<CUOrderStateDTO> HardDeleteAsync(int sizeId);
        public Task<List<CUOrderStateDTO>> GetAllAsync();
        public Task<List<CUOrderStateDTO>> GetAllWithDeletedAsync();
        public Task<GetOrderStateDTO> GetByIdAsync(int sizeId);
    }
}
