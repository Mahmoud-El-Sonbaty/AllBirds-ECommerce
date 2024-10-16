using AllBirds.Application.Services.ProductServices;
using AllBirds.DTOs.ProductDTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AllBirds.AdminDashboard.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productservice;
        private readonly IMapper mapper;

        public ProductController(IProductService _productService)
        {
            productservice = _productService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AllProducts()
        {
            //List<GetAllProductDTO> getAllProductDTOs = (await productservice.GetAllAsync());
            //return View(getAllProductDTOs);
            return View();
        }
    }
}
