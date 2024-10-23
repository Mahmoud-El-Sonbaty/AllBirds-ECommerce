
using AllBirds.Application.Services.CategoryServices;
using AllBirds.Application.Services.ProductColorServices;
using AllBirds.Application.Services.ProductServices;
using AllBirds.Application.Services.ProductSpecificationServices;
using AllBirds.Application.Services.SpecificationServices;
using AllBirds.DTOs.CategoryDTOs;
using AllBirds.DTOs.ProductColorDTOs;
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
        private readonly IProductColorService productColorService;

        public ProductController(IProductService _productService, ICategoryService _categoryService, IProductSpecificationService _productSpecservice, ISpecificationService _specificationService, IProductColorService _productColorService)
        {
            productService = _productService;
            categoryService = _categoryService;
            productSpecService = _productSpecservice;
            specificationService = _specificationService;
            productColorService = _productColorService;
        }
        public IActionResult Index()
        {
            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            ResultView<List<GetAllProductDTO>> getAllProductDTOs = (await productService.GetAllAsync());
            return View(getAllProductDTOs.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ResultView<List<GetAllCategoryDTO>> allCategoryDTOs = await categoryService.GetAllAsync();
            ViewBag.Categories = allCategoryDTOs.Data;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CUProductDTO cUProductDTO)
        {
            var ff = Request;
            if (ModelState.IsValid)
            {
                ResultView<CUProductDTO> createRes = await productService.CreateAsync(cUProductDTO);
                if (createRes.IsSuccess)
                {
                    TempData["Msg"] = createRes.Msg;
                    TempData["IsSuccess"] = createRes.IsSuccess;
                    return RedirectToAction("GetAll");
                }
                TempData["Msg"] = createRes.Msg;
                TempData["IsSuccess"] = createRes.IsSuccess;
                return View(createRes.Data);
            }
            TempData["Msg"] = ModelState.ErrorCount > 1 ? $"There Are {ModelState.ErrorCount} Validation Errors" : $"There Is {ModelState.ErrorCount} Validation Error";
            TempData["IsSuccess"] = false;
            return View(cUProductDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            ResultView<List<GetAllCategoryDTO>> allCategoryDTOs = await categoryService.GetAllAsync();
            ViewBag.Categories = allCategoryDTOs.Data;
            var Prd = await productService.GetByIdAsync(id);
            return View(Prd.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CUProductDTO cUProductDTO)
        {
            if (ModelState.IsValid)
            {
                ResultView<CUProductDTO> updateRes = await productService.UpdateAsync(cUProductDTO);
                if (updateRes.IsSuccess)
                {
                    TempData["Msg"] = updateRes.Msg;
                    TempData["IsSuccess"] = updateRes.IsSuccess;
                    return RedirectToAction("GetAll");
                }
                TempData["Msg"] = updateRes.Msg;
                TempData["IsSuccess"] = updateRes.IsSuccess;
                return View(updateRes.Data);
            }
            TempData["Msg"] = ModelState.ErrorCount > 1 ? $"There Are {ModelState.ErrorCount} Validation Errors" : $"There Is {ModelState.ErrorCount} Validation Error";
            TempData["IsSuccess"] = false;
            return View(cUProductDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id > 0)
            {
                ResultView<CUProductDTO> deletedProduct = await productService.SoftDeleteAsync(id);
                if (deletedProduct.IsSuccess)
                {
                    TempData.Add("Msg", $"Product {deletedProduct.Data.ProductNo} Soft Deleted Successfully");
                    TempData.Add("IsSuccess", true);
                }
                TempData.Add("Msg", $"Product {deletedProduct?.Data?.ProductNo ?? $"{id}"} Soft Delete Wasn't Successful ({deletedProduct.Msg})");
                TempData.Add("IsSuccess", false);
            }
            return RedirectToAction("GetAll");
        }

        /////////////////////////////////////////////////// Product Specifications //////////////////////////////////////////////////////////////

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
                return res.IsSuccess ? Json(new { success = true, id = res.Data!.Id, message = res.Msg }) : Json(new { success = false, message = res.Msg });
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
                return res.IsSuccess ? Json(new { success = true, id = res.Data!.Id, message = res.Msg }) : Json(new { success = false, message = res.Msg });
            }

            // If the model is invalid, return an error response
            return Json(new { success = false, message = "Invalid data" });
        }

        [HttpGet]
        public async Task<IActionResult> DeletePrdSpec(int id)
        {
            ResultView<GetProductSpecificationDTO> res = await productSpecService.HardDeleteAsync(id);
            if (res.IsSuccess)
                return Redirect($"/Product/GetAllSpecs/{res.Data.ProductId}");
            return Json(new { success = false, message = res.Msg });
        }

        /////////////////////////////////////////////////// Product Color //////////////////////////////////////////////////////////////

        [HttpGet]
        public async Task<IActionResult> GetAllProductColors()
        {

            var PrColor = await productColorService.GetAllAsync();
            if (PrColor.IsSuccess)
            {
                return View(PrColor.Data);
            }
            else
            {

                ViewBag.ErrMsg = PrColor.Msg;
                return View();
            }
        }


    }
}
