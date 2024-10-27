using AllBirds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Application.Contracts
{
    public interface IProductColorRepository: IGenericRepository<ProductColor,int>
    {
    }
}
