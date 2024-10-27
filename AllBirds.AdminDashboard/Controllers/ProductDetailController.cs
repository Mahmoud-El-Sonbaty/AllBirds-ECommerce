using AllBirds.Application.Services.CategoryServices;
using AllBirds.Application.Services.ProductDetailService;
using AllBirds.Application.Services.ProductServices;
using AllBirds.DTOs.AccountDTOs;
using AllBirds.DTOs.CategoryDTOs;
using AllBirds.DTOs.ProductDetailDTOs;
using AllBirds.DTOs.ProductDTOs;
using AllBirds.DTOs.Shared;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace AllBirds.AdminDashboard.Controllers
{
    public class ProductDetailController : Controller
    {
        private readonly IProductDetailsService productDetailsService;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ProductDetailController(IProductDetailsService _producDetailstService, IWebHostEnvironment _webHostEnvironment)
        {
            productDetailsService = _producDetailstService;
            webHostEnvironment = _webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            ViewBag.ProductId = id;
            ResultView <CRProductDetails> resultView = new();
            return View(resultView);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ResultView<CRProductDetails> cUProductDetails)
        {
            ResultView<CRProductDetails> resultView = new();
            if (ModelState.IsValid)
            {
               
                cUProductDetails.Data.ImagePath = Path.Combine(new string[] { webHostEnvironment.WebRootPath, "Images", "ProductDetails" });

                resultView = await productDetailsService.CreateProductDetails(cUProductDetails.Data);

                if (resultView.IsSuccess)
                {
                    return Redirect($"/ProductDetail/AllProductDetails/{resultView.Data.ProductId}"); 
                }
                else
                {
                    return View(resultView);
                }
                
            }
            resultView.IsSuccess = false;
            resultView.Data = null;
            resultView.Msg = "This Product Detail Not Valid Please Enter Valid Data";
            return View(resultView);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {

            ResultView<UpdateProductDetail> getAllProductDTOS = await productDetailsService.GetOnePrdDetails(id);
            
            if (getAllProductDTOS.IsSuccess)
                return View(getAllProductDTOS);
            else
                return View(getAllProductDTOS);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ResultView<UpdateProductDetail> cUProductDTO)
        {
            ResultView<UpdateProductDetail> ProductDetailsUpdated = new();

            cUProductDTO.Data.ImagePath += "~@#$%&"+ Path.Combine(new string[] { webHostEnvironment.WebRootPath, "Images", "ProductDetails" });
            if (ModelState.IsValid)
            {
                ProductDetailsUpdated = await productDetailsService.UpdateProductDetails(cUProductDTO.Data);
                if (ProductDetailsUpdated.IsSuccess)
                {
                    return Redirect($"/ProductDetail/AllProductDetails/{ProductDetailsUpdated.Data!.ProductId}");
                }
                return View(ProductDetailsUpdated);
            }
            ProductDetailsUpdated.Data = null;
            ProductDetailsUpdated.IsSuccess = false;
            ProductDetailsUpdated.Msg = "This Detail Not Valid Please Enter Valid Data";
            return View(ProductDetailsUpdated);
        }
        [HttpGet]
        public async Task<IActionResult> AllProductDetails(int id)
        {
            List<GetAllProductDetailsDTOS> getAllProductDTOs = await productDetailsService.GetAllProductDetails(id);
            ViewBag.ProductId = id;
            return View(getAllProductDTOs);

        }
        public async Task<IActionResult> DeleteProductDetails(UpdateProductDetail cUProductDTO)
            {
            ResultView<UpdateProductDetail> DeletedProductDetails = await productDetailsService.HardDeletePrdDetails(cUProductDTO);

            return Redirect($"/ProductDetail/AllProductDetails/{cUProductDTO.ProductId}");

        }

    }
}