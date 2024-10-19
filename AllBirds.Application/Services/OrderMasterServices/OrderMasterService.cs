using AllBirds.Application.Contracts;
using AllBirds.Application.Services.OrderDetailsServices;
using AllBirds.DTOs.OrderDetailsDTOs;
using AllBirds.DTOs.OrderMasterDTOs;
using AllBirds.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Application.Services.OrderServices
{
    public class OrderMasterService : IOrderMasterService
    {

        private readonly IOrderMasterRepository _OrderMasterRepository;
        private readonly IMapper _mapper;
        private readonly IOrderDetailsService orderDetailsService;

        public OrderMasterService(IOrderMasterRepository orderM,IMapper Mapper,IOrderDetailsService _orderDetailService)
        {
            _OrderMasterRepository= orderM;
            _mapper= Mapper;
            orderDetailsService=_orderDetailService;
        }
        public async Task<createOrderMasterDTO> CreateAsync(createOrderMasterDTO createOrderMDTo)
        {
            OrderMaster mapedorder=_mapper.Map<OrderMaster>(createOrderMDTo);
           OrderMaster createOrder=await _OrderMasterRepository.CreateAsync(mapedorder);
           await _OrderMasterRepository.SaveChangesAsync();
            foreach(CreateOrderDetailsDTO prd in createOrderMDTo.ProductColorSizeId)
            {
                await orderDetailsService.CreateAsync(prd);

                    
            }
            await _OrderMasterRepository.SaveChangesAsync();

            return _mapper.Map<createOrderMasterDTO>(createOrder);

        }

        public async Task<List<GetAllOrderMastersDTO>> GetAllAsync()
        {
            var orderMasters=(await _OrderMasterRepository.GetAllAsync()).Where(b=>!b.IsDeleted);

            return _mapper.Map<List<GetAllOrderMastersDTO>>(orderMasters);

        }

        public async Task<List<GetAllOrderMastersDTO>> GetAllWithDeletedAsync()
        {
            var orderMasters = await _OrderMasterRepository.GetAllAsync();

            return _mapper.Map<List<GetAllOrderMastersDTO>>(orderMasters);
        }

        public async Task<GetOneOdrerMasterDTO> GetByIdAsync(int OrderId)
        {
           var ordermaster=(await _OrderMasterRepository.GetOneAsync(OrderId));
            //if (ordermaster.IsDeleted) { }
            return _mapper.Map<GetOneOdrerMasterDTO>(ordermaster);
        }

        public async Task<GetOneOdrerMasterDTO> HardDeleteAsync(int OrderID)
        {
            OrderMaster  order = (await _OrderMasterRepository.GetAllAsync()).FirstOrDefault(b=>b.Id==OrderID&&!b.IsDeleted);
            if (order != null)
            {
                OrderMaster deletedOrderMaster = (await _OrderMasterRepository.DeleteAsync(order));
                await _OrderMasterRepository.SaveChangesAsync();
                return _mapper.Map<GetOneOdrerMasterDTO>(deletedOrderMaster);
            }
            
            return null;
        }

        public async Task<GetOneOdrerMasterDTO> SoftDeleteAsync(int OrderID)
        {
           OrderMaster order= (await _OrderMasterRepository.GetAllAsync()).FirstOrDefault(b=>!b.IsDeleted&& b.Id==OrderID);
            if (order != null)
            {
                order.IsDeleted=true;
                await _OrderMasterRepository.SaveChangesAsync();
                return _mapper.Map<GetOneOdrerMasterDTO>(order);
            }
            return null;
        }

        public async Task<createOrderMasterDTO> UpdateAsync(createOrderMasterDTO createOrderMDTo)
        {
            bool orderMaster = (await _OrderMasterRepository.GetAllAsync()).Any(b=>b.Id==createOrderMDTo.Id&&!b.IsDeleted);
            if (orderMaster )
            {
                var order = _mapper.Map<OrderMaster>(createOrderMDTo);
                OrderMaster updatedOredermaster = await _OrderMasterRepository.UpdateAsync(order);
                
                await _OrderMasterRepository.SaveChangesAsync();
                return _mapper.Map<createOrderMasterDTO>(updatedOredermaster);
            }
            return null;
        }

    }
}
