using AllBirds.DTOs.AccountDTOs;
using AllBirds.DTOs.ColorDTOs;
using AllBirds.DTOs.CouponDTOs;
using AllBirds.DTOs.OrderStateDTOs;
using AllBirds.DTOs.ProductDTOs;
using AllBirds.DTOs.SizeDTOs;
using AllBirds.Models;
using AutoMapper;
using AllBirds.DTOs.CategoryDTOs;
using AllBirds.Models;
using AllBirds.DTOs.CategorySizeDTOS;
using AllBirds.DTOs.CategorySizeDTOS;
using AllBirds.DTOs.CategoryProductDTOS;
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
            CreateMap<CreateOrUpdateCategoryDTO, Category>().ReverseMap();
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
            #endregion

            #region OrderMaster
            //CreateMap<GetAllBookAuthorDTO, BookAuthor>().ReverseMap()
            //    .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name));
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

            CreateMap<Product, CUProductDTO>()
                .ForMember(dest => dest.HighlightsAr, opt => opt.MapFrom(src => SplitJoinedString(src.HighlightsAr)))
                .ForMember(dest => dest.HighlightsEn, opt => opt.MapFrom(src => SplitJoinedString(src.HighlightsEn)))
                .ForMember(dest => dest.SustainableMaterialsAr, opt => opt.MapFrom(src => SplitJoinedString(src.SustainableMaterialsAr)))
                .ForMember(dest => dest.SustainableMaterialsEn, opt => opt.MapFrom(src => SplitJoinedString(src.SustainableMaterialsEn)));
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
