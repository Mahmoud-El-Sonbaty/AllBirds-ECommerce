using AllBirds.Application.Contracts;
using AllBirds.Context;
using AllBirds.DTOs.ProductDetailDTOs;
using AllBirds.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Infrastructure
{
    public class ProductDetailsRepository : GenericRepository<ProductDetail , int> , IProductDetailsRepository
    {
        public ProductDetailsRepository(AllBirdsContext context) : base(context)
        {
            
        }


    }
}
