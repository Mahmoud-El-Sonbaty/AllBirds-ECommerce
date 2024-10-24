﻿using AllBirds.Application.Contracts;
using AllBirds.Context;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Infrastructure
{
    public class AccountRoleRepository(AllBirdsContext context) : IAccountRoleRepository
    {
        public Task<IQueryable<IdentityUserRole<int>>> GetAllAccountRolesAsync() => Task.FromResult((IQueryable<IdentityUserRole<int>>)context.AccountRoles);
    }
}
