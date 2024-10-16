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
            var coupons = await _couponService.GetAllCouponsAsync();
            return View(coupons);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CouponDTO couponDto)
        {
            if (ModelState.IsValid)
            {
                await _couponService.AddCouponAsync(couponDto);
                return RedirectToAction(nameof(Index));
            }
            return View(couponDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var coupon = await _couponService.GetCouponByIdAsync(id);
            if (coupon == null)
            {
                return NotFound();
            }
            return View(coupon);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CouponDTO couponDto)
        {
            if (ModelState.IsValid)
            {
                await _couponService.UpdateCouponAsync(couponDto);
                return RedirectToAction(nameof(Index));
            }
            return View(couponDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var coupon = await _couponService.GetCouponByIdAsync(id);
            if (coupon == null)
            {
                return NotFound();
            }
            return View(coupon);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _couponService.DeleteCouponAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }

}
