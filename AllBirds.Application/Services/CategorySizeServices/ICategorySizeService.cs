using AllBirds.DTOs.Shared;
using AllBirds.DTOs.CategoryProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllBirds.DTOs.CategorySizeDTOS;
namespace AllBirds.Application.Services.CategorySizeServices
{
    public interface ICategorySizeService
    {
        public Task<ResultView<CreateOrUpdateCategorySizeDTO>> CreateAsync(CreateOrUpdateCategorySizeDTO entity);
        public Task<ResultView<CreateOrUpdateCategorySizeDTO>> UpdateAsync(CreateOrUpdateCategorySizeDTO entity);
        public Task<ResultView<GetOneCategorySizeDTO>> HardDeleteAsync(GetOneCategorySizeDTO entity);
        public Task<ResultView<GetOneCategorySizeDTO>> SoftDeleteAsync(GetOneCategorySizeDTO entity);
        public Task<List<GetAllCategorySizeDTO>> GetAllAsync();
        public Task<ResultView<GetOneCategorySizeDTO>> GetOneAsync(int id);
        //public Task<int> SaveChangesAsync();
    }
}
