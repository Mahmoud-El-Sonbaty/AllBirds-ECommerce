using AllBirds.DTOs.OrderDetailsDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Application.Services.OrderDetailsServices
{
    public interface IOrderDetailsService
    {
        public Task<CreateOrderDetailsDTO> CreateAsync(CreateOrderDetailsDTO createOrderMDTo);
        public Task<CreateOrderDetailsDTO> UpdateAsync(CreateOrderDetailsDTO createOrderMDTo);
        public Task<GetOneOrderDetailsDTO> SoftDeleteAsync(int OrderID);
        public Task<GetOneOrderDetailsDTO> HardDeleteAsync(int OrderID);
        public Task<List<GetAllOrderDetailsDTO>> GetAllAsync();
        public Task<List<GetAllOrderDetailsDTO>> GetAllWithDeletedAsync();
        public Task<GetOneOrderDetailsDTO> GetByIdAsync(int OrderId);
    }
}
