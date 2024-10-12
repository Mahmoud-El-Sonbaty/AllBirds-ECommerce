using AllBirds.DTOs.AccountDTOs;
using AllBirds.DTOs.ColorDTOs;
using AllBirds.DTOs.CouponDTOs;
using AllBirds.DTOs.OrderStateDTOs;
using AllBirds.DTOs.SizeDTOs;
using AllBirds.Models;
using AutoMapper;

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
            //CreateMap<GetAllBookAuthorDTO, BookAuthor>().ReverseMap()
            //    .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name));
            #endregion

            #region CategoryProduct
            //CreateMap<GetOneBookAuthorDTO, BookAuthor>().ReverseMap()
            //    .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title));
            #endregion

            #region CategorySize
            //CreateMap<GetOneBookAuthorDTO, BookAuthor>().ReverseMap()
            //    .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title));
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
    }
}
