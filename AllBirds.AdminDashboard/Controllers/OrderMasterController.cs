using AllBirds.Application.Services.AccountServices;
using AllBirds.Application.Services.OrderMasterServices;
using AllBirds.Application.Services.OrderStateServices;
using AllBirds.DTOs.OrderDetailsDTOs;
using AllBirds.DTOs.OrderMasterDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.Diagnostics;

namespace AllBirds.AdminDashboard.Controllers
{
    public class OrderMasterController : Controller
    {
        public IOrderMasterService OrderService { get; set; }
        public IOrderStateService OrderSateService { get; set; }

        public OrderMasterController(IOrderMasterService _orderService,
                                       IOrderStateService _orderSate
                                      )
        {
            this.OrderService = _orderService; 
            this.OrderSateService = _orderSate;
        }
        public IActionResult Index()
        {
            return View();

        }
        public async Task< IActionResult> changingState(int stateID ,int orderID )
        {
            

           var item= await OrderService.ChangingStateAsync(stateID, orderID);
            if (item.IsSuccess)
            {
                return RedirectToAction("GetAllOrderMasters");

            }
            else
            {
                ViewBag.ErrMsg=item.Msg;
                return RedirectToAction("GetAllOrderMasters");

            }

        }

        public async Task<IActionResult> GetAllOrderMasters()
        {
            var OrderMaster = await OrderService.GetAllAsync();
            var orderState =  await OrderSateService.GetAllAsync();
            ViewBag.OrderSate = orderState;

            if (OrderMaster.IsSuccess)
            {
              
                return View(OrderMaster.Data);  

            }
            else
            {
                ViewBag.ErrMsg = OrderMaster.Msg;

                return View(OrderMaster.Data);
            }
          

        }


        //public async Task<IActionResult> GetOneOrderMaster(int id)
        //{
        //    var OrderMaster = (await OrderService.GetByIdAsync(id));
        //    if (OrderMaster.IsSuccess)
        //    {

        //        return View(OrderMaster.Data);

        //    }
        //    else
        //    {
        //        ViewBag.ErrMsg = OrderMaster.Msg;

        //        return View(OrderMaster.Data);
        //    }

        //}


        //public async Task<IActionResult> CreateOrderMaster(createOrderMasterDTO orderMasterDTO)
        //{

        //    if (!ModelState.IsValid)
        //    {
        //        return View(orderMasterDTO);

        //    }
        //    var operationResult = await OrderService.GetByIdAsync(orderMasterDTO.Id);
        //    if (!operationResult.IsSuccess)
        //    {

        //        var create=await OrderService.CreateAsync(orderMasterDTO);
        //        if (!create.IsSuccess) {
        //           ViewBag.ErrMsg = create.Msg;

        //        }
        //    }
        //    else
        //    {
        //        var update = await OrderService.UpdateAsync(orderMasterDTO);
        //        if (!update.IsSuccess)
        //        {
        //            ViewBag.ErrMsg = update.Msg;

        //        }
        //    }

        //    return RedirectToAction("GetAllOrderMasters");
        //}

        //public async Task<IActionResult> DeletOrderMaster(int num, int Id)
        //{
        //    dynamic item;
        //    if (num == 0)
        //    {
        //        item = await OrderService.HardDeleteAsync(Id);
        //        ViewBag.Msg = item.Msg;
               

        //    }
        //    else
        //    {
        //        item = await OrderService.SoftDeleteAsync(Id);
        //        ViewBag.Msg = item.Msg;


        //    }
        //    return View(item);

        //}
    }
}
