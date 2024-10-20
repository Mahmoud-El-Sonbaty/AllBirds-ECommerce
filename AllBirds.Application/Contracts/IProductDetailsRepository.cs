using AllBirds.DTOs.ProductDetailDTOs;
using AllBirds.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Application.Contracts
{
    public interface IProductDetailsRepository : IGenericRepository<ProductDetail, int>
    {
        //public async Task<CUProductDetails> GetByIdAsync(int id) => await DbSet<ProductDetail>.FindAsync(id);
        //public async Task<CUProductDetails> GetByIdAsync(int id) => await dbset.FindAsync(id);
    }
}
