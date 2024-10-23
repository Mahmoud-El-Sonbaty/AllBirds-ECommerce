using AllBirds.DTOs.OrderStateDTOs;
using AllBirds.DTOs.ProductColorDTOs;
using AllBirds.DTOs.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Application.Services.ProductColorServices
{
    public interface IProductColorService 
    {
        public Task<ResultView<CreateProductColorDTO>> CreateAsync(CreateProductColorDTO createProductColorDTO);
        public Task<ResultView<CreateProductColorDTO>> UpdateAsync(CreateProductColorDTO createProductColorDTO);
        public Task<ResultView<GetOneProductColorDTO>> SoftDeleteAsync(int sizeId);
        public Task<ResultView<GetOneProductColorDTO>> HardDeleteAsync(int sizeId);
        public Task<ResultView<List<GetOneProductColorDTO>>> GetAllAsync();
        public Task<ResultView<List<GetOneProductColorDTO>>> GetAllWithDeletedAsync();
        public Task<ResultView<GetOneProductColorDTO>> GetByIdAsync(int Id);
    }
}
