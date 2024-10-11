using AllBirds.Application.Mapper;
using AllBirds.DTOs.AccountDTOs;
using AllBirds.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Application.Services.AccountServices
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<CustomUser> userManager;
        private readonly SignInManager<CustomUser> signInManager;
        private readonly RoleManager<IdentityRole<int>> roleManager;
        private readonly IMapper mapper;
        public AccountService(
            UserManager<CustomUser> _userManager, SignInManager<CustomUser> _signInManager,
            RoleManager<IdentityRole<int>> _roleManager, IMapper _mapper)
        {
            this.signInManager = _signInManager;
            this.userManager = _userManager;
            this.roleManager = _roleManager;
            this.mapper = _mapper;
        }

        public List<IdentityRole<int>> GetRoles() => [.. roleManager.Roles];

        public async Task<bool> LoginAsync(AccountLoginDTO accountLoginDTO)
        {
            CustomUser? findUserEmail = await userManager.FindByEmailAsync(accountLoginDTO.Email);
            if (findUserEmail is not null)
            {
                bool checkPassword = await userManager.CheckPasswordAsync(findUserEmail, accountLoginDTO.Password);
                if (checkPassword)
                {
                    await signInManager.SignInAsync(findUserEmail, accountLoginDTO.RememberMe);
                    return true;
                }
                // add to the msg password is incorrect for this email
            }
            // add to the msg this email doesn't exist
            return false;
        }

        public async Task<bool> RegisterAsync(CUAccountDTO cUAccountDTO)
        {
            CustomUser? findUserEmail = await userManager.FindByEmailAsync(cUAccountDTO.Email);
            if (findUserEmail == null)
            {
                if (cUAccountDTO.ImageData is not null)
                {
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(cUAccountDTO.ImageData.FileName);
                    if (!Directory.Exists(cUAccountDTO.ImagePath))
                    {
                        Directory.CreateDirectory(cUAccountDTO.ImagePath);
                    }
                    string filePath = Path.Combine(cUAccountDTO.ImagePath, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await cUAccountDTO.ImageData.CopyToAsync(fileStream);
                    }
                    cUAccountDTO.ImagePath = Path.Combine("/Images/Accounts/", uniqueFileName);
                }
                CustomUser mappedUser = mapper.Map<CustomUser>(cUAccountDTO);
                mappedUser.NormalizedEmail = cUAccountDTO.Email.ToUpper();
                mappedUser.UserName = cUAccountDTO.Email.Split("@")[0];
                mappedUser.NormalizedUserName = mappedUser.UserName.ToUpper();
                IdentityResult? userToCreate = await userManager.CreateAsync(user: mappedUser, password: cUAccountDTO.Password);
                if (userToCreate.Succeeded)
                {
                    if (cUAccountDTO.AccountRoles is not null && cUAccountDTO.AccountRoles.Count > 0)
                    {
                        foreach (int roleId in cUAccountDTO.AccountRoles)
                        {
                            IdentityRole<int>? role = await roleManager.FindByIdAsync(roleId.ToString());
                            if (role is not null)
                            {
                                IdentityResult roleToBind = await userManager.AddToRoleAsync(mapper.Map<CustomUser>(cUAccountDTO), role.Name);
                                if (!roleToBind.Succeeded)
                                {
                                    // add to the msg what happend and the roleToBind.Errors
                                }
                            }
                            else
                            {
                                // add to the msg that role doesn't exist in the db
                            }
                        }
                    }
                    return true;
                }
                // add to the msg that the user failed to create and send the userToCreate.Errors
                return false;
            }
            // add to the msg that this email already exist
            return false;
        }

        public async Task LogoutAsync()
        {
            await signInManager.SignOutAsync();
        }
    }
}
