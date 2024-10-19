using AllBirds.Application.Contracts;
using AllBirds.DTOs.OrderDetailsDTOs;
using AllBirds.DTOs.OrderMasterDTOs;
using AllBirds.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Application.Services.OrderDetailsServices
{
    public class OrderDetailsService : IOrderDetailsService
    {
        IOrderDetailsRepository orderDetailsRepository;
        IMapper mapper;

        public OrderDetailsService(IOrderDetailsRepository _orderDeatilsReposit,IMapper _Mapper)
        {
            this.orderDetailsRepository = _orderDeatilsReposit;
            this.mapper = _Mapper;
        }
        public async Task<CreateOrderDetailsDTO> CreateAsync(CreateOrderDetailsDTO createOrderMDTo)
        {
            OrderDetail mappedOrderDetails= mapper.Map<OrderDetail>(createOrderMDTo);
            OrderDetail createdOrderDetails= await orderDetailsRepository.CreateAsync(mappedOrderDetails);
            //await orderDetailsRepository.SaveChangesAsync();
            return mapper.Map<CreateOrderDetailsDTO>(createdOrderDetails);


        }

        public async Task<List<GetAllOrderDetailsDTO>> GetAllAsync()
        {
            var order =( await orderDetailsRepository.GetAllAsync()).Where(b=>!b.IsDeleted);
            return mapper.Map<List<GetAllOrderDetailsDTO>>(order);
        }

        public async Task<List<GetAllOrderDetailsDTO>> GetAllWithDeletedAsync()
        {
            var order = await orderDetailsRepository.GetAllAsync();
            return mapper.Map<List<GetAllOrderDetailsDTO>>(order);
        }

        public async Task<GetOneOrderDetailsDTO> GetByIdAsync(int OrderId)
        {
            var order = await orderDetailsRepository.GetOneAsync(OrderId);
            return mapper.Map<GetOneOrderDetailsDTO>(order);
        }

        public async Task<GetOneOrderDetailsDTO> HardDeleteAsync(int OrderID)
        {
           var item =( await orderDetailsRepository.GetAllAsync()).FirstOrDefault(b=>!b.IsDeleted&& b.Id==OrderID);
           if (item!=null)
           {
                var order= await orderDetailsRepository.DeleteAsync(item);
                await  orderDetailsRepository.SaveChangesAsync();
                return mapper.Map<GetOneOrderDetailsDTO>(order);
           }
            return null;
        }

        public async Task<GetOneOrderDetailsDTO> SoftDeleteAsync(int OrderID)
        {
            var item = (await orderDetailsRepository.GetAllAsync()).FirstOrDefault(b => !b.IsDeleted && b.Id == OrderID);
            if (item != null)
            {
                item.IsDeleted = true; 
                orderDetailsRepository.SaveChangesAsync();
                return mapper.Map<GetOneOrderDetailsDTO>(item);
            }
            return null;
        }

        public async Task<CreateOrderDetailsDTO> UpdateAsync(CreateOrderDetailsDTO createOrderMDTo)
        {
            OrderDetail orderDetails = (await orderDetailsRepository.GetAllAsync()).FirstOrDefault(b => b.Id == createOrderMDTo.Id && !b.IsDeleted);
            if (orderDetails != null)
            {
                var updatedOrederDetails = await orderDetailsRepository.UpdateAsync(orderDetails);
                await orderDetailsRepository.SaveChangesAsync();
                return mapper.Map<CreateOrderDetailsDTO>(updatedOrederDetails);
            }
            return null;
        }
    }
}

