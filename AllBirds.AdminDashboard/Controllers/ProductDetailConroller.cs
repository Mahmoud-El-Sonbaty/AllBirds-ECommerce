using AllBirds.Application.Services.CategoryServices;
using AllBirds.Application.Services.ProductDetailService;
using AllBirds.Application.Services.ProductServices;
using AllBirds.DTOs.AccountDTOs;
using AllBirds.DTOs.CategoryDTOs;
using AllBirds.DTOs.ProductDetailDTOs;
using AllBirds.DTOs.ProductDTOs;
using AllBirds.DTOs.Shared;
using Microsoft.AspNetCore.Mvc;

namespace AllBirds.AdminDashboard.Controllers
{
    public class ProductDetailConroller : Controller
    {
        private readonly IProductDetailsService productDetailsService;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ProductDetailConroller(IProductDetailsService _producDetailstService, IWebHostEnvironment _webHostEnvironment)
        {
            productDetailsService = _producDetailstService;
            webHostEnvironment = _webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            CUProductDetails cUProductDetails = new CUProductDetails();

            return View(cUProductDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CUProductDetails cUProductDetailsDTO)
        {
            if (ModelState.IsValid)
            {
                cUProductDetailsDTO.ImagePath = Path.Combine(new string[] { webHostEnvironment.WebRootPath, "Images", "ProductDetails" });

                ResultView<CUProductDetails> createdProductDetail = await productDetailsService.CreateProductDetails(cUProductDetailsDTO);
                if (createdProductDetail.IsSuccess)
                    return RedirectToAction("AllProductDetails");
                else
                    return View();
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {

            ResultView<GetAllProductDetailsDTOS> getAllProductDTOS = await productDetailsService.GetOnePrdDetails(id);
            if (getAllProductDTOS.IsSuccess)
                return View(getAllProductDTOS);
            else
                return View();

        }

        [HttpPost]
        public async Task<IActionResult> Update(CUProductDetails cUProductDTO)
        {
            if (ModelState.IsValid)
            {
                ResultView<CUProductDetails> ProductDetailsUpdated = await productDetailsService.UpdateProductDetails(cUProductDTO);
                if (ProductDetailsUpdated.IsSuccess)
                {
                    return RedirectToAction("AllProductDetails");
                }
                return View();
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> AllProductDetails()
        {
            List<GetAllProductDetailsDTOS> getAllProductDTOs = await productDetailsService.GetAllProductDetails();

            return View(getAllProductDTOs);

        }
        public async Task<IActionResult> DeleteProductDetails(CUProductDetails cUProductDTO)
        {
            ResultView<CUProductDetails> DeletedProductDetails = await productDetailsService.HardDeletePrdDetails(cUProductDTO);
            /*if(DeletedProductDetails.IsSuccess)
            {
                return RedirectToAction("AllProductDetails");
            }
            else
            {
                return RedirectToAction("AllProductDetails");
            }*/ // Revision
            return RedirectToAction("AllProductDetails");

        }

    }
}