using AllBirds.Application.Services.AccountServices;
using AllBirds.DTOs.AccountDTOs;
using AllBirds.DTOs.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AllBirds.AdminDashboard.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;
        private readonly IWebHostEnvironment webHostEnvironment;
        public AccountController(IAccountService _accountService, IWebHostEnvironment _webHostEnvironment)
        {
            this.webHostEnvironment = _webHostEnvironment;
            this.accountService = _accountService;
        }

        [HttpGet, Authorize(Roles = "SuperUser,Manager,Admin")]
        public async Task<IActionResult> GetAll(string role)
        {
            var dd = ViewBag;
            var fd = HttpContext;
            ResultView<List<GetAllAdminsDTO>> adminsResult = await accountService.GetAllAsync(role);
            if (!adminsResult.IsSuccess)
            {
                TempData.Add("IsSuccess", false);
                TempData.Add("Msg", adminsResult.Msg);
            }
            return View(adminsResult.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            ViewBag.Roles = accountService.GetRoles();
            return View();
        }

        // should be converted to add admin
        [HttpPost]
        public async Task<IActionResult> Register(CUAccountDTO cUAccountDTO)
        {
            if (ModelState.IsValid)
            {
                cUAccountDTO.ImagePath = Path.Combine(new string[] { webHostEnvironment.WebRootPath, "Images", "Accounts" });
            }
            return await accountService.RegisterAsync(cUAccountDTO) is not null ? RedirectToAction("Login") : View();
        }

        [HttpGet]
        public async Task<IActionResult> Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginDTO accountLoginDTO)
        {
            if (ModelState.IsValid)
            {
                if ((await accountService.LoginAsync(accountLoginDTO, true)).IsSuccess)
                {
                    ResultView<GetAllAdminsDTO> getUserRes = await accountService.GetUserById(User.Claims.FirstOrDefault()?.Value);
                    if (getUserRes.IsSuccess)
                    {
                        // all those disappear once you redirect so we have to store it in cookies i think
                        ViewBag.UserId = getUserRes.Data.Id;
                        ViewBag.Name = getUserRes.Data.FirstName + " " + getUserRes.Data.LastName;
                        ViewBag.Img = getUserRes.Data.ImagePath;
                        ViewBag.Role = "Super User";
                        User.AddIdentity(new(new List<Claim>() { new("UserImg", getUserRes.Data.ImagePath), new("Name", $"{getUserRes.Data.FirstName} {getUserRes.Data.LastName}"), new("Role", "testrole") }));
                    }
                    return Redirect("/");
                }
            }
            return View();
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> Logout()
        {
            await accountService.LogoutAsync();
            return RedirectToAction("Login");
        }
    }
}
