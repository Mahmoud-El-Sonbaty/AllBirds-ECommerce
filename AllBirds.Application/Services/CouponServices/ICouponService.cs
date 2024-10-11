using AllBirds.DTOs.CouponDTOs;

namespace AllBirds.Application.Services.CouponServices
{
    public interface ICouponService
    {
        public Task<CUCouponDTO> CreateAsync(CUCouponDTO CUCouponDTO);
        public Task<CUCouponDTO> UpdateAsync(CUCouponDTO CUCouponDTO);
        public Task<CUCouponDTO> SoftDeleteAsync(int sizeId);
        public Task<CUCouponDTO> HardDeleteAsync(int sizeId);
        public Task<List<CUCouponDTO>> GetAllAsync();
        public Task<List<CUCouponDTO>> GetAllWithDeletedAsync();
        public Task<GetCouponDTO> GetByIdAsync(int sizeId);
    }
}
