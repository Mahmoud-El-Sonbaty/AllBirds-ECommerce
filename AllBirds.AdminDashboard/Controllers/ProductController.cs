using AllBirds.Application.Services.ProductServices;
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
            //List<GetAllProductDTO> getAllProductDTOs = (await productservice.GetAllAsync());
            //return View(getAllProductDTOs);
            return View();
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


    }
}
