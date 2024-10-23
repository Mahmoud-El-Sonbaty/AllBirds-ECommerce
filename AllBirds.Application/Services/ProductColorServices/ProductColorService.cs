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
        public IMapper Mapper { get; set; }
        public IProductColorRepository ProductColorRepository { get; set; }
        public ProductColorService(IMapper _mapper,IProductColorRepository _productColorRepository)
        {
            Mapper = _mapper;
            ProductColorRepository = _productColorRepository;
        }
        public async Task<ResultView<CreateProductColorDTO>> CreateAsync(CreateProductColorDTO ProductColorDTO)
        {
            ResultView<CreateProductColorDTO> result = new();
            try
            {
                var item =(await ProductColorRepository.GetAllAsync()).Any(b=>b.Id==ProductColorDTO.Id ||
                b.ProductId == ProductColorDTO.ProductId && b.ColorId == ProductColorDTO.ColorId);
                if (!item)
                {


                    var PrCL = Mapper.Map<ProductColor>(ProductColorDTO);
                    var Created = await ProductColorRepository.CreateAsync(PrCL);
                    await ProductColorRepository.SaveChangesAsync();

                    result.IsSuccess=true;
                    result.Data = Mapper.Map<CreateProductColorDTO>(Created);
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
                var item = (await ProductColorRepository.GetAllAsync()).Where(b=>!b.IsDeleted)
                            .Include(b=>b.Product)
                            .Include(s=>s.Color)
                            .Include(r=>r.Images);
                if (item.Count()!=0)
                {



                    result.IsSuccess = true;
                    result.Data = Mapper.Map<List<GetOneProductColorDTO>>(item);
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
                var item = (await ProductColorRepository.GetAllAsync())
                    .Include(b => b.Product)
                    .Include(s => s.Color)
                    .Include(r => r.Images);
                ;
                if (item.Count() != 0)
                {



                    result.IsSuccess = true;
                    result.Data = Mapper.Map<List<GetOneProductColorDTO>>(item);
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

        public async Task<ResultView<GetOneProductColorDTO>> GetByIdAsync(int Id)
        {


            ResultView<GetOneProductColorDTO> result = new();
            try
            {
                var item = (await ProductColorRepository.GetAllAsync())
                    .Where(b => !b.IsDeleted && b.Id == Id)
                     .Include(b => b.Product)
                    .Include(s => s.Color)
                    .Include(r => r.Images)
                    .Include(s=>s.AvailableSizes).ThenInclude(a=>a.Size);
                
                if (item !=null)
                {



                    result.IsSuccess = true;
                    result.Data = Mapper.Map<GetOneProductColorDTO>(item);
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

        public async Task<ResultView<GetOneProductColorDTO>> HardDeleteAsync(int Id)
        {

            ResultView<GetOneProductColorDTO> result = new();
            try
            {
                var item = (await ProductColorRepository.GetAllAsync()).FirstOrDefault( b=>b.Id == Id);
                if (item != null)
                {

                    var deleted = await ProductColorRepository.DeleteAsync(item);
                    await ProductColorRepository.SaveChangesAsync();

                    result.IsSuccess = true;
                    result.Data = Mapper.Map<GetOneProductColorDTO>(deleted);
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

        public async Task<ResultView<GetOneProductColorDTO>> SoftDeleteAsync(int Id)
        {
            ResultView<GetOneProductColorDTO> result = new();
            try
            {
                var item = (await ProductColorRepository.GetAllAsync()).FirstOrDefault(b => b.Id == Id);
                if (item != null)
                {

                    item.IsDeleted = true;
                    await ProductColorRepository.SaveChangesAsync();

                    result.IsSuccess = true;
                    result.Data = Mapper.Map<GetOneProductColorDTO>(item);
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

        public async Task<ResultView<CreateProductColorDTO>> UpdateAsync(CreateProductColorDTO ProductColorDTO)
        {
            ResultView<CreateProductColorDTO> result = new();
            try
            {
                var item = (await ProductColorRepository.GetAllAsync()).Any(b => b.Id == ProductColorDTO.Id);
                if (item)
                {


                    var PrCL = Mapper.Map<ProductColor>(ProductColorDTO);
                    var Created = await ProductColorRepository.UpdateAsync(PrCL);
                    await ProductColorRepository.SaveChangesAsync();

                    result.IsSuccess = true;
                    result.Data = Mapper.Map<CreateProductColorDTO>(Created);
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
