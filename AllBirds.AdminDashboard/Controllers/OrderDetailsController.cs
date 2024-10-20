using AllBirds.Application.Services.OrderDetailsServices;
using Microsoft.AspNetCore.Mvc;

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
            var OrderMaster = (await OrderService.GetAllAsync()).ToList();


            return View(OrderMaster);

        }


        //public async Task<IActionResult> GetOneOrderDetails(int id )
        //{
        //    var OrderDetail = (await OrderService.GetByIdAsync(id));

        //    return View(OrderDetail);

        //}


        //public async Task<IActionResult> CreateOrderDetails(createOrderDetailsDTO createOrderDetailsDTO)
        //{
        //    if(!ModelState.IsValid)
        //    {
        //        return View(createOrderDetailsDTO);

        //    }
        //    var operationResult = await OrderService.GetByIdAsync(createOrderDetailsDTO.Id);
        //    if (operationResult == null)
        //    { 

        //        await OrderService.CreateAsync(createOrderDetailsDTO);
        //    }
        //    else
        //    {
        //        await OrderService.UpdateAsync(createOrderDetailsDTO);
        //    }

        //   return RedirectToAction("GetAllOrderDetails");
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
