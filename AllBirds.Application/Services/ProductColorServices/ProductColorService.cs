﻿using AllBirds.Application.Contracts;
using AllBirds.Application.Services.ProductColorImageServices;
using AllBirds.DTOs.OrderStateDTOs;
using AllBirds.DTOs.ProductColorDTOs;
using AllBirds.DTOs.ProductColorImageDTOs;
using AllBirds.DTOs.Shared;
using AllBirds.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
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
        private readonly IProductColotImageService productColotImageService;

        public ProductColorService(IMapper _mapper, IProductColorRepository _productColorRepository, IProductColotImageService _productColotImageService)
        {
            mapper = _mapper;
            productColorRepository = _productColorRepository;
            productColotImageService = _productColotImageService;

        }
        public async Task<ResultView<CreateProductColorDTO>> CreateAsync(CreateProductColorDTO productColorDTO, string ImagePath)
        {
            ResultView<CreateProductColorDTO> result = new();
            try
            {
                var item = (await productColorRepository.GetAllAsync()).Any(b => b.ProductId == productColorDTO.ProductId && b.ColorId == productColorDTO.ColorId);
                if (item)
                {
                    result.IsSuccess = false;
                    result.Data = null;
                    result.Msg = "Product Color  Is ALready Exist";
                    return result;
                }

                ProductColor prCL = mapper.Map<ProductColor>(productColorDTO);

                if (prCL.Images == null)
                {
                    prCL.Images = new List<ProductColorImage>();
                }

                foreach (IFormFile formFile in productColorDTO.Images)
                {

                    if (!Directory.Exists(ImagePath))
                    {
                        Directory.CreateDirectory(ImagePath);
                    }

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + formFile.FileName;
                    string filePath = Path.Combine(ImagePath, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(fileStream);
                    }

                    ProductColorImage productColorImage = new() { ImagePath = "/Images/" + "/ProductColorImages/" + uniqueFileName };

                    prCL.Images.Add(productColorImage);

                }
                ProductColor created = await productColorRepository.CreateAsync(prCL);

                await productColorRepository.SaveChangesAsync();



                result.IsSuccess = true;
                result.Data = mapper.Map<CreateProductColorDTO>(created);
                result.Msg = "Product Color is Created successfully";

            }
            catch (Exception ex)
            {

                result.IsSuccess = false;
                result.Data = null;
                result.Msg = "Error Happened While Creating With Ex :" + ex.Message;

            }



            return result;

        }

        public async Task<ResultView<UpdateProductColorDTO>> UpdateAsync(UpdateProductColorDTO productColorDTO)
        {
            ResultView<UpdateProductColorDTO> result = new();
            try
            {
                var item = (await productColorRepository.GetAllAsync()).Any(b => b.Id == productColorDTO.Id);
                if (item)
                {

                    var prCL = mapper.Map<ProductColor>(productColorDTO);
                    var updated = await productColorRepository.UpdateAsync(prCL);
                    await productColorRepository.SaveChangesAsync();

                    result.IsSuccess = true;
                    result.Data = mapper.Map<UpdateProductColorDTO>(updated);
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
        public async Task<ResultView<List<GetALlProductColorDTO>>> GetAllAsync(int Id)
        {
            ResultView<List<GetALlProductColorDTO>> result = new();
            try
            {
                //var item = (await productColorRepository.GetAllAsync()).Where(b => b.ProductId == Id)
                //            .Include(b => b.Product)
                //            .Include(s => s.Color)
                //            .Include(r => r.Images);
                var item = (await productColorRepository.GetAllAsync()).Where(b => b.ProductId == Id).Select(P => new GetALlProductColorDTO
                {
                    PNameEn = P.Product.NameEn,
                    PNameAr = P.Product.NameAr,
                    ProductNo = P.Product.ProductNo,
                    Id = P.Id,
                    ColorNameAr = P.Color.NameAr,
                    ColorNameEn = P.Color.NameEn,
                    ColorCode = P.Color.Code,
                    MainImageId = P.MainImageId,
                    IsDeleted = P.IsDeleted,
                    MainImagePath = P.Images.FirstOrDefault(I => I.Id == P.MainImageId).ImagePath
                });
            //.Include(b => b.Product)
            //.Include(s => s.Color)
            //.Include(r => r.Images);




                if (item.Count() != 0)
                {

                    result.IsSuccess = true;
                    result.Data = mapper.Map<List<GetALlProductColorDTO>>(item);

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
                ProductColor item = (await productColorRepository.GetAllAsync())
                    .Where(b => b.Id == id)
                    .Include(b => b.Product)
                    .Include(s => s.Color)
                    .Include(r => r.Images)
                    .Include(s => s.AvailableSizes).ThenInclude(a => a.Size).FirstOrDefault();

                if (item != null)
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
                ProductColor item = (await productColorRepository.GetAllAsync()).FirstOrDefault(b => b.Id == id);
                if (item != null)
                {
                    if (item.IsDeleted)
                    {

                        bool CheckImage = (await productColorRepository.GetAllAsync()).AsNoTracking().Any(P => P.Images.Any());
                        bool CheckSize = (await productColorRepository.GetAllAsync()).AsNoTracking().Any(P => P.AvailableSizes.Any());
                        if (CheckImage && CheckSize)
                        {
                            result.Data = mapper.Map<GetOneProductColorDTO>(item);
                            result.IsSuccess = false;
                            result.Msg = $"This Product Color Have Depend On It Like Images And Size..! Sorry Cannot Delete Thais Color ";
                        }
                        else if (CheckSize)
                        {
                            result.Data = mapper.Map<GetOneProductColorDTO>(item);
                            result.IsSuccess = false;
                            result.Msg = $"This Product Color Have Depend On It Like Size..! Sorry Cannot Delete Thais Color ";
                        }
                        else if (CheckImage)
                        {
                            result.Data = mapper.Map<GetOneProductColorDTO>(item);
                            result.IsSuccess = false;
                            result.Msg = $"This Product Color Have Depend On It Like Image..! Sorry Cannot Delete Thais Color ";
                        }
                        else
                        {
                            ProductColor deleted = await productColorRepository.DeleteAsync(item);
                            await productColorRepository.SaveChangesAsync();
                            
                            result.IsSuccess = true;
                            result.Data = mapper.Map<GetOneProductColorDTO>(deleted);
                            result.Msg = "Deleting Product's Color successfully";

                        }
                    }
                    else
                    {
                        item.IsDeleted = true;
                        int saveStatus = await productColorRepository.SaveChangesAsync();
                        //if (saveStatus > 0)
                        //{
                        result.IsSuccess = true;
                        result.Data = mapper.Map<GetOneProductColorDTO>(item);
                        result.Msg = $"Product Color Soft Deleted Successfully";
                    }
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

        //public async Task<ResultView<GetOneProductColorDTO>> SoftDeleteAsync(int id)
        //{
        //    ResultView<GetOneProductColorDTO> result = new();
        //    try
        //    {
        //        var item = (await productColorRepository.GetAllAsync()).FirstOrDefault(b => b.Id == id);
        //        if (item != null)
        //        {

        //            item.IsDeleted = true;
        //            await productColorRepository.SaveChangesAsync();

        //            result.IsSuccess = true;
        //            result.Data = mapper.Map<GetOneProductColorDTO>(item);
        //            result.Msg = "Soft Deleting Product's Color successfully";
        //        }
        //        else
        //        {
        //            result.IsSuccess = false;
        //            result.Data = null;
        //            result.Msg = "Product's Color Not Found";
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //        result.IsSuccess = false;
        //        result.Data = null;
        //        result.Msg = "Error Happened While Delete Product Color By Id With Ex :" + ex.Message;


        //    }

        //    return result;
        //}


    }
}
