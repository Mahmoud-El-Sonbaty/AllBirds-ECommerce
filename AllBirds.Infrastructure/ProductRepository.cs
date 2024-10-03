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
    public class ProductRepository(AllBirdsContext context) : GenericRepository<Product, int>(context), IProductRepository
    {
        //public async Task<Product> GetByIdAsync(int id) =>
        //    await context.Products.Include(p => p.Images).Include(p => p.AvailableSizes).Include(p => p.AvailableColors)
        //    .Include(p => p.Reviews).ThenInclude(r => r.Client)
        //    .FirstOrDefaultAsync(p => p.Id == id);
    }
}
