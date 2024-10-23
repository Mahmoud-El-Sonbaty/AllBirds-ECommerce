using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllBirds.Models;
namespace AllBirds.Application.Contracts
{
    public interface ICategoryProductRepository : IGenericRepository<CategoryProduct, int>
    {
    }
}
