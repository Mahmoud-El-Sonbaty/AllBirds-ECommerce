using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.DTOs.UserDTOs
{
    public record GetAccountAdminDTOs
    {
        public string UserName { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
    }
}
