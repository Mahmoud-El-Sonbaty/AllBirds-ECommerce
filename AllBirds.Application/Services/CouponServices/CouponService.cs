using AllBirds.Application.Contracts;
using AllBirds.DTOs.CouponDTOs;
using AllBirds.Models;
using AutoMapper;

namespace AllBirds.Application.Services.CouponServices
{
    public class CouponService : ICouponService
    {
        private readonly ICouponRepository _couponRepository;
        private readonly IMapper _mapper;

        public CouponService(ICouponRepository CouponRepository, IMapper mapper)
        {
            _couponRepository = CouponRepository;
            _mapper = mapper;
        }

        public async Task<CUCouponDTO> CreateAsync(CUCouponDTO cUCouponDTO)
        {
            Coupon mappedCoupon = _mapper.Map<Coupon>(cUCouponDTO);
            Coupon createdCoupon = await _couponRepository.CreateAsync(mappedCoupon);
            await _couponRepository.SaveChangesAsync();
            return _mapper.Map<CUCouponDTO>(createdCoupon);
        }

        public async Task<CUCouponDTO> UpdateAsync(CUCouponDTO cUCouponDTO)
        {
            Coupon? couponObj = (await _couponRepository.GetAllAsync()).FirstOrDefault(s => s.Id == cUCouponDTO.Id && s.IsDeleted == false);
            if (couponObj is not null)
            {
                couponObj.Code = cUCouponDTO.Code;
                couponObj.Discount = cUCouponDTO.Discount;
                couponObj.IsActive = cUCouponDTO.IsActive;
                await _couponRepository.SaveChangesAsync();
                return _mapper.Map<CUCouponDTO>(couponObj);
            }
            return null;
        }

        public async Task<CUCouponDTO> SoftDeleteAsync(int couponId)
        {
            Coupon? couponObj = (await _couponRepository.GetAllAsync()).FirstOrDefault(s => s.Id == couponId && s.IsDeleted == false);
            if (couponObj is not null)
            {
                couponObj.IsDeleted = true;
                await _couponRepository.SaveChangesAsync();
                return _mapper.Map<CUCouponDTO>(couponObj);
            }
            return null;
        }

        public async Task<CUCouponDTO> HardDeleteAsync(int couponId) // will this throw tracking exception ??
        {
            Coupon? couponObj = (await _couponRepository.GetAllAsync()).FirstOrDefault(s => s.Id == couponId && s.IsDeleted == false);
            if (couponObj is not null)
            {
                Coupon deletedCoupon = await _couponRepository.DeleteAsync(couponObj);
                await _couponRepository.SaveChangesAsync();
                return _mapper.Map<CUCouponDTO>(deletedCoupon);
            }
            return null;
        }

        public async Task<List<CUCouponDTO>> GetAllAsync()
        {
            //List<Coupon> couponsList = (await _couponRepository.GetAllAsync()).Where(s => s.IsDeleted == false).ToList();
            // this is called collection expression
            List<Coupon> couponsList = [.. (await _couponRepository.GetAllAsync()).Where(s => !s.IsDeleted)];
            return _mapper.Map<List<CUCouponDTO>>(couponsList);
        }

        public async Task<List<CUCouponDTO>> GetAllWithDeletedAsync()
        {
            List<Coupon> couponsList = [.. (await _couponRepository.GetAllAsync())];
            return _mapper.Map<List<CUCouponDTO>>(couponsList);
        }

        public async Task<GetCouponDTO> GetByIdAsync(int couponId)
        {
            IQueryable<Coupon> couponList = await _couponRepository.GetAllAsync();
            Coupon? couponObj = couponList.FirstOrDefault(s => s.Id == couponId && !s.IsDeleted); // bool operator
            if (couponObj != null)
            {
                return _mapper.Map<GetCouponDTO>(couponObj);
            }
            return null;
        }
    }
}
