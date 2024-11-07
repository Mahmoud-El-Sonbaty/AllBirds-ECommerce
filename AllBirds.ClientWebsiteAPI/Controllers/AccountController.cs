using AllBirds.Application.Services.AccountServices;
using AllBirds.DTOs.AccountDTOs;
using AllBirds.DTOs.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AllBirds.ClientWebsiteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;
        private readonly IConfiguration configuration;

        public AccountController(IAccountService _accountService, IConfiguration _configuration)
        {
            accountService = _accountService;
            configuration = _configuration;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(AccountLoginDTO accountLoginDTO)
        {
            ResultView<CUAccountDTO> loginResult = await accountService.LoginAsync(accountLoginDTO, false);
            if (loginResult.IsSuccess)
            {
                List<Claim> claims = new List<Claim>()
                {
                    new(ClaimTypes.Name , accountLoginDTO.Email.Split('@')[0]),
                    new(ClaimTypes.Email, accountLoginDTO.Email),
                    new(ClaimTypes.PrimarySid, loginResult.Data.Id.ToString()),
                    new(ClaimTypes.Role, "Client")
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:key"]));
                var token = new JwtSecurityToken(
                    issuer: configuration["jwt:Issuer"],
                    audience: configuration["jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddHours(2),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                    );
                string TokenStr = new JwtSecurityTokenHandler().WriteToken(token);
                return Ok(TokenStr);
            }
            else
            {
                return BadRequest(loginResult.Msg);
            }
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(ClientRegisterDTO cUAccountDTO)
        {
            ResultView<ClientRegisterDTO> resultView = await accountService.RegisterAsync(cUAccountDTO);
            if (resultView.IsSuccess)
            {
                return Created();
            }
            else
            {
                return BadRequest(resultView.Msg);
            }
        }
    }
}
