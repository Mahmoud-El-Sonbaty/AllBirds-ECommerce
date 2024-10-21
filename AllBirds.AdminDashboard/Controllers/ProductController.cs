
using AllBirds.Application.Services.CategoryServices;
using AllBirds.Application.Services.ProductServices;
using AllBirds.Application.Services.ProductSpecificationServices;
using AllBirds.Application.Services.SpecificationServices;
using AllBirds.DTOs.CategoryDTOs;
using AllBirds.DTOs.ProductDTOs;
using AllBirds.DTOs.ProductSpecificationDTOs;
using AllBirds.DTOs.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Serialization;

namespace AllBirds.AdminDashboard.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly IProductSpecificationService productSpecService;
        private readonly ICategoryService categoryService;
        private readonly ISpecificationService specificationService;

        public ProductController(IProductService _productService, ICategoryService _categoryService, IProductSpecificationService _productSpecservice, ISpecificationService _specificationService)
        {
            productService = _productService;
            categoryService = _categoryService;
            productSpecService = _productSpecservice;
            specificationService = _specificationService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<GetAllProductDTO> getAllProductDTOs = (await productService.GetAllAsync());
            return View(getAllProductDTOs);

        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            List<GetAllCategoryDTO> allCategoryDTOs = await categoryService.GetAllAsync();
            ViewBag.Categories = allCategoryDTOs;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CUProductDTO cUProductDTO)
        {
            if (ModelState.IsValid)
            {
                CUProductDTO createdProduct = await productService.CreateAsync(cUProductDTO);
                return RedirectToAction("GetAll");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            List<GetAllCategoryDTO> allCategoryDTOs = await categoryService.GetAllAsync();
            ViewBag.Categories = allCategoryDTOs;
            var Prd = await productService.GetByIdAsync(id);
            return View(Prd);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CUProductDTO cUProductDTO)
        {
            if (ModelState.IsValid)
            {
                CUProductDTO createdProduct = await productService.UpdateAsync(cUProductDTO);
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
                CUProductDTO deletedProduct = await productService.SoftDeleteAsync(id);
                if (deletedProduct is not null)
                {
                    return RedirectToAction("GetAll");
                }
            }
            return null;
        }

        /////////////////////////////////////////////////// Product Specifications//////////////////////////////////////////////////////////////

        [HttpGet]
        public async Task<IActionResult> GetAllSpecs(int id)
        {
            if (id > 0)
            {
                ResultView<List<GetProductSpecificationDTO>> getProductSpecs = (await productSpecService.GetByProductIdAsync(id));
                ViewBag.Specs = await specificationService.GetAllAsync();
                return getProductSpecs.IsSuccess ? View(getProductSpecs) : View();
            }
            else
            {
                return RedirectToAction("GetAll");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddProductSpec([FromBody] CUProductSpecificationDTO cUproductSpec)
        {
            if (ModelState.IsValid)
            {
                ResultView<CUProductSpecificationDTO> res = await productSpecService.CreateAsync(cUproductSpec);
                return res.IsSuccess ? Json(new { success = true, id = res.Data!.Id }) : Json(new { success = false, message = $"creation not successfull {res.Msg}" });
            }

            // If the model is invalid, return an error response
            return Json(new { success = false, message = "Invalid data" });
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProductSpec([FromBody] CUProductSpecificationDTO cUproductSpec)
        {
            if (ModelState.IsValid)
            {
                ResultView<CUProductSpecificationDTO> res = await productSpecService.UpdateAsync(cUproductSpec);
                return res.IsSuccess ? Json(new { success = true, id = res.Data!.Id }) : Json(new { success = false, message = $"creation not successfull {res.Msg}" });
            }

            // If the model is invalid, return an error response
            return Json(new { success = false, message = "Invalid data" });
        }

        [HttpGet]
        public async Task<IActionResult> DeletePrdSpec(int id)
        {
            ResultView<GetProductSpecificationDTO> deletedSpec = await productSpecService.HardDeleteAsync(id);
            return Redirect($"/Product/GetAllSpecs/{id}");
        }
    }
}
