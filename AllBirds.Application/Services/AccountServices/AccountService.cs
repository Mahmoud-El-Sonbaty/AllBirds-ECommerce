using AllBirds.DTOs.AccountDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Application.Services.AccountServices
{
    public class AccountService : IAccountService
    {
        public bool Login(UserLoginDTO userLoginDTO)
        {
            throw new NotImplementedException();
        }

        public bool Register(ClientRegisterDTO clientRegisterDTO)
        {
            throw new NotImplementedException();
        }

        public bool RegisterAdmin(AdminRegisterDTO adminregisterDTO)
        {
            throw new NotImplementedException();
        }
    }
}
