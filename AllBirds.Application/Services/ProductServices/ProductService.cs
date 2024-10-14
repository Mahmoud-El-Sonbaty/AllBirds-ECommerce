using AllBirds.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Application.Services.ProductServices
{
    public class ProductService : IProductService
    {
        public Task<CUProductDTO> CreateAsync(CUProductDTO cUProductDTO)
        {
            throw new NotImplementedException();
        }

        public Task<GetAllProductDTO> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CUProductDTO> GetByIdAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<CUProductDTO> HardDeleteAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<CUProductDTO> SoftDeleteAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<CUProductDTO> UpdateAsync(CUProductDTO cUProductDTO)
        {
            throw new NotImplementedException();
        }
    }
}
