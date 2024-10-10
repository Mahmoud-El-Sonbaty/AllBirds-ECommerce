using AllBirds.Application.Services.AccountServices;
using AllBirds.DTOs.AccountDTOs;
using Microsoft.AspNetCore.Mvc;

namespace AllBirds.AdminDashboard.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;
        public AccountController(IAccountService _accountService)
        {
            this.accountService = _accountService;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(ClientRegisterDTO clientRegisterDTO)
        {
            accountService.Register(clientRegisterDTO);
            return RedirectToAction("Login");
        }
    }
}
