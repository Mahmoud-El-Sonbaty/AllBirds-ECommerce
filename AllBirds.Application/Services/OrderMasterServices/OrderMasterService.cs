using AllBirds.Application.Contracts;
using AllBirds.Application.Services.OrderDetailServices;
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

namespace AllBirds.Application.Services.OrderMasterServices
{
    public class OrderMasterService : IOrderMasterService
    {
        private readonly IOrderMasterRepository orderMasterRepository;
        private readonly IMapper mapper;
        private readonly IOrderDetailService orderDetailsService;
        private readonly IProductRepository productRepository;

        public OrderMasterService(IOrderMasterRepository orderM, IMapper Mapper, IOrderDetailService _orderDetailService, IProductRepository _productRepository)
        {
            orderMasterRepository = orderM;
            mapper = Mapper;
            orderDetailsService = _orderDetailService;
            productRepository = _productRepository;
        }

        public async Task<ResultView<bool>> ChangingStateAsync(int StateId, int OrderID)
        {
            ResultView<bool> result = new();

            try
            {
                var item = await orderMasterRepository.GetOneAsync(OrderID);
                if (item != null)
                {
                    item.OrderStateId = StateId;
                    await orderMasterRepository.SaveChangesAsync();
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

        public async Task<ResultView<CreateOrderMasterDTO>> CreateAsync(CreateOrderMasterDTO createOrderMDTo)
        {
            ResultView<CreateOrderMasterDTO> result = new();
            try
            {
                bool orderExists = (await orderMasterRepository.GetAllAsync()).Any(b =>
                    b.Id == createOrderMDTo.Id ||
                    b.ClientId == createOrderMDTo.ClientId &&
                    b.OrderStateId == 1
                );
                if (orderExists)
                {
                    result.IsSuccess = false;
                    result.Data = null;
                    result.Msg = "the Order already Exists";

                }
                else
                {
                    var gg = createOrderMDTo.ProductColorSizeId.Select(p => $"{p.ProductId}{p.Quantity}{(int)p.DetailPrice}");
                    OrderMaster mapedorder = mapper.Map<OrderMaster>(createOrderMDTo);
                    if (createOrderMDTo.ProductColorSizeId is not null)
                    {
                        mapedorder.OrderDetails = [];
                        //List<int> ids = createOrderMDTo.ProductColorSizeId.Select(i => i.ProductId).ToList();
                        //List<Product> productsInOrder = (await productRepository.GetAllAsync())
                        //    .Include(p => p.AvailableColors).ThenInclude(pc => pc.Color)
                        //    .Include(p => p.AvailableColors).ThenInclude(pc => pc.AvailableSizes).ThenInclude(pcs => pcs.Size)
                        //    .Where(p => p.AvailableColors.Any(pc => pc.AvailableSizes.Any(pcs => ids.Contains(pcs.Id)))).ToList();
                        foreach (CreateOrderDetailsDTO item in createOrderMDTo.ProductColorSizeId)
                        {
                            //decimal prdPrice = productsInOrder.FirstOrDefault(p => p.AvailableColors.Any(pc =>
                            //pc.AvailableSizes.Any(pcs => pcs.Id == item.ProductId))).Price;
                            //item.DetailPrice = item.Quantity * prdPrice;
                            mapedorder.OrderDetails.Add(mapper.Map<OrderDetail>(item));
                        }
                    }
                    mapedorder.OrderStateId = 1;
                    OrderMaster createOrder = await orderMasterRepository.CreateAsync(mapedorder);
                    await orderMasterRepository.SaveChangesAsync();
                    result.IsSuccess = true;
                    result.Data = mapper.Map<CreateOrderMasterDTO>(createOrder);
                    result.Msg = $"Order number {createOrder.OrderNo} Is created Successfully ";
                    foreach (OrderDetail orderDetail in createOrder.OrderDetails)
                    {
                        CreateOrderDetailsDTO detailDTO = new()
                        {
                            Id = orderDetail.Id,
                            ProductId = orderDetail.ProductColorSizeId,
                            OrderMasterId = orderDetail.OrderMasterId,
                            DetailPrice = orderDetail.DetailPrice,
                            Quantity = orderDetail.Quantity,
                            Notes = orderDetail.Notes
                        };
                        var mappedOrderDetail = mapper.Map<CreateOrderDetailsDTO>(orderDetail);
                        //result.Data.ProductColorSizeId.Add(detailDTO);
                    }
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
            ResultView<List<GetAllOrderMastersDTO>> result = new();
            try
            {
                var orderMasters = (await orderMasterRepository.GetAllAsync()).Where(b => !b.IsDeleted).Include(src => src.OrderState).ToList();
                if (orderMasters.Count() != 0)
                {
                    result.IsSuccess = true;
                    result.Data = mapper.Map<List<GetAllOrderMastersDTO>>(orderMasters);
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
            //var orderMasters=(await _OrderMasterRepository.GetAllAsync()).Where(b=>!b.IsDeleted);

            //return _mapper.Map<List<GetAllOrderMastersDTO>>(orderMasters);

        }

        public async Task<ResultView<List<GetAllOrderMastersDTO>>> GetAllWithDeletedAsync()
        {
            ResultView<List<GetAllOrderMastersDTO>> result = new();
            try
            {
                var orderMasters = await orderMasterRepository.GetAllAsync();
                if (orderMasters.Count() != 0)
                {
                    result.IsSuccess = true;
                    result.Data = mapper.Map<List<GetAllOrderMastersDTO>>(orderMasters);
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
                var ordermaster = await orderMasterRepository.GetOneAsync(OrderId);
                if (ordermaster != null)
                {
                    result.IsSuccess = true;
                    result.Data = mapper.Map<GetOneOdrerMasterDTO>(ordermaster);
                    result.Msg = $"Get Order Number {ordermaster.OrderNo} Done  ";


                }
                else
                {
                    result.IsSuccess = false;
                    result.Data = null;
                    result.Msg = "Order Is Not Founds";
                }

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Data = null;
                result.Msg = $"Error Happen While Get The Order With Id {OrderId} " + ex.Message;

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
                OrderMaster order = (await orderMasterRepository.GetAllAsync()).FirstOrDefault(b => b.Id == OrderID);
                if (order != null)
                {
                    OrderMaster deletedOrderMaster = await orderMasterRepository.DeleteAsync(order);
                    await orderMasterRepository.SaveChangesAsync();

                    result.IsSuccess = true;
                    result.Data = mapper.Map<GetOneOdrerMasterDTO>(deletedOrderMaster);
                    result.Msg = $"Delete order with Id: {OrderID}  Is done  ";


                }
                else
                {
                    result.IsSuccess = false;
                    result.Data = null;
                    result.Msg = "Order is not found";
                }

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Data = null;
                result.Msg = $"Error Happen While Delete the Order With Id {OrderID} " + ex.Message;

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
                OrderMaster order = (await orderMasterRepository.GetAllAsync()).FirstOrDefault(b => !b.IsDeleted && b.Id == OrderID);
                if (order != null)
                {
                    order.IsDeleted = true;
                    await orderMasterRepository.SaveChangesAsync();

                    result.IsSuccess = true;
                    result.Data = mapper.Map<GetOneOdrerMasterDTO>(order);
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
                result.Msg = $"Error Happen While Delete The Order With Id {OrderID} " + ex.Message;

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

        public async Task<ResultView<CreateOrderMasterDTO>> UpdateAsync(CreateOrderMasterDTO createOrderMDTo)
        {


            ResultView<CreateOrderMasterDTO> result = new();
            try
            {
                bool orderMaster = (await orderMasterRepository.GetAllAsync()).Any(b => b.Id == createOrderMDTo.Id && !b.IsDeleted);
                if (!orderMaster)
                {
                    result.IsSuccess = false;
                    result.Data = null;
                    result.Msg = "The Order Is Not Exist";

                }
                else
                {

                    var order = mapper.Map<OrderMaster>(createOrderMDTo);


                    OrderMaster updatedOredermaster = await orderMasterRepository.UpdateAsync(order);

                    await orderMasterRepository.SaveChangesAsync();


                    result.IsSuccess = true;
                    result.Data = mapper.Map<CreateOrderMasterDTO>(updatedOredermaster);
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
