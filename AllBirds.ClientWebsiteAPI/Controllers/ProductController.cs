using AllBirds.Application.Services.CategoryServices;
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
        private readonly ICategoryService categoryService;
        public ProductController(IProductService _productService , ICategoryService _categoryService)
        {
            productService = _productService;
        categoryService = _categoryService;
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

          [HttpGet]
        public async Task<IActionResult> GetSocks()
        {

            var cat = await categoryService.GetAllAsync();
            foreach (var item in cat.Data)
            {
                if (item.NameEn == "Socks") {
                    ResultView<List<ProductCardDTO>> productCardDTOs = await productService.GetAllPrdCatIdAsync(item.Id);
                    return Ok(productCardDTOs);

                }
            }
            return Ok(null);


        }


    }

}
