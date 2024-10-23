using AllBirds.Application.Contracts;
using AllBirds.DTOs.OrderStateDTOs;
using AllBirds.DTOs.ProductColorDTOs;
using AllBirds.DTOs.Shared;
using AllBirds.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Application.Services.ProductColorServices
{
    public class ProductColorService : IProductColorService
    {
        private readonly IMapper mapper;
        private readonly IProductColorRepository productColorRepository;
        public ProductColorService(IMapper _mapper,IProductColorRepository _productColorRepository)
        {
            mapper = _mapper;
            productColorRepository = _productColorRepository;
        }
        public async Task<ResultView<CreateProductColorDTO>> CreateAsync(CreateProductColorDTO productColorDTO)
        {
            ResultView<CreateProductColorDTO> result = new();
            try
            {
                var item =(await productColorRepository.GetAllAsync()).Any(b=>b.Id==productColorDTO.Id ||
                b.ProductId == productColorDTO.ProductId && b.ColorId == productColorDTO.ColorId);
                if (!item)
                {


                    var prCL = mapper.Map<ProductColor>(productColorDTO);
                    var created = await productColorRepository.CreateAsync(prCL);
                    await productColorRepository.SaveChangesAsync();

                    result.IsSuccess=true;
                    result.Data = mapper.Map<CreateProductColorDTO>(created);
                    result.Msg = "Product Color is Created successfully";
                }
                else
                {
                    result.IsSuccess = false;
                    result.Data=null;
                    result.Msg = "Product Color  Is ALready Exist";
                }

            }
            catch (Exception ex)
            {

                result.IsSuccess = false;
                result.Data = null;
                result.Msg = "Error Happened While Creating With Ex :"+ex.Message;


            }



            return result;




        }

        public async Task<ResultView<List<GetOneProductColorDTO>>> GetAllAsync()
        {
            ResultView<List<GetOneProductColorDTO>> result = new();
            try
            {
                var item = (await productColorRepository.GetAllAsync()).Where(b=>!b.IsDeleted)
                            .Include(b=>b.Product)
                            .Include(s=>s.Color)
                            .Include(r=>r.Images);
                if (item.Count()!=0)
                {



                    result.IsSuccess = true;
                    result.Data = mapper.Map<List<GetOneProductColorDTO>>(item);
                    result.Msg = "Get All Product's Colors successfully";
                }
                else
                {
                    result.IsSuccess = false;
                    result.Data = null;
                    result.Msg = "Product's Color's list Is Empty";
                }

            }
            catch (Exception ex)
            {

                result.IsSuccess = false;
                result.Data = null;
                result.Msg = "Error Happened While Getting All Product Colors With Ex :" + ex.Message;


            }

            return result;


        }

        public async Task<ResultView<List<GetOneProductColorDTO>>> GetAllWithDeletedAsync()
        {
            ResultView<List<GetOneProductColorDTO>> result = new();
            try
            {
                var item = (await productColorRepository.GetAllAsync())
                    .Include(b => b.Product)
                    .Include(s => s.Color)
                    .Include(r => r.Images);
                ;
                if (item.Count() != 0)
                {



                    result.IsSuccess = true;
                    result.Data = mapper.Map<List<GetOneProductColorDTO>>(item);
                    result.Msg = "Get All Product's Colors successfully";
                }
                else
                {
                    result.IsSuccess = false;
                    result.Data = null;
                    result.Msg = "Product's Color's list Is Empty";
                }

            }
            catch (Exception ex)
            {

                result.IsSuccess = false;
                result.Data = null;
                result.Msg = "Error Happened While Getting All Product Colors With Ex :" + ex.Message;


            }

            return result;
        }

        public async Task<ResultView<GetOneProductColorDTO>> GetByIdAsync(int id)
        {


            ResultView<GetOneProductColorDTO> result = new();
            try
            {
                var item = (await productColorRepository.GetAllAsync())
                    .Where(b => !b.IsDeleted && b.Id == id)
                     .Include(b => b.Product)
                    .Include(s => s.Color)
                    .Include(r => r.Images)
                    .Include(s=>s.AvailableSizes).ThenInclude(a=>a.Size);
                
                if (item !=null)
                {



                    result.IsSuccess = true;
                    result.Data = mapper.Map<GetOneProductColorDTO>(item);
                    result.Msg = "Get  Product's Color successfully";
                }
                else
                {
                    result.IsSuccess = false;
                    result.Data = null;
                    result.Msg = "Product's Color Not Found";
                }

            }
            catch (Exception ex)
            {

                result.IsSuccess = false;
                result.Data = null;
                result.Msg = "Error Happened While Getting  Product Color By Id With Ex :" + ex.Message;


            }

            return result;
        }

        public async Task<ResultView<GetOneProductColorDTO>> HardDeleteAsync(int id)
        {

            ResultView<GetOneProductColorDTO> result = new();
            try
            {
                var item = (await productColorRepository.GetAllAsync()).FirstOrDefault( b=>b.Id == id);
                if (item != null)
                {

                    var deleted = await productColorRepository.DeleteAsync(item);
                    await productColorRepository.SaveChangesAsync();

                    result.IsSuccess = true;
                    result.Data = mapper.Map<GetOneProductColorDTO>(deleted);
                    result.Msg = "Deleting Product's Color successfully";
                }
                else
                {
                    result.IsSuccess = false;
                    result.Data = null;
                    result.Msg = "Product's Color Not Found";
                }

            }
            catch (Exception ex)
            {

                result.IsSuccess = false;
                result.Data = null;
                result.Msg = "Error Happened While Delete Product Color By Id With Ex :" + ex.Message;


            }

            return result;
        }

        public async Task<ResultView<GetOneProductColorDTO>> SoftDeleteAsync(int id)
        {
            ResultView<GetOneProductColorDTO> result = new();
            try
            {
                var item = (await productColorRepository.GetAllAsync()).FirstOrDefault(b => b.Id == id);
                if (item != null)
                {

                    item.IsDeleted = true;
                    await productColorRepository.SaveChangesAsync();

                    result.IsSuccess = true;
                    result.Data = mapper.Map<GetOneProductColorDTO>(item);
                    result.Msg = "Soft Deleting Product's Color successfully";
                }
                else
                {
                    result.IsSuccess = false;
                    result.Data = null;
                    result.Msg = "Product's Color Not Found";
                }

            }
            catch (Exception ex)
            {

                result.IsSuccess = false;
                result.Data = null;
                result.Msg = "Error Happened While Delete Product Color By Id With Ex :" + ex.Message;


            }

            return result;
        }

        public async Task<ResultView<CreateProductColorDTO>> UpdateAsync(CreateProductColorDTO productColorDTO)
        {
            ResultView<CreateProductColorDTO> result = new();
            try
            {
                var item = (await productColorRepository.GetAllAsync()).Any(b => b.Id == productColorDTO.Id);
                if (item)
                {


                    var prCL = mapper.Map<ProductColor>(productColorDTO);
                    var updated = await productColorRepository.UpdateAsync(prCL);
                    await productColorRepository.SaveChangesAsync();

                    result.IsSuccess = true;
                    result.Data = mapper.Map<CreateProductColorDTO>(updated);
                    result.Msg = "Product Color is Updated successfully";
                }
                else
                {
                    result.IsSuccess = false;
                    result.Data = null;
                    result.Msg = "Product Color  Is Not Exist";
                }

            }
            catch (Exception ex)
            {

                result.IsSuccess = false;
                result.Data = null;
                result.Msg = "Error Happened While Creating With Ex :" + ex.Message;


            }



            return result;


        }
    }
}
