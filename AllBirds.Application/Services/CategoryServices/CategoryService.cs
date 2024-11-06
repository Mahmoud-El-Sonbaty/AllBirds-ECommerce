using AllBirds.Application.Contracts;
using AllBirds.DTOs.Shared;
using AllBirds.DTOs.CategoryDTOs;
using AllBirds.Models;
using AutoMapper;
using System.Text.Json;

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
                bool exist = (await categoryRepository.GetAllAsync()).Any(c => (c.NameEn == entity.NameEn) || (c.NameAr == entity.NameAr));
                if (exist)
                {
                    resultView.IsSuccess = false;
                    resultView.Data = null;
                    resultView.Msg = $"Category With Same Name ({entity.NameEn}) Already Exist";
                }
                else
                {
                    // modifications by sonbaty
                    if (entity.ParentCategoryId != 0)
                    {
                        Category? parentCategory = (await categoryRepository.GetAllAsync()).FirstOrDefault(c => c.Id == entity.ParentCategoryId);
                        if (parentCategory is not null)
                        {
                            entity.Level = parentCategory.Level + 1;
                        }
                        else
                        {
                            resultView.IsSuccess = false;
                            resultView.Data = null;
                            resultView.Msg = $"Parent Category ({entity.ParentCategoryId}) Doesn't Exist";
                            return resultView;
                        }
                    }
                    // end modifications
                    Category category = mapper.Map<Category>(entity);
                    Category successCategory = await categoryRepository.CreateAsync(category);
                    await categoryRepository.SaveChangesAsync();
                    CUCategoryDTO successCategoryDTO = mapper.Map<CUCategoryDTO>(successCategory);
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
                // modifications by sonbaty
                if (entity.ParentCategoryId != 0)
                {
                    Category? parentCategory = (await categoryRepository.GetAllAsync()).FirstOrDefault(c => c.Id == entity.ParentCategoryId);
                    if (parentCategory is not null)
                    {
                        entity.Level = parentCategory.Level + 1;
                    }
                    else
                    {
                        resultView.IsSuccess = false;
                        resultView.Data = null;
                        resultView.Msg = $"Parent Category ({entity.ParentCategoryId}) Doesn't Exist";
                        return resultView;
                    }
                }
                // end modifications
                Category category = mapper.Map<Category>(entity);
                Category successCategory = await categoryRepository.UpdateAsync(category);
                await categoryRepository.SaveChangesAsync();
                CUCategoryDTO successCategoryDTO = mapper.Map<CUCategoryDTO>(successCategory);
                resultView.IsSuccess = true;
                resultView.Data = successCategoryDTO;
                resultView.Msg = $"Category ({entity.NameEn}) updated Successfully";
                return resultView;
            }
            catch (Exception ex)
            {
                resultView.IsSuccess = false;
                resultView.Data = null;
                resultView.Msg = $"Error Happen While updating Category ({entity.NameEn})" + ex.Message;
                return resultView;
            }
        }

        public async Task<ResultView<List<GetAllCategoryDTO>>> GetAllAsync()
        {
            ResultView<List<GetAllCategoryDTO>> resultView = new();
            try
            {
                List<Category> allCats = (await categoryRepository.GetAllAsync()).Where(a => !a.IsDeleted).ToList();
                List<GetAllCategoryNestedDTO> result = new();
                List<Category> grandParents = allCats.Where(c => c.ParentCategoryId == 0).ToList();
                foreach (Category parent in grandParents)
                {
                    GetAllCategoryNestedDTO mappedObj = new();
                    mappedObj.Id = parent.Id;
                    mappedObj.NameAr = parent.NameAr;
                    mappedObj.NameEn = parent.NameEn;
                    mappedObj.Level = parent.Level;
                    mappedObj.IsParentCategory = parent.IsParentCategory;
                    mappedObj.ParentCategoryId = parent.ParentCategoryId;
                    //mappedObj.Children = parent.IsParentCategory ? test(allCats.Where(ch => ch.ParentCategoryId == parent.Id).ToList(), parent) : [];
                    mappedObj.Children = [];
                    if (parent.IsParentCategory)
                    {
                        mappedObj.Children = test(allCats, parent.Id);
                    }
                    result.Add(mappedObj);
                }
                var jsonResult = JsonSerializer.Serialize(result);

                Console.WriteLine(jsonResult);


                //================================================================================================
                List<Category> successCategorys = (await categoryRepository.GetAllAsync()).Where(a => !a.IsDeleted).ToList();
                List<GetAllCategoryDTO> successCategorySDTO = mapper.Map<List<GetAllCategoryDTO>>(successCategorys);
                resultView.IsSuccess=true;
                resultView.Data = successCategorySDTO;
                resultView.Msg = "All Categories Fetched Successfully";
            }
            catch (Exception ex)
            {
                resultView.IsSuccess = false;
                resultView.Data = null;
                resultView.Msg = $"Error Happened While Fetching All Categories {ex.Message}";
            }
            return resultView;
        }

        public async Task<ResultView<CUCategoryDTO>> GetOneAsync(int id)
        {
            ResultView<CUCategoryDTO> resultView = new();
            try
            {
                Category? category = (await categoryRepository.GetAllAsync()).FirstOrDefault(c => c.Id == id && !c.IsDeleted);
                if (category is not null)
                {
                    CUCategoryDTO successCategoryDTO = mapper.Map<CUCategoryDTO>(category);
                    resultView.IsSuccess = true;
                    resultView.Data = successCategoryDTO;
                    resultView.Msg = $"Category {category.NameEn} Is Found";
                    return resultView;
                }
                else
                {
                    resultView.IsSuccess = false;
                    resultView.Data = null;
                    resultView.Msg = $"category {id} is not found";
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

        //public async Task<ResultView<GetOneCategoryDTO>> HardDeleteAsync(int id)
        //{
        //    ResultView<GetOneCategoryDTO> resultView = new();
        //    try
        //    {
        //        Category? successCategory = (await categoryRepository.GetAllAsync()).FirstOrDefault(c => c.Id == id);
        //        if (successCategory is not null)
        //        {
        //            bool dependentCats = (await categoryRepository.GetAllAsync()).Any(c => c.ParentCategoryId == id);
        //            if (dependentCats)
        //            {
        //                resultView.IsSuccess = false;
        //                resultView.Data = null;
        //                resultView.Msg = $"category {successCategory.NameEn} cannot be hard deleted as there are categories that depend on it";
        //            }
        //            else
        //            {
        //                Category successCategory2 = await categoryRepository.DeleteAsync(successCategory);
        //                await categoryRepository.SaveChangesAsync();
        //                GetOneCategoryDTO successCategoryDTO = mapper.Map<GetOneCategoryDTO>(successCategory2);
        //                resultView.IsSuccess = true;
        //                resultView.Data = successCategoryDTO;
        //                resultView.Msg = $"category {successCategory.NameEn} is hard deleted successfully";
        //            }
        //        }
        //        else
        //        {
        //            resultView.IsSuccess = false;
        //            resultView.Data = null;
        //            resultView.Msg = $"category ({id}) is not found";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        resultView.IsSuccess = false;
        //        resultView.Data = null;
        //        resultView.Msg = $"Error Happen While find Category ({id}) {ex.Message}";
        //    }
        //    return resultView;
        //}

        public async Task<ResultView<GetOneCategoryDTO>> DeleteAsync(int id)
        {
            ResultView<GetOneCategoryDTO> resultView = new();
            try
            {
                //Category category = mapper.Map<Category>(entity);
                // GetOneCategoryDTO SuccessCategoryDTO = mapper.Map<GetOneCategoryDTO>(category);
                Category? successCategory = (await categoryRepository.GetAllAsync()).FirstOrDefault(c => c.Id == id);
                if (successCategory is not null)
                {
                    bool dependentCats = (await categoryRepository.GetAllAsync()).Any(c => c.ParentCategoryId == id);
                        if (dependentCats)
                        {
                            successCategory.IsDeleted = true;
                            resultView.Msg = $"Category {successCategory.NameEn} Was Soft Deleted As There Are Categories That Depend On It";
                        }
                        else
                        {
                            Category successCategory2 = await categoryRepository.DeleteAsync(successCategory);
                            resultView.Msg = $"Category {successCategory.NameEn} Was Hard Deleted Successfully";
                        }
                    await categoryRepository.SaveChangesAsync();
                    GetOneCategoryDTO successCategoryDTO = mapper.Map<GetOneCategoryDTO>(successCategory);
                    resultView.IsSuccess = true;
                    resultView.Data = successCategoryDTO;
                    return resultView;
                }
                else
                {
                    resultView.IsSuccess = false;
                    resultView.Data = null;
                    resultView.Msg = $"category {successCategory.NameEn} is not found";
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

        // services for api project
        //================================================================================================

        public List<GetAllCategoryNestedDTO> test(List<Category> allCats, int parentId)
        {
            List<GetAllCategoryNestedDTO> result = new();
            //List<Category> subChildren = children.Where(c => c.ParentCategoryId == parentId).ToList();
            foreach (Category child in allCats.Where(c => c.ParentCategoryId == parentId))
            {
                GetAllCategoryNestedDTO obj = new();
                obj.Id = child.Id;
                obj.NameAr = child.NameAr;
                obj.NameEn = child.NameEn;
                obj.Level = child.Level;
                obj.IsParentCategory = child.IsParentCategory;
                obj.ParentCategoryId = child.ParentCategoryId;
                obj.Children = [];
                if (child.IsParentCategory)
                {
                    obj.Children = test(allCats, child.Id);
                }
                result.Add(obj);
            }
            return result;
        }

        public async Task<ResultView<List<GetAllCategoryNestedDTO>>> GetAllAPI()
        {
            ResultView<List<GetAllCategoryNestedDTO>> resultView = new();
            try
            {
                List<Category> allCats = (await categoryRepository.GetAllAsync()).Where(a => !a.IsDeleted).ToList();
                List<GetAllCategoryNestedDTO> result = new();
                List<Category> grandParents = allCats.Where(c => c.ParentCategoryId == 0).ToList();
                foreach (Category parent in grandParents)
                {
                    GetAllCategoryNestedDTO mappedObj = new();
                    mappedObj.Id = parent.Id;
                    mappedObj.NameAr = parent.NameAr;
                    mappedObj.NameEn = parent.NameEn;
                    mappedObj.Level = parent.Level;
                    mappedObj.IsParentCategory = parent.IsParentCategory;
                    mappedObj.ParentCategoryId = parent.ParentCategoryId;
                    //mappedObj.Children = parent.IsParentCategory ? test(allCats.Where(ch => ch.ParentCategoryId == parent.Id).ToList(), parent) : [];
                    mappedObj.Children = [];
                    if (parent.IsParentCategory)
                    {
                        mappedObj.Children = test(allCats, parent.Id);
                    }
                    result.Add(mappedObj);
                }
                resultView.IsSuccess = true;
                resultView.Data = result;
                resultView.Msg = "All Categories Fetched Successfully";
            }
            catch (Exception ex)
            {
                resultView.IsSuccess = false;
                resultView.Data = null;
                resultView.Msg = $"Error Happen While Fetching Categories {ex.Message}";
            }
            return resultView;
            //string jsonResult = JsonSerializer.Serialize(result);
            //Console.WriteLine(jsonResult);
        }
    }
}
