using AllBirds.Application.Contracts;
using AllBirds.Context;
using AllBirds.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Infrastructure
{
    public class OrderMasterRepository(AllBirdsContext context) : GenericRepository<OrderMaster, int>(context), IOrderMasterRepository
    {
        public async Task<OrderMaster> GetOneAsync(int id) =>
              await context.Set<OrderMaster>().FirstOrDefaultAsync(e => e.Id.Equals(id) && e.IsDeleted == false);
    }
}
