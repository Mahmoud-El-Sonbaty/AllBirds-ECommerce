using AllBirds.DTOs.ProductDTOs;
using AllBirds.DTOs.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Application.Services.ProductServices
{
    public interface IProductService
    {
        public Task<ResultView<CUProductDTO>> CreateAsync(CUProductDTO cUProductDTO);
        public Task<ResultView<CUProductDTO>> UpdateAsync(CUProductDTO cUProductDTO);
        public Task<ResultView<CUProductDTO>> SoftDeleteAsync(int productId);
        public Task<ResultView<CUProductDTO>> HardDeleteAsync(int productId);
        public Task<ResultView<List<GetAllProductDTO>>> GetAllAsync();
        public Task<ResultView<CUProductDTO>> GetByIdAsync(int productId);
        public Task<ResultView<List<ProductCardDTO>>> GetAllPrdCatIdAsync( int CatId);
        public Task<ResultView<List<ProductCardDTO>>> ProductFilterationAsync(TypeFilterOfProductDTO typeFilterOfProductDTO);
        public Task<ResultView<List<ProductSearchDTOWithLang>>> GetProductSearchAsync(string PrdName , string Lang);
        public Task<ResultView<SingleProductAPIWithLangDTO>> GetSingleProduct(int id);




    }
}
