using AllBirds.Application.Services.ColorServices;
using AllBirds.DTOs.ColorDTOs;
using Microsoft.AspNetCore.Mvc;

namespace AllBirds.AdminDashboard.Controllers
{
    public class ColorController : Controller
    {
        private readonly IColorService _colorService;

        public ColorController(IColorService colorService)
        {
            _colorService = colorService;
        }

        public async Task<IActionResult> Index()
        {
            var colors = await _colorService.GetAllColorsAsync();
            return View(colors);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ColorDTO colorDto)
        {
            if (ModelState.IsValid)
            {
                await _colorService.AddColorAsync(colorDto);
                return RedirectToAction(nameof(Index));
            }
            return View(colorDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var color = await _colorService.GetColorByIdAsync(id);
            if (color == null)
            {
                return NotFound();
            }
            return View(color);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ColorDTO colorDto)
        {
            if (ModelState.IsValid)
            {
                await _colorService.UpdateColorAsync(colorDto);
                return RedirectToAction(nameof(Index));
            }
            return View(colorDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var color = await _colorService.GetColorByIdAsync(id);
            if (color == null)
            {
                return NotFound();
            }
            return View(color);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _colorService.DeleteColorAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }

}
