using AllBirds.DTOs.Shared;
using AllBirds.DTOs.ProductSpecificationDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Application.Services.ProductSpecificationServices
{
    public interface IProductSpecificationService
    {
        public Task<ResultView<CUProductSpecificationDTO>> CreateAsync(CUProductSpecificationDTO entity);
        public Task<ResultView<CUProductSpecificationDTO>> UpdateAsync(CUProductSpecificationDTO entity);
        //public Task<ResultView<GetSpecificationDTO>> SoftDeleteAsync(int id);
        public Task<ResultView<GetProductSpecificationDTO>> HardDeleteAsync(int id);
        public Task<ResultView<List<GetProductSpecificationDTO>>> GetByProductIdAsync(int id);
        //public Task<ResultView<GetSpecificationDTO>> GetByIdAsync(int id);
        public Task<int> SaveChangesAsync();
    }
}
