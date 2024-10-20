using AllBirds.Application.Contracts;
using AllBirds.DTOs.ProductDetailDTOs;
using AllBirds.DTOs.Shared;
using AllBirds.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;


namespace AllBirds.Application.Services.ProductDetailService
{
    public class ProductDetailsService : IProductDetailsService
    {
        private readonly IProductDetailsRepository productDetailsRepository;
        private readonly IProductRepository productRepository;
        public IMapper mapper;
        public ProductDetailsService(IProductDetailsRepository _productDetailsRepository, IMapper _mapper, IProductRepository _productRepository)
        {
            productDetailsRepository = _productDetailsRepository;
            productRepository = _productRepository;
            mapper = _mapper;
        }
        public async Task<ResultView<CUProductDetails>> CreateProductDetails(CUProductDetails cUProductDetails)
        {
            bool existPrdDetails = (await productDetailsRepository.GetAllAsync()).Any(P => P.Title == cUProductDetails.Title || P.Description == cUProductDetails.Description);
            if (existPrdDetails)
            {
                return new ResultView<CUProductDetails>() { Data = null, IsSuccess = false, Msg = $"Product Detail Title : ({cUProductDetails.Title}) Already Exist" };
            }
            else
            {
                string uploadFolder = cUProductDetails.ImagePath;

                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + cUProductDetails.ImageData.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await cUProductDetails.ImageData.CopyToAsync(fileStream);
                }
                cUProductDetails.ImagePath = "/Images/" + uniqueFileName;
                ProductDetail productDetail = mapper.Map<ProductDetail>(cUProductDetails);
                ProductDetail productDetailCreated = await productDetailsRepository.CreateAsync(productDetail);

                if (productDetailCreated is not null)
                {

                    CUProductDetails cUProductDetails1 = mapper.Map<CUProductDetails>(productDetailCreated);
                    await productDetailsRepository.SaveChangesAsync();
                    return new ResultView<CUProductDetails>() { Data = cUProductDetails1, IsSuccess = true, Msg = $"Product Detail Title : ({cUProductDetails1.Title}) Created Successfully" };
                }
                else
                {
                    return null;
                }

            }
        }

        public async Task<List<GetAllProductDetailsDTOS>> GetAllProductDetails()
        {
            List<ProductDetail> getAllProduct = [.. (await productDetailsRepository.GetAllAsync())];

            if (getAllProduct is not null)
            {


                List<GetAllProductDetailsDTOS> getAllProductDTOs = mapper.Map<List<GetAllProductDetailsDTOS>>(getAllProduct);
                return getAllProductDTOs;

            }
            return null;

        }
        public async Task<ResultView<GetAllProductDetailsDTOS>> GetOnePrdDetails(int id)
        {

            ResultView<GetAllProductDetailsDTOS> resultView = new();

            ProductDetail productDetail = (await productDetailsRepository.GetAllAsync()).FirstOrDefault(p => p.Id == id);
            if (productDetail == null)
            {
                resultView.Data = null;
                resultView.IsSuccess = false;
                resultView.Msg = "Product Details Not Exist";
                return resultView;
            }

            GetAllProductDetailsDTOS getAllProductDetailsDTOS = mapper.Map<GetAllProductDetailsDTOS>(productDetail);
            resultView.Data = getAllProductDetailsDTOS;
            resultView.IsSuccess = true;
            resultView.Msg = "Product Details Fetched Successfully";
            return resultView;
        }

        public async Task<ResultView<CUProductDetails>> HardDeletePrdDetails(CUProductDetails cUProductDetails)
        {
            ProductDetail productDetail = (await productDetailsRepository.GetAllAsync()).FirstOrDefault(P => P.Id == cUProductDetails.Id);
            if (productDetail is not null)
            {
                ProductDetail productDetail1 = await productDetailsRepository.DeleteAsync(productDetail);
                CUProductDetails cUProductDeleted = mapper.Map<CUProductDetails>(productDetail1);
                ResultView<CUProductDetails> resultView = new ResultView<CUProductDetails>()
                {
                    Data = cUProductDeleted,
                    IsSuccess = true,
                    Msg = $"Product Detail : ({cUProductDeleted.Title}) Deleted Successfully"
                };
                await productDetailsRepository.SaveChangesAsync();
                return resultView;
            }
            else
            {
                ResultView<CUProductDetails> resultView1 = new ResultView<CUProductDetails>()
                {
                    Data = cUProductDetails,
                    IsSuccess = false,
                    Msg = $"Product Detail : ({cUProductDetails.Title}) Not Exist"
                };
                return resultView1;
            }

        }

        public async Task<ResultView<CUProductDetails>> UpdateProductDetails(CUProductDetails cUProductDetails)
        {
            bool Exist = await (await productDetailsRepository.GetAllAsync()).AnyAsync(P => P.Id == cUProductDetails.Id && P.ProductId == cUProductDetails.ProductId);
            if (!Exist)
            {
                ResultView<CUProductDetails> resultView = new()
                {
                    Data = null,
                    IsSuccess = false,
                    Msg = "This Product Detail Is Not Exist"
                };

            }
            ProductDetail productDetail = mapper.Map<ProductDetail>(cUProductDetails);
            ProductDetail productDetailUpdated = await productDetailsRepository.UpdateAsync(productDetail);
            await productDetailsRepository.SaveChangesAsync();
            CUProductDetails productDetailsDTO = mapper.Map<CUProductDetails>(productDetailUpdated);
            ResultView<CUProductDetails> resultView1 = new()
            {
                Data = productDetailsDTO,
                IsSuccess = true,
                Msg = $"Product Detail : ({productDetailsDTO.Title}) Updated successfully"

            };
            return resultView1;
        }
    }
}
