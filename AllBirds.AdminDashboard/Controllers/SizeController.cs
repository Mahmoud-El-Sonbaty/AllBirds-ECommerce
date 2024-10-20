using AllBirds.Application.Services.SizeServices;
using AllBirds.DTOs.SizeDTOs;
using Microsoft.AspNetCore.Mvc;

namespace AllBirds.AdminDashboard.Controllers
{
    public class SizeController : Controller
    {
        private readonly ISizeService _sizeService;

        public SizeController(ISizeService sizeService)
        {
            _sizeService = sizeService;
        }

        public async Task<IActionResult> Index()
        {
            var sizes = await _sizeService.GetAllAsync();
            return View(sizes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CUSizeDTO sizeDto)
        {
            if (ModelState.IsValid)
            {
                await _sizeService.CreateAsync(sizeDto);
                return RedirectToAction(nameof(Index));
            }
            return View(sizeDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var size = await _sizeService.GetByIdAsync(id);
            if (size == null)
            {
                return NotFound();
            }
            return View(size);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CUSizeDTO sizeDto)
        {
            if (ModelState.IsValid)
            {
                await _sizeService.UpdateAsync(sizeDto);
                return RedirectToAction(nameof(Index));
            }
            return View(sizeDto);
        }
        //
        public async Task<IActionResult> Delete(int id)
        {
            var size = await _sizeService.GetByIdAsync(id);
            if (size == null)
            {
                return NotFound();
            }
            return View(size);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, bool hardDelete)
        {
            if (hardDelete)
            {
                await _sizeService.HardDeleteAsync(id);
            }
            else
            {
                await _sizeService.SoftDeleteAsync(id);
            }
            return RedirectToAction(nameof(Index));
        }
    }

}

