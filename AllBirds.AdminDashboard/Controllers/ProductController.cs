
using AllBirds.Application.Services.CategoryServices;
using AllBirds.Application.Services.ProductColorServices;
using AllBirds.Application.Services.ProductServices;
using AllBirds.DTOs.CategoryDTOs;
using AllBirds.DTOs.ProductColorDTOs;
using AllBirds.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Serialization;

namespace AllBirds.AdminDashboard.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productservice;
        private readonly ICategoryService categoryservice;
        private readonly IProductColorService ProductColorService;

        public ProductController(IProductService _productService, ICategoryService _categoryService ,IProductColorService _ProductColorService)
        {
            productservice = _productService;
            categoryservice = _categoryService;
            productservice= _productService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<GetAllProductDTO> getAllProductDTOs = (await productservice.GetAllAsync());
            return View(getAllProductDTOs);

        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            List<GetAllCategoryDTO> allCategoryDTOs = await categoryservice.GetAllAsync();
            ViewBag.Categories = allCategoryDTOs;
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(CUProductDTO cUProductDTO)
        {
            if (ModelState.IsValid)
            {
                CUProductDTO createdProduct = await productservice.CreateAsync(cUProductDTO);
                return RedirectToAction("GetAll");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            List<GetAllCategoryDTO> allCategoryDTOs = await categoryservice.GetAllAsync();
            ViewBag.Categories = allCategoryDTOs;
            var Prd = await productservice.GetByIdAsync(id);
            return View(Prd);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CUProductDTO cUProductDTO)
        {
            if (ModelState.IsValid)
            {
                CUProductDTO createdProduct = await productservice.UpdateAsync(cUProductDTO);
                if (createdProduct is not null)
                {
                    return RedirectToAction("GetAll");
                }
                else
                    return View();
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id > 0)
            {
                CUProductDTO deletedProduct = await productservice.SoftDeleteAsync(id);
                if (deletedProduct is not null)
                {
                    return RedirectToAction("GetAll");
                }
            }
            return null;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProductColors()
        {

            var PrColor = await ProductColorService.GetAllAsync();
            if (PrColor.IsSuccess)
            {
                return View(PrColor.Data);
            }
            else
            {

                ViewBag.ErrMsg = PrColor.Msg;
                return View( new GetALlProductColorDTO ());
            }
        }


    }
}
