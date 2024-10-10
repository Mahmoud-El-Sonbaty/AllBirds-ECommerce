using AllBirds.DTOs.AccountDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Application.Services.AccountServices
{
    public interface IAccountService
    {
        public bool Login(UserLoginDTO userLoginDTO);
        public bool Register(ClientRegisterDTO clientRegisterDTO);
        public bool RegisterAdmin(AdminRegisterDTO adminregisterDTO);
    }
}
