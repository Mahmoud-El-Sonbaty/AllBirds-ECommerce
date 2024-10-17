using AllBirds.Application.Services.CategoryServices;
using AllBirds.Application.Services.ProductServices;
using AllBirds.DTOs.CategoryDTOs;
using AllBirds.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AllBirds.AdminDashboard.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productservice;
        //private readonly ICategoryService categoryservice;

        public ProductController(IProductService _productService )
        {
            productservice = _productService;
            //categoryservice = _categoryService;
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
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(CUProductDTO cUProductDTO)
        {
            if (ModelState.IsValid)
            {
                CUProductDTO createdProduct = await productservice.CreateAsync(cUProductDTO);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            ViewBag.Categories = new List<CUCategoryDTO>()
            { 
                new CUCategoryDTO() { Id = 1 , NameAr = " قسم 1", NameEn = "Category 1" },
                new CUCategoryDTO() { Id = 2 , NameAr = " قسم 2", NameEn = "Category 2" },
                new CUCategoryDTO() { Id = 3 , NameAr = " قسم 3", NameEn = "Category 3" },
                new CUCategoryDTO() { Id = 4 , NameAr = " قسم 4", NameEn = "Category 4" },
                new CUCategoryDTO() { Id = 5 , NameAr = " قسم 5", NameEn = "Category 5" } 
            };
            var Prd = await productservice.GetByIdAsync(id);
            return View(Prd);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CUProductDTO cUProductDTO)
        {
            if (ModelState.IsValid)
            {
                CUProductDTO createdProduct = await productservice.UpdateAsync(cUProductDTO);
                if(createdProduct is not null)
                {
                    return RedirectToAction("GetAll");
                }
            }
            return View();
        }


    }
}
