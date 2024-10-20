using AllBirds.Application.Contracts;
using AllBirds.DTOs.ProductDTOs;
using AllBirds.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Application.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productrepoistory;
        //private readonly ICategoryProductService categoryProductService;
        public IMapper mapper;

        public ProductService(IProductRepository _productRepository, IMapper _mapper)
        {
            productrepoistory = _productRepository;
            mapper = _mapper;
        }
        public async Task<CUProductDTO> CreateAsync(CUProductDTO cUProductDTO)
        {
            bool productExist = (await productrepoistory.GetAllAsync()).Any(p => p.Id == cUProductDTO.Id && p.ProductNo == cUProductDTO.ProductNo);
            if (productExist)
            {
                // product already exist
                return null;
            }
            Product mappedProduct = mapper.Map<Product>(cUProductDTO);
            Product createdProduct = await productrepoistory.CreateAsync(mappedProduct);
            if (createdProduct != null)
            {
                await productrepoistory.SaveChangesAsync();
                CUProductDTO mappedCUProductDTO = mapper.Map<CUProductDTO>(createdProduct);
                return mappedCUProductDTO;
            }
            // product not created successfully
            return null;
        }

        public async Task<List<GetAllProductDTO>> GetAllAsync()
        {
            List<Product> productsList = [.. (await productrepoistory.GetAllAsync())];
            List<GetAllProductDTO> result = mapper.Map<List<GetAllProductDTO>>(productsList);
            return result;
        }

        public async Task<CUProductDTO> GetByIdAsync(int productId)
        {

            Product? getProduct = (await productrepoistory.GetAllAsync()).FirstOrDefault(p => p.Id == productId && !p.IsDeleted);
            Product? getProduct1 = (await productrepoistory.GetAllAsync()).Include(P => P.Categories).FirstOrDefault(p => p.Id == productId && !p.IsDeleted);
            if (getProduct is not null)
            {
                CUProductDTO mappedCUProductDTO = mapper.Map<CUProductDTO>(getProduct);
                return mappedCUProductDTO;
            }
            // product not found
            return null;

        }

        public async Task<CUProductDTO> HardDeleteAsync(int productId)
        {
            bool productExist = (await productrepoistory.GetAllAsync()).Any(p => p.Id == productId);
            if (productExist)
            {
                // product not found
                return null;
            }
            Product? getProduct = (await productrepoistory.GetAllAsync()).FirstOrDefault(p => p.Id == productId);
            if (getProduct != null)
            {
                await productrepoistory.DeleteAsync(getProduct);
                await productrepoistory.SaveChangesAsync();
                CUProductDTO mappedCUProductDTO = mapper.Map<CUProductDTO>(getProduct);
                return mappedCUProductDTO;
            }
            // product not found
            return null;
        }

        public async Task<CUProductDTO> SoftDeleteAsync(int productId)
        {
            bool productExist = (await productrepoistory.GetAllAsync()).Any(p => p.Id == productId);
            if (!productExist)
            {
                // product not found
                return null;
            }
            Product? getProduct = (await productrepoistory.GetAllAsync()).FirstOrDefault(p => p.Id == productId && !p.IsDeleted);
            if (getProduct != null)
            {
                getProduct.IsDeleted = true;
                await productrepoistory.DeleteAsync(getProduct);
                await productrepoistory.SaveChangesAsync();
                CUProductDTO mappedCUProductDTO = mapper.Map<CUProductDTO>(getProduct);
                return mappedCUProductDTO;
            }
            // product soft deleted before
            return null;
        }

        public async Task<CUProductDTO> UpdateAsync(CUProductDTO cUProductDTO)
        {
            bool CheckInDB = (await productrepoistory.GetAllAsync()).Any(P => P.Id == cUProductDTO.Id && !P.IsDeleted);
            if(CheckInDB)
            {
                Product prdUpdat = mapper.Map<Product>(cUProductDTO);
                Product prdUpdated = await productrepoistory.UpdateAsync(prdUpdat);
                /*foreach (int catId in cUProductDTO.CategoriesId)
                {
                    var cat = new CategoryProduct() {  CategoryId = catId, ProductId = cUProductDTO.Id };
                    //ICategoryRepository.createAsync(cat)
                }*/
                await productrepoistory.SaveChangesAsync();
                return mapper.Map<CUProductDTO>(prdUpdated);
            }
            else
            {
                return null;
            }
        }
    }
}
