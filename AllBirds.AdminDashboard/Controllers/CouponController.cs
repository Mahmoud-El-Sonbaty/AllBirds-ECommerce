using AllBirds.Application.Services.CouponServices;
using AllBirds.DTOs.CouponDTOs;
using Microsoft.AspNetCore.Mvc;

namespace AllBirds.AdminDashboard.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        public async Task<IActionResult> Index()
        {
            var coupons = await _couponService.GetAllAsync();
            return View(coupons);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CUCouponDTO couponDto)
        {
            if (ModelState.IsValid)
            {
                await _couponService.CreateAsync(couponDto);
                return RedirectToAction(nameof(Index));
            }
            return View(couponDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var coupon = await _couponService.GetByIdAsync(id);
            if (coupon == null)
            {
                return NotFound();
            }
            return View(coupon);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CUCouponDTO couponDto)
        {
            if (ModelState.IsValid)
            {
                await _couponService.UpdateAsync(couponDto);
                return RedirectToAction(nameof(Index));
            }
            return View(couponDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var coupon = await _couponService.GetByIdAsync(id);
            if (coupon == null)
            {
                return NotFound();
            }
            return View(coupon);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _couponService.SoftDeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }

}
