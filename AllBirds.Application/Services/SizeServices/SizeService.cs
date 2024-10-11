using AllBirds.Application.Contracts;
using AllBirds.DTOs.SizeDTOs;
using AllBirds.Models;
using AutoMapper;

namespace AllBirds.Application.Services.SizeServices
{
    public class SizeService : ISizeService
    {
        private readonly ISizeRepository _sizeRepository;
        private readonly IMapper _mapper;

        public SizeService(ISizeRepository sizeRepository, IMapper mapper)
        {
            _sizeRepository = sizeRepository;
            _mapper = mapper;
        }

        public async Task<CUSizeDTO> CreateAsync(CUSizeDTO cUSizeDTO)
        {
            Size mappedSize = _mapper.Map<Size>(cUSizeDTO);
            Size createdSize = await _sizeRepository.CreateAsync(mappedSize);
            await _sizeRepository.SaveChangesAsync();
            return _mapper.Map<CUSizeDTO>(createdSize);
        }

        public async Task<CUSizeDTO> UpdateAsync(CUSizeDTO cUSizeDTO)
        {
            Size? sizeObj = (await _sizeRepository.GetAllAsync()).FirstOrDefault(s => s.Id == cUSizeDTO.Id && s.IsDeleted == false);
            if (sizeObj is not null)
            {
                sizeObj.SizeNumber = cUSizeDTO.SizeNumber;
                await _sizeRepository.SaveChangesAsync();
                return _mapper.Map<CUSizeDTO>(sizeObj);
            }
            return null;
        }

        public async Task<CUSizeDTO> SoftDeleteAsync(int sizeId)
        {
            Size? sizeObj = (await _sizeRepository.GetAllAsync()).FirstOrDefault(s => s.Id == sizeId && s.IsDeleted == false);
            if (sizeObj is not null)
            {
                sizeObj.IsDeleted = true;
                await _sizeRepository.SaveChangesAsync();
                return _mapper.Map<CUSizeDTO>(sizeObj);
            }
            return null;
        }
        public async Task<CUSizeDTO> HardDeleteAsync(int sizeId) // will this throw tracking exception ??
        {
            Size? sizeObj = (await _sizeRepository.GetAllAsync()).FirstOrDefault(s => s.Id == sizeId && s.IsDeleted == false);
            if (sizeObj is not null)
            {
                Size deletedSize = await _sizeRepository.DeleteAsync(sizeObj);
                await _sizeRepository.SaveChangesAsync();
                return _mapper.Map<CUSizeDTO>(deletedSize);
            }
            return null;
        }

        public async Task<List<CUSizeDTO>> GetAllAsync()
        {
            //List<Size> sizesList = (await _sizeRepository.GetAllAsync()).Where(s => s.IsDeleted == false).ToList();
            // this is called collection expression
            List<Size> sizesList = [.. (await _sizeRepository.GetAllAsync()).Where(s => !s.IsDeleted)];
            return _mapper.Map<List<CUSizeDTO>>(sizesList);
        }

        public async Task<List<CUSizeDTO>> GetAllWithDeletedAsync()
        {
            List<Size> sizesList = [.. (await _sizeRepository.GetAllAsync())];
            return _mapper.Map<List<CUSizeDTO>>(sizesList);
        }
        public async Task<GetSizeDTO> GetByIdAsync(int sizeId)
        {
            var sizeList = await _sizeRepository.GetAllAsync();
            var sizeObj = sizeList.FirstOrDefault(s => s.Id == sizeId && !s.IsDeleted); // bool operator

            if (sizeObj != null)
            {
                return _mapper.Map<GetSizeDTO>(sizeObj);
            }
            return null;
        }
    }
}
