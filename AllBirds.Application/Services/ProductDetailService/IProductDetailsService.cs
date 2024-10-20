using AllBirds.DTOs.ProductDetailDTOs;
using AllBirds.DTOs.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Application.Services.ProductDetailService
{
    public interface IProductDetailsService
    {
        public Task<ResultView<CUProductDetails>> CreateProductDetails(CUProductDetails cUProductDetails);
        public Task<ResultView<CUProductDetails>> UpdateProductDetails(CUProductDetails cUProductDetails);
        public Task<ResultView<CUProductDetails>> HardDeletePrdDetails(CUProductDetails cUProductDetails);
        public Task<List<GetAllProductDetailsDTOS>> GetAllProductDetails(int id);
        public Task<ResultView<GetAllProductDetailsDTOS>> GetOnePrdDetails(int id);



    }
}
