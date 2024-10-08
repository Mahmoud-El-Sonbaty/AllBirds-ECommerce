using AllBirds.DTOs.UserDTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AllBirds.Presentaion.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager , RoleManager<IdentityRole> roleManager )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register() 
        {
            var modelDTO = new AdminAccRegisterDTOs();
            return View(modelDTO);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AdminAccRegisterDTOs addminAcc)
        {
            if(ModelState.IsValid)
            {
                var UserName = await _userManager.FindByNameAsync(addminAcc.UserName);
                if(UserName != null)
                {
                    return Json(new { success = false, message = "User Name Already Exist ." });
                }

               var Email = await _userManager.FindByEmailAsync(addminAcc.Email);
                
                if(Email != null)
                {
                    return Json(new {success = false , message ="Email Already Exist ."});
                }
                IdentityUser identityUser = new IdentityUser()
                {
                     UserName = addminAcc.UserName,
                      Email = addminAcc.Email,
                };
                var UserIdentity = await _userManager.CreateAsync(identityUser , addminAcc.Password);
                if(UserIdentity.Succeeded)
                {
                        var role = new IdentityRole() { Name = "Admin"};
                    if(!await _roleManager.RoleExistsAsync("Admin"))
                    {
                         var ResRole = await _roleManager.CreateAsync(role);
                        if(ResRole.Succeeded)
                        {
                           await _userManager.AddToRoleAsync(identityUser, role.Name);
                           await _signInManager.SignInAsync(identityUser, isPersistent: true);
                        }
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(identityUser, role.Name);
                        await _signInManager.SignInAsync(identityUser, isPersistent: true);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    var Errors = UserIdentity.Errors.Select(P => P.Description).ToList();
                    return Json(new { success = false , Errors });
                }
            }

                return Json(new { success = false , errors = ModelState.Values.SelectMany(X => X.Errors).Select(P => P.ErrorMessage) });
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginUserDTOs loginUser , bool rememberMe)
        {
           var User =await _userManager.FindByEmailAsync(loginUser.Email);
            if (User == null)
            {
                return Json(false);
            }
            var password = await _userManager.CheckPasswordAsync(User, loginUser.Password);
            if (!password)
            {
                return Json(false);
            }
            await _signInManager.SignInAsync(User, isPersistent: rememberMe);
            return RedirectToAction("Index", "Home");

        }


        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
