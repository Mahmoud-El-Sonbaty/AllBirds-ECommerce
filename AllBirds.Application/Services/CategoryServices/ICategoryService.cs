using AllBirds.DTOs._ٍShared;
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
        public Task<ResultView< CreateOrUpdateCategoryDTO>> CreateAsync(CreateOrUpdateCategoryDTO Entity);


        public Task<ResultView<GetOneCategoryDTO>> HeardDeleteAsync(GetOneCategoryDTO Entity);
        public Task<ResultView<GetOneCategoryDTO>> SoftDeleteAsync(GetOneCategoryDTO Entity);

        public Task<List<GetAllCategoryDTO>> GetAllAsync();
        public Task<ResultView<GetOneCategoryDTO>> GetOneAsync(int id);
        public Task<int> SaveChangesAsync();
         public Task<ResultView <CreateOrUpdateCategoryDTO>> UpdateAsync(CreateOrUpdateCategoryDTO Entity);
    }
}
