using AllBirds.Application.Services.ColorServices;
using AllBirds.Application.Services.CouponServices;
using AllBirds.Application.Services.OrderStateServices;
using AllBirds.Application.Services.SizeServices;
using AllBirds.DTOs.ColorDTOs;
using Microsoft.AspNetCore.Mvc;

namespace AllBirds.AdminDashboard.Controllers
{
    public class ColorController : Controller
    {
        private readonly IColorService _colorService;
        private readonly ISizeService _sizeService;
        private readonly IOrderStateService _orderStateService;
        private readonly ICouponService _couponService;

        public ColorController(IColorService colorService,ISizeService sizeService,IOrderStateService orderStateService,ICouponService couponService)
        {
            _colorService = colorService;
            _sizeService = sizeService;
            _orderStateService = orderStateService;
            _couponService = couponService;
        }
        public async Task<IActionResult> GetALLForC_CO_S_OS()
        {

            var AllColor=await _colorService.GetAllAsync();
            var AllSize=await _sizeService.GetAllAsync();
            var AllCoupon=await _couponService.GetAllAsync();
            var AllOrderState=await _orderStateService.GetAllAsync();
            var Model = new GetAll
            {
                cUColorDTOs =AllColor,
                cUSizeDTOs =AllSize,
                cUCoupons=AllCoupon,
                OrderStateDTOs=AllOrderState,
            };
            return View(Model);
        }
        public async Task<IActionResult> Index()
        {
            var colors = await _colorService.GetAllAsync();
            return View(colors);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CUColorDTO colorDto)
        {
            if (ModelState.IsValid)
            {
                await _colorService.CreateAsync(colorDto);
                return RedirectToAction(nameof(GetALLForC_CO_S_OS));
            }
            return View(colorDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var color = await _colorService.GetByIdAsync(id);
            if (color == null)
            {
                return NotFound();
            }
            return View(color);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CUColorDTO colorDto)
        {
            if (ModelState.IsValid)
            {
                await _colorService.UpdateAsync(colorDto);
                return RedirectToAction(nameof(Index));
            }
            return View(colorDto);
        }
        //---------------------------

        public async Task<IActionResult> Delete(int id)
        {
            var color = await _colorService.GetByIdAsync(id);
            if (color == null)
            {
                return NotFound();
            }
            return View(color);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, bool hardDelete)
        {
            if (hardDelete)
            {
                await _colorService.HardDeleteAsync(id);
            }
            else
            {
                await _colorService.SoftDeleteAsync(id);
            }
            return RedirectToAction(nameof(Index));
        }












        //-----------------

        //public async Task<IActionResult> Delete(int id)
        //{
        //    var color = await _colorService.GetByIdAsync(id);
        //    if (color == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(color);
        //}

        //[HttpPost]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    await _colorService.SoftDeleteAsync(id);
        //    return RedirectToAction(nameof(Index));
        //}

        //[HttpPost]
        //public async Task<IActionResult> HardDelete(int id)
        //{
        //    await _colorService.HardDeleteAsync(id);
        //    return RedirectToAction(nameof(Index));
        //}

    }

}
