
﻿using AllBirds.Application.Services.ProductServices;
using AllBirds.DTOs.CategoryDTOs;
using AllBirds.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Serialization;

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
        public async Task<IActionResult> Update(int id)
        {
            List<CUCategoryDTO> cUCategoryDTOs = new List<CUCategoryDTO>()
            { 
                new CUCategoryDTO() { Id = 1 , NameAr = " قسم 1", NameEn = "Category 1" },
                new CUCategoryDTO() { Id = 2 , NameAr = " قسم 2", NameEn = "Category 2" },
                new CUCategoryDTO() { Id = 3 , NameAr = " قسم 3", NameEn = "Category 3" },
                new CUCategoryDTO() { Id = 4 , NameAr = " قسم 4", NameEn = "Category 4" },
                new CUCategoryDTO() { Id = 5 , NameAr = " قسم 5", NameEn = "Category 5" } 
            };
            ViewBag.Categories = cUCategoryDTOs;
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
            public async Task<IActionResult> Delete(int id)
            {
                if (id > 0)
                {
                    CUProductDTO deletedProduct = await productservice.SoftDeleteAsync(id);
                if (deletedProduct is not null)
                {
                    return RedirectToAction("GetAll");

                }
                else
                    return View();
                }
                    return View();
            }
        }
}
