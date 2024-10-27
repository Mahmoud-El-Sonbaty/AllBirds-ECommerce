using AllBirds.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Application.Contracts
{
    public interface IAccountRoleRepository
    {
        public Task<IQueryable<IdentityUserRole<int>>> GetAllAccountRolesAsync();
    }
}
