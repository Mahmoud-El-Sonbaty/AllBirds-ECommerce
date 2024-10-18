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
        public Task<ResultView< CreateOrUpdateCategoryDTO>> CreateAsync(CreateOrUpdateCategoryDTO entity);
        public Task<ResultView <CreateOrUpdateCategoryDTO>> UpdateAsync(CreateOrUpdateCategoryDTO entity);
        public Task<ResultView<GetOneCategoryDTO>> HardDeleteAsync(GetOneCategoryDTO entity);
        public Task<ResultView<GetOneCategoryDTO>> SoftDeleteAsync(GetOneCategoryDTO entity);
        public Task<ResultView<GetOneCategoryDTO>> GetOneAsync(int id);
        public Task<List<GetAllCategoryDTO>> GetAllAsync();
        //public Task<int> SaveChangesAsync();
    }
}
