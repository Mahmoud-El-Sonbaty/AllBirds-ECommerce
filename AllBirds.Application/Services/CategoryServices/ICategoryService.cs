using AllBirds.DTOs.Shared;
using AllBirds.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Application.Services.CategoryServices
{
    public interface ICategoryService
    {
        public Task<ResultView< CUCategoryDTO>> CreateAsync(CUCategoryDTO entity);
        public Task<ResultView <CUCategoryDTO>> UpdateAsync(CUCategoryDTO entity);
        public Task<ResultView<GetOneCategoryDTO>> HardDeleteAsync(GetOneCategoryDTO entity);
        public Task<ResultView<GetOneCategoryDTO>> SoftDeleteAsync(GetOneCategoryDTO entity);
        public Task<List<GetAllCategoryDTO>> GetAllAsync();
        public Task<ResultView<GetOneCategoryDTO>> GetOneAsync(int id);
        //public Task<int> SaveChangesAsync();
    }
}
