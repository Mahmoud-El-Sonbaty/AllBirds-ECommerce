using AllBirds.DTOs.AccountDTOs;
using AllBirds.DTOs.ColorDTOs;
using AllBirds.DTOs.CouponDTOs;
using AllBirds.DTOs.OrderStateDTOs;
using AllBirds.DTOs.ProductDTOs;
using AllBirds.DTOs.SizeDTOs;
using AllBirds.Models;
using AutoMapper;
using AllBirds.DTOs.CategoryDTOs;
using AllBirds.DTOs.CategorySizeDTOS;
using AllBirds.DTOs.CategoryProductDTOS;
using AllBirds.DTOs.OrderDetailsDTOs;
using AllBirds.DTOs.OrderMasterDTOs;
namespace AllBirds.Application.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Account
            CreateMap<CUAccountDTO, CustomUser>().ReverseMap();
            CreateMap<AccountLoginDTO, CustomUser>().ReverseMap();
            #endregion

            #region Category
            CreateMap<CUCategoryDTO, Category>().ReverseMap();
            CreateMap<GetAllCategoryDTO, Category>().ReverseMap();
            //CreateMap<GetAllBookAuthorDTO, BookAuthor>().ReverseMap()
            //    .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name));
            #endregion

            #region CategoryProduct
            //CreateMap<CreateOrUpdateBookAuthorDTO, BookAuthor>().ReverseMap();
            //CreateMap<GetAllBookAuthorDTO, BookAuthor>().ReverseMap()
            //    .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title))
            //    .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name));
            //CreateMap<GetOneBookAuthorDTO, BookAuthor>().ReverseMap()
            //    .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title));
            #endregion

            #region CategorySize
            //CreateMap<GetOneBookAuthorDTO, BookAuthor>().ReverseMap()
            //    .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title));
            CreateMap<CreateOrUpdateCategorySizeDTO, CategorySize>().ReverseMap();
            CreateMap<GetAllCategorySizeDTO, CategorySize>().ReverseMap();
            CreateMap<GetOneCategorySizeDTO, CategorySize>().ReverseMap();
            #endregion

            #region ClientFavorite
            //CreateMap<GetAllBookAuthorDTO, BookAuthor>().ReverseMap()
            //    .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name));
            #endregion

            #region Color
            CreateMap<CUColorDTO, Color>().ReverseMap();
            CreateMap<GetColorDTO, Color>().ReverseMap();
            #endregion

            #region Coupon
            CreateMap<CUCouponDTO, Coupon>().ReverseMap();
            CreateMap<GetCouponDTO, Coupon>().ReverseMap();
            #endregion

            #region OrderDetail
            //CreateMap<GetAllBookAuthorDTO, BookAuthor>().ReverseMap()
            //    .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name));
            CreateMap<GetAllOrderDetailsDTO, OrderDetail>().ReverseMap()
                .ForMember(dest=>dest.OrderMasterNo,opt=>opt.MapFrom(src=>src.OrderMaster.OrderNo));
            CreateMap<GetOneOrderDetailsDTO, OrderDetail>().ReverseMap();
            CreateMap<CreateOrderDetailsDTO, OrderDetail>().ReverseMap();
            CreateMap<OrderDetail,ProductColorSizeImageDTO>()
                .ForMember(dest=>dest.ColorNameAR,opt=>opt.MapFrom(src=>src.ProductColorSize.ProductColor.Color.NameAr))
                .ForMember(dest=>dest.ColorNameEN,opt=>opt.MapFrom(src=>src.ProductColorSize.ProductColor.Color.NameEn))
                .ForMember(dest=>dest.ProductNameAR,opt=>opt.MapFrom(src=>src.ProductColorSize.ProductColor.Product.NameAr))
                .ForMember(dest=>dest.ProductNameEN,opt=>opt.MapFrom(src=>src.ProductColorSize.ProductColor.Product.NameEn))
                .ForMember(dest=>dest.Size,opt=>opt.MapFrom(src=>src.ProductColorSize.Size.SizeNumber))
                .ForMember(dest=>dest.MainImage,opt=>opt.MapFrom(src=>src.ProductColorSize.ProductColor.Images.FirstOrDefault().ImagePath))
                ;
            #endregion

            #region OrderMaster
            //CreateMap<GetAllBookAuthorDTO, BookAuthor>().ReverseMap()
            //    .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name));


            CreateMap<GetAllOrderMastersDTO, OrderMaster>().ReverseMap()
                  .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => $"{src.Client.FirstName} {src.Client.LastName}"))
                .ForMember(dest => dest.OrderStateName, opt => opt.MapFrom(src => src.OrderState.StateEn))
                .ForMember(dest => dest.DiscountPerctnage, opt => opt.MapFrom(src => $"{src.Coupon.Discount} %"))
                .ForMember(dest => dest.DiscountAmount, opt => opt.MapFrom(src => src.Coupon.Discount * src.Total));

            CreateMap<createOrderMasterDTO, OrderMaster>().ReverseMap();
            CreateMap<GetOneOdrerMasterDTO, OrderMaster>().ReverseMap()
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => $"{src.Client.FirstName} {src.Client.LastName}"))
                .ForMember(dest => dest.OrderStateName, opt => opt.MapFrom(src => src.OrderState.StateEn))
                .ForMember(dest => dest.DiscountPerctnage, opt => opt.MapFrom(src => $"{src.Coupon.Discount} %"))
                .ForMember(dest => dest.DiscountAmount, opt => opt.MapFrom(src => src.Coupon.Discount * src.Total));
            #endregion

            #region OrderState
            CreateMap<CUOrderStateDTO, OrderState>().ReverseMap();
            CreateMap<GetOrderStateDTO, OrderState>().ReverseMap();
            #endregion

            #region Product
            CreateMap<GetAllProductDTO, Product>().ReverseMap();
            CreateMap<CUProductDTO, Product>()
                .ForMember(dest => dest.HighlightsAr, opt => opt.MapFrom(src => JoinStringList(src.HighlightsAr)))
                .ForMember(dest => dest.HighlightsEn, opt => opt.MapFrom(src => JoinStringList(src.HighlightsEn)))
                .ForMember(dest => dest.SustainableMaterialsAr, opt => opt.MapFrom(src => JoinStringList(src.SustainableMaterialsAr)))
                .ForMember(dest => dest.SustainableMaterialsEn, opt => opt.MapFrom(src => JoinStringList(src.SustainableMaterialsEn)));
                //.ForMember(dest => dest.Categories, opt => opt.MapFrom(src => new CategoryProduct() { CategoryId = src.CategoriesId[0], ProductId = src.Id }));

            CreateMap<Product, CUProductDTO>()
                .ForMember(dest => dest.HighlightsAr, opt => opt.MapFrom(src => SplitJoinedString(src.HighlightsAr)))
                .ForMember(dest => dest.HighlightsEn, opt => opt.MapFrom(src => SplitJoinedString(src.HighlightsEn)))
                .ForMember(dest => dest.SustainableMaterialsAr, opt => opt.MapFrom(src => SplitJoinedString(src.SustainableMaterialsAr)))
                .ForMember(dest => dest.SustainableMaterialsEn, opt => opt.MapFrom(src => SplitJoinedString(src.SustainableMaterialsEn)))
                .ForMember(dest => dest.CategoriesId, opt => opt.MapFrom(src => src.Categories.Select(cp => cp.CategoryId).ToList()));
            //CreateMap<GetAllBookAuthorDTO, BookAuthor>().ReverseMap()
            //    .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name));
            #endregion

            #region ProductColor
            //CreateMap<GetAllBookAuthorDTO, BookAuthor>().ReverseMap()
            //    .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name));
            #endregion

            #region ProductColorImage
            //CreateMap<GetAllBookAuthorDTO, BookAuthor>().ReverseMap()
            //    .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name));
            #endregion

            #region ProductReview
            //CreateMap<GetAllBookAuthorDTO, BookAuthor>().ReverseMap()
            //    .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name));
            #endregion

            #region ProductSize
            //CreateMap<GetAllBookAuthorDTO, BookAuthor>().ReverseMap()
            //    .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name));
            #endregion

            #region Size
            CreateMap<CUSizeDTO, Size>().ReverseMap();
            CreateMap<GetSizeDTO, Size>().ReverseMap();
            #endregion



            //CreateMap<AdminAccRegisterDTOs, IdentityUser>()
            //.ForMember(dest => dest.Id, opt => opt.Ignore()) 
            //.ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            //.ReverseMap();
            //CreateMap<GetAccountAdminDTOs , IdentityUser>().ReverseMap();
            //CreateMap<LoginUserDTOs , IdentityUser>().ReverseMap();

        }
        private static string? JoinStringList(List<string>? list)
        {
            if (list == null)
                return null;

            return list.Count > 1
                ? String.Join("~@#$%&", list)
                : list.FirstOrDefault();
        }
        private static List<string>? SplitJoinedString(string? str) => str is not null ? str.Split("~@#$%&", StringSplitOptions.RemoveEmptyEntries).ToList() : null;
    }
}
