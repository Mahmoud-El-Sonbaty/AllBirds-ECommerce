using AllBirds.Application.Services.OrderDetailServices;
using AllBirds.Application.Services.OrderMasterServices;
using AllBirds.DTOs.OrderDetailsDTOs;
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
        private readonly IOrderDetailService orderDetailService;

        public OrderController(IOrderMasterService _orderMasterService, IOrderDetailService _orderDetailService)
        {
            orderMasterService = _orderMasterService;
            orderDetailService = _orderDetailService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserCart(int userId)
        {
            ResultView<GetUserCartCheckoutDTO> userCart = await orderMasterService.GetUserCartAsync(userId);
            if (userCart.IsSuccess)
                return Ok(userCart);
            else
                return NotFound();

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

        [HttpDelete("DeleteOrderMaster")]
        public async Task<IActionResult> DeleteOrderMaster(int orderMasterId)
        {
            ResultView<GetOneOrderMasterDTO> deletedOrder = await orderMasterService.HardDeleteAsync(orderMasterId);
            if (deletedOrder.IsSuccess)
                return Ok(deletedOrder);
            else
                return BadRequest(deletedOrder.Msg);
        }

        [HttpDelete("DeleteOrderDetail")]
        public async Task<IActionResult> DeleteOrderDetail(int orderDetailId)
        {
            ResultView<GetOneOrderDetailsDTO> deletedOrderDetail = await orderDetailService.HardDeleteAsync(orderDetailId);
            if (deletedOrderDetail.IsSuccess)
                return Ok(deletedOrderDetail);
            else
                return BadRequest(deletedOrderDetail.Msg);
        }

        [HttpPatch("UpdateQuantity")]
        public async Task<IActionResult> UpdateQuantity(int orderDetailId, int newQuantity)
        {
            ResultView<CreateOrderDetailDTO> orderDetailUpdated = await orderDetailService.UpdataQuantityAsync(orderDetailId, newQuantity);
            if (orderDetailUpdated.IsSuccess)
                return Ok(orderDetailUpdated);
            else
                return BadRequest(orderDetailUpdated.Msg);
        }
    }
}
