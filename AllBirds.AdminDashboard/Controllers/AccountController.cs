using AllBirds.Application.Services.AccountServices;
using AllBirds.DTOs.AccountDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            ViewBag.Roles = accountService.GetRoles();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(CUAccountDTO cUAccountDTO)
        {
            if (ModelState.IsValid)
            {
                cUAccountDTO.ImagePath = Path.Combine(new string[] { webHostEnvironment.WebRootPath, "Images", "Accounts" });
            }
            return await accountService.RegisterAsync(cUAccountDTO) ? RedirectToAction("Login") : View();
        }

        [HttpGet]
        public async Task<IActionResult> Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginDTO accountLoginDTO)
        {
            if (ModelState.IsValid)
            {
                if (await accountService.LoginAsync(accountLoginDTO))
                {
                    return RedirectToAction("DashBoard");
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
