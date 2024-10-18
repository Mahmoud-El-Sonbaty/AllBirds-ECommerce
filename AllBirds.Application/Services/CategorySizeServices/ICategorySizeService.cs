using AllBirds.DTOs._ٍShared;
using AllBirds.DTOs.CategoryProductDTOS;
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
        public Task<ResultView<CreateOrUpdateCategorySizeDTO>> CreateAsync(CreateOrUpdateCategorySizeDTO Entity);


        public Task<ResultView<GetOneCategorySizeDTO>> HeardDeleteAsync(GetOneCategorySizeDTO Entity);
        public Task<ResultView<GetOneCategorySizeDTO>> SoftDeleteAsync(GetOneCategorySizeDTO Entity);

        public Task<List<GetAllCategorySizeDTO>> GetAllAsync();
        public Task<ResultView<GetOneCategorySizeDTO>> GetOneAsync(int id);
        public Task<int> SaveChangesAsync();
        public Task<ResultView<CreateOrUpdateCategorySizeDTO>> UpdateAsync(CreateOrUpdateCategorySizeDTO Entity);

    }
}
