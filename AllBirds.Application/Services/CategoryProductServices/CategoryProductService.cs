using AllBirds.Application.Contracts;
using AllBirds.Application.Services.CategoryServices;
using AllBirds.DTOs.Shared;
using AllBirds.DTOs.CategoryProductDTOS;
using AllBirds.Models;
using AutoMapper;

namespace AllBirds.Application.Services.CategoryProductServices
{
    public class CategoryProductService : ICategoryProductService
    {
        private readonly ICategoryProductRepository categoryProductRepository;
        private readonly IMapper mapper;

        public CategoryProductService(ICategoryProductRepository _categoryProductRepository, IMapper _mapper)
        {
            categoryProductRepository = _categoryProductRepository;
            mapper = _mapper;
        }

        public async Task<ResultView<CreateOrUpdateCategoryProductDTO>> CreateAsync(CreateOrUpdateCategoryProductDTO Entity)
        {
            ResultView<CreateOrUpdateCategoryProductDTO> resultView = new ResultView<CreateOrUpdateCategoryProductDTO>();
            try
            {
                bool Exist = (await categoryProductRepository.GetAllAsync()).Any(c => (c.CategoryId == Entity.CategoryId) && (c.ProductId == Entity.ProductId));
                if (Exist)
                {
                    resultView.IsSuccess = false;
                    resultView.Data = null;
                    resultView.Msg = $"Category ({Entity.Id}) IS  Already Found";

                }
                else
                {



                    CategoryProduct category = mapper.Map<CategoryProduct>(Entity);


                    CategoryProduct SuccessCategory = await categoryProductRepository.CreateAsync(category);
                    CreateOrUpdateCategoryProductDTO SuccessCategoryDTO = mapper.Map<CreateOrUpdateCategoryProductDTO>(SuccessCategory);
                    categoryProductRepository.SaveChangesAsync();
                    resultView.IsSuccess = true;
                    resultView.Data = SuccessCategoryDTO;
                    resultView.Msg = $"Category ({Entity.Id}) Created Successfully";




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

        public async Task<ResultView<CreateOrUpdateCategoryProductDTO>> UpdateAsync(CreateOrUpdateCategoryProductDTO Entity)
        {
            ResultView<CreateOrUpdateCategoryProductDTO> resultView = new ResultView<CreateOrUpdateCategoryProductDTO>();
            try
            {
                bool Exist = (await categoryProductRepository.GetAllAsync()).Any(c => c.Id == Entity.Id);
                if (Exist==false)
                {
                    resultView.IsSuccess = false;
                    resultView.Data = null;
                    resultView.Msg = $"Category ({Entity.Id}) IS  Notfound Found";
                    return resultView;
                }

                bool sameCategoryProductExist = (await categoryProductRepository.GetAllAsync()).Any(ba => ba.CategoryId == Entity.CategoryId && ba.ProductId == Entity.ProductId);
                if(sameCategoryProductExist)
                {
                    resultView.IsSuccess = false;
                    resultView.Data = null;
                    resultView.Msg = $"Category ({Entity.Id}) IS  Notfound Found";
                    return resultView;
                }

                CategoryProduct category = mapper.Map<CategoryProduct>(Entity);
                CategoryProduct SuccessCategory = await categoryProductRepository.UpdateAsync(category);

                CreateOrUpdateCategoryProductDTO SuccessCategoryDTO = mapper.Map<CreateOrUpdateCategoryProductDTO>(SuccessCategory);
                categoryProductRepository.SaveChangesAsync();
                resultView.IsSuccess = true;
                resultView.Data = SuccessCategoryDTO;
                resultView.Msg = $"Category ({Entity.Id}) update Successfully";
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

        public async Task<List<GetAllCategoryProductDTO>> GetAllAsync()
        {
            ////import to show  IsDeleted==false only
            var SuccessCategorys = (await categoryProductRepository.GetAllAsync())
                .Select(a => a.IsDeleted == false).ToList();



            List<GetAllCategoryProductDTO> SuccessCategorySDTO = mapper.Map<List<GetAllCategoryProductDTO>>(SuccessCategorys);

            return SuccessCategorySDTO;
        }

        public async Task<ResultView<GetOneCategoryProductDTO>> GetOneAsync(int id)
        {
            ResultView<GetOneCategoryProductDTO> resultView = new ResultView<GetOneCategoryProductDTO>();
            try
            {
                CategoryProduct? category = (await categoryProductRepository.GetAllAsync()).FirstOrDefault(cp => cp.Id == id && !cp.IsDeleted);
                if (category != null)
                {
                    GetOneCategoryProductDTO SuccessCategoryDTO = mapper.Map<GetOneCategoryProductDTO>(category);
                    resultView.IsSuccess = true;
                    resultView.Data = SuccessCategoryDTO;
                    resultView.Msg = $"category {category.Id}is found";

                    return resultView;
                }
                else
                {
                    resultView.IsSuccess = false;
                    resultView.Data = null;
                    resultView.Msg = $"category {category.Id}is not found";

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

        public async Task<ResultView<GetOneCategoryProductDTO>> HeardDeleteAsync(GetOneCategoryProductDTO Entity)
        {
            ResultView<GetOneCategoryProductDTO> resultView = new ResultView<GetOneCategoryProductDTO>();


            try
            {
                CategoryProduct category = mapper.Map<CategoryProduct>(Entity);
                // GetOneCategoryDTO SuccessCategoryDTO = mapper.Map<GetOneCategoryDTO>(category);

                CategoryProduct SuccessCategory = (await categoryProductRepository.GetAllAsync()).FirstOrDefault(cp => cp.Id == category.Id);
                if (SuccessCategory != null)
                {

                    CategoryProduct SuccessCategory2 = await categoryProductRepository.DeleteAsync(SuccessCategory);

                    GetOneCategoryProductDTO SuccessCategoryDTO = mapper.Map<GetOneCategoryProductDTO>(SuccessCategory2);

                    resultView.IsSuccess = true;
                    resultView.Data = SuccessCategoryDTO;
                    resultView.Msg = $"category {category.Id}is found";
                    categoryProductRepository.SaveChangesAsync();
                    return resultView;

                }
                else
                {
                    resultView.IsSuccess = false;
                    resultView.Data = null;
                    resultView.Msg = $"category {category.Id}is not found";

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
        public async Task<ResultView<GetOneCategoryProductDTO>> SoftDeleteAsync(GetOneCategoryProductDTO Entity)
        {
            ResultView<GetOneCategoryProductDTO> resultView = new ResultView<GetOneCategoryProductDTO>();


            try
            {
                CategoryProduct category = mapper.Map<CategoryProduct>(Entity);
                // GetOneCategoryDTO SuccessCategoryDTO = mapper.Map<GetOneCategoryDTO>(category);

                CategoryProduct SuccessCategory = (await categoryProductRepository.GetAllAsync()).FirstOrDefault(cp => cp.Id == category.Id);

                if (SuccessCategory != null)
                {
                    SuccessCategory.IsDeleted = true;
                    //Category SuccessCategory2 = await categoryRepository.DeleteAsync(SuccessCategory);

                    GetOneCategoryProductDTO SuccessCategoryDTO = mapper.Map<GetOneCategoryProductDTO>(SuccessCategory);

                    resultView.IsSuccess = true;
                    resultView.Data = SuccessCategoryDTO;
                    resultView.Msg = $"category {category.Id}is found";
                    // categoryRepository.SaveChangesAsync();
                    return resultView;

                }
                else
                {
                    resultView.IsSuccess = false;
                    resultView.Data = null;
                    resultView.Msg = $"category {category.Id}is not found";

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
        //public Task<int> SaveChangesAsync()
        //{
        //    return categoryProductRepository.SaveChangesAsync();
        //}
    }
}
