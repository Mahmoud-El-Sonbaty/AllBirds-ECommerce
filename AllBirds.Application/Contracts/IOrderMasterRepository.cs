using AllBirds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Application.Contracts
{
    public interface IOrderMasterRepository:IGenericRepository<OrderMaster,int>
    {
        public Task<OrderMaster> GetOneAsync(int id);

    }
}
