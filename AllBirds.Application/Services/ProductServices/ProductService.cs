using AllBirds.Application.Contracts;
using AllBirds.Application.Services.CategoryProductServices;
using AllBirds.DTOs.ProductColorDTOs;
using AllBirds.DTOs.ProductColorImageDTOs;
using AllBirds.DTOs.ProductColorSizeDTOs;
using AllBirds.DTOs.ProductDetailDTOs;
using AllBirds.DTOs.ProductDTOs;
using AllBirds.DTOs.ProductSpecificationDTOs;
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
            //List<Product> productsList = [.. (await productrepoistory.GetAllAsync())
            //    .Include(p => p.AvailableColors)
            //        .ThenInclude(pc => pc.Color)
            //    .Include(p => p.AvailableColors)
            //        .ThenInclude(pc => pc.Images)];
            List<GetAllProductDTO> getAllPrds = [.. (await productrepoistory.GetAllAsync()).Select(p => new GetAllProductDTO()
            {
                Id = p.Id,
                ProductNo = p.ProductNo,
                NameAr = p.NameAr,
                NameEn = p.NameEn,
                Price = p.Price,
                Discount = p.Discount,
                FreeShipping = p.FreeShipping,
                IsDeleted = p.IsDeleted,
                MainColorCode = p.AvailableColors.FirstOrDefault(pc => pc.Id == p.MainColorId).Color.Code,
                MainImagePath = p.AvailableColors.FirstOrDefault(pc => pc.Id == p.MainColorId).Images.FirstOrDefault(pci => pci.Id == p.AvailableColors.FirstOrDefault(pc => pc.Id == p.MainColorId).MainImageId).ImagePath,
            })];
            //List<GetAllProductDTO> getAllPrds = mapper.Map<List<GetAllProductDTO>>(productsList);
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
                if (CheckPrdExist)
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
        //======================== Product API ======================
        public async Task<ResultView<List<ProductCardDTO>>> GetAllPrdCatIdAsync(int CatId)
        {
            ResultView<List<ProductCardDTO>> resultView = new();

            if (CatId != 0)
            {
                List<ProductCardDTO> productCardDTOs = new();

                List<Product> filteredProducts = (await productrepoistory.GetAllAsync()).Include(p => p.Categories)
                    .Include(P => P.AvailableColors).ThenInclude(P => P.Images).Include(P => P.AvailableColors).ThenInclude(P => P.Color).Include(P => P.AvailableColors).ThenInclude(P => P.AvailableSizes).ThenInclude(P => P.Size)
                    .Where(p => p.Categories.Any(c => c.CategoryId == CatId)).ToList();

                foreach (Product product in filteredProducts)
                {
                    ProductCardDTO productCardDTO = new()
                    {
                        ProductColors = [],
                    };
                    productCardDTO.Id = product.Id;
                    productCardDTO.NameEn = product.NameEn;
                    productCardDTO.NameAr = product.NameAr;
                    productCardDTO.Price = product.Price;
                    foreach (ProductColor productColor in product.AvailableColors)
                    {
                        GetAllProductColorImageDTO getAllProductColorImageDTO = new();
                        getAllProductColorImageDTO.ProductColorId = productColor.Id;
                        getAllProductColorImageDTO.NameEn = productColor.Color.NameEn;
                        getAllProductColorImageDTO.NameAr = productColor.Color.NameAr;
                        getAllProductColorImageDTO.Code = productColor.Color.Code;
                        getAllProductColorImageDTO.ImagePath = productColor.Images.FirstOrDefault(P => P.Id == productColor.MainImageId)?.ImagePath;
                        getAllProductColorImageDTO.ProductSizes = new List<GetPCSDTO>();
                        foreach (ProductColorSize productColorSize in productColor.AvailableSizes)
                        {
                            GetPCSDTO getPCSDTO = new();
                            getPCSDTO.ProductColorSizeId = productColorSize.Id;
                            getPCSDTO.SizeNumber = productColorSize.Size.SizeNumber;
                            getPCSDTO.UnitsInStock = productColorSize.UnitsInStock;
                            getAllProductColorImageDTO.ProductSizes.Add(getPCSDTO);
                        }
                        productCardDTO.ProductColors.Add(getAllProductColorImageDTO);

                    }

                    productCardDTOs.Add(productCardDTO);
                }


                if (productCardDTOs != null && productCardDTOs.Count > 0)
                {
                    resultView.Data = productCardDTOs;
                    resultView.IsSuccess = true;
                    resultView.Msg = "All Products Fetched Successfully";
                }
                else
                {
                    resultView.IsSuccess = false;
                    resultView.Msg = "This Category Not Contain Any Product .. Sorry! ";
                    resultView.Data = null;
                }
            }
            /*else
            {

                List<ProductCardDTO> productCardDTOs = new();

                List<Product> filteredProducts = (await productrepoistory.GetAllAsync()).Include(p => p.Categories)
                    .Include(P => P.AvailableColors).ThenInclude(P => P.Images).Include(P => P.AvailableColors).ThenInclude(P => P.Color).Include(P => P.AvailableColors).ThenInclude(P => P.AvailableSizes).ThenInclude(P => P.Size)
                    .ToList();

                foreach (Product product in filteredProducts)
                {
                    ProductCardDTO productCardDTO = new()
                    {
                        ProductColors = [],
                    };
                    productCardDTO.Id = product.Id;
                    productCardDTO.NameEn = product.NameEn;
                    productCardDTO.NameAr = product.NameAr;
                    productCardDTO.Price = product.Price;
                    foreach (ProductColor productColor in product.AvailableColors)
                    {
                        GetAllProductColorImageDTO getAllProductColorImageDTO = new();
                        getAllProductColorImageDTO.ProductColorId = productColor.Id;
                        getAllProductColorImageDTO.NameEn = productColor.Color.NameEn;
                        getAllProductColorImageDTO.NameAr = productColor.Color.NameAr;
                        getAllProductColorImageDTO.Code = productColor.Color.Code;
                        getAllProductColorImageDTO.ImagePath = productColor.Images.FirstOrDefault(P => P.Id == productColor.MainImageId)?.ImagePath;
                        getAllProductColorImageDTO.ProductSizes = new List<GetPCSDTO>();
                        foreach (ProductColorSize productColorSize in productColor.AvailableSizes)
                        {
                            GetPCSDTO getPCSDTO = new();
                            getPCSDTO.ProductColorSizeId = productColorSize.Id;
                            getPCSDTO.SizeNumber = productColorSize.Size.SizeNumber;
                            getPCSDTO.UnitsInStock = productColorSize.UnitsInStock;
                            getAllProductColorImageDTO.ProductSizes.Add(getPCSDTO);
                        }
                        productCardDTO.ProductColors.Add(getAllProductColorImageDTO);

                    }

                    productCardDTOs.Add(productCardDTO);
                }


                if (productCardDTOs != null && productCardDTOs.Count > 0)
                {
                    resultView.Data = productCardDTOs;
                    resultView.IsSuccess = true;
                    resultView.Msg = "All Products Fetched Successfully";
                }
                else
                {
                    resultView.IsSuccess = false;
                    resultView.Msg = "This Category Not Contain Any Product .. Sorry! ";
                    resultView.Data = null;
                }

            }*/
            return resultView;
        }

        public async Task<ResultView<List<ProductCardDTO>>> ProductFilterationAsync(TypeFilterOfProductDTO typeFilterOfProductDTO)
        {
            ResultView<List<ProductCardDTO>> resultView = new();
            List<ProductCardDTO> productCardDTOs = new();

            if (typeFilterOfProductDTO.colorCode != null || typeFilterOfProductDTO.sizeNumber != null || typeFilterOfProductDTO.categoryId != null)
            {

                List<Product> filteredProducts = [];

                List<Product> products = (await productrepoistory.GetAllAsync()).Include(p => p.Categories)
                        .Include(P => P.AvailableColors).ThenInclude(P => P.Images).Include(P => P.AvailableColors).ThenInclude(P => P.Color).Include(P => P.AvailableColors).ThenInclude(P => P.AvailableSizes).ThenInclude(P => P.Size)
                        .Where(p => p.Categories.Any(c => c.CategoryId == typeFilterOfProductDTO.categoryId)).ToList();

                if (products.Count > 0 && typeFilterOfProductDTO.colorCode != null && typeFilterOfProductDTO.colorCode.Count > 0)
                {
                    if (filteredProducts.Count > 0 && filteredProducts != null)
                    {
                        filteredProducts = filteredProducts
                              .Where(p => typeFilterOfProductDTO.colorCode
                                  .All(colorCode => p.AvailableColors.Any(pc => pc.Color.Code == colorCode)))
                              .ToList();
                    }
                    else
                    {
                        filteredProducts = products
                              .Where(p => typeFilterOfProductDTO.colorCode
                                  .All(colorCode => p.AvailableColors.Any(pc => pc.Color.Code == colorCode)))
                              .ToList();
                    }
                }

                if (typeFilterOfProductDTO.sizeNumber != null && typeFilterOfProductDTO.sizeNumber.Count > 0)
                {
                    if (filteredProducts != null && filteredProducts.Count > 0)
                    {
                        filteredProducts = filteredProducts
                            .Where(p => p.AvailableColors != null && p.AvailableColors.Any(pc => pc.AvailableSizes != null && typeFilterOfProductDTO.sizeNumber.All(sizeNumber => pc.AvailableSizes.Any(ps => ps.Size != null && ps.Size.SizeNumber == sizeNumber))))
                            .ToList();
                    }
                    else
                    {
                        filteredProducts = products.Where(p => p.AvailableColors != null && p.AvailableColors.Any(pc => pc.AvailableSizes != null && typeFilterOfProductDTO.sizeNumber
                        .All(sizeNumber => pc.AvailableSizes
                        .Any(ps => ps.Size != null && ps.Size.SizeNumber == sizeNumber))))
                        .ToList();
                    }
                }

                if (filteredProducts != null && filteredProducts.Count > 0)
                {
                    foreach (Product product in filteredProducts)
                    {
                        ProductCardDTO productCardDTO = new()
                        {
                            ProductColors = [],
                        };
                        productCardDTO.Id = product.Id;
                        productCardDTO.NameEn = product.NameEn;
                        productCardDTO.NameAr = product.NameAr;
                        productCardDTO.Price = product.Price;
                        foreach (ProductColor productColor in product.AvailableColors)
                        {
                            GetAllProductColorImageDTO getAllProductColorImageDTO = new();
                            getAllProductColorImageDTO.ProductColorId = productColor.Id;
                            getAllProductColorImageDTO.NameEn = productColor.Color.NameEn;
                            getAllProductColorImageDTO.NameAr = productColor.Color.NameAr;
                            getAllProductColorImageDTO.Code = productColor.Color.Code;
                            getAllProductColorImageDTO.ImagePath = productColor.Images.FirstOrDefault(P => P.Id == productColor.MainImageId)?.ImagePath;
                            getAllProductColorImageDTO.ProductSizes = new List<GetPCSDTO>();
                            foreach (ProductColorSize productColorSize in productColor.AvailableSizes)
                            {
                                GetPCSDTO getPCSDTO = new();
                                getPCSDTO.ProductColorSizeId = productColorSize.Id;
                                getPCSDTO.SizeNumber = productColorSize.Size.SizeNumber;
                                getPCSDTO.UnitsInStock = productColorSize.UnitsInStock;
                                getAllProductColorImageDTO.ProductSizes.Add(getPCSDTO);
                            }
                            productCardDTO.ProductColors.Add(getAllProductColorImageDTO);

                        }

                        productCardDTOs.Add(productCardDTO);
                    }
                }
                else
                {
                    foreach (Product product in products)
                    {
                        ProductCardDTO productCardDTO = new()
                        {
                            ProductColors = [],
                        };
                        productCardDTO.Id = product.Id;
                        productCardDTO.NameEn = product.NameEn;
                        productCardDTO.NameAr = product.NameAr;
                        productCardDTO.Price = product.Price;
                        foreach (ProductColor productColor in product.AvailableColors)
                        {
                            GetAllProductColorImageDTO getAllProductColorImageDTO = new();
                            getAllProductColorImageDTO.ProductColorId = productColor.Id;
                            getAllProductColorImageDTO.NameEn = productColor.Color.NameEn;
                            getAllProductColorImageDTO.NameAr = productColor.Color.NameAr;
                            getAllProductColorImageDTO.Code = productColor.Color.Code;
                            getAllProductColorImageDTO.ImagePath = productColor.Images.FirstOrDefault(P => P.Id == productColor.MainImageId)?.ImagePath;
                            getAllProductColorImageDTO.ProductSizes = new List<GetPCSDTO>();
                            foreach (ProductColorSize productColorSize in productColor.AvailableSizes)
                            {
                                GetPCSDTO getPCSDTO = new();
                                getPCSDTO.ProductColorSizeId = productColorSize.Id;
                                getPCSDTO.SizeNumber = productColorSize.Size.SizeNumber;
                                getPCSDTO.UnitsInStock = productColorSize.UnitsInStock;
                                getAllProductColorImageDTO.ProductSizes.Add(getPCSDTO);
                            }
                            productCardDTO.ProductColors.Add(getAllProductColorImageDTO);

                        }

                        productCardDTOs.Add(productCardDTO);
                    }
                }



                if (productCardDTOs != null && productCardDTOs.Count > 0)
                {
                    resultView.Data = productCardDTOs;
                    resultView.IsSuccess = true;
                    resultView.Msg = "All Products Fetched Successfully";
                }
                else
                {
                    resultView.IsSuccess = false;
                    resultView.Msg = "This Type Of Filter Not Contain Any Product .. Sorry! ";
                    resultView.Data = null;
                }

            }
            else
            {

                resultView.IsSuccess = false;
                resultView.Msg = "This Type Of Filter Not Contain Any Product .. Sorry! ";
                resultView.Data = null;
            }
            return resultView;

        }


        public async Task<ResultView<List<ProductSearchDTOWithLang>>> GetProductSearchAsync(string PrdName, string Lang)
        {
            ResultView<List<ProductSearchDTOWithLang>> resultView = new();

            if (PrdName != null)
            {
                List<ProductSearchDTOWithLang> productCardDTOs = [];
                if (Lang == "En")
                {
                    productCardDTOs = [.. (await productrepoistory.GetAllAsync()).Where(N => N.NameEn.Contains(PrdName)).Select(P => new ProductSearchDTOWithLang()
                {
                    Id = P.Id,
                    Name = P.NameEn,
                    Price = P.Price,
                    ColorName = P.AvailableColors.FirstOrDefault(ac => ac.Id == P.MainColorId).Color.NameEn,
                    MainImagePath = P.AvailableColors.FirstOrDefault(ac => ac.Id == P.MainColorId)
                   .Images.FirstOrDefault(img => img.Id == P.AvailableColors.FirstOrDefault(ac => ac.Id == P.MainColorId).MainImageId).ImagePath
                })];

                }
                else
                {
                    productCardDTOs = [.. (await productrepoistory.GetAllAsync()).Where(N => N.NameAr.Contains(PrdName)).Select(P => new ProductSearchDTOWithLang()
                {
                    Id = P.Id,
                    Name = P.NameAr,
                    Price = P.Price,
                    ColorName = P.AvailableColors.FirstOrDefault(ac => ac.Id == P.MainColorId).Color.NameAr,
                    MainImagePath = P.AvailableColors.FirstOrDefault(ac => ac.Id == P.MainColorId)
                   .Images.FirstOrDefault(img => img.Id == P.AvailableColors.FirstOrDefault(ac => ac.Id == P.MainColorId).MainImageId).ImagePath
                })];
                    if (productCardDTOs.Count > 0 && productCardDTOs != null)
                    {
                        resultView.Data = productCardDTOs;
                        resultView.IsSuccess = true;
                        resultView.Msg = "Products Fetched Successfull";

                    }
                    else
                    {
                        resultView.Data = null;
                        resultView.IsSuccess = false;
                        resultView.Msg = $"لا يوجد منتج بهذا الأسم {PrdName} معذره";
                    }

                }
            }
            return resultView;
        }



    }
}
