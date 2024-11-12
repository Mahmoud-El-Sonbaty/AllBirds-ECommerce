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

        /*=======================================================================
                                     for localization 
         ========================================================================*/


        [HttpGet]
        [Route("{CatId:int}/{numberofProduct:int}/{Lang:twoLetterLang}")]

        public async Task<IActionResult> GetNOfProductByCatIdWithlang(int CatId, int numberofProduct ,string Lang)
        {
            ResultView<List<GetTopProductWithLangDTO>> products = await productService.GetNOfProductByCatIdWithLang(CatId, numberofProduct, Lang);
            if (products.IsSuccess)
                return Ok(products);
            else
                return BadRequest(products.Msg);

        }

        [HttpGet]
        [Route("{CatId:int}/{Lang:twoLetterLang}")]
        public async Task<IActionResult> GetProductsByCategoryIdWithlang(int CatId, string Lang)
        {
            ResultView<List<GetProductCardWithlangDTO>> productCardDTOs = await productService.GetAllPrdCatIdWithLangAsync(CatId, Lang);
            return Ok(productCardDTOs);
        }


        [HttpGet]
        [Route("{PrdName}/{Lang:twoLetterLang}")]
        public async Task<IActionResult> GetProductSearchAsync(string PrdName, string Lang)
        {

            ResultView<List<ProductSearchDTOWithLang>> productResult = await productService.GetProductSearchAsync(PrdName, Lang);
            return Ok(productResult);
        }



        [HttpGet]
        [Route("SingleProduct/{id:int}/{Lang:twoLetterLang}")]
        public async Task<IActionResult> GetSingleProduct(int id,string Lang)
        {
            ResultView<SingleProductAPIWithLangDTO> prdResultView = await productService.GetSingleProduct(id,Lang);
            if(prdResultView.IsSuccess)
                return Ok(prdResultView);
            return BadRequest(prdResultView);
        }

        [HttpGet("GetSocks")]
        public async Task<IActionResult> GetSocks()
        {
            ResultView<List<ProductCardDTO>> productCardDTOs = await productService.GetAllPrdCatIdAsync(34);
            if (productCardDTOs.IsSuccess)
                return Ok(productCardDTOs);
            return BadRequest(productCardDTOs);


        }
    }

}
