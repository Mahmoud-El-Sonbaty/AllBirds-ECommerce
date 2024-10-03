using AllBirds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Application.Contracts
{
    public interface IProductRepository : IGenericRepository<Product, int>
    {
        //public Task<Product> GetByIdAsync(int id);
    }
}
