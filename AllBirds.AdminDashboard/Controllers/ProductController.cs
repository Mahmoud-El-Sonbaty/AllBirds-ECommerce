using AllBirds.Application.Services.ProductServices;
using AllBirds.DTOs.CategoryDTOs;
using AllBirds.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Mvc;

namespace AllBirds.AdminDashboard.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productservice;

        public ProductController(IProductService _productService)
        {
            productservice = _productService;
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
            //return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new List<CUCategoryDTO>()
            {
                new() { Id = 1, NameAr = "رجالي", NameEn = "Men" },
                new() { Id = 2, NameAr = "نسائي", NameEn = "Women" },
                new() { Id = 3, NameAr = "أحذية رجالي", NameEn = "Men Shoes" },
                new() { Id = 4, NameAr = "أحذية نشطة رجالي", NameEn = "Men Active Shoes" },
                new() { Id = 5, NameAr = "أحذية نسائية", NameEn = "Women Shoes" },
                new() { Id = 6, NameAr = "أحذية رياضية يومية نسائية", NameEn = "Women Every Day Sneakers" }
            };
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
        public async Task<IActionResult> Delete(int id)
        {
            if (id > 0)
            {
                CUProductDTO deletedProduct = await productservice.SoftDeleteAsync(id);
                if (deletedProduct != null)
                {
                    return RedirectToAction("GetAll");
                }
            }
            return null;
        }
    }
}
