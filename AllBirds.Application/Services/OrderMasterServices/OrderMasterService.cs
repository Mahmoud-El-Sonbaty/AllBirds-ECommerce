using AllBirds.Application.Contracts;
using AllBirds.Application.Services.OrderDetailsServices;
using AllBirds.DTOs.OrderDetailsDTOs;
using AllBirds.DTOs.OrderMasterDTOs;
using AllBirds.DTOs.Shared;
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

        public async Task<ResultView<bool>> ChangingStateAsync(int StateId, int OrderID)
        {
            ResultView<bool> result = new();

            try {
                var item = await _OrderMasterRepository.GetOneAsync(OrderID);
                if (item != null)
                {
                    item.OrderStateId = StateId;
                    await _OrderMasterRepository.SaveChangesAsync();
                    result.IsSuccess = true;
                    result.Data = true;
                    result.Msg = $"Order State Is changed and be {item.OrderState}";
                }
                else
                {
                    result.IsSuccess = false;
                    result.Data = false;
                    result.Msg = $"Order Is Not Found";

                }

                 
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Data = false;
                result.Msg = $"Error Happen While changing the order State , " + ex.Message;

            }

            return result;

            //var item =  await _OrderMasterRepository.GetOneAsync(OrderID);
            //if (item != null)
            //{
            //    item.OrderStateId = StateId;
            //    await _OrderMasterRepository.SaveChangesAsync();
            //    return true;
            //}
            
            //return false;

        }

        public async Task<ResultView<createOrderMasterDTO>> CreateAsync(createOrderMasterDTO createOrderMDTo)
        {
            ResultView<createOrderMasterDTO> result = new();
            try
            {
                var item =(await _OrderMasterRepository.GetAllAsync()).Any(b=>b.Id==createOrderMDTo.Id);
                if (item)
                {
                    result.IsSuccess = false;
                    result.Data = null;
                    result.Msg = "the Order Is already Exist";

                }
                else
                {
                    OrderMaster mapedorder = _mapper.Map<OrderMaster>(createOrderMDTo);
                    OrderMaster createOrder = await _OrderMasterRepository.CreateAsync(mapedorder);
                    await _OrderMasterRepository.SaveChangesAsync();
                    foreach (CreateOrderDetailsDTO prd in createOrderMDTo.ProductColorSizeId)
                    {
                        await orderDetailsService.CreateAsync(prd);


                    }
                    await _OrderMasterRepository.SaveChangesAsync();

                    result.IsSuccess = true;
                    result.Data = _mapper.Map<createOrderMasterDTO>(createOrder);
                    result.Msg = $"Order number {createOrder.OrderNo} Is created Successfully ";

                }

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Data = null;
                result.Msg = $"Error Happen While Creating Order " + ex.Message;

            }
            return result;

           // OrderMaster mapedorder=_mapper.Map<OrderMaster>(createOrderMDTo);
           //OrderMaster createOrder=await _OrderMasterRepository.CreateAsync(mapedorder);
           //await _OrderMasterRepository.SaveChangesAsync();
           // foreach(CreateOrderDetailsDTO prd in createOrderMDTo.ProductColorSizeId)
           // {
           //     await orderDetailsService.CreateAsync(prd);

                    
           // }
            //await _OrderMasterRepository.SaveChangesAsync();

            //return _mapper.Map<createOrderMasterDTO>(createOrder);

        }

        public async Task<ResultView<List<GetAllOrderMastersDTO>>> GetAllAsync()
        {
            ResultView<List<GetAllOrderMastersDTO>> result= new();
            try
            {
                var orderMasters = (await _OrderMasterRepository.GetAllAsync()).Where(b => !b.IsDeleted);
                if (orderMasters.Count() != 0) { 
                 result.IsSuccess=true;
                 result.Data = _mapper.Map<List<GetAllOrderMastersDTO>>(orderMasters);
                    result.Msg = "get all orders done  ";


                }
                else
                {
                    result.IsSuccess=false;
                    result.Data = null;
                    result.Msg = " order list is Empty ";
                }

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Data = null;
                result.Msg = $"Error Happen While get Orders " + ex.Message;

            }
            return result;
            //var orderMasters=(await _OrderMasterRepository.GetAllAsync()).Where(b=>!b.IsDeleted);

            //return _mapper.Map<List<GetAllOrderMastersDTO>>(orderMasters);

        }

        public async Task<ResultView<List<GetAllOrderMastersDTO>>> GetAllWithDeletedAsync()
        {
            ResultView<List<GetAllOrderMastersDTO>> result = new();
            try
            {
                var orderMasters = await _OrderMasterRepository.GetAllAsync();
                if (orderMasters.Count() != 0)
                {
                    result.IsSuccess = true;
                    result.Data = _mapper.Map<List<GetAllOrderMastersDTO>>(orderMasters);
                    result.Msg = "get all orders done  ";


                }
                else
                {
                    result.IsSuccess = false;
                    result.Data = null;
                    result.Msg = " order list is Empty ";
                }

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Data = null;
                result.Msg = $"Error Happen While get Orders " + ex.Message;

            }
            return result;







            //var orderMasters = await _OrderMasterRepository.GetAllAsync();

            //return _mapper.Map<List<GetAllOrderMastersDTO>>(orderMasters);
        }

        public async Task<ResultView<GetOneOdrerMasterDTO>> GetByIdAsync(int OrderId)
        {

            ResultView<GetOneOdrerMasterDTO> result = new();
            try
            {
                var ordermaster=(await _OrderMasterRepository.GetOneAsync(OrderId));
                if (ordermaster != null)
                {
                    result.IsSuccess = true;
                    result.Data = _mapper.Map<GetOneOdrerMasterDTO>(ordermaster);
                    result.Msg = $"get order number {ordermaster.OrderNo} done  ";


                }
                else
                {
                    result.IsSuccess = false;
                    result.Data = null;
                    result.Msg = " order is not found Empty ";
                }

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Data = null;
                result.Msg = $"Error Happen While get the order With Id {OrderId} " + ex.Message;

            }
            return result;


            //var ordermaster=(await _OrderMasterRepository.GetOneAsync(OrderId));
            //if (ordermaster.IsDeleted) { }
            //return _mapper.Map<GetOneOdrerMasterDTO>(ordermaster);
        }

        public async Task<ResultView<GetOneOdrerMasterDTO>> HardDeleteAsync(int OrderID)
        {


            ResultView<GetOneOdrerMasterDTO> result = new();
            try
            {
                OrderMaster order = (await _OrderMasterRepository.GetAllAsync()).FirstOrDefault(b => b.Id == OrderID );
                if (order != null)
                {
                    OrderMaster deletedOrderMaster = (await _OrderMasterRepository.DeleteAsync(order));
                    await _OrderMasterRepository.SaveChangesAsync();

                    result.IsSuccess = true;
                    result.Data = _mapper.Map<GetOneOdrerMasterDTO>(deletedOrderMaster);
                    result.Msg = $"Delete order with Id: {OrderID}  Is done  ";


                }
                else
                {
                    result.IsSuccess = false;
                    result.Data = null;
                    result.Msg = " order is not found Empty ";
                }

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Data = null;
                result.Msg = $"Error Happen While Delete the order With Id {OrderID} " + ex.Message;

            }
            return result;










            //OrderMaster  order = (await _OrderMasterRepository.GetAllAsync()).FirstOrDefault(b=>b.Id==OrderID&&!b.IsDeleted);
            //if (order != null)
            //{
            //    OrderMaster deletedOrderMaster = (await _OrderMasterRepository.DeleteAsync(order));
            //    await _OrderMasterRepository.SaveChangesAsync();
            //    return _mapper.Map<GetOneOdrerMasterDTO>(deletedOrderMaster);
            //}
            
            //return null;
        }

        public async Task<ResultView<GetOneOdrerMasterDTO>> SoftDeleteAsync(int OrderID)
        {

            ResultView<GetOneOdrerMasterDTO> result = new();
            try
            {
                OrderMaster order = (await _OrderMasterRepository.GetAllAsync()).FirstOrDefault(b => !b.IsDeleted && b.Id == OrderID);
                if (order != null)
                {
                    order.IsDeleted = true;
                    await _OrderMasterRepository.SaveChangesAsync();

                    result.IsSuccess = true;
                    result.Data = _mapper.Map<GetOneOdrerMasterDTO>(order);
                    result.Msg = $"soft Delete  for the order with Id: {OrderID}  Is done  ";


                }
                else
                {
                    result.IsSuccess = false;
                    result.Data = null;
                    result.Msg = " order is not found Empty ";
                }

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Data = null;
                result.Msg = $"Error Happen While Delete the order With Id {OrderID} " + ex.Message;

            }
            return result;






            //OrderMaster order= (await _OrderMasterRepository.GetAllAsync()).FirstOrDefault(b=>!b.IsDeleted&& b.Id==OrderID);
            //if (order != null)
            //{
            //    order.IsDeleted=true;
            //    await _OrderMasterRepository.SaveChangesAsync();
            //    return _mapper.Map<GetOneOdrerMasterDTO>(order);
            //}
            //return null;
        }

        public async Task<ResultView<createOrderMasterDTO>> UpdateAsync(createOrderMasterDTO createOrderMDTo)
        {


            ResultView<createOrderMasterDTO> result = new();
            try
            {
                bool orderMaster = (await _OrderMasterRepository.GetAllAsync()).Any(b => b.Id == createOrderMDTo.Id && !b.IsDeleted); 
                if (!orderMaster)
                {
                    result.IsSuccess = false;
                    result.Data = null;
                    result.Msg = "The Order Is Not Exist";

                }
                else
                {

                    var order = _mapper.Map<OrderMaster>(createOrderMDTo);


                    OrderMaster updatedOredermaster = await _OrderMasterRepository.UpdateAsync(order);

                    await _OrderMasterRepository.SaveChangesAsync();


                    result.IsSuccess = true;
                    result.Data = _mapper.Map<createOrderMasterDTO>(updatedOredermaster);
                    result.Msg = $"Order Number {updatedOredermaster.OrderNo} Is Updated Successfully ";

                }

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Data = null;
                result.Msg = $"Error Happen While Updating Order " + ex.Message;

            }
            return result;




            //bool orderMaster = (await _OrderMasterRepository.GetAllAsync()).Any(b=>b.Id==createOrderMDTo.Id&&!b.IsDeleted);
            //if (orderMaster )
            //{
            //    var order = _mapper.Map<OrderMaster>(createOrderMDTo);
            //    OrderMaster updatedOredermaster = await _OrderMasterRepository.UpdateAsync(order);

            //    await _OrderMasterRepository.SaveChangesAsync();
            //    return _mapper.Map<createOrderMasterDTO>(updatedOredermaster);
            //}
            //return null;
        }

    }
}
