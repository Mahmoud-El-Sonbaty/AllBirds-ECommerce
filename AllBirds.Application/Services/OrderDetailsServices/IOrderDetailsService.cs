using AllBirds.DTOs.OrderDetailsDTOs;
using AllBirds.DTOs.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Application.Services.OrderDetailsServices
{
    public interface IOrderDetailsService
    {
        public Task<ResultView<CreateOrderDetailsDTO>> CreateAsync(CreateOrderDetailsDTO createOrderMDTo);
        public Task<ResultView<CreateOrderDetailsDTO>> UpdateAsync(CreateOrderDetailsDTO createOrderMDTo);
        public Task<ResultView<GetOneOrderDetailsDTO>> SoftDeleteAsync(int OrderID);
        public Task<ResultView<GetOneOrderDetailsDTO>> HardDeleteAsync(int OrderID);
        public Task<ResultView<List<GetAllOrderDetailsDTO>>> GetAllAsync();
        public Task<ResultView<List<GetAllOrderDetailsDTO>>> GetAllWithDeletedAsync();
        public Task<ResultView<GetOneOrderDetailsDTO>> GetByIdAsync(int OrderId);
    }
}
