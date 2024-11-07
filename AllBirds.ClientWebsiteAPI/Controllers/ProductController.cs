using AllBirds.Application.Services.OrderMasterServices;
using AllBirds.Application.Services.ProductServices;
using AllBirds.DTOs.OrderMasterDTOs;
using AllBirds.DTOs.ProductDTOs;
using AllBirds.DTOs.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AllBirds.ClientWebsiteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        public ProductController(IProductService _ProductService)
        {
            productService = _ProductService;
        }

        [HttpGet]
        public async Task<IActionResult> GetNOfProductByCatId(int catId, int numberofProduct)
        {
            ResultView<List<GetTopProductsDTO>> products = await productService.GetNOfProductByCatId(catId,numberofProduct);
            if (products.IsSuccess)
                return Ok(products);
            else
                return BadRequest(products.Msg);

        }
    }
        public ProductController(IProductService _productService)
        {
            productService = _productService;
        }

        [HttpGet]
        [Route("{CatId:int}")]
        public async Task<IActionResult> GetProductsByCategoryId(int CatId)
        {
            ResultView<List<ProductCardDTO>> productCardDTOs = await productService.GetAllPrdCatIdAsync(CatId);
            return Ok(productCardDTOs);
        }

        [HttpPost("filter")]
        public async Task<IActionResult> getProductFilter( TypeFilterOfProductDTO typeFilterOfProductDTO)
        {
            ResultView<List<ProductCardDTO>> productCardDTOs = await productService.ProductFilteration(typeFilterOfProductDTO);

            return Ok(productCardDTOs);
        }



    }

}
