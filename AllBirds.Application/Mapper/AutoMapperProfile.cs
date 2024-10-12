using AllBirds.DTOs.CategoryDTOs;
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
            #region Category
            CreateMap<CreateOrUpdateCategoryDTO, Category>().ReverseMap();
            CreateMap<GetAllCategoryDTO, Category>().ReverseMap();
            CreateMap<GetOneCategoryDTO, Category>().ReverseMap();
            //.ForMember(dest => dest.AuthorsName, opt => opt.MapFrom(src => src.BookAuthors!.Select(b => b.Author!.Name).ToList()))
            // .ForMember(dest => dest.BookAuthorIds, opt => opt.MapFrom(src => src.BookAuthors!.Select(b => b.Author!.Id).ToList()));
            #endregion

            #region Product
            //CreateMap<CreateOrUpdateAuthorDTO, Author>().ReverseMap();
            //CreateMap<GetAllAuthorDTO, Author>().ReverseMap();
            //CreateMap<GetOneAuthorDTO, Author>().ReverseMap();
            #endregion

            #region CategoryProduct

            CreateMap<CreateOrUpdateCategoryProductDTO, CategoryProduct>().ReverseMap();
            CreateMap<GetAllCategoryProductDTO, CategoryProduct>().ReverseMap();
            CreateMap<GetOneCategoryProductDTO, CategoryProduct>().ReverseMap();

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

            //add by hossam
            #region CategorySize

            CreateMap<CreateOrUpdateCategorySizeDTO, CategorySize>().ReverseMap();
            CreateMap<GetAllCategorySizeDTO, CategorySize>().ReverseMap();
            CreateMap<GetOneCategorySizeDTO, CategorySize>().ReverseMap();

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
        }
    }
}
