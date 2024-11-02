using AllBirds.Application.Services.OrderMasterServices;
using AllBirds.DTOs.OrderMasterDTOs;
using AllBirds.DTOs.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AllBirds.ClientWebsiteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderMasterService orderMasterService;

        public OrderController(IOrderMasterService _orderMasterService)
        {
            orderMasterService = _orderMasterService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderMasterDTO createOrderMasterDTO)
        {
            if (ModelState.IsValid)
            {
                ResultView<CreateOrderMasterDTO> createdOrderMaster = await orderMasterService.CreateAsync(createOrderMasterDTO);
                return Ok(createdOrderMaster);
            }
            return NotFound();
        }
    }
}
