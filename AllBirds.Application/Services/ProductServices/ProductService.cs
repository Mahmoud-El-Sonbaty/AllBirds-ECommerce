using AllBirds.Application.Contracts;
using AllBirds.Application.Services.CategoryProductServices;
using AllBirds.DTOs.ProductDTOs;
using AllBirds.DTOs.Shared;
using AllBirds.DTOs.SpecificationDTOs;
using AllBirds.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AllBirds.Application.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productrepoistory;
        private readonly ICategoryProductService categoryProductService;
        public IMapper mapper;

        public ProductService(IProductRepository _productRepository, IMapper _mapper, ICategoryProductService _categoryProductService)
        {
            productrepoistory = _productRepository;
            mapper = _mapper;
            categoryProductService = _categoryProductService;
        }

        public async Task<ResultView<CUProductDTO>> CreateAsync(CUProductDTO cUProductDTO)
        {
            ResultView<CUProductDTO> resultView = new();
            try
            {
                bool productExist = (await productrepoistory.GetAllAsync()).Any(p => p.Id == cUProductDTO.Id || p.ProductNo == cUProductDTO.ProductNo);
                if (productExist)
                {
                    resultView.IsSuccess = false;
                    resultView.Data = cUProductDTO;
                    resultView.Msg = $"Product {cUProductDTO.ProductNo} Already Exists";
                    return resultView;
                }
                Product mappedProduct = mapper.Map<Product>(cUProductDTO);
                Product createdProduct = await productrepoistory.CreateAsync(mappedProduct);
                if (createdProduct is not null) // would it ever be null ?
                {
                    await productrepoistory.SaveChangesAsync();
                    CUProductDTO mappedCUProductDTO = mapper.Map<CUProductDTO>(createdProduct);
                    resultView.IsSuccess = true;
                    resultView.Data = mappedCUProductDTO;
                    resultView.Msg = $"Product {mappedCUProductDTO.ProductNo} Created Successfully";
                }
                else
                {
                    resultView.IsSuccess = false;
                    resultView.Data = cUProductDTO;
                    resultView.Msg = $"Product {cUProductDTO.ProductNo} Not Created Successfully";
                }
            }
            catch (Exception ex)
            {
                resultView.IsSuccess = false;
                resultView.Data = null;
                resultView.Msg = $"Error Happened While Creating Product ${cUProductDTO.ProductNo} ${ex.Message}";
            }
            return resultView;
        }

        public async Task<ResultView<List<GetAllProductDTO>>> GetAllAsync()
        {
            ResultView<List<GetAllProductDTO>> result = new();
            List<Product> productsList = [.. (await productrepoistory.GetAllAsync())];
            List<GetAllProductDTO> getAllPrds = mapper.Map<List<GetAllProductDTO>>(productsList);
            result.IsSuccess = true;
            result.Data = getAllPrds;
            result.Msg = "All Products Fetched Successfully";
            return result;
        }

        public async Task<ResultView<CUProductDTO>> GetByIdAsync(int productId)
        {
            ResultView<CUProductDTO> resultView = new();
            Product? getProduct = (await productrepoistory.GetAllAsync()).Include(p => p.Categories).FirstOrDefault(p => p.Id == productId && !p.IsDeleted);
            if (getProduct is not null)
            {
                CUProductDTO mappedCUProductDTO = mapper.Map<CUProductDTO>(getProduct);
                resultView.IsSuccess = true;
                resultView.Data = mappedCUProductDTO;
                resultView.Msg = $"Product {mappedCUProductDTO.ProductNo} Fetched Successfully";
            }
            else
            {
                resultView.IsSuccess = false;
                resultView.Data = null;
                resultView.Msg = $"Product {productId} Not Found";
            }
            return resultView;

        }

        public async Task<ResultView<CUProductDTO>> HardDeleteAsync(int productId)
        {
            ResultView<CUProductDTO> resultView = new();
            try
            {
                Product? getProduct = (await productrepoistory.GetAllAsync()).FirstOrDefault(p => p.Id == productId);
                if (getProduct is not null)
                {
                    Product deletedPrd = await productrepoistory.DeleteAsync(getProduct);
                    int saveStatus = await productrepoistory.SaveChangesAsync();
                    //if (saveStatus > 0)
                    //{
                    CUProductDTO mappedCUProductDTO = mapper.Map<CUProductDTO>(getProduct);
                    resultView.IsSuccess = true;
                    resultView.Data = mappedCUProductDTO;
                    resultView.Msg = $"Product {mappedCUProductDTO.ProductNo} Hard Deleted Successfully";
                    //}
                    //else
                    //{
                    //resultView.IsSuccess = false;
                    //resultView.Data = null;
                    //resultView.Msg = $"Product {mappedCUProductDTO.ProductNo} Hard Deletion Not Saved Successfully";
                    //return resultView;
                    //}
                }
                else
                {
                    resultView.IsSuccess = false;
                    resultView.Data = null;
                    resultView.Msg = $"Product {productId} Not Found";
                }
            }
            catch (Exception ex)
            {
                resultView.IsSuccess = false;
                resultView.Data = null;
                resultView.Msg = $"Error Happened While Hard Deleting Product ${productId} ${ex.Message}";
            }
            return resultView;
        }

        public async Task<ResultView<CUProductDTO>> SoftDeleteAsync(int productId)
        {
            ResultView<CUProductDTO> resultView = new();
            try
            {
                Product? getProduct = (await productrepoistory.GetAllAsync()).FirstOrDefault(p => p.Id == productId);
                if (getProduct is not null)
                {
                    CUProductDTO mappedCUProductDTO = mapper.Map<CUProductDTO>(getProduct);
                    if (getProduct.IsDeleted)
                    {
                        resultView.IsSuccess = false;
                        resultView.Data = mappedCUProductDTO;
                        resultView.Msg = $"Product {mappedCUProductDTO.ProductNo} Is Soft Deleted Already";
                    }
                    else
                    {
                        getProduct.IsDeleted = true;
                        int saveStatus = await productrepoistory.SaveChangesAsync();
                        //if (saveStatus > 0)
                        //{
                        resultView.IsSuccess = true;
                        resultView.Data = mappedCUProductDTO;
                        resultView.Msg = $"Product {mappedCUProductDTO.ProductNo} Soft Deleted Successfully";
                        //}
                        //else
                        //{
                        //resultView.IsSuccess = false;
                        //resultView.Data = null;
                        //resultView.Msg = $"Product {mappedCUProductDTO.ProductNo} Soft Deletion Not Saved Successfully";
                        //return resultView;
                        //}
                    }
                }
                else
                {
                    resultView.IsSuccess = false;
                    resultView.Data = null;
                    resultView.Msg = $"Product {productId} Not Found";
                }
            }
            catch (Exception ex)
            {
                resultView.IsSuccess = false;
                resultView.Data = null;
                resultView.Msg = $"Error Happened While Soft Deleting Product ${productId} ${ex.Message}";
            }
            return resultView;
        }

        public async Task<ResultView<CUProductDTO>> UpdateAsync(CUProductDTO cUProductDTO)
        {
            ResultView<CUProductDTO> resultView = new();
            try
            {
                bool CheckPrdExist = (await productrepoistory.GetAllAsync()).Any(P => P.Id == cUProductDTO.Id && !P.IsDeleted);
                if(CheckPrdExist)
                {
                    Product prdUpdat = mapper.Map<Product>(cUProductDTO);
                    Product prdUpdated = await productrepoistory.UpdateAsync(prdUpdat);
                    //var prdCats = await categoryProductService.ge
                    /*foreach (int catId in cUProductDTO.CategoriesId)
                    {
                        var cat = new CategoryProduct() {  CategoryId = catId, ProductId = cUProductDTO.Id };
                        //ICategoryRepository.createAsync(cat)
                    }*/
                    await productrepoistory.SaveChangesAsync();
                    CUProductDTO mappedUpdatedPrd = mapper.Map<CUProductDTO>(prdUpdated);
                    resultView.IsSuccess = true;
                    resultView.Data = mappedUpdatedPrd;
                    resultView.Msg = $"Product {cUProductDTO.ProductNo} Updated Successfully";
                }
                else
                {
                    resultView.IsSuccess = false;
                    resultView.Data = cUProductDTO;
                    resultView.Msg = $"Product {cUProductDTO.ProductNo} Not Found Or Soft Deleted";
                }
            }
            catch (Exception ex)
            {
                resultView.IsSuccess = false;
                resultView.Data = null;
                resultView.Msg = $"Error Happened While Hard Deleting Product ${cUProductDTO.ProductNo} ${ex.Message}";
            }
            return resultView;
        }
    }
}
