using AllBirds.DTOs.OrderMasterDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Application.Services.OrderServices
{
    public interface IOrderMasterService
    {
        public Task<createOrderMasterDTO> CreateAsync(createOrderMasterDTO createOrderMDTo);
        public Task<createOrderMasterDTO> UpdateAsync(createOrderMasterDTO createOrderMDTo);
        public Task<GetOneOdrerMasterDTO> SoftDeleteAsync(int OrderID);
        public Task<GetOneOdrerMasterDTO> HardDeleteAsync(int OrderID);
        public Task<List<GetAllOrderMastersDTO>> GetAllAsync();
        public Task<List<GetAllOrderMastersDTO>> GetAllWithDeletedAsync();
        public Task<GetOneOdrerMasterDTO> GetByIdAsync(int OrderId);
        public  Task<bool> ChangingStateAsync(int StateId,int OrderID);
    }
}
