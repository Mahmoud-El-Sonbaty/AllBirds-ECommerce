using AllBirds.Application.Contracts;
using AllBirds.Application.Services.CategoryServices;
using AllBirds.DTOs._ٍShared;
using AllBirds.DTOs.CategoryProductDTOS;

//using AllBirds.DTOs.CategoryDTOs;
using AllBirds.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Application.Services.CategoryProductServices
{
    public class CategoryProductService
   : ICategoryProductService
    {



        private ICategoryProductRepository CategoryProductRepository;
        private IMapper mapper;


        public CategoryProductService(ICategoryProductRepository _CategoryProductRepository, IMapper _mapper)
        {
            CategoryProductRepository = _CategoryProductRepository;
            mapper = _mapper;
        }

        public async Task<ResultView<CreateOrUpdateCategoryProductDTO>> CreateAsync(CreateOrUpdateCategoryProductDTO Entity)
        {
            ResultView<CreateOrUpdateCategoryProductDTO> resultView = new ResultView<CreateOrUpdateCategoryProductDTO>();
            try
            {
                bool Exist = (await CategoryProductRepository.GetAllAsync()).Any(c => (c.CategoryId == Entity.CategoryId) && (c.ProductId == Entity.ProductId));
                if (Exist)
                {
                    resultView.IsSuccess = false;
                    resultView.Entity = null;
                    resultView.Msg = $"Category ({Entity.Id}) IS  Already Found";

                }
                else
                {



                    CategoryProduct category = mapper.Map<CategoryProduct>(Entity);


                    CategoryProduct SuccessCategory = await CategoryProductRepository.CreateAsync(category);
                    CreateOrUpdateCategoryProductDTO SuccessCategoryDTO = mapper.Map<CreateOrUpdateCategoryProductDTO>(SuccessCategory);
                    CategoryProductRepository.SaveChangesAsync();
                    resultView.IsSuccess = true;
                    resultView.Entity = SuccessCategoryDTO;
                    resultView.Msg = $"Category ({Entity.Id}) Created Successfully";




                }
            }
            catch (Exception ex)
            {

                resultView.IsSuccess = false;
                resultView.Entity = null;
                resultView.Msg = $"Error Happen While Creating Category " + ex.Message;

            }


            return resultView;
        }

        public async Task<ResultView<CreateOrUpdateCategoryProductDTO>> UpdateAsync(CreateOrUpdateCategoryProductDTO Entity)
        {
            ResultView<CreateOrUpdateCategoryProductDTO> resultView = new ResultView<CreateOrUpdateCategoryProductDTO>();
            try
            {
                bool Exist = (await CategoryProductRepository.GetAllAsync()).Any(c => c.Id == Entity.Id);
                if (Exist==false)
                {
                    resultView.IsSuccess = false;
                    resultView.Entity = null;
                    resultView.Msg = $"Category ({Entity.Id}) IS  Notfound Found";
                    return resultView;
                }

                bool sameCategoryProductExist = (await CategoryProductRepository.GetAllAsync()).Any(ba => ba.CategoryId == Entity.CategoryId && ba.ProductId == Entity.ProductId);
                if(sameCategoryProductExist)
                {
                    resultView.IsSuccess = false;
                    resultView.Entity = null;
                    resultView.Msg = $"Category ({Entity.Id}) IS  Notfound Found";
                    return resultView;
                }

                CategoryProduct category = mapper.Map<CategoryProduct>(Entity);
                CategoryProduct SuccessCategory = await CategoryProductRepository.UpdateAsync(category);

                CreateOrUpdateCategoryProductDTO SuccessCategoryDTO = mapper.Map<CreateOrUpdateCategoryProductDTO>(SuccessCategory);
                CategoryProductRepository.SaveChangesAsync();
                resultView.IsSuccess = true;
                resultView.Entity = SuccessCategoryDTO;
                resultView.Msg = $"Category ({Entity.Id}) update Successfully";
                return resultView;
            }
            catch (Exception ex)
            {

                resultView.IsSuccess = false;
                resultView.Entity = null;
                resultView.Msg = $"Error Happen While update Category " + ex.Message;
                return resultView;
            }



        }

        public async Task<List<GetAllCategoryProductDTO>> GetAllAsync()
        {
            ////import to show  IsDeleted==false only
            var SuccessCategorys = (await CategoryProductRepository.GetAllAsync())
                .Select(a => a.IsDeleted == false).ToList();



            List<GetAllCategoryProductDTO> SuccessCategorySDTO = mapper.Map<List<GetAllCategoryProductDTO>>(SuccessCategorys);

            return SuccessCategorySDTO;
        }

        public async Task<ResultView<GetOneCategoryProductDTO>> GetOneAsync(int id)
        {
            ResultView<GetOneCategoryProductDTO> resultView = new ResultView<GetOneCategoryProductDTO>();
            try
            {
                CategoryProduct category = await CategoryProductRepository.GetOneAsync(id);
                if (category != null && category.IsDeleted == false)
                {
                    GetOneCategoryProductDTO SuccessCategoryDTO = mapper.Map<GetOneCategoryProductDTO>(category);
                    resultView.IsSuccess = true;
                    resultView.Entity = SuccessCategoryDTO;
                    resultView.Msg = $"category {category.Id}is found";

                    return resultView;
                }
                else
                {
                    resultView.IsSuccess = false;
                    resultView.Entity = null;
                    resultView.Msg = $"category {category.Id}is not found";

                    return resultView;
                }
            }
            catch (Exception ex)
            {
                resultView.IsSuccess = false;
                resultView.Entity = null;
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

                CategoryProduct SuccessCategory = await CategoryProductRepository.GetOneAsync(category.Id);



                if (SuccessCategory != null)
                {

                    CategoryProduct SuccessCategory2 = await CategoryProductRepository.DeleteAsync(SuccessCategory);

                    GetOneCategoryProductDTO SuccessCategoryDTO = mapper.Map<GetOneCategoryProductDTO>(SuccessCategory2);

                    resultView.IsSuccess = true;
                    resultView.Entity = SuccessCategoryDTO;
                    resultView.Msg = $"category {category.Id}is found";
                    CategoryProductRepository.SaveChangesAsync();
                    return resultView;

                }
                else
                {
                    resultView.IsSuccess = false;
                    resultView.Entity = null;
                    resultView.Msg = $"category {category.Id}is not found";

                    return resultView;
                }
            }
            catch (Exception ex)
            {
                resultView.IsSuccess = false;
                resultView.Entity = null;
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

                CategoryProduct SuccessCategory = await CategoryProductRepository.GetOneAsync(category.Id);



                if (SuccessCategory != null)
                {
                    SuccessCategory.IsDeleted = true;
                    //Category SuccessCategory2 = await categoryRepository.DeleteAsync(SuccessCategory);

                    GetOneCategoryProductDTO SuccessCategoryDTO = mapper.Map<GetOneCategoryProductDTO>(SuccessCategory);

                    resultView.IsSuccess = true;
                    resultView.Entity = SuccessCategoryDTO;
                    resultView.Msg = $"category {category.Id}is found";
                    // categoryRepository.SaveChangesAsync();
                    return resultView;

                }
                else
                {
                    resultView.IsSuccess = false;
                    resultView.Entity = null;
                    resultView.Msg = $"category {category.Id}is not found";

                    return resultView;
                }
            }
            catch (Exception ex)
            {
                resultView.IsSuccess = false;
                resultView.Entity = null;
                resultView.Msg = $"Error Happen While find Category " + ex.Message;

                return resultView;
            }





        }
        public Task<int> SaveChangesAsync()
        {
            return CategoryProductRepository.SaveChangesAsync();
        }


    }
}
