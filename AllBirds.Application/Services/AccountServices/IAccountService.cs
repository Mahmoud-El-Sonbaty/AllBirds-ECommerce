using AllBirds.DTOs.AccountDTOs;
using AllBirds.DTOs.Shared;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Application.Services.AccountServices
{
    public interface IAccountService
    {
        public Task<ResultView<GetAllAdminsDTO>> GetUserById(string id);
        public Task<ResultView<List<GetAllAdminsDTO>>> GetAllAsync(string role);
        public List<IdentityRole<int>> GetRoles();
        public Task<ResultView<CUAccountDTO>> LoginAsync(AccountLoginDTO accountLoginDTO, bool mustMod = false);
        public Task<ResultView<ClientRegisterDTO>> RegisterAsync(ClientRegisterDTO cUAccountDTO);
        public Task<ResultView<CUAccountDTO>> AddModerator(CUAccountDTO cUAccountDTO);
        public Task LogoutAsync();
        public Task<ResultView<CUAccountDTO>> DeleteAsync(int userId);
    }
}
