using AutoMapper;
using BookCatalog.DAL.DTO;

namespace BookCatalog.DAL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapping from Author to AuthorDTO
            CreateMap<Author, AuthorDTO>()
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src =>
                    src.Books.Select(b => new BookDTO
                    {
                        Id = b.Id,
                        Title = b.Title,
                        CreationDate = b.CreationDate,
                        LastUpdated = b.LastUpdated,
                        PublicationYear = b.PublicationYear,
                        IsAvailable = b.IsAvailable,
                        ImgUrl = b.ImgUrl,
                        ISBN = b.ISBN,
                        PageCount = b.PageCount,
                        Price = b.Price,
                        PublisherId = b.PublisherId
                    }).ToList()
                ));

            // Book to BookDTO
            CreateMap<Book, BookDTO>()
                .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher))
                        .ForMember(dest => dest.Genres, opt => opt.MapFrom(src =>
                    src.Genres.Select(b => new GenreDTO
                    {
                        Id = b.Id,
                        CreationDate = b.CreationDate,
                        LastUpdated = b.LastUpdated,
                        Name = b.Name,
                    }).ToList()))
                    .ForMember(dest => dest.Authors, opt => opt.MapFrom(src =>
                    src.Authors.Select(b => new AuthorDTO
                    {
                        Id = b.Id,
                        CreationDate = b.CreationDate,
                        LastUpdated = b.LastUpdated,
                        Name = b.Name,
                    }).ToList()
                ));

            // Publisher to PublisherDTO
            CreateMap<Publisher, PublisherDTO>()
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src =>
                    src.Books.Select(b => new BookDTO
                    {
                        Id = b.Id,
                        Title = b.Title,
                        CreationDate = b.CreationDate,
                        LastUpdated = b.LastUpdated,
                        PublicationYear = b.PublicationYear,
                        IsAvailable = b.IsAvailable,
                        ImgUrl = b.ImgUrl,
                        ISBN = b.ISBN,
                        PageCount = b.PageCount,
                        Price = b.Price,
                        PublisherId = b.PublisherId
                    }).ToList()
                ));

            // Genre to GenreDTO
            CreateMap<Genre, GenreDTO>()
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src =>
                    src.Books
                ));

            // Book to BookCreateDTO
            CreateMap<Book, BookCreateDTO>();
        }
    }
}
