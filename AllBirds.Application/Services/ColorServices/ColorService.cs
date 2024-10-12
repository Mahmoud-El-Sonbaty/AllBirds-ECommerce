using AllBirds.Application.Contracts;
using AllBirds.DTOs.ColorDTOs;
using AllBirds.Models;
using AutoMapper;

namespace AllBirds.Application.Services.ColorServices
{
    public class ColorService : IColorService
    {
        private readonly IColorRepository _colorRepository;
        private readonly IMapper _mapper;

        public ColorService(IColorRepository colorRepository, IMapper mapper)
        {
            _colorRepository = colorRepository;
            _mapper = mapper;
        }

        public async Task<CUColorDTO> CreateAsync(CUColorDTO cUColorDTO)
        {
            Color mappedColor = _mapper.Map<Color>(cUColorDTO);
            Color createdColor = await _colorRepository.CreateAsync(mappedColor);
            await _colorRepository.SaveChangesAsync();
            return _mapper.Map<CUColorDTO>(createdColor);
        }

        public async Task<CUColorDTO> UpdateAsync(CUColorDTO cUColorDTO)
        {
            Color? colorObj = (await _colorRepository.GetAllAsync()).FirstOrDefault(s => s.Id == cUColorDTO.Id && s.IsDeleted == false);
            if (colorObj is not null)
            {
                colorObj.Name = cUColorDTO.Name;
                colorObj.Code = cUColorDTO.Name;
                await _colorRepository.SaveChangesAsync();
                return _mapper.Map<CUColorDTO>(colorObj);
            }
            return null;
        }

        public async Task<CUColorDTO> SoftDeleteAsync(int colorId)
        {
            Color? colorObj = (await _colorRepository.GetAllAsync()).FirstOrDefault(s => s.Id == colorId && s.IsDeleted == false);
            if (colorObj is not null)
            {
                colorObj.IsDeleted = true;
                await _colorRepository.SaveChangesAsync();
                return _mapper.Map<CUColorDTO>(colorObj);
            }
            return null;
        }
        public async Task<CUColorDTO> HardDeleteAsync(int colorId) // will this throw tracking exception ??
        {
            Color? colorObj = (await _colorRepository.GetAllAsync()).FirstOrDefault(s => s.Id == colorId && s.IsDeleted == false);
            if (colorObj is not null)
            {
                Color deletedColor = await _colorRepository.DeleteAsync(colorObj);
                await _colorRepository.SaveChangesAsync();
                return _mapper.Map<CUColorDTO>(deletedColor);
            }
            return null;
        }

        public async Task<List<CUColorDTO>> GetAllAsync()
        {
            //List<Color> ColorsList = (await _ColorRepository.GetAllAsync()).Where(s => s.IsDeleted == false).ToList();
            // this is called collection expression
            List<Color> colorsList = [.. (await _colorRepository.GetAllAsync()).Where(s => !s.IsDeleted)];
            return _mapper.Map<List<CUColorDTO>>(colorsList);
        }

        public async Task<List<CUColorDTO>> GetAllWithDeletedAsync()
        {
            List<Color> colorsList = [.. (await _colorRepository.GetAllAsync())];
            return _mapper.Map<List<CUColorDTO>>(colorsList);
        }
        public async Task<GetColorDTO> GetByIdAsync(int colorId)
        {
            IQueryable<Color> ColorList = await _colorRepository.GetAllAsync();
            Color? ColorObj = ColorList.FirstOrDefault(s => s.Id == colorId && !s.IsDeleted); // bool operator
            if (ColorObj != null)
            {
                return _mapper.Map<GetColorDTO>(ColorObj);
            }
            return null;
        }
    }
}
