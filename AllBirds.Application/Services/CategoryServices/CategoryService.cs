using AllBirds.Application.Contracts;
using AllBirds.DTOs._ٍShared;
using AllBirds.DTOs.CategoryDTOs;
using AllBirds.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Application.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
       


        private ICategoryRepository categoryRepository;
        private IMapper mapper;


        public CategoryService(ICategoryRepository _categoryRepository, IMapper _mapper)
        {
            categoryRepository = _categoryRepository;
            mapper = _mapper;   
        }

        public async Task<ResultView <CreateOrUpdateCategoryDTO>> CreateAsync(CreateOrUpdateCategoryDTO Entity)
        {
            ResultView<CreateOrUpdateCategoryDTO> resultView = new ResultView<CreateOrUpdateCategoryDTO>();
            try
            {
                bool Exist = (await categoryRepository.GetAllAsync()).Any(c => (c.NameEn == Entity.NameEn) || (c.NameAr == Entity.NameAr));
                if (Exist)
                {
                    resultView.IsSuccess = false;
                    resultView.Entity = null;
                    resultView.Msg = $"Category ({Entity.NameEn}) IS  Already Found";

                }
                else
                {



                    Category category = mapper.Map<Category>(Entity);


                    Category SuccessCategory = await categoryRepository.CreateAsync(category);
                    CreateOrUpdateCategoryDTO SuccessCategoryDTO = mapper.Map<CreateOrUpdateCategoryDTO>(SuccessCategory);
                    categoryRepository.SaveChangesAsync();
                    resultView.IsSuccess = true;
                    resultView.Entity = SuccessCategoryDTO;
                    resultView.Msg = $"Category ({Entity.NameEn}) Created Successfully";
                  



                }
            }
            catch (Exception ex) {

                resultView.IsSuccess = false;
                resultView.Entity =null;
                resultView.Msg = $"Error Happen While Creating Category "+ ex.Message;
               
            }


            return resultView;
        }
      
        public async Task<ResultView<CreateOrUpdateCategoryDTO>> UpdateAsync(CreateOrUpdateCategoryDTO Entity)
        {
            ResultView<CreateOrUpdateCategoryDTO> resultView = new ResultView<CreateOrUpdateCategoryDTO>();
            try
            {
                bool Exist = (await categoryRepository.GetAllAsync()).Any(c =>c.Id == Entity.Id);
                if (!Exist)
                {
                    resultView.IsSuccess = false;
                    resultView.Entity = null;
                    resultView.Msg = $"Category ({Entity.NameEn}) IS  Notfound Found";
                    return resultView;
                }
                
                    Category category = mapper.Map<Category>(Entity);
                    Category SuccessCategory = await categoryRepository.UpdateAsync(category);
                   
                CreateOrUpdateCategoryDTO SuccessCategoryDTO = mapper.Map<CreateOrUpdateCategoryDTO>(SuccessCategory);
                    categoryRepository.SaveChangesAsync();
                    resultView.IsSuccess = true;
                    resultView.Entity = SuccessCategoryDTO;
                    resultView.Msg = $"Category ({Entity.NameEn}) update Successfully";
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

        public async Task<List<GetAllCategoryDTO>> GetAllAsync()
        {
            ////import to show  IsDeleted==false only
            var SuccessCategorys = (await categoryRepository.GetAllAsync())
                .Select(a=>a.IsDeleted==false).ToList();



          List<GetAllCategoryDTO>  SuccessCategorySDTO=mapper.Map<List<GetAllCategoryDTO>>(SuccessCategorys);

            return SuccessCategorySDTO;
        }

        public async Task<ResultView< GetOneCategoryDTO>> GetOneAsync(int id)
        {
            ResultView<GetOneCategoryDTO> resultView = new ResultView<GetOneCategoryDTO>();
            try {
                Category category = await categoryRepository.GetOneAsync(id);
                if (category != null&&category.IsDeleted==false)
                {
                    GetOneCategoryDTO SuccessCategoryDTO = mapper.Map<GetOneCategoryDTO>(category);
                    resultView.IsSuccess = true;
                    resultView.Entity = SuccessCategoryDTO;
                    resultView.Msg = $"category {category.NameEn}is found";

                    return resultView;
                }
                else
                {
                    resultView.IsSuccess = false;
                    resultView.Entity = null;
                    resultView.Msg = $"category {category.NameEn}is not found";

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

        public  async Task<ResultView<GetOneCategoryDTO>> HeardDeleteAsync(GetOneCategoryDTO Entity)
        {
            ResultView<GetOneCategoryDTO> resultView = new ResultView<GetOneCategoryDTO>();


            try
            {
                Category category=mapper.Map<Category>(Entity);
               // GetOneCategoryDTO SuccessCategoryDTO = mapper.Map<GetOneCategoryDTO>(category);

                Category SuccessCategory = await categoryRepository.GetOneAsync(category.Id);



                if (SuccessCategory != null)
                {

                    Category SuccessCategory2 = await categoryRepository.DeleteAsync(SuccessCategory);
                    
                    GetOneCategoryDTO SuccessCategoryDTO = mapper.Map<GetOneCategoryDTO>(SuccessCategory2);

                    resultView.IsSuccess = true;
                    resultView.Entity = SuccessCategoryDTO;
                    resultView.Msg = $"category {category.NameEn}is found";
                    categoryRepository.SaveChangesAsync();
                    return resultView;

                }
                else
                {
                    resultView.IsSuccess = false;
                    resultView.Entity = null;
                    resultView.Msg = $"category {category.NameEn}is not found";

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
        public async Task<ResultView< GetOneCategoryDTO>> SoftDeleteAsync(GetOneCategoryDTO Entity)
        {
            ResultView<GetOneCategoryDTO> resultView = new ResultView<GetOneCategoryDTO>();


            try
            {
                Category category = mapper.Map<Category>(Entity);
                // GetOneCategoryDTO SuccessCategoryDTO = mapper.Map<GetOneCategoryDTO>(category);

                Category SuccessCategory = await categoryRepository.GetOneAsync(category.Id);



                if (SuccessCategory != null)
                {
                    SuccessCategory.IsDeleted = true;
                   //Category SuccessCategory2 = await categoryRepository.DeleteAsync(SuccessCategory);

                   GetOneCategoryDTO SuccessCategoryDTO = mapper.Map<GetOneCategoryDTO>(SuccessCategory);

                    resultView.IsSuccess = true;
                    resultView.Entity = SuccessCategoryDTO;
                    resultView.Msg = $"category {category.NameEn}is found";
                   // categoryRepository.SaveChangesAsync();
                    return resultView;

                }
                else
                {
                    resultView.IsSuccess = false;
                    resultView.Entity = null;
                    resultView.Msg = $"category {category.NameEn}is not found";

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
            return categoryRepository.SaveChangesAsync();
        }

     
    }
}
