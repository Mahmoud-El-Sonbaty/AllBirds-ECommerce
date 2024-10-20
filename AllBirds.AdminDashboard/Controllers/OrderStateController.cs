using AllBirds.Application.Services.OrderStateServices;
using AllBirds.DTOs.OrderStateDTOs;
using Microsoft.AspNetCore.Mvc;

namespace AllBirds.AdminDashboard.Controllers
{
    public class OrderStateController : Controller
    {
        private readonly IOrderStateService _orderStateService;

        public OrderStateController(IOrderStateService orderStateService)
        {
            _orderStateService = orderStateService;
        }

        public async Task<IActionResult> Index()
        {
            var orderStates = await _orderStateService.GetAllAsync();
            return View(orderStates);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CUOrderStateDTO orderStateDto)
        {
            if (ModelState.IsValid)
            {
                await _orderStateService.CreateAsync(orderStateDto);
                return RedirectToAction(nameof(Index));
            }
            return View(orderStateDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var orderState = await _orderStateService.GetByIdAsync(id);
            if (orderState == null)
            {
                return NotFound();
            }
            return View(orderState);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CUOrderStateDTO orderStateDto)
        {
            if (ModelState.IsValid)
            {
                await _orderStateService.UpdateAsync(orderStateDto);
                return RedirectToAction(nameof(Index));
            }
            return View(orderStateDto);
        }

        //
        public async Task<IActionResult> Delete(int id)
        {
            var orderState = await _orderStateService.GetByIdAsync(id);
            if (orderState == null)
            {
                return NotFound();
            }
            return View(orderState);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, bool hardDelete)
        {
            if (hardDelete)
            {
                await _orderStateService.HardDeleteAsync(id);
            }
            else
            {
                await _orderStateService.SoftDeleteAsync(id);
            }
            return RedirectToAction(nameof(Index));
        }
    }


}
