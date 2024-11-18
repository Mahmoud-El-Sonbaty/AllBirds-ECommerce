
using AllBirds.Application.Services.CategoryServices;
using AllBirds.Application.Services.ColorServices;
using AllBirds.Application.Services.ProductColorImageServices;
using AllBirds.Application.Services.ProductColorServices;
using AllBirds.Application.Services.ProductColorSizeServices;
using AllBirds.Application.Services.ProductServices;
using AllBirds.Application.Services.ProductSpecificationServices;
using AllBirds.Application.Services.SizeServices;
using AllBirds.Application.Services.SpecificationServices;
using AllBirds.DTOs.CategoryDTOs;
using AllBirds.DTOs.ProductColorDTOs;
using AllBirds.DTOs.ProductColorImageDTOs;
using AllBirds.DTOs.ProductColorSizeDTOs;
using AllBirds.DTOs.ProductDetailDTOs;
using AllBirds.DTOs.ProductDTOs;
using AllBirds.DTOs.ProductSpecificationDTOs;
using AllBirds.DTOs.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllBirds.AdminDashboard.Controllers
{
    [Authorize(Roles = "SuperUser,Manager,Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly IProductSpecificationService productSpecService;
        private readonly ICategoryService categoryService;
        private readonly ISpecificationService specificationService;
        private readonly IProductColorService productColorService;
        private readonly IProductColotImageService productColotImageService;
        private readonly IProductColorSizeService productColorSizeService;
        private readonly ISizeService sizeService;
        private readonly IColorService colorService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductController(IProductService _productService, ICategoryService _categoryService, IProductSpecificationService _productSpecservice, ISpecificationService _specificationService,
            IProductColorService _productColorService, IColorService _colorService , IProductColotImageService _productColotImageService, IProductColorSizeService _productColorSizeService, ISizeService _sizeService,
            IWebHostEnvironment _webHostEnvironment)
        {
            productService = _productService;
            categoryService = _categoryService;
            productSpecService = _productSpecservice;
            specificationService = _specificationService;
            productColorService = _productColorService;
            productColotImageService = _productColotImageService;
            productColorSizeService = _productColorSizeService;
            colorService = _colorService;
            sizeService = _sizeService;
            webHostEnvironment = _webHostEnvironment;
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
                TempData["IsSuccess"] = deletedProduct.IsSuccess;
                TempData["Msg"] = deletedProduct.Msg;

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
                if (getProductSpecs.IsSuccess)
                    return View(getProductSpecs);
            }
            TempData["IsSuccess"] = false;
            TempData["Msg"] = "This Product Specifications Were Not Found";
            return RedirectToAction("GetAll");
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
            {
                TempData["IsSuccess"] = true;
                TempData["Msg"] = res.Msg;
                return Redirect($"/Product/GetAllSpecs/{res.Data.ProductId}");
            }
            return Json(new { success = false, message = res.Msg });
        }

        /////////////////////////////////////////////////// Product Color //////////////////////////////////////////////////////////////

        [HttpGet]
        public async Task<IActionResult> GetAllProductColors(int Id)
        {

            ResultView<List<GetALlProductColorDTO>> PrColor = await productColorService.GetAllAsync(Id);
            ViewBag.PrdId = Id;
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
        [HttpGet]
        public async Task<IActionResult> GetProductColorImages(int id)
        {
            ResultView<GetOneProductColorDTO> resultView = await productColorService.GetByIdAsync(id);
            if(resultView.IsSuccess)
            {
                return View(resultView);
            }
            else
            {
                return View(resultView.Msg);
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteProductColorImage(int id)
        {
            ResultView<CUProductColorImageDTO> resultView = await productColotImageService.HardDeleteProductColorImage(id);
            TempData["IsSuccess"] = resultView.IsSuccess;
            TempData["Msg"] = resultView.Msg;
            return RedirectToAction("GetProductColorImages");
        }


        [HttpGet]
        public async Task<IActionResult> CreateProductColor(int Id)
        {
            ViewBag.ProductId = Id;
            ViewBag.Colors = await colorService.GetAllAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductColor(CreateProductColorDTO createProductColorDTO)
        {
            if (ModelState.IsValid)
            {
                CUProductColorImageDTO cUProductColorImageDTO = new();

                var ImagePath = Path.Combine(new string[] { webHostEnvironment.WebRootPath, "Images", "ProductColorImages" });

                ResultView<CreateProductColorDTO> CrProductColorDTO = await productColorService.CreateAsync(createProductColorDTO, ImagePath);
                if (CrProductColorDTO.IsSuccess)
                {
                    return Redirect($"/Product/GetAllProductColors/{CrProductColorDTO.Data.ProductId}");
                }
                else
                {
                    TempData["Msg"] = CrProductColorDTO.Msg;
                    TempData["IsSuccess"] = CrProductColorDTO.IsSuccess;

                }
            }
            else
            {
                TempData["Msg"] = ModelState.ErrorCount > 1 ? $"There Are {ModelState.ErrorCount} Validation Errors" : $"There Is {ModelState.ErrorCount} Validation Error";
                TempData["IsSuccess"] = false;

            }
            ViewBag.Colors = await colorService.GetAllAsync();

            return View();


        }
        public async Task<IActionResult> DeleteProductColor(int Id)
        {
            ResultView<GetOneProductColorDTO> resultView = await productColorService.HardDeleteAsync(Id);
            TempData["Msg"] = resultView.Msg;
            TempData["IsSuccess"] = resultView.IsSuccess;
            return Redirect($"/Product/GetAllProductColors/{resultView.Data.ProductId}");
        }

        /////////////////////////////////////////////////// Product Color Size //////////////////////////////////////////////////////////////
        
        public async Task<IActionResult> GetAllProductColorSizes(int prdColorId)
        {
            ResultView<List<GetPCSDTO>> resultView = await productColorSizeService.GetAllAsync(prdColorId);
            if (!resultView.IsSuccess)
            {
                TempData["Msg"] = resultView.Msg;
                TempData["IsSuccess"] = resultView.IsSuccess;
            }
            ViewBag.PrdColorId = prdColorId;
            ViewBag.Sizes = await sizeService.GetAllAsync();
            return View(resultView.Data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductColorSize([FromBody] CreatePCSDTO createPCSDTO)
        {
            if (ModelState.IsValid)
            {
                ResultView<UpdatePCSDTO> res = await productColorSizeService.CreateAsync(createPCSDTO);
                return res.IsSuccess ? Json(new { success = true, id = res.Data!.Id, message = res.Msg }) : Json(new { success = false, message = res.Msg });
            }

            // If the model is invalid, return an error response
            return Json(new { success = false, message = "Invalid data" });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProductColorSize([FromBody] UpdatePCSDTO updatePCSDTO)
        {
            if (ModelState.IsValid)
            {
                ResultView<UpdatePCSDTO> res = await productColorSizeService.UpdateAsync(updatePCSDTO);
                return res.IsSuccess ? Json(new { success = true, id = res.Data!.Id, message = res.Msg }) : Json(new { success = false, message = res.Msg });
            }

            // If the model is invalid, return an error response
            return Json(new { success = false, message = "Invalid data" });
        }

        [HttpGet]
        public async Task<IActionResult> DeleteProductColorSize(int prdColorSizeId, int prdColorId)
        {
            ResultView<CreatePCSDTO> resultView = await productColorSizeService.DeleteAsync(prdColorSizeId);
            TempData["Msg"] = resultView.Msg;
            TempData["IsSuccess"] = resultView.IsSuccess;
            return Redirect($"GetAllProductColorSizes?prdColorId={prdColorId}");
        }
    }
}
