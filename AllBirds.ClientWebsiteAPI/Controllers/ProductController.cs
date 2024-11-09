using AllBirds.Application.Services.ProductServices;
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
            ResultView<List<ProductCardDTO>> productCardDTOs = await productService.ProductFilterationAsync(typeFilterOfProductDTO);

            return Ok(productCardDTOs);
        }
        [HttpGet]
        [Route("{PrdName}/{Lang}")]
        public async Task<IActionResult> GetProductSearchAsync(string PrdName , string Lang)
        {

            ResultView<List<ProductSearchDTOWithLang>> productResult = await productService.GetProductSearchAsync(PrdName , Lang);
            return Ok(productResult);
        }



    }

}
