using AllBirds.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Application.Services.ProductServices
{
    public interface IProductService
    {
        public Task<CUProductDTO> CreateAsync(CUProductDTO cUProductDTO);
        public Task<CUProductDTO> UpdateAsync(CUProductDTO cUProductDTO);
        public Task<CUProductDTO> SoftDeleteAsync(int productId);
        public Task<CUProductDTO> HardDeleteAsync(int productId);
        public Task<List<GetAllProductDTO>> GetAllAsync();
        public Task<CUProductDTO> GetByIdAsync(int productId);
    }
}
