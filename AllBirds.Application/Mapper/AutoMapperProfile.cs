using AllBirds.DTOs.UserDTOs;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AllBirds.Application.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Category
            //CreateMap<CreateOrUpdateBookDTO, Book>().ReverseMap();
            //CreateMap<GetAllBookDTO, Book>().ReverseMap();
            //CreateMap<GetOneBookDTO, Book>().ReverseMap()
            //    .ForMember(dest => dest.AuthorsName, opt => opt.MapFrom(src => src.BookAuthors!.Select(b => b.Author!.Name).ToList()))
            //    .ForMember(dest => dest.BookAuthorIds, opt => opt.MapFrom(src => src.BookAuthors!.Select(b => b.Author!.Id).ToList()));
            #endregion

            #region Product
            //CreateMap<CreateOrUpdateAuthorDTO, Author>().ReverseMap();
            //CreateMap<GetAllAuthorDTO, Author>().ReverseMap();
            //CreateMap<GetOneAuthorDTO, Author>().ReverseMap();
            #endregion

            #region CategoryProduct
            //CreateMap<CreateOrUpdateBookAuthorDTO, BookAuthor>().ReverseMap();
            //CreateMap<GetAllBookAuthorDTO, BookAuthor>().ReverseMap()
            //    .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title))
            //    .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name));
            //CreateMap<GetOneBookAuthorDTO, BookAuthor>().ReverseMap()
            //    .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title))
            //    .ForMember(dest => dest.BookPrice, opt => opt.MapFrom(src => src.Book.Price))
            //    .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name))
            //    .ForMember(dest => dest.AuthorAge, opt => opt.MapFrom(src => src.Author.Age));
            #endregion

            #region Client
            //CreateMap<CreateOrUpdateBookAuthorDTO, BookAuthor>().ReverseMap();
            //CreateMap<GetAllBookAuthorDTO, BookAuthor>().ReverseMap()
            //    .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title))
            //    .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name));
            //CreateMap<GetOneBookAuthorDTO, BookAuthor>().ReverseMap()
            //    .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title))
            //    .ForMember(dest => dest.BookPrice, opt => opt.MapFrom(src => src.Book.Price))
            //    .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name))
            //    .ForMember(dest => dest.AuthorAge, opt => opt.MapFrom(src => src.Author.Age));
            #endregion

            #region ClientFavorite
            //CreateMap<CreateOrUpdateBookAuthorDTO, BookAuthor>().ReverseMap();
            //CreateMap<GetAllBookAuthorDTO, BookAuthor>().ReverseMap()
            //    .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title))
            //    .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name));
            //CreateMap<GetOneBookAuthorDTO, BookAuthor>().ReverseMap()
            //    .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title))
            //    .ForMember(dest => dest.BookPrice, opt => opt.MapFrom(src => src.Book.Price))
            //    .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name))
            //    .ForMember(dest => dest.AuthorAge, opt => opt.MapFrom(src => src.Author.Age));
            #endregion


            #region OrderMaster
            //CreateMap<CreateOrUpdateBookAuthorDTO, BookAuthor>().ReverseMap();
            //CreateMap<GetAllBookAuthorDTO, BookAuthor>().ReverseMap()
            //    .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title))
            //    .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name));
            //CreateMap<GetOneBookAuthorDTO, BookAuthor>().ReverseMap()
            //    .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title))
            //    .ForMember(dest => dest.BookPrice, opt => opt.MapFrom(src => src.Book.Price))
            //    .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name))
            //    .ForMember(dest => dest.AuthorAge, opt => opt.MapFrom(src => src.Author.Age));
            #endregion


            #region OrderDetail
            //CreateMap<CreateOrUpdateBookAuthorDTO, BookAuthor>().ReverseMap();
            //CreateMap<GetAllBookAuthorDTO, BookAuthor>().ReverseMap()
            //    .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title))
            //    .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name));
            //CreateMap<GetOneBookAuthorDTO, BookAuthor>().ReverseMap()
            //    .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title))
            //    .ForMember(dest => dest.BookPrice, opt => opt.MapFrom(src => src.Book.Price))
            //    .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name))
            //    .ForMember(dest => dest.AuthorAge, opt => opt.MapFrom(src => src.Author.Age));
            #endregion


            #region ProductImage
            //CreateMap<CreateOrUpdateBookAuthorDTO, BookAuthor>().ReverseMap();
            //CreateMap<GetAllBookAuthorDTO, BookAuthor>().ReverseMap()
            //    .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title))
            //    .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name));
            //CreateMap<GetOneBookAuthorDTO, BookAuthor>().ReverseMap()
            //    .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title))
            //    .ForMember(dest => dest.BookPrice, opt => opt.MapFrom(src => src.Book.Price))
            //    .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name))
            //    .ForMember(dest => dest.AuthorAge, opt => opt.MapFrom(src => src.Author.Age));
            #endregion


            #region ProductRating
            //CreateMap<CreateOrUpdateBookAuthorDTO, BookAuthor>().ReverseMap();
            //CreateMap<GetAllBookAuthorDTO, BookAuthor>().ReverseMap()
            //    .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title))
            //    .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name));
            //CreateMap<GetOneBookAuthorDTO, BookAuthor>().ReverseMap()
            //    .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title))
            //    .ForMember(dest => dest.BookPrice, opt => opt.MapFrom(src => src.Book.Price))
            //    .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name))
            //    .ForMember(dest => dest.AuthorAge, opt => opt.MapFrom(src => src.Author.Age));
            #endregion



            CreateMap<AdminAccRegisterDTOs, IdentityUser>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) 
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            .ReverseMap();
            CreateMap<GetAccountAdminDTOs , IdentityUser>().ReverseMap();
            CreateMap<LoginUserDTOs , IdentityUser>().ReverseMap();

        }
    }
}
