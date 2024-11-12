using AllBirds.Application.Services.CategoryServices;
using AllBirds.Application.Services.ProductServices;
using AllBirds.DTOs.ProductDTOs;
using AllBirds.DTOs.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;

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
                
                    //Shoes
                if (item.NameEn == "Socks") {
                    ResultView<List<ProductCardDTO>> productCardDTOs = await productService.GetAllPrdCatIdAsync(item.Id);
                    return Ok(productCardDTOs);

                }
            }
            return Ok(null);


        }

        [HttpGet]
        [Route("SingleProduct/{id:int}")]
        public async Task<IActionResult> GetSingleProduct(int id)
        {
            ResultView<SingleProductAPIWithLangDTO> prdResultView = await productService.GetSingleProduct(id);
            if(prdResultView.IsSuccess)
                return Ok(prdResultView);
            return BadRequest(prdResultView);
        }
    }

}
