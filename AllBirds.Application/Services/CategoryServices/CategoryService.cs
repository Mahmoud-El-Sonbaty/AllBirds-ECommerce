using AllBirds.Application.Contracts;
using AllBirds.DTOs.Shared;
using AllBirds.DTOs.CategoryDTOs;
using AllBirds.Models;
using AutoMapper;

namespace AllBirds.Application.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public CategoryService(ICategoryRepository _categoryRepository, IMapper _mapper)
        {
            categoryRepository = _categoryRepository;
            mapper = _mapper;
        }

        public async Task<ResultView<CUCategoryDTO>> CreateAsync(CUCategoryDTO entity)
        {
            ResultView<CUCategoryDTO> resultView = new();
            try
            {
                bool Exist = (await categoryRepository.GetAllAsync()).Any(c => (c.NameEn == entity.NameEn) || (c.NameAr == entity.NameAr));
                if (Exist)
                {
                    resultView.IsSuccess = false;
                    resultView.Data = null;
                    resultView.Msg = $"Category With Same Name ({entity.NameEn}) Already Exist";
                }
                else
                {
                    Category category = mapper.Map<Category>(entity);
                    Category successCategory = await categoryRepository.CreateAsync(category);
                    CUCategoryDTO successCategoryDTO = mapper.Map<CUCategoryDTO>(successCategory);
                    await categoryRepository.SaveChangesAsync();
                    resultView.IsSuccess = true;
                    resultView.Data = successCategoryDTO;
                    resultView.Msg = $"Category ({entity.NameEn}) Created Successfully";
                }
            }
            catch (Exception ex)
            {

                resultView.IsSuccess = false;
                resultView.Data = null;
                resultView.Msg = $"Error Happen While Creating Category " + ex.Message;
            }
            return resultView;
        }

        public async Task<ResultView<CUCategoryDTO>> UpdateAsync(CUCategoryDTO entity)
        {
            ResultView<CUCategoryDTO> resultView = new();
            try
            {
                bool exist = (await categoryRepository.GetAllAsync()).Any(c => c.Id == entity.Id);
                if (!exist)
                {
                    resultView.IsSuccess = false;
                    resultView.Data = null;
                    resultView.Msg = $"Category ({entity.NameEn}) IS Not Found";
                    return resultView;
                }
                Category category = mapper.Map<Category>(entity);
                Category successCategory = await categoryRepository.UpdateAsync(category);
                CUCategoryDTO successCategoryDTO = mapper.Map<CUCategoryDTO>(successCategory);
                await categoryRepository.SaveChangesAsync();
                resultView.IsSuccess = true;
                resultView.Data = successCategoryDTO;
                resultView.Msg = $"Category ({entity.NameEn}) update Successfully";
                return resultView;
            }
            catch (Exception ex)
            {
                resultView.IsSuccess = false;
                resultView.Data = null;
                resultView.Msg = $"Error Happen While update Category " + ex.Message;
                return resultView;
            }
        }

        public async Task<List<GetAllCategoryDTO>> GetAllAsync()
        {
            ////import to show  IsDeleted==false only
            var successCategorys = (await categoryRepository.GetAllAsync()).Where(a => !a.IsDeleted).ToList();
            List<GetAllCategoryDTO> successCategorySDTO = mapper.Map<List<GetAllCategoryDTO>>(successCategorys);
            return successCategorySDTO;
        }

        public async Task<ResultView<GetOneCategoryDTO>> GetOneAsync(int id)
        {
            ResultView<GetOneCategoryDTO> resultView = new ResultView<GetOneCategoryDTO>();
            try
            {
                Category? category = (await categoryRepository.GetAllAsync()).FirstOrDefault(c => c.Id == id && !c.IsDeleted);
                if (category is not null)
                {
                    GetOneCategoryDTO successCategoryDTO = mapper.Map<GetOneCategoryDTO>(category);
                    resultView.IsSuccess = true;
                    resultView.Data = successCategoryDTO;
                    resultView.Msg = $"Category {category.NameEn} Is Found";
                    return resultView;
                }
                else
                {
                    resultView.IsSuccess = false;
                    resultView.Data = null;
                    resultView.Msg = $"category {category.NameEn}is not found";
                    return resultView;
                }
            }
            catch (Exception ex)
            {
                resultView.IsSuccess = false;
                resultView.Data = null;
                resultView.Msg = $"Error Happen While find Category " + ex.Message;
                return resultView;
            }
        }

        public async Task<ResultView<GetOneCategoryDTO>> HardDeleteAsync(GetOneCategoryDTO entity)
        {
            ResultView<GetOneCategoryDTO> resultView = new ResultView<GetOneCategoryDTO>();
            try
            {
                Category category = mapper.Map<Category>(entity);
                // GetOneCategoryDTO successCategoryDTO = mapper.Map<GetOneCategoryDTO>(category);
                Category successCategory = (await categoryRepository.GetAllAsync()).FirstOrDefault(c => c.Id == category.Id);
                if (successCategory != null)
                {
                    Category successCategory2 = await categoryRepository.DeleteAsync(successCategory);
                    GetOneCategoryDTO successCategoryDTO = mapper.Map<GetOneCategoryDTO>(successCategory2);
                    resultView.IsSuccess = true;
                    resultView.Data = successCategoryDTO;
                    resultView.Msg = $"category {category.NameEn}is found";
                    await categoryRepository.SaveChangesAsync();
                    return resultView;
                }
                else
                {
                    resultView.IsSuccess = false;
                    resultView.Data = null;
                    resultView.Msg = $"category {category.NameEn}is not found";
                    return resultView;
                }
            }
            catch (Exception ex)
            {
                resultView.IsSuccess = false;
                resultView.Data = null;
                resultView.Msg = $"Error Happen While find Category " + ex.Message;
                return resultView;
            }
        }

        public async Task<ResultView<GetOneCategoryDTO>> SoftDeleteAsync(GetOneCategoryDTO entity)
        {
            ResultView<GetOneCategoryDTO> resultView = new ResultView<GetOneCategoryDTO>();
            try
            {
                Category category = mapper.Map<Category>(entity);
                // GetOneCategoryDTO SuccessCategoryDTO = mapper.Map<GetOneCategoryDTO>(category);
                Category successCategory = (await categoryRepository.GetAllAsync()).FirstOrDefault(c => c.Id == category.Id);
                if (successCategory != null)
                {
                    successCategory.IsDeleted = true;
                    //Category SuccessCategory2 = await categoryRepository.DeleteAsync(SuccessCategory);
                    GetOneCategoryDTO successCategoryDTO = mapper.Map<GetOneCategoryDTO>(successCategory);
                    resultView.IsSuccess = true;
                    resultView.Data = successCategoryDTO;
                    resultView.Msg = $"category {category.NameEn}is found";
                    // categoryRepository.SaveChangesAsync();
                    return resultView;
                }
                else
                {
                    resultView.IsSuccess = false;
                    resultView.Data = null;
                    resultView.Msg = $"category {category.NameEn}is not found";
                    return resultView;
                }
            }
            catch (Exception ex)
            {
                resultView.IsSuccess = false;
                resultView.Data = null;
                resultView.Msg = $"Error Happen While find Category " + ex.Message;
                return resultView;
            }
        }

        public Task<int> SaveChangesAsync()
        {
            return categoryRepository.SaveChangesAsync();
        }
    }
}
