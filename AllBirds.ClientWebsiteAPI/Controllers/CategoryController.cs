using AllBirds.Application.Services.CategoryServices;
using AllBirds.DTOs.CategoryDTOs;
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
    }
}
