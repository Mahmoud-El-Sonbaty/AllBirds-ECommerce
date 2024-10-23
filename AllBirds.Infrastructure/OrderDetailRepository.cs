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
    public class OrderDetailRepository(AllBirdsContext context) : GenericRepository<OrderDetail, int>(context), IOrderDetailRepository
    {
        public async Task<OrderDetail> GetOneAsync(int id) =>
            await context.Set<OrderDetail>().FirstOrDefaultAsync(e => e.Id.Equals(id) && e.IsDeleted == false);
    }
}
