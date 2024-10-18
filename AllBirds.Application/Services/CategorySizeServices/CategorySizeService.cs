using AllBirds.Application.Contracts;
using AllBirds.Application.Services.CategoryProductServices;
using AllBirds.DTOs._ٍShared;
//using AllBirds.DTOs.CategoryProductDTOS;
using AllBirds.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllBirds.DTOs.CategorySizeDTOS;
namespace AllBirds.Application.Services.CategorySizeServices
{
    public class CategorySizeService

   : ICategorySizeService
    {



        private ICategorySizeRepository CategorySizeRepository;
        private IMapper mapper;


        public CategorySizeService(ICategorySizeRepository _CategorySizeRepository, IMapper _mapper)
        {
            CategorySizeRepository = _CategorySizeRepository;
            mapper = _mapper;
        }

        public async Task<ResultView<CreateOrUpdateCategorySizeDTO>> CreateAsync(CreateOrUpdateCategorySizeDTO Entity)
        {
            ResultView<CreateOrUpdateCategorySizeDTO> resultView = new ResultView<CreateOrUpdateCategorySizeDTO>();
            try
            {
                bool Exist = (await CategorySizeRepository.GetAllAsync()).Any(c => (c.CategoryId == Entity.CategoryId) && (c.SizeId == Entity.SizeId));
                if (Exist)
                {
                    resultView.IsSuccess = false;
                    resultView.Entity = null;
                    resultView.Msg = $"Category ({Entity.Id}) IS  Already Found";

                }
                else
                {



                    CategorySize category = mapper.Map<CategorySize>(Entity);


                    CategorySize SuccessCategory = await CategorySizeRepository.CreateAsync(category);
                    CreateOrUpdateCategorySizeDTO SuccessCategoryDTO = mapper.Map<CreateOrUpdateCategorySizeDTO>(SuccessCategory);
                    CategorySizeRepository.SaveChangesAsync();
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

        public async Task<ResultView<CreateOrUpdateCategorySizeDTO>> UpdateAsync(CreateOrUpdateCategorySizeDTO Entity)
        {
            ResultView<CreateOrUpdateCategorySizeDTO> resultView = new ResultView<CreateOrUpdateCategorySizeDTO>();
            try
            {
                bool Exist = (await CategorySizeRepository.GetAllAsync()).Any(c => c.Id == Entity.Id);
                if (Exist == false)
                {
                    resultView.IsSuccess = false;
                    resultView.Entity = null;
                    resultView.Msg = $"Category ({Entity.Id}) IS  Notfound Found";
                    return resultView;
                }

                bool sameCategoryProductExist = (await CategorySizeRepository.GetAllAsync()).Any(ba => ba.CategoryId == Entity.CategoryId && ba.SizeId == Entity.SizeId);
                if (sameCategoryProductExist)
                {
                    resultView.IsSuccess = false;
                    resultView.Entity = null;
                    resultView.Msg = $"Category ({Entity.Id}) IS  Notfound Found";
                    return resultView;
                }

                CategorySize category = mapper.Map<CategorySize>(Entity);
                CategorySize SuccessCategory = await CategorySizeRepository.UpdateAsync(category);

                CreateOrUpdateCategorySizeDTO SuccessCategoryDTO = mapper.Map<CreateOrUpdateCategorySizeDTO>(SuccessCategory);
                CategorySizeRepository.SaveChangesAsync();
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

        public async Task<List<GetAllCategorySizeDTO>> GetAllAsync()
        {
            ////import to show  IsDeleted==false only
            var SuccessCategorys = (await CategorySizeRepository.GetAllAsync())
                .Select(a => a.IsDeleted == false).ToList();



            List<GetAllCategorySizeDTO> SuccessCategorySDTO = mapper.Map<List<GetAllCategorySizeDTO>>(SuccessCategorys);

            return SuccessCategorySDTO;
        }

        public async Task<ResultView<GetOneCategorySizeDTO>> GetOneAsync(int id)
        {
            ResultView<GetOneCategorySizeDTO> resultView = new ResultView<GetOneCategorySizeDTO>();
            try
            {
                CategorySize category = await CategorySizeRepository.GetOneAsync(id);
                if (category != null && category.IsDeleted == false)
                {
                    GetOneCategorySizeDTO SuccessCategoryDTO = mapper.Map<GetOneCategorySizeDTO>(category);
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

        public async Task<ResultView<GetOneCategorySizeDTO>> HeardDeleteAsync(GetOneCategorySizeDTO Entity)
        {
            ResultView<GetOneCategorySizeDTO> resultView = new ResultView<GetOneCategorySizeDTO>();


            try
            {
                CategorySize category = mapper.Map<CategorySize>(Entity);
                // GetOneCategoryDTO SuccessCategoryDTO = mapper.Map<GetOneCategoryDTO>(category);

                CategorySize SuccessCategory = await CategorySizeRepository.GetOneAsync(category.Id);



                if (SuccessCategory != null)
                {

                    CategorySize SuccessCategory2 = await CategorySizeRepository.DeleteAsync(SuccessCategory);

                    GetOneCategorySizeDTO SuccessCategoryDTO = mapper.Map<GetOneCategorySizeDTO>(SuccessCategory2);

                    resultView.IsSuccess = true;
                    resultView.Entity = SuccessCategoryDTO;
                    resultView.Msg = $"category {category.Id}is found";
                    CategorySizeRepository.SaveChangesAsync();
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
        public async Task<ResultView<GetOneCategorySizeDTO>> SoftDeleteAsync(GetOneCategorySizeDTO Entity)
        {
            ResultView<GetOneCategorySizeDTO> resultView = new ResultView<GetOneCategorySizeDTO>();


            try
            {
                CategorySize category = mapper.Map<CategorySize>(Entity);
                // GetOneCategoryDTO SuccessCategoryDTO = mapper.Map<GetOneCategoryDTO>(category);

                CategorySize SuccessCategory = await CategorySizeRepository.GetOneAsync(category.Id);



                if (SuccessCategory != null)
                {
                    SuccessCategory.IsDeleted = true;
                    //Category SuccessCategory2 = await categoryRepository.DeleteAsync(SuccessCategory);

                    GetOneCategorySizeDTO SuccessCategoryDTO = mapper.Map<GetOneCategorySizeDTO>(SuccessCategory);

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
            return CategorySizeRepository.SaveChangesAsync();
        }


    }
}
