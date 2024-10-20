using AllBirds.Application.Services.AccountServices;
using AllBirds.Application.Services.OrderServices;
using AllBirds.Application.Services.OrderStateServices;
using AllBirds.DTOs.OrderDetailsDTOs;
using AllBirds.DTOs.OrderMasterDTOs;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AllBirds.AdminDashboard.Controllers
{
    public class OrderMasterController : Controller
    {
        public IOrderMasterService OrderService { get; set; }

        public OrderMasterController(IOrderMasterService _orderService
                                      )
        {
            this.OrderService = _orderService;  
        }
        public IActionResult Index()
        {
            return View();

        }
        public async Task< IActionResult> changingState(int stateID ,int orderID )
        {
            

           var item= await OrderService.ChangingStateAsync(stateID, orderID);
            if (item)
            {
                return RedirectToAction("GetAllOrderMasters");

            }
            else
            {
                Debug.WriteLine("order not Found");
                return RedirectToAction("GetAllOrderMasters");

            }

        }

        public async Task<IActionResult> GetAllOrderMasters()
        {
            var OrderMaster = (await OrderService.GetAllAsync()).ToList();
            
           
            return View(OrderMaster);

        }


        //public async Task<IActionResult> GetOneOrderMaster(int id )
        //{
        //    var OrderMaster = (await OrderService.GetByIdAsync(id));

        //    return View(OrderMaster);

        //}


        //public async Task<IActionResult> CreateOrderMaster(createOrderMasterDTO orderMasterDTO)
        //{
        //    if(!ModelState.IsValid)
        //    {
        //        return View(orderMasterDTO);

        //    }
        //    var operationResult = await OrderService.GetByIdAsync(orderMasterDTO.Id);
        //    if (operationResult == null)
        //    { 

        //        await OrderService.CreateAsync(orderMasterDTO);
        //    }
        //    else
        //    {
        //        await OrderService.UpdateAsync(orderMasterDTO);
        //    }

        //   return RedirectToAction("GetAllOrderMasters");
        //}

        //public async Task< IActionResult> DeletOrderMaster(int num,int Id)
        //{
        //    GetOneOdrerMasterDTO item;
        //    if (num == 0)
        //    {
        //        item = await OrderService.HardDeleteAsync(Id);

        //    }
        //    else
        //    {
        //        item = await OrderService.SoftDeleteAsync(Id);

        //    }
        //    return View(item);

        //}
    }
}
