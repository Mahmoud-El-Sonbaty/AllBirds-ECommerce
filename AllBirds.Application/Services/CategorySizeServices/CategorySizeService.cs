using AllBirds.Application.Contracts;
using AllBirds.DTOs.Shared;
using AllBirds.Models;
using AutoMapper;
using AllBirds.DTOs.CategorySizeDTOS;
namespace AllBirds.Application.Services.CategorySizeServices
{
    public class CategorySizeService : ICategorySizeService
    {
        private readonly ICategorySizeRepository categorySizeRepository;
        private readonly IMapper mapper;

        public CategorySizeService(ICategorySizeRepository _categorySizeRepository, IMapper _mapper)
        {
            categorySizeRepository = _categorySizeRepository;
            mapper = _mapper;
        }

        public async Task<ResultView<CreateOrUpdateCategorySizeDTO>> CreateAsync(CreateOrUpdateCategorySizeDTO entity)
        {
            ResultView<CreateOrUpdateCategorySizeDTO> resultView = new();
            try
            {
                bool exist = (await categorySizeRepository.GetAllAsync()).Any(c => (c.CategoryId == entity.CategoryId) && (c.SizeId == entity.SizeId));
                if (exist)
                {
                    resultView.IsSuccess = false;
                    resultView.Data = null;
                    resultView.Msg = $"Category ({entity.Id}) IS Already Found";
                }
                else
                {
                    CategorySize category = mapper.Map<CategorySize>(entity);
                    CategorySize successCategory = await categorySizeRepository.CreateAsync(category);
                    CreateOrUpdateCategorySizeDTO successCategoryDTO = mapper.Map<CreateOrUpdateCategorySizeDTO>(successCategory);
                    await categorySizeRepository.SaveChangesAsync();
                    resultView.IsSuccess = true;
                    resultView.Data = successCategoryDTO;
                    resultView.Msg = $"Category ({entity.Id}) Created Successfully";
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

        public async Task<ResultView<CreateOrUpdateCategorySizeDTO>> UpdateAsync(CreateOrUpdateCategorySizeDTO entity)
        {
            ResultView<CreateOrUpdateCategorySizeDTO> resultView = new();
            try
            {
                bool exist = (await categorySizeRepository.GetAllAsync()).Any(c => c.Id == entity.Id);
                if (exist == false)
                {
                    resultView.IsSuccess = false;
                    resultView.Data = null;
                    resultView.Msg = $"Category ({entity.Id}) IS Not Found";
                    return resultView;
                }
                bool sameCategoryProductExist = (await categorySizeRepository.GetAllAsync()).Any(ba => ba.CategoryId == entity.CategoryId && ba.SizeId == entity.SizeId);
                if (sameCategoryProductExist)
                {
                    resultView.IsSuccess = false;
                    resultView.Data = null;
                    resultView.Msg = $"Category ({entity.Id}) IS  Notfound Found";
                    return resultView;
                }
                CategorySize category = mapper.Map<CategorySize>(entity);
                CategorySize successCategory = await categorySizeRepository.UpdateAsync(category);
                CreateOrUpdateCategorySizeDTO successCategoryDTO = mapper.Map<CreateOrUpdateCategorySizeDTO>(successCategory);
                await categorySizeRepository.SaveChangesAsync();
                resultView.IsSuccess = true;
                resultView.Data = successCategoryDTO;
                resultView.Msg = $"Category ({entity.Id}) update Successfully";
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

        public async Task<List<GetAllCategorySizeDTO>> GetAllAsync()
        {
            ////import to show  IsDeleted==false only
            var successCategories = (await categorySizeRepository.GetAllAsync()).Where(a => !a.IsDeleted).ToList();
            List<GetAllCategorySizeDTO> successCategoriesDTO = mapper.Map<List<GetAllCategorySizeDTO>>(successCategories);
            return successCategoriesDTO;
        }

        public async Task<ResultView<GetOneCategorySizeDTO>> GetOneAsync(int id)
        {
            ResultView<GetOneCategorySizeDTO> resultView = new ResultView<GetOneCategorySizeDTO>();
            try
            {
                CategorySize? categorySize = (await categorySizeRepository.GetAllAsync()).FirstOrDefault(cs => cs.Id == id && !cs.IsDeleted);
                if (categorySize != null)
                {
                    GetOneCategorySizeDTO SuccessCategoryDTO = mapper.Map<GetOneCategorySizeDTO>(categorySize);
                    resultView.IsSuccess = true;
                    resultView.Data = SuccessCategoryDTO;
                    resultView.Msg = $"Category {categorySize.Id} Found";
                    return resultView;
                }
                else
                {
                    resultView.IsSuccess = false;
                    resultView.Data = null;
                    resultView.Msg = $"Category {categorySize.Id} Is Not Found";

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

        public async Task<ResultView<GetOneCategorySizeDTO>> HardDeleteAsync(GetOneCategorySizeDTO Entity)
        {
            ResultView<GetOneCategorySizeDTO> resultView = new ResultView<GetOneCategorySizeDTO>();
            try
            {
                CategorySize category = mapper.Map<CategorySize>(Entity); // guess the value of category id will be equal to what ?
                CategorySize? successCategory = (await categorySizeRepository.GetAllAsync()).FirstOrDefault(cs => cs.Id == category.Id);
                if (successCategory != null)
                {
                    CategorySize successCategory2 = await categorySizeRepository.DeleteAsync(successCategory);
                    GetOneCategorySizeDTO successCategoryDTO = mapper.Map<GetOneCategorySizeDTO>(successCategory2);
                    resultView.IsSuccess = true;
                    resultView.Data = successCategoryDTO;
                    resultView.Msg = $"Category {category.Id} Found";
                    await categorySizeRepository.SaveChangesAsync();
                    return resultView;
                }
                else
                {
                    resultView.IsSuccess = false;
                    resultView.Data = null;
                    resultView.Msg = $"Category {category.Id} Is Not Found";

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

        public async Task<ResultView<GetOneCategorySizeDTO>> SoftDeleteAsync(GetOneCategorySizeDTO entity)
        {
            ResultView<GetOneCategorySizeDTO> resultView = new();
            try
            {
                CategorySize category = mapper.Map<CategorySize>(entity); // guess the value of category id will be equal to what ?
                CategorySize? successCategory = (await categorySizeRepository.GetAllAsync()).FirstOrDefault(cs => cs.Id == category.Id && !cs.IsDeleted);
                if (successCategory != null)
                {
                    successCategory.IsDeleted = true;
                    GetOneCategorySizeDTO successCategoryDTO = mapper.Map<GetOneCategorySizeDTO>(successCategory);
                    resultView.IsSuccess = true;
                    resultView.Data = successCategoryDTO;
                    resultView.Msg = $"Category {category.Id} Is Found";
                    await categorySizeRepository.SaveChangesAsync();
                    return resultView;
                }
                else
                {
                    resultView.IsSuccess = false;
                    resultView.Data = null;
                    resultView.Msg = $"Category {category.Id} Is Not Found";
                    return resultView;
                }
            }
            catch (Exception ex)
            {
                resultView.IsSuccess = false;
                resultView.Data = null;
                resultView.Msg = $"Error Happen While Find Category " + ex.Message;
                return resultView;
            }
        }

        public Task<int> SaveChangesAsync()
        {
            return categorySizeRepository.SaveChangesAsync();
        }
    }
}
