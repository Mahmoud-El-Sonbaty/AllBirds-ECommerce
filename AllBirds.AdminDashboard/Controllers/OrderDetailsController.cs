using AllBirds.Application.Services.OrderDetailsServices;
using AllBirds.DTOs.OrderDetailsDTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace AllBirds.AdminDashboard.Controllers
{
    public class OrderDetailsController : Controller
    {
        public IOrderDetailsService OrderService { get; set; }

        public OrderDetailsController(IOrderDetailsService orderDetailsService)
        {
            this.OrderService=orderDetailsService;


        }
        public IActionResult Index()
        {
            return View();

        }
     

        public async Task<IActionResult> GetAllOrderDetails()
        {
            var OrderDetail = (await OrderService.GetAllAsync());
            if (!OrderDetail.IsSuccess)
            {
                ViewBag.ErrMsg= OrderDetail.Msg;
            }


            return View(OrderDetail);

        }


        //public async Task<IActionResult> GetOneOrderDetails(int id)
        //{
        //    var OrderDetail = (await OrderService.GetByIdAsync(id));
        //    if (!OrderDetail.IsSuccess)
        //    {
        //        ViewBag.ErrMsg = OrderDetail.Msg;
        //    }

        //    return View(OrderDetail);

        //}


        //public async Task<IActionResult> CreateOrderDetails(CreateOrderDetailsDTO createOrderDetailsDTO)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(createOrderDetailsDTO);

        //    }
        //    var operationResult = await OrderService.GetByIdAsync(createOrderDetailsDTO.Id);
        //    if (!operationResult.IsSuccess)
        //    {

        //      var create=  await OrderService.CreateAsync(createOrderDetailsDTO);

        //        if (!create.IsSuccess)
        //        {
        //            ViewBag.ErrMsg = create.Msg;
        //        }

        //    }
        //     else
        //    {
        //       var update = await OrderService.UpdateAsync(createOrderDetailsDTO);
        //        if (!update.IsSuccess)
        //        {
        //            ViewBag.ErrMsg = update.Msg;
        //        }
        //    }

        //    return RedirectToAction("GetAllOrderDetails");
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
