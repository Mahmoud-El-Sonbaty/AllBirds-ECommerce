using AllBirds.Application.Contracts;
using AllBirds.DTOs.SpecificationDTOs;
using AllBirds.DTOs.ProductDTOs;
using AllBirds.DTOs.Shared;
using AllBirds.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Application.Services.SpecificationServices
{
    public class SpecificationService : ISpecificationService
    {
        private readonly ISpecificationRepository specificationRepository;
        private readonly IMapper mapper;

        public SpecificationService(ISpecificationRepository _specificationRepository, IMapper _mapper)
        {
            specificationRepository = _specificationRepository;
            mapper = _mapper;
        }

        public async Task<ResultView<CUSpecificationDTO>> CreateAsync(CUSpecificationDTO entity)
        {
            ResultView<CUSpecificationDTO> resultView = new();
            try
            {
                bool Exist = (await specificationRepository.GetAllAsync()).Any(c => c.Id == entity.Id || c.NameEn == entity.NameEn || c.NameAr == entity.NameAr);
                if (Exist)
                {
                    resultView.IsSuccess = false;
                    resultView.Data = null;
                    resultView.Msg = $"Specification With Same Name ({entity.NameEn}) Already Exist";
                }
                else
                {
                    Specification specification = mapper.Map<Specification>(entity);
                    Specification successSpecification = await specificationRepository.CreateAsync(specification);
                    CUSpecificationDTO successSpecificationDTO = mapper.Map<CUSpecificationDTO>(successSpecification);
                    await specificationRepository.SaveChangesAsync();
                    resultView.IsSuccess = true;
                    resultView.Data = successSpecificationDTO;
                    resultView.Msg = $"Specification ({entity.NameEn}) Created Successfully";
                }
            }
            catch (Exception ex)
            {

                resultView.IsSuccess = false;
                resultView.Data = null;
                resultView.Msg = $"Error Happen While Creating Specification ${ex.Message}";
            }
            return resultView;
        }

        public async Task<ResultView<CUSpecificationDTO>> UpdateAsync(CUSpecificationDTO entity)
        {
            ResultView<CUSpecificationDTO> resultView = new();
            try
            {
                bool exist = (await specificationRepository.GetAllAsync()).Any(c => c.Id == entity.Id);
                if (!exist)
                {
                    resultView.IsSuccess = false;
                    resultView.Data = null;
                    resultView.Msg = $"Specification ({entity.NameEn}) Doesn't Exist";
                    return resultView;
                }
                else if ((await specificationRepository.GetAllAsync()).Any(s => s.NameAr == entity.NameAr || s.NameEn == entity.NameEn))
                {
                    resultView.IsSuccess = false;
                    resultView.Data = null;
                    resultView.Msg = $"Specification With Same Name ({entity.NameEn} || {entity.NameAr}) Already Exist";
                    return resultView;
                }
                Specification specification = mapper.Map<Specification>(entity);
                Specification successSpecification = await specificationRepository.UpdateAsync(specification);
                await specificationRepository.SaveChangesAsync();
                CUSpecificationDTO successSpecificationDTO = mapper.Map<CUSpecificationDTO>(successSpecification);
                resultView.IsSuccess = true;
                resultView.Data = successSpecificationDTO;
                resultView.Msg = $"Specification ({entity.NameEn}) Updated Successfully";
                return resultView;
            }
            catch (Exception ex)
            {
                resultView.IsSuccess = false;
                resultView.Data = null;
                resultView.Msg = $"Error Happen While Updating Specification ${entity.NameEn} ${ex.Message}";
                return resultView;
            }
        }

        public async Task<ResultView<GetSpecificationDTO>> SoftDeleteAsync(int id)
        {
            ResultView<GetSpecificationDTO> resultView = new ResultView<GetSpecificationDTO>();
            try
            {
                Specification? specExist = (await specificationRepository.GetAllAsync()).FirstOrDefault(c => c.Id == id);
                if (specExist is not null)
                {
                    specExist.IsDeleted = true;
                    await specificationRepository.SaveChangesAsync();
                    GetSpecificationDTO getSpecDTO = mapper.Map<GetSpecificationDTO>(specExist);
                    resultView.IsSuccess = true;
                    resultView.Data = getSpecDTO;
                    resultView.Msg = $"Specification {getSpecDTO.NameEn} Deleted Successfully";
                    return resultView;
                }
                else
                {
                    resultView.IsSuccess = false;
                    resultView.Data = null;
                    resultView.Msg = $"Specification {specExist?.NameEn} Not Found";
                    return resultView;
                }
            }
            catch (Exception ex)
            {
                resultView.IsSuccess = false;
                resultView.Data = null;
                resultView.Msg = $"Error Happen While Deleting Specification ${ex.Message}";
                return resultView;
            }
        }

        public async Task<ResultView<GetSpecificationDTO>> HardDeleteAsync(int id)
        {
            ResultView<GetSpecificationDTO> resultView = new();
            try
            {
                Specification? specExist = (await specificationRepository.GetAllAsync()).FirstOrDefault(c => c.Id == id);
                if (specExist is not null)
                {
                    Specification deletedSpec = await specificationRepository.DeleteAsync(specExist);
                    int saveStatus = await specificationRepository.SaveChangesAsync();
                    //if (saveStatus > 0)
                    //{
                        GetSpecificationDTO getSpecDTO = mapper.Map<GetSpecificationDTO>(deletedSpec);
                        resultView.IsSuccess = true;
                        resultView.Data = getSpecDTO;
                        resultView.Msg = $"Specification {getSpecDTO.NameEn} Hard Deleted Successfully";
                        return resultView;
                    //}
                    //else
                    //{
                        //resultView.IsSuccess = false;
                        //resultView.Data = null;
                        //resultView.Msg = $"Specification {specExist?.NameEn} Hard Deletion Not Saved Successfully";
                        //return resultView;
                    //}
                }
                else
                {
                    resultView.IsSuccess = false;
                    resultView.Data = null;
                    resultView.Msg = $"Specification {specExist?.NameEn} Not Found";
                    return resultView;
                }
            }
            catch (Exception ex)
            {
                resultView.IsSuccess = false;
                resultView.Data = null;
                resultView.Msg = $"Error Happen While Hard Deleting Specification ${ex.Message}";
                return resultView;
            }
        }

        public async Task<ResultView<List<CUSpecificationDTO>>> GetAllAsync()
        {
            ResultView<List<CUSpecificationDTO>> resultView = new();
            try
            {
                List<Specification> specsList = (await specificationRepository.GetAllAsync()).Where(a => !a.IsDeleted).ToList();
                if (specsList is not null)
                {
                    List<CUSpecificationDTO> getSpecsDTO = mapper.Map<List<CUSpecificationDTO>>(specsList);
                    resultView.IsSuccess = true;
                    resultView.Data = getSpecsDTO;
                    resultView.Msg = "All Specifications Fetched Successfully";
                    return resultView;
                }
                else
                {
                    resultView.IsSuccess = false;
                    resultView.Data = null;
                    resultView.Msg = "Specifications Not Found";
                    return resultView;
                }
            }
            catch (Exception ex)
            {
                resultView.IsSuccess = false;
                resultView.Data = null;
                resultView.Msg = $"Error Happen While Fetching Specifications ${ex.Message}";
                return resultView;
            }
        }

        public async Task<ResultView<GetSpecificationDTO>> GetByIdAsync(int id)
        {
            ResultView<GetSpecificationDTO> resultView = new();
            try
            {
                Specification? specification = (await specificationRepository.GetAllAsync()).FirstOrDefault(c => c.Id == id && !c.IsDeleted);
                if (specification is not null)
                {
                    GetSpecificationDTO getSpecDTO = mapper.Map<GetSpecificationDTO>(specification);
                    resultView.IsSuccess = true;
                    resultView.Data = getSpecDTO;
                    resultView.Msg = $"Specification {specification.NameEn} Fetched Successfully";
                    return resultView;
                }
                else
                {
                    resultView.IsSuccess = false;
                    resultView.Data = null;
                    resultView.Msg = $"Specification {specification?.NameEn} Not Found";
                    return resultView;
                }
            }
            catch (Exception ex)
            {
                resultView.IsSuccess = false;
                resultView.Data = null;
                resultView.Msg = $"Error Happen While Fetching Requsted Specification ${ex.Message}";
                return resultView;
            }
        }
        
        public async Task<int> SaveChangesAsync()
        {
            return await specificationRepository.SaveChangesAsync();
        }
    }
}
