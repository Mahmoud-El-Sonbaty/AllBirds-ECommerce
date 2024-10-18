using AllBirds.DTOs.Shared;
using AllBirds.DTOs.CategoryDTOs;
using AllBirds.DTOs.CategoryProductDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Application.Services.CategoryProductServices
{
    public interface ICategoryProductService
    {
        public Task<ResultView<CreateOrUpdateCategoryProductDTO>> CreateAsync(CreateOrUpdateCategoryProductDTO Entity);
        public Task<ResultView<CreateOrUpdateCategoryProductDTO>> UpdateAsync(CreateOrUpdateCategoryProductDTO Entity);
        public Task<ResultView<GetOneCategoryProductDTO>> HeardDeleteAsync(GetOneCategoryProductDTO Entity);
        public Task<ResultView<GetOneCategoryProductDTO>> SoftDeleteAsync(GetOneCategoryProductDTO Entity);
        public Task<List<GetAllCategoryProductDTO>> GetAllAsync();
        public Task<ResultView<GetOneCategoryProductDTO>> GetOneAsync(int id);
        //public Task<int> SaveChangesAsync();
    }
}
