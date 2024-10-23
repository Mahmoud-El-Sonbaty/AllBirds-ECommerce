using AllBirds.Application.Contracts;
using AllBirds.DTOs.OrderDetailsDTOs;
using AllBirds.DTOs.OrderMasterDTOs;
using AllBirds.DTOs.Shared;
using AllBirds.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        public async Task<ResultView<CreateOrderDetailsDTO>> CreateAsync(CreateOrderDetailsDTO createOrderMDTo)
        {

            ResultView<CreateOrderDetailsDTO> result = new();
            try
            {
                var item = (await orderDetailsRepository.GetAllAsync()).Any(b => b.Id == createOrderMDTo.Id);
                if (item)
                {
                    result.IsSuccess = false;
                    result.Data = null;
                    result.Msg = "the Order's item Is already Exist";

                }
                else
                {
                    OrderDetail mappedOrderDetails = mapper.Map<OrderDetail>(createOrderMDTo);
                    OrderDetail createdOrderDetails = await orderDetailsRepository.CreateAsync(mappedOrderDetails);
                    await orderDetailsRepository.SaveChangesAsync();
                    
                    

                    result.IsSuccess = true;
                    result.Data = mapper.Map<CreateOrderDetailsDTO>(createdOrderDetails);
                    result.Msg = $"Order item with id {createdOrderDetails.Id} Is created Successfully ";

                }

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Data = null;
                result.Msg = $"Error Happen While Creating Order's item " + ex.Message;

            }
            return result;
            //OrderDetail mappedOrderDetails= mapper.Map<OrderDetail>(createOrderMDTo);
            //OrderDetail createdOrderDetails= await orderDetailsRepository.CreateAsync(mappedOrderDetails);
            ////await orderDetailsRepository.SaveChangesAsync();
            //return mapper.Map<CreateOrderDetailsDTO>(createdOrderDetails);


        }

        public async Task<ResultView<List<GetAllOrderDetailsDTO>>> GetAllAsync()
        {


            ResultView<List<GetAllOrderDetailsDTO>> result = new();
            try
            {
                var order = (await orderDetailsRepository.GetAllAsync()).Where(b => !b.IsDeleted)
                    .Include(c => c.ProductColorSize.Size.SizeNumber)
                    .Include(r => r.ProductColorSize.ProductColor.Product)
                    .Include(a => a.ProductColorSize.ProductColor.Color)
                    .Include(m => m.ProductColorSize.ProductColor.Images.FirstOrDefault(d => d.ProductColorId == m.ProductColorSize.ProductColor.MainImageId));

                if (order.Count() != 0)
                {
                    var mapping = mapper.Map<List<GetAllOrderDetailsDTO>>(order);

                    result.IsSuccess = true;
                    result.Data = mapping;
                    result.Msg = "get all order's Items is done  ";


                }
                else
                {
                    result.IsSuccess = false;
                    result.Data = null;
                    result.Msg = " Order's Item list is Empty ";
                }

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Data = null;
                result.Msg = $"Error Happen While get Order's Items " + ex.Message;

            }
            return result;









            //var order =( await orderDetailsRepository.GetAllAsync()).Where(b=>!b.IsDeleted)
            //    .Include(c=>c.ProductColorSize.Size.SizeNumber)
            //    .Include(r=>r.ProductColorSize.ProductColor.Product)
            //    .Include(a=>a.ProductColorSize.ProductColor.Color)
            //    .Include(m=>m.ProductColorSize.ProductColor.Images.FirstOrDefault(d=>d.ProductColorId==m.ProductColorSize.ProductColor.MainImageId));

            //var mapping = mapper.Map<List<GetAllOrderDetailsDTO>>(order);
            //return  mapping;
        }

        public async Task<ResultView<List<GetAllOrderDetailsDTO>>> GetAllWithDeletedAsync()
        {

            ResultView<List<GetAllOrderDetailsDTO>> result = new();
            try
            {
                var order = (await orderDetailsRepository.GetAllAsync())
                    .Include(c => c.ProductColorSize.Size.SizeNumber)
                    .Include(r => r.ProductColorSize.ProductColor.Product)
                    .Include(a => a.ProductColorSize.ProductColor.Color)
                    .Include(m => m.ProductColorSize.ProductColor.Images.FirstOrDefault(d => d.ProductColorId == m.ProductColorSize.ProductColor.MainImageId));

                if (order.Count() != 0)
                {
                    var mapping = mapper.Map<List<GetAllOrderDetailsDTO>>(order);

                    result.IsSuccess = true;
                    result.Data = mapping;
                    result.Msg = "get all order's Items is done  ";


                }
                else
                {
                    result.IsSuccess = false;
                    result.Data = null;
                    result.Msg = " Order's Item list is Empty ";
                }

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Data = null;
                result.Msg = $"Error Happen While get Order's Items " + ex.Message;

            }
            return result;

            //var order = await orderDetailsRepository.GetAllAsync();
            //return mapper.Map<List<GetAllOrderDetailsDTO>>(order);
        }

        public async Task<ResultView<GetOneOrderDetailsDTO>> GetByIdAsync(int OrderId)
        {





            ResultView<GetOneOrderDetailsDTO> result = new();
            try
            {
                var ordermaster = (await orderDetailsRepository.GetOneAsync(OrderId));
                if (ordermaster != null)
                {
                    result.IsSuccess = true;
                    result.Data = mapper.Map<GetOneOrderDetailsDTO>(ordermaster);
                    result.Msg = $"get order item with number {ordermaster.Id}  is :done  ";


                }
                else
                {
                    result.IsSuccess = false;
                    result.Data = null;
                    result.Msg = $"Order's Item With ID :{ordermaster.Id} is not found  ";
                }

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Data = null;
                result.Msg = $"Error Happen While get the Order's Item With Id {OrderId} " + ex.Message;

            }
            return result;

            //var order = await orderDetailsRepository.GetOneAsync(OrderId);
            //return mapper.Map<GetOneOrderDetailsDTO>(order);
        }

        public async Task<ResultView<GetOneOrderDetailsDTO>> HardDeleteAsync(int OrderID)
        {


            ResultView<GetOneOrderDetailsDTO> result = new();
            try
            {
                var order = (await orderDetailsRepository.GetAllAsync()).FirstOrDefault(b=>b.Id == OrderID);
                if (order != null)
                {
                    OrderDetail deletedOrderMaster = await orderDetailsRepository.DeleteAsync(order);
                    await orderDetailsRepository.SaveChangesAsync();

                    result.IsSuccess = true;
                    result.Data = mapper.Map<GetOneOrderDetailsDTO>(order);
                    result.Msg = $"Delete Order's Item with Id: {OrderID}  Is done  ";


                }
                else
                {
                    result.IsSuccess = false;
                    result.Data = null;
                    result.Msg = "Order's Item is not found  ";
                }

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Data = null;
                result.Msg = $"Error Happen While Delete the Order With Id {OrderID} " + ex.Message;

            }
            return result;




            //var item =( await orderDetailsRepository.GetAllAsync()).FirstOrDefault(b=>!b.IsDeleted&& b.Id==OrderID);
            //if (item!=null)
            //{
            //     var order= await orderDetailsRepository.DeleteAsync(item);
            //     await  orderDetailsRepository.SaveChangesAsync();
            //     return mapper.Map<GetOneOrderDetailsDTO>(order);
            //}
            // return null;
        }

        public async Task<ResultView<GetOneOrderDetailsDTO>> SoftDeleteAsync(int OrderID)
        {

            ResultView<GetOneOrderDetailsDTO> result = new();
            try
            {
                var orderItem = (await orderDetailsRepository.GetAllAsync()).FirstOrDefault(b => !b.IsDeleted && b.Id == OrderID);
                if (orderItem != null)
                {
                    orderItem.IsDeleted = true; 

                    await orderDetailsRepository.SaveChangesAsync();

                    result.IsSuccess = true;
                    result.Data = mapper.Map<GetOneOrderDetailsDTO>(orderItem);
                    result.Msg = $"Soft Delete Order's Item With Id: {OrderID}  Is Done  ";


                }
                else
                {
                    result.IsSuccess = false;
                    result.Data = null;
                    result.Msg = "Order's Item Is not found  ";
                }

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Data = null;
                result.Msg = $"Error Happen While Delete The Order With Id {OrderID} " + ex.Message;

            }
            return result;

            //var item = (await orderDetailsRepository.GetAllAsync()).FirstOrDefault(b => !b.IsDeleted && b.Id == OrderID);
            //if (item != null)
            //{
            //    item.IsDeleted = true; 
            //    orderDetailsRepository.SaveChangesAsync();
            //    return mapper.Map<GetOneOrderDetailsDTO>(item);
            //}
            //return null;
        }

        public async Task<ResultView<CreateOrderDetailsDTO>>  UpdateAsync(CreateOrderDetailsDTO createOrderMDTo)
        {
            ResultView<CreateOrderDetailsDTO> result = new();
            try
            {
                bool orderDetails = (await orderDetailsRepository.GetAllAsync()).Any(b => b.Id == createOrderMDTo.Id && !b.IsDeleted);
                if (!orderDetails)
                {
                    result.IsSuccess = false;
                    result.Data = null;
                    result.Msg = "The Order's Item Is Not Exist";

                }
                else
                {

                    var order = mapper.Map<OrderDetail>(createOrderMDTo);


                    OrderDetail updatedOredermaster = await orderDetailsRepository.UpdateAsync(order);

                    await orderDetailsRepository.SaveChangesAsync();


                    result.IsSuccess = true;
                    result.Data = mapper.Map<CreateOrderDetailsDTO>(updatedOredermaster);
                    result.Msg = $"Order'Item  With Id: {updatedOredermaster.Id} Is Updated Successfully ";

                }

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Data = null;
                result.Msg = $"Error Happen While Updating Order's Item " + ex.Message;

            }
            return result;







            //OrderDetail orderDetails = (await orderDetailsRepository.GetAllAsync()).FirstOrDefault(b => b.Id == createOrderMDTo.Id && !b.IsDeleted);
            //if (orderDetails != null)
            //{
            //    var updatedOrederDetails = await orderDetailsRepository.UpdateAsync(orderDetails);
            //    await orderDetailsRepository.SaveChangesAsync();
            //    return mapper.Map<CreateOrderDetailsDTO>(updatedOrederDetails);
            //}
            //return null;
        }
    }
}

