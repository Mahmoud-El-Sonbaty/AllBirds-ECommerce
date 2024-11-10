using AllBirds.Application.Services.OrderDetailServices;
using AllBirds.Application.Services.OrderMasterServices;
using AllBirds.DTOs.OrderDetailsDTOs;
using AllBirds.DTOs.OrderMasterDTOs;
using AllBirds.DTOs.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUserCart()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid);
            if (userIdClaim is not null && int.TryParse(userIdClaim.Value, out int userId))
            {
                ResultView<GetUserCartCheckoutDTO> userCart = await orderMasterService.GetUserCartAsync(userId);
                if (userCart.IsSuccess)
                    return Ok(userCart);
                else
                    return NotFound(userCart.Msg);
            }
            else
            {
                return Unauthorized("Invalid token or user ID");
            }
        }




        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderMasterDTO createOrderMasterDTO)
        {
            if (ModelState.IsValid)
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid);
                if (userIdClaim is not null && int.TryParse(userIdClaim.Value, out int userId))
                {
                    createOrderMasterDTO.ClientId = userId;
                    ResultView<CreateOrderMasterDTO> createdOrderMaster = await orderMasterService.CreateAsync(createOrderMasterDTO);
                    if (createdOrderMaster.IsSuccess)
                        return Ok(createdOrderMaster);
                    else
                        return BadRequest(createdOrderMaster.Msg);
                }
                return Unauthorized();
            }
            return NotFound();
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateOrder(CreateOrderMasterDTO createOrderMasterDTO)
        {
            if (!ModelState.IsValid)
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid);
                if (userIdClaim is not null && int.TryParse(userIdClaim.Value, out int userId))
                {
                    createOrderMasterDTO.ClientId = userId;
                    ResultView<CreateOrderMasterDTO> createdOrderMaster = await orderMasterService.UpdateAsync(createOrderMasterDTO);
                    if (createdOrderMaster.IsSuccess)
                        return Ok(createdOrderMaster);
                    else
                        return BadRequest(createdOrderMaster.Msg);
                }
                return Unauthorized();
            }
            return NotFound();
        }

        [Authorize]
        [HttpDelete("DeleteOrderMaster")]
        public async Task<IActionResult> DeleteOrderMaster(int orderMasterId)
        {
            ResultView<GetOneOrderMasterDTO> deletedOrder = await orderMasterService.HardDeleteAsync(orderMasterId);
            if (deletedOrder.IsSuccess)
                return Ok(deletedOrder);
            else
                return BadRequest(deletedOrder.Msg);
        }

        [Authorize]
        [HttpDelete("DeleteOrderDetail")]
        public async Task<IActionResult> DeleteOrderDetail(int orderDetailId)
        {
            ResultView<GetOneOrderDetailsDTO> deletedOrderDetail = await orderDetailService.HardDeleteAsync(orderDetailId);
            if (deletedOrderDetail.IsSuccess)
                return Ok(deletedOrderDetail);
            else
                return BadRequest(deletedOrderDetail.Msg);
        }

        [Authorize]
        [HttpPatch("UpdateQuantity")]
        public async Task<IActionResult> UpdateQuantity(int orderDetailId, int newQuantity)
        {
            ResultView<CreateOrderDetailDTO> orderDetailUpdated = await orderDetailService.UpdataQuantityAsync(orderDetailId, newQuantity);
            if (orderDetailUpdated.IsSuccess)
                return Ok(orderDetailUpdated);
            else
                return BadRequest(orderDetailUpdated.Msg);
        }



        [Authorize]
        [HttpGet("PlaceOrder")]
        public async Task<IActionResult> PlaceOrder() // here we should minus the units of stock of the products in the order details
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid);
            if (userIdClaim is not null && int.TryParse(userIdClaim.Value, out int userId))
            {
                ResultView<CreateOrderMasterDTO> resultView = await orderMasterService.PlaceOrderAsync(userId);
                if (resultView.IsSuccess)
                {
                    return Ok(resultView);
                }
                return BadRequest(resultView.Msg);
            }
            return Unauthorized();
        }

        [Authorize]
        [HttpGet("GetAllClientOrders")]
        public async Task<IActionResult> GetByUser()
       {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid);
            if (userIdClaim is not null && int.TryParse(userIdClaim.Value, out int userId))
            {
                ResultView<List<GetAllClientOrderMasterDTO>> resultView = await orderMasterService.GetByUserAsync(userId);
                if (resultView.IsSuccess)
                {
                    return Ok(resultView);
                }
                return BadRequest(resultView.Msg);
            }
            return Unauthorized();
        }







        /*=======================================================================
                                    for localization 
        ========================================================================*/


        [Authorize]
        [HttpGet]
        [Route("{Lang:twoLetterLang}")]
        public async Task<IActionResult> GetUserCartWithLang(string Lang)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid);
            if (userIdClaim is not null && int.TryParse(userIdClaim.Value, out int userId))
            {
                ResultView<GetUserCartCheckOutWithLangDTO> userCart = await orderMasterService.GetUserCartWithLangAsync(userId,Lang);
                if (userCart.IsSuccess)
                    return Ok(userCart);
                else
                    return NotFound();
            }
            else
            {
                return Unauthorized("Invalid token or user ID");
            }

        }




        [Authorize]
        [HttpGet]
        [Route("GetAllClientOrders/{Lang:twoLetterLang}")]

        public async Task<IActionResult> GetByUser(string Lang)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid);
            if (userIdClaim is not null && int.TryParse(userIdClaim.Value, out int userId))
            {
                ResultView<List<GetAllClientOrderMasterDTO>> resultView = await orderMasterService.GetByUserWithLangAsync(userId,Lang);
                if (resultView.IsSuccess)
                {
                    return Ok(resultView);
                }
                return BadRequest(resultView.Msg);
            }
            return Unauthorized();
        }




    }
}
