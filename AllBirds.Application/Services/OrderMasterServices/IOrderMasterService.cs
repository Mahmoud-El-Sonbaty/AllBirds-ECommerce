using AllBirds.DTOs.OrderMasterDTOs;
using AllBirds.DTOs.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Application.Services.OrderMasterServices
{
    public interface IOrderMasterService
    {
        public Task<ResultView<createOrderMasterDTO>> CreateAsync(createOrderMasterDTO createOrderMDTo);
        public Task<ResultView<createOrderMasterDTO>> UpdateAsync(createOrderMasterDTO createOrderMDTo);
        public Task<ResultView<GetOneOdrerMasterDTO>> SoftDeleteAsync(int OrderID);
        public Task<ResultView<GetOneOdrerMasterDTO>> HardDeleteAsync(int OrderID);
        public Task<ResultView<List<GetAllOrderMastersDTO>>> GetAllAsync();
        public Task<ResultView<List<GetAllOrderMastersDTO>>> GetAllWithDeletedAsync();
        public Task<ResultView<GetOneOdrerMasterDTO>> GetByIdAsync(int OrderId);
        public Task<ResultView<bool>> ChangingStateAsync(int StateId, int OrderID);
    }
}
