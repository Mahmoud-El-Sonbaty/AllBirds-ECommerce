using AllBirds.Application.Services.CategoryServices;
using AllBirds.Application.Services.ProductServices;
using AllBirds.DTOs.CategoryDTOs;
using AllBirds.DTOs.ProductDTOs;
using AllBirds.DTOs.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AllBirds.ClientWebsiteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        public CategoryController(ICategoryService _categoryService)
        {
            categoryService = _categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            ResultView<List<GetAllCategoryNestedDTO>> allCats = await categoryService.GetAllAPI();
            return Ok(allCats);
        }


        

        [HttpGet]
        [Route("{CatId:int}")]
        public async Task<IActionResult> GetCategoryById(int CatId)
        {
            ResultView<GetAllCategoryNestedDTO> productCardDTOs = await categoryService.GetCategoryByIdAPI(CatId);
            return Ok(productCardDTOs);
        }

        //for localization 


        [HttpGet]
        [Route("{Lang:twoLetterLang}")]
        public async Task<IActionResult> GetAllCategoriesWithLang(string Lang)
        {
            ResultView<List<GetAllCategoryWithLangDTO>> allCats = await categoryService.GetAllAPIWithlang(Lang);
            return Ok(allCats);
        }

        [HttpGet]
        [Route("category/{CatId:int}/{Lang:twoLetterLang}")]
        public async Task<IActionResult> GetCategoryByIdWithLang(int CatId, string Lang)
        {
            ResultView<GetAllCategoryWithLangDTO> productCardDTOs = await categoryService.GetCategoryByIdAPIWithLang(CatId, Lang);
            return Ok(productCardDTOs);
        }
    }
}
