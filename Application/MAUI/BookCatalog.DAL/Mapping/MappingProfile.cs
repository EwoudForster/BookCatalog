using AutoMapper;
using BookCatalog.DAL.DTO;
using BookCatalog.DAL.DTO.CalculatedValueModel;
using BookCatalog.DAL.Logging;
using BookCatalog.DAL.Mapping.DTO;
using BookCatalog.DAL.Models;
using BookCatalog.DAL.Models.CalculatedValueModel;

namespace BookCatalog.DAL.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<LoggingEntry, LoggingEntryDTO>();
        CreateMap<Book, BookDTO>()
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres))
            .ForMember(dest => dest.Pictures, opt => opt.MapFrom(src => src.Pictures))
            .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher))
            .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors))
            .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.Reviews))
            .ForMember(dest => dest.MoreInfos, opt => opt.MapFrom(src => src.MoreInfos))
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));
        CreateMap<Author, AuthorDTOShort>();
        CreateMap<Order, OrderDTOShort>();

        CreateMap<Picture, PictureDTOShort>();
        CreateMap<Review, ReviewDTOShort>();
        CreateMap<MoreInfo, MoreInfoDTOShort>();
        CreateMap<Genre, GenreDTOShort>();
        CreateMap<Publisher, PublisherDTOShort>();

        CreateMap<PublisherCalculated, PublisherCalculatedDTO>();
        CreateMap<GenreCalculated, GenreCalculatedDTO>();
        CreateMap<AuthorCalculated, AuthorCalculatedDTO>();
        CreateMap<MoreInfoCalculated, MoreInfoCalculatedDTO>();

        CreateMap<OrderDTO, OrderDTOShort>();
        CreateMap<OrderItemDTO, OrderItemDTOShort>();
        CreateMap<CreateOrderDTO, Order>();
        CreateMap<CreateOrderItemDTO, OrderItem>();
        CreateMap<Order, OrderItemDTO>();
        CreateMap<OrderItem, OrderItemDTO>();
        CreateMap<OrderItem, OrderItemDTOShort>();


        CreateMap<Publisher, PublisherDTO>()
            .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books));

        CreateMap<Author, AuthorDTO>().
            ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books));

        CreateMap<Genre, GenreDTO>().
            ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books));

        CreateMap<Picture, PictureDTO>()
            .ForMember(dest => dest.Book, opt => opt.MapFrom(src => src.Book));
        CreateMap<MoreInfo, MoreInfoDTO>()
            .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books));

        CreateMap<Review, ReviewDTO>()
            .ForMember(dest => dest.Book, opt => opt.MapFrom(src => src.Book))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));

        CreateMap<User, UserDTO>();
        CreateMap<BookStore, BookStoreDTO>();
        CreateMap<GeneralStatistics, GeneralStatisticsDTO>();

        CreateMap<Order, OrderDTO>();

        CreateMap<BookDTO, Book>();
        CreateMap<BookCreateDTO, Book>();
        CreateMap<Book, BookCreateDTO>();


    }
}
