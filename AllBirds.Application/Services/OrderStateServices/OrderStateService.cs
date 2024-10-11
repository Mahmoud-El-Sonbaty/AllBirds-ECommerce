using AllBirds.Application.Contracts;
using AllBirds.DTOs.OrderStateDTOs;
using AllBirds.Models;
using AutoMapper;

namespace AllBirds.Application.Services.OrderStateServices
{
    public class OrderStateService : IOrderStateService
    {
        private readonly IOrderStateRepository _orderStateRepository;
        private readonly IMapper _mapper;

        public OrderStateService(IOrderStateRepository orderStateRepository, IMapper mapper)
        {
            _orderStateRepository = orderStateRepository;
            _mapper = mapper;
        }

        public async Task<CUOrderStateDTO> CreateAsync(CUOrderStateDTO cUorderStateDTO)
        {
            OrderState mappedorderState = _mapper.Map<OrderState>(cUorderStateDTO);
            OrderState createdorderState = await _orderStateRepository.CreateAsync(mappedorderState);
            await _orderStateRepository.SaveChangesAsync();
            return _mapper.Map<CUOrderStateDTO>(createdorderState);
        }

        public async Task<CUOrderStateDTO> UpdateAsync(CUOrderStateDTO cUorderStateDTO)
        {
            OrderState? orderStateObj = (await _orderStateRepository.GetAllAsync()).FirstOrDefault(s => s.Id == cUorderStateDTO.Id && s.IsDeleted == false);
            if (orderStateObj is not null)
            {
                orderStateObj.StateAr = cUorderStateDTO.StateAr;
                orderStateObj.StateEn = cUorderStateDTO.StateEn;
                await _orderStateRepository.SaveChangesAsync();
                return _mapper.Map<CUOrderStateDTO>(orderStateObj);
            }
            return null;
        }

        public async Task<CUOrderStateDTO> SoftDeleteAsync(int orderStateId)
        {
            OrderState? orderStateObj = (await _orderStateRepository.GetAllAsync()).FirstOrDefault(s => s.Id == orderStateId && s.IsDeleted == false);
            if (orderStateObj is not null)
            {
                orderStateObj.IsDeleted = true;
                await _orderStateRepository.SaveChangesAsync();
                return _mapper.Map<CUOrderStateDTO>(orderStateObj);
            }
            return null;
        }
        public async Task<CUOrderStateDTO> HardDeleteAsync(int orderStateId) // will this throw tracking exception ??
        {
            OrderState? orderStateObj = (await _orderStateRepository.GetAllAsync()).FirstOrDefault(s => s.Id == orderStateId && s.IsDeleted == false);
            if (orderStateObj is not null)
            {
                OrderState deletedorderState = await _orderStateRepository.DeleteAsync(orderStateObj);
                await _orderStateRepository.SaveChangesAsync();
                return _mapper.Map<CUOrderStateDTO>(deletedorderState);
            }
            return null;
        }

        public async Task<List<CUOrderStateDTO>> GetAllAsync()
        {
            //List<orderState> orderStatesList = (await _orderStateRepository.GetAllAsync()).Where(s => s.IsDeleted == false).ToList();
            // this is called collection expression
            List<OrderState> orderStatesList = [.. (await _orderStateRepository.GetAllAsync()).Where(s => !s.IsDeleted)];
            return _mapper.Map<List<CUOrderStateDTO>>(orderStatesList);
        }

        public async Task<List<CUOrderStateDTO>> GetAllWithDeletedAsync()
        {
            List<OrderState> orderStatesList = [.. (await _orderStateRepository.GetAllAsync())];
            return _mapper.Map<List<CUOrderStateDTO>>(orderStatesList);
        }
        public async Task<GetOrderStateDTO> GetByIdAsync(int orderStateId)
        {
            IQueryable<OrderState> orderStateList = await _orderStateRepository.GetAllAsync();
            OrderState? orderStateObj = orderStateList.FirstOrDefault(s => s.Id == orderStateId && !s.IsDeleted); // bool operator
            if (orderStateObj != null)
            {
                return _mapper.Map<GetOrderStateDTO>(orderStateObj);
            }
            return null;
        }
    }
}
