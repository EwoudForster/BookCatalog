using BookCatalog.DAL.Services.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BookCatalog.DAL.Storage.DataBase.Seeder
{
    public static class DbInitializer
    {
        public async static Task Seed(IApplicationBuilder applicationBuilder)
        {
            BookDbContext context = applicationBuilder.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<BookDbContext>();

            if (!await context.Genres.AnyAsync())
            {
                List<Genre> Genres = new() {
                        new Genre { Name = "Fiction" },
                        new Genre { Name = "Science Fiction" },
                        new Genre { Name = "Romance" },
                        new Genre { Name = "Fantasy" },
                        new Genre { Name = "Young Adult" },
                        new Genre { Name = "Mystery" },
                        new Genre { Name = "Historical Fiction" },
                        new Genre { Name = "Epic Poetry" },
                        new Genre { Name = "Horror" },
                        new Genre { Name = "Adventure" },
                        new Genre { Name = "Magical Realism" },
                        new Genre { Name = "Literary Fiction" },
                        new Genre { Name = "Gothic Fiction" },
                        new Genre { Name = "Thriller" },
                        new Genre { Name = "Memoir" },
                        new Genre { Name = "Non-fiction" },
                        new Genre { Name = "Autobiography" },
                        new Genre { Name = "Post-Apocalyptic" },
                        new Genre { Name = "Dystopian" },
                        new Genre { Name = "Satire" },
                        new Genre { Name = "Self-help" },
                        new Genre { Name = "Personal Finance" },
                        new Genre { Name = "Psychology" }
                    };
                await context.Genres.AddRangeAsync(Genres);
                await context.SaveChangesAsync();

            }

            if (!await context.Authors.AnyAsync())
            {
                List<Author> Authors = new(){
                        new Author { Name = "Harper Lee" },
                        new Author { Name = "George Orwell" },
                        new Author { Name = "F. Scott Fitzgerald" },
                        new Author { Name = "Jane Austen" },
                        new Author { Name = "J.R.R. Tolkien" },
                        new Author { Name = "J.K. Rowling" },
                        new Author { Name = "J.D. Salinger" },
                        new Author { Name = "Aldous Huxley" },
                        new Author { Name = "Paulo Coelho" },
                        new Author { Name = "Suzanne Collins" },
                        new Author { Name = "Dan Brown" },
                        new Author { Name = "Charles Dickens" },
                        new Author { Name = "Homer" },
                        new Author { Name = "Stephen King" },
                        new Author { Name = "John Steinbeck" },
                        new Author { Name = "Herman Melville" },
                        new Author { Name = "Gabriel Garc\u00EDa M\u00E1rquez" },
                        new Author { Name = "Fyodor Dostoevsky" },
                        new Author { Name = "Emily Brontë" },
                        new Author { Name = "Oscar Wilde" },
                        new Author { Name = "Alexandre Dumas" },
                        new Author { Name = "Alex Michaelides" },
                        new Author { Name = "Delia Owens" },
                        new Author { Name = "Tara Westover" },
                        new Author { Name = "Yuval Noah Harari" },
                        new Author { Name = "Michelle Obama" },
                        new Author { Name = "Paula Hawkins" },
                        new Author { Name = "George R.R. Martin" },
                        new Author { Name = "Khaled Hosseini" },
                        new Author { Name = "Yann Martel" },
                        new Author { Name = "Markus Zusak" },
                        new Author { Name = "Cormac McCarthy" },
                        new Author { Name = "Ray Bradbury" },
                        new Author { Name = "Margaret Atwood" },
                        new Author { Name = "Lois Lowry" },
                        new Author { Name = "Andy Weir" },
                        new Author { Name = "Gillian Flynn" },
                        new Author { Name = "John Green" },
                        new Author { Name = "Douglas Adams" },
                        new Author { Name = "Joseph Heller" },
                        new Author { Name = "Ernest Cline" },
                        new Author { Name = "Patrick Rothfuss" },
                        new Author { Name = "Frank Herbert" },
                        new Author { Name = "M. Scott Peck" },
                        new Author { Name = "Robert T. Kiyosaki" },
                        new Author { Name = "Dale Carnegie" },
                        new Author { Name = "Daniel Kahneman" }
                    };
                await context.Authors.AddRangeAsync(Authors);
                await context.SaveChangesAsync();
            }

            if (!await context.Publishers.AnyAsync())
            {
                List<Publisher> Publishers = new() {
                    new Publisher{Name = "J. B. Lippincott \u0026 Co."},
                    new Publisher { Name = "Secker \u0026 Warburg" },
                    new Publisher { Name = "Charles Scribner\u0027s Sons" },
                    new Publisher { Name = "T. Egerton" },
                    new Publisher { Name = "George Allen \u0026 Unwin" },
                    new Publisher { Name = "Bloomsbury" },
                    new Publisher { Name = "Allen \u0026 Unwin" },
                    new Publisher { Name = "Little, Brown and Company" },
                    new Publisher { Name = "Chatto \u0026 Windus" },
                    new Publisher { Name = "HarperOne" },
                    new Publisher { Name = "Scholastic Press" },
                    new Publisher { Name = "Doubleday" },
                    new Publisher { Name = "Chapman \u0026 Hall" },
                    new Publisher { Name = "Oxford University Press" },
                    new Publisher { Name = "The Viking Press" },
                    new Publisher { Name = "Harper \u0026 Brothers" },
                    new Publisher { Name = "Harper \u0026 Row" },
                    new Publisher { Name = "The Russian Messenger" },
                    new Publisher { Name = "Thomas Cautley Newby" },
                    new Publisher { Name = "Lippincott's Monthly Magazine" },
                    new Publisher { Name = "P\u00E9tion" },
                    new Publisher { Name = "Celadon Books" },
                    new Publisher { Name = "G.P. Putnam's Sons" },
                    new Publisher { Name = "Random House" },
                    new Publisher { Name = "Harper" },
                    new Publisher { Name = "Crown" },
                    new Publisher { Name = "Riverhead Books" },
                    new Publisher { Name = "Bantam Spectra" },
                    new Publisher { Name = "Knopf Canada" },
                    new Publisher { Name = "Picador" },
                    new Publisher { Name = "Alfred A. Knopf" },
                    new Publisher { Name = "Ballantine Books" },
                    new Publisher { Name = "McClelland & Stewart" },
                    new Publisher { Name = "Houghton Mifflin" },
                    new Publisher { Name = "Crown Publishing Group" },
                    new Publisher { Name = "Dutton Books" },
                    new Publisher { Name = "Pan Books" },
                    new Publisher { Name = "Simon \u0026 Schuster" },
                    new Publisher { Name = "DAW Books" },
                    new Publisher { Name = "Chilton Books" },
                    new Publisher { Name = "Warner Books" },
                    new Publisher { Name = "Farrar, Straus and Giroux" }
                };
                await context.Publishers.AddRangeAsync(Publishers);
                await context.SaveChangesAsync();
            }

            if (!await context.Books.AnyAsync())
            {
                List<Book> Books = new() {

                    new Book
                    {
                        Title = "To Kill a Mockingbird",
                        Authors = new List<Author> { context.Authors.FirstOrDefault(x => x.Name == "Harper Lee") },
                        PublicationYear = 1960,
                        Genres = new List<Genre> { context.Genres.FirstOrDefault(x => x.Name == "Fiction") },
                        ISBN = "978-0061120084",
                        Publisher = context.Publishers.FirstOrDefault(x => x.Name == "J. B. Lippincott \u0026 Co."),
                        PageCount = 336,
                        Price = 12.99M,
                        IsAvailable = true,
                        ImgUrl = "https://th.bing.com/th/id/OIP.W1ngiF2AkQaO78CY9yC1HQHaLd?rs=1&pid=ImgDetMain",
                        Id = Guid.Parse("b58bfc10-270a-4395-697d-08dd686bd604")
                    },
                       new Book
                       {
                           Title = "1984",
                           Authors = new List<Author> { context.Authors.FirstOrDefault(x => x.Name == "George Orwell") },
                           PublicationYear = 1949,
                           Genres = new List<Genre> { context.Genres.FirstOrDefault(x => x.Name == "Science Fiction") },
                           ISBN = "978-0451524935",
                           Publisher = context.Publishers.FirstOrDefault(x => x.Name == "Secker \u0026 Warburg"),
                           PageCount = 328,
                           Price = 9.99M,
                           IsAvailable = true,
                           ImgUrl = "https://th.bing.com/th/id/R.e5f82111504d28779262bf2dca0fad56?rik=9etdGLc9mstPyw&pid=ImgRaw&r=0"
                       },
                       new Book
                       {
                           Title = "The Great Gatsby",
                           Authors = new List<Author> { context.Authors.FirstOrDefault(x => x.Name == "F. Scott Fitzgerald") },
                           PublicationYear = 1925,
                           Genres = new List<Genre> { context.Genres.FirstOrDefault(x => x.Name == "Fiction") },
                           ISBN = "978-0743273565",
                           Publisher = context.Publishers.FirstOrDefault(x => x.Name == "Charles Scribner\u0027s Sons"),
                           PageCount = 180,
                           Price = 8.99M,
                           IsAvailable = true,
                           ImgUrl = "https://hachette.imgix.net/books/9780762498130.jpg?auto=compress,format"
                       },
                       new Book
                       {
                           Title = "Pride and Prejudice",
                           Authors = new List<Author> { context.Authors.FirstOrDefault(x => x.Name == "Jane Austen") },
                           PublicationYear = 1813,
                           Genres = new List<Genre> { context.Genres.FirstOrDefault(x => x.Name == "Romance") },
                           ISBN = "978-0141439518",
                           Publisher = context.Publishers.FirstOrDefault(x => x.Name == "T. Egerton"),
                           PageCount = 432,
                           Price = 7.99M,
                           IsAvailable = true,
                           ImgUrl = "https://cdn2.penguin.com.au/covers/original/9780141330167.jpg"
                       },
                       new Book
                       {
                           Title = "The Hobbit",
                           Authors = new List<Author> { context.Authors.FirstOrDefault(x => x.Name == "J.R.R. Tolkien") },
                           PublicationYear = 1937,
                           Genres = new List<Genre> { context.Genres.FirstOrDefault(x => x.Name == "Fantasy") },
                           ISBN = "978-0547928227",
                           Publisher = context.Publishers.FirstOrDefault(x => x.Name == "George Allen \u0026 Unwin"),
                           PageCount = 300,
                           Price = 14.99M,
                           IsAvailable = true,
                           ImgUrl = "https://th.bing.com/th/id/R.8947741487564891cf18c8b32752c2bf?rik=gsUGQLJSJMRUSQ&pid=ImgRaw&r=0"
                       },
                       new Book
                       {
                           Title = "Harry Potter and the Philosopher\u0027s Stone",
                           Authors = new List<Author> { context.Authors.FirstOrDefault(x => x.Name == "J.K. Rowling") },
                           PublicationYear = 1997,
                           Genres = new List<Genre> { context.Genres.FirstOrDefault(x => x.Name == "Fantasy") },
                           ISBN = "978-1408855652",
                           Publisher = context.Publishers.FirstOrDefault(x => x.Name == "Bloomsbury"),
                           PageCount = 223,
                           Price = 10.99M,
                           IsAvailable = true,
                           ImgUrl = "https://res.cloudinary.com/bloomsbury-atlas/image/upload/w_568,c_scale/jackets/9781408855652.jpg"
                       },
                       new Book
                       {
                           Title = "The Lord of the Rings",
                           Authors = new List<Author> { context.Authors.FirstOrDefault(x => x.Name == "J.R.R. Tolkien") },
                           PublicationYear = 1954,
                           Genres = new List<Genre> { context.Genres.FirstOrDefault(x => x.Name == "Fantasy") },
                           ISBN = "978-0618640157",
                           Publisher = context.Publishers.FirstOrDefault(x => x.Name == "Allen \u0026 Unwin"),
                           PageCount = 1178,
                           Price = 29.99M,
                           IsAvailable = true,
                           ImgUrl = "https://cdn11.bigcommerce.com/s-gibnfyxosi/images/stencil/1920w/products/154740/156431/51eq24cRtRL__98083.1615576774.jpg?c=1"
                       },
                       new Book
                       {
                           Title = "The Catcher in the Rye",
                           Authors = new List<Author> { context.Authors.FirstOrDefault(x => x.Name == "J.D. Salinger") },
                           PublicationYear = 1951,
                           Genres = new List<Genre> { context.Genres.FirstOrDefault(x => x.Name == "Fiction") },
                           ISBN = "978-0316769488",
                           Publisher = context.Publishers.FirstOrDefault(x => x.Name == "Little, Brown and Company"),
                           PageCount = 277,
                           Price = 8.99M,
                           IsAvailable = true,
                           ImgUrl = "https://th.bing.com/th/id/OIP.7vHm4DvQa3iwZAoiOFcZtwHaKr?rs=1&pid=ImgDetMain"
                       },
                       new Book
                       {
                           Title = "Brave New World",
                           Authors = new List<Author> { context.Authors.FirstOrDefault(x => x.Name == "Aldous Huxley") },
                           PublicationYear = 1932,
                           Genres = new List<Genre> { context.Genres.FirstOrDefault(x => x.Name == "Science Fiction") },
                           ISBN = "978-0060850524",
                           Publisher = context.Publishers.FirstOrDefault(x => x.Name == "Chatto \u0026 Windus"),
                           PageCount = 288,
                           Price = 10.99M,
                           IsAvailable = true,
                           ImgUrl = "https://th.bing.com/th/id/R.9f4b5965d0bc99a4e4e0130b713285a0?rik=d5ac2urjW9GuEw&pid=ImgRaw&r=0"

                       },
                       new Book
                       {
                           Title = "The Alchemist",
                           Authors = new List<Author> { context.Authors.FirstOrDefault(x => x.Name == "Paulo Coelho") },
                           PublicationYear = 1988,
                           Genres = new List<Genre> { context.Genres.FirstOrDefault(x => x.Name == "Fiction") },
                           ISBN = "978-0062315007",
                           Publisher = context.Publishers.FirstOrDefault(x => x.Name == "HarperOne"),
                           PageCount = 208,
                           Price = 11.99M,
                           IsAvailable = true,
                           ImgUrl = "https://th.bing.com/th/id/OIP.9-v0mfum2BK2IFqSLAXf3gHaL7?rs=1&pid=ImgDetMain"
                       },
                       new Book
                       {
                           Title = "The Hunger Games",
                           Authors = new List<Author> { context.Authors.FirstOrDefault(x => x.Name == "Suzanne Collins") },
                           PublicationYear = 2008,
                           Genres = new List<Genre> { context.Genres.FirstOrDefault(x => x.Name == "Young Adult") },
                           ISBN = "978-0439023481",
                           Publisher = context.Publishers.FirstOrDefault(x => x.Name == "Scholastic Press"),
                           PageCount = 374,
                           Price = 12.50M,
                           IsAvailable = true,
                           ImgUrl = "https://th.bing.com/th/id/OIP.QdhjN4JxwcYPvlvKjwPKygHaLH?rs=1&pid=ImgDetMain"

                       },
                       new Book
                       {
                           Title = "The Da Vinci Code",
                           Authors = new List<Author> { context.Authors.FirstOrDefault(x => x.Name == "Dan Brown") },
                           PublicationYear = 2003,
                           Genres = new List<Genre> { context.Genres.FirstOrDefault(x => x.Name == "Mystery") },
                           ISBN = "978-0307474278",
                           Publisher = context.Publishers.FirstOrDefault(x => x.Name == "Doubleday"),
                           PageCount = 489,
                           Price = 9.99M,
                           IsAvailable = true,
                           ImgUrl = "https://th.bing.com/th/id/R.bd9481b3c05998fdbb7b1fc49ba1f3ef?rik=yUOPSX2aJliuAA&pid=ImgRaw&r=0"
                       },
                       new Book
                       {
                           Title = "A Tale of Two Cities",
                           Authors = new List<Author> { context.Authors.FirstOrDefault(x => x.Name == "Charles Dickens") },
                           PublicationYear = 1859,
                           Genres = new List<Genre> { context.Genres.FirstOrDefault(x => x.Name == "Historical Fiction") },
                           ISBN = "978-0141439600",
                           Publisher = context.Publishers.FirstOrDefault(x => x.Name == "Chapman \u0026 Hall"),
                           PageCount = 489,
                           Price = 6.99M,
                           IsAvailable = true,
                           ImgUrl = "https://th.bing.com/th/id/OIP.B4wjpTB6Gg1o4O72nV6A7AHaJ4?rs=1&pid=ImgDetMain"

                       },
                       new Book
                       {
                           Title = "The Odyssey",
                           Authors = new List<Author> { context.Authors.FirstOrDefault(x => x.Name == "Homer") },
                           PublicationYear = -800,
                           Genres = new List<Genre> { context.Genres.FirstOrDefault(x => x.Name == "Epic Poetry") },
                           ISBN = "978-0199536788",
                           Publisher = context.Publishers.FirstOrDefault(x => x.Name == "Oxford University Press"),
                           PageCount = 416,
                           Price = 11.95M,
                           IsAvailable = true,
                           ImgUrl = "https://th.bing.com/th/id/OIP._5vHs3lHPIM5CyLsVE-XUwHaMK?rs=1&pid=ImgDetMain"

                       },
                       new Book
                       {
                           Title = "The Shining",
                           Authors = new List<Author> { context.Authors.FirstOrDefault(x => x.Name == "Stephen King") },
                           PublicationYear = 1977,
                           Genres = new List<Genre> { context.Genres.FirstOrDefault(x => x.Name == "Horror") },
                           ISBN = "978-0307743657",
                           Publisher = context.Publishers.FirstOrDefault(x => x.Name == "Doubleday"),
                           PageCount = 688,
                           Price = 9.99M,
                           IsAvailable = true,
                           ImgUrl = "https://th.bing.com/th/id/OIP.SwfiX3jY9A5_pwTQ2IaZigHaLH?rs=1&pid=ImgDetMain"

                       },
                       new Book
                       {
                           Title = "The Grapes of Wrath",
                           Authors = new List<Author> { context.Authors.FirstOrDefault(x => x.Name == "John Steinbeck") },
                           PublicationYear = 1939,
                           Genres = new List<Genre> { context.Genres.FirstOrDefault(x => x.Name == "Fiction") },
                           ISBN = "978-0143039433",
                           Publisher = context.Publishers.FirstOrDefault(x => x.Name == "The Viking Press"),
                           PageCount = 464,
                           Price = 12.99M,
                           IsAvailable = true,
                           ImgUrl = "https://th.bing.com/th/id/OIP.ibj2AYzaoo9_OwOOTHHxYwHaLd?rs=1&pid=ImgDetMain"

                       },
                       new Book
                       {
                           Title = "Moby-Dick",
                           Authors = new List<Author> { context.Authors.FirstOrDefault(x => x.Name == "Herman Melville") },
                           PublicationYear = 1851,
                           Genres = new List<Genre> { context.Genres.FirstOrDefault(x => x.Name == "Adventure") },
                           ISBN = "978-0142437247",
                           Publisher = context.Publishers.FirstOrDefault(x => x.Name == "Harper \u0026 Brothers"),
                           PageCount = 720,
                           Price = 14.99M,
                           IsAvailable = true,
                           ImgUrl = "https://th.bing.com/th/id/OIP.WTbE7_qIyqfSbkS2CNdPRAHaJl?rs=1&pid=ImgDetMain"

                       },
                       new Book
                       {
                           Title = "One Hundred Years of Solitude",
                           Authors = new List<Author> { context.Authors.FirstOrDefault(x => x.Name == "Gabriel Garc\u00EDa M\u00E1rquez") },
                           PublicationYear = 1967,
                           Genres = new List<Genre> { context.Genres.FirstOrDefault(x => x.Name == "Magical Realism") },
                           ISBN = "978-0060883287",
                           Publisher = context.Publishers.FirstOrDefault(x => x.Name == "Harper \u0026 Row"),
                           PageCount = 448,
                           Price = 15.99M,
                           IsAvailable = true,
                           ImgUrl = "https://th.bing.com/th/id/R.8697cc74db5911afdc6e83680530401b?rik=1vDwtG3ci2QYLg&riu=http%3a%2f%2fdwcp78yw3i6ob.cloudfront.net%2fwp-content%2fuploads%2f2016%2f12%2f12162813%2f100_Years_FirstOrDefault_Ed_Hi_Res-768x1153.jpg&ehk=%2b6OxuEL8iXKiqX9LZgGO6Tac3AslNULuU8MjLqgFkYU%3d&risl=&pid=ImgRaw&r=0"

                       },
                       new Book
                       {
                           Title = "Crime and Punishment",
                           Authors = new List<Author> { context.Authors.FirstOrDefault(x => x.Name == "Fyodor Dostoevsky") },
                           PublicationYear = 1866,
                           Genres = new List<Genre> { context.Genres.FirstOrDefault(x => x.Name == "Literary Fiction") },
                           ISBN = "978-0143058144",
                           Publisher = context.Publishers.FirstOrDefault(x => x.Name == "The Russian Messenger"),
                           PageCount = 671,
                           Price = 12.99M,
                           IsAvailable = true,
                           ImgUrl = "https://cdn2.penguin.com.au/covers/original/9781857150353.jpg"

                       },
                       new Book
                       {
                           Title = "Wuthering Heights",
                           Authors = new List<Author> { context.Authors.FirstOrDefault(x => x.Name == "Emily Bront\u00EB") },
                           PublicationYear = 1847,
                           Genres = new List<Genre> { context.Genres.FirstOrDefault(x => x.Name == "Gothic Fiction") },
                           ISBN = "978-0141439556",
                           Publisher = context.Publishers.FirstOrDefault(x => x.Name == "Thomas Cautley Newby"),
                           PageCount = 416,
                           Price = 7.99M,
                           IsAvailable = true,
                           ImgUrl = "https://th.bing.com/th/id/R.e1d1b936c903c72d27b818a5c69b93b9?rik=47BZwvJmMrUBPQ&riu=http%3a%2f%2fassets.allenandunwin.com.s3.amazonaws.com%2fimages%2foriginal%2f9780571337118.jpg&ehk=nRjkVhb9omqg9ee5rtw3mvERugf1a29dwSSdI7kV5RY%3d&risl=&pid=ImgRaw&r=0"

                       },
                       new Book
                       {
                           Title = "The Picture of Dorian Gray",
                           Authors = new List<Author> { context.Authors.FirstOrDefault(x => x.Name == "Oscar Wilde") },
                           PublicationYear = 1890,
                           Genres = new List<Genre> { context.Genres.FirstOrDefault(x => x.Name == "Gothic Fiction") },
                           ISBN = "978-0199535989",
                           Publisher = context.Publishers.FirstOrDefault(x => x.Name == "Lippincott\u0027s Monthly Magazine"),
                           PageCount = 272,
                           Price = 7.95M,
                           IsAvailable = true,
                           ImgUrl = "https://wordsworth-editions.com/wp-content/uploads/2017/07/9781853260155.jpg"

                       },
                       new Book
                       {
                           Title = "The Count of Monte Cristo",
                           Authors = new List<Author> { context.Authors.FirstOrDefault(x => x.Name == "Alexandre Dumas") },
                           PublicationYear = 1844,
                           Genres = new List<Genre> { context.Genres.FirstOrDefault(x => x.Name == "Adventure") },
                           ISBN = "978-0140449266",
                           Publisher = context.Publishers.FirstOrDefault(x => x.Name == "P\u00E9tion"),
                           PageCount = 1276,
                           Price = 16.00M,
                           IsAvailable = true,
                           ImgUrl = "https://cdn.kobo.com/book-images/f93d05eb-4306-48be-926b-8914b36bc9c3/1200/1200/False/the-count-of-monte-cristo-51.jpg"

                       },
                       new Book
                       {
                           Title = "The Silent Patient",
                           Authors = new List<Author> { context.Authors.FirstOrDefault(x => x.Name == "Alex Michaelides") },
                           PublicationYear = 2019,
                           Genres = new List<Genre> { context.Genres.FirstOrDefault(x => x.Name == "Thriller") },
                           ISBN = "978-1250301697",
                           Publisher = context.Publishers.FirstOrDefault(x => x.Name == "Celadon Books"),
                           PageCount = 336,
                           Price = 14.99M,
                           IsAvailable = true,
                           ImgUrl = "https://th.bing.com/th/id/OIP.MsbTboenjRO2EoLRDG_Q-wHaLM?rs=1&pid=ImgDetMain"

                       },
                       new Book
                       {
                           Title = "Where the Crawdads Sing",
                           Authors = new List<Author> { context.Authors.FirstOrDefault(x => x.Name == "Delia Owens") },
                           PublicationYear = 2018,
                           Genres = new List<Genre> { context.Genres.FirstOrDefault(x => x.Name == "Fiction") },
                           ISBN = "978-0735219090",
                           Publisher = context.Publishers.FirstOrDefault(x => x.Name == "G.P. Putnam\u0027s Sons"),
                           PageCount = 384,
                           Price = 16.00M,
                           IsAvailable = true,
                           ImgUrl = "https://th.bing.com/th/id/OIP.h1ZkrWQ1gu-ZDl-4T1ymBQHaLp?rs=1&pid=ImgDetMain"

                       },
                       new Book
                       {
                           Title = "Educated",
                           Authors = new List<Author> { context.Authors.FirstOrDefault(x => x.Name == "Tara Westover") },
                           PublicationYear = 2018,
                           Genres = new List<Genre> { context.Genres.FirstOrDefault(x => x.Name == "Memoir") },
                           ISBN = "978-0399590504",
                           Publisher = context.Publishers.FirstOrDefault(x => x.Name == "Random House"),
                           PageCount = 352,
                           Price = 15.95M,
                           IsAvailable = true,
                           ImgUrl = "https://th.bing.com/th/id/OIP.Kn8v8LjGkvpgg8BanIjFnAHaLQ?rs=1&pid=ImgDetMain"

                       },
                       new Book
                       {
                           Title = "Sapiens: A Brief History of Humankind",
                           Authors = new List<Author> { context.Authors.FirstOrDefault(x => x.Name == "Yuval Noah Harari") },
                           PublicationYear = 2011,
                           Genres = new List<Genre> { context.Genres.FirstOrDefault(x => x.Name == "Non-fiction") },
                           ISBN = "978-0062316097",
                           Publisher = context.Publishers.FirstOrDefault(x => x.Name == "Harper"),
                           PageCount = 464,
                           Price = 24.99M,
                           IsAvailable = true,
                           ImgUrl = "https://th.bing.com/th/id/OIP.Wdw8knp_mCAsj_Y4hZ4MQwHaLX?rs=1&pid=ImgDetMain"

                       },
                       new Book
                       {
                           Title = "Becoming",
                           Authors = new List<Author> { context.Authors.FirstOrDefault(x => x.Name == "Michelle Obama") },
                           PublicationYear = 2018,
                           Genres = new List<Genre> { context.Genres.FirstOrDefault(x => x.Name == "Autobiography") },
                           ISBN = "978-1524763138",
                           Publisher = context.Publishers.FirstOrDefault(x => x.Name == "Crown"),
                           PageCount = 448,
                           Price = 32.50M,
                           IsAvailable = true,
                           ImgUrl = "https://th.bing.com/th/id/OIP.e__NEYvJpiqMPy4lAR_zHgHaLQ?rs=1&pid=ImgDetMain"

                       },
                       new Book
                       {
                           Title = "The Girl on the Train",
                           Authors = new List<Author> { context.Authors.FirstOrDefault(x => x.Name == "Paula Hawkins") },
                           PublicationYear = 2015,
                           Genres = new List<Genre> { context.Genres.FirstOrDefault(x => x.Name == "Mystery") },
                           ISBN = "978-1594634024",
                           Publisher = context.Publishers.FirstOrDefault(x => x.Name == "Riverhead Books"),
                           PageCount = 336,
                           Price = 9.99M,
                           IsAvailable = true,
                           ImgUrl = "https://i5.walmartimages.com/asr/a6513e10-23f9-4f18-9100-554fddcaa750_1.edd1069e46c70d49a412c4ec82f6dd21.jpeg"

                       },
                       new Book
                       {
                           Title = "A Game of Thrones",
                           Authors = new List<Author> { context.Authors.FirstOrDefault(x => x.Name == "George R.R. Martin") },
                           PublicationYear = 1996,
                           Genres = new List<Genre> { context.Genres.FirstOrDefault(x => x.Name == "Fantasy") },
                           ISBN = "978-0553593716",
                           Publisher = context.Publishers.FirstOrDefault(x => x.Name == "Bantam Spectra"),
                           PageCount = 835,
                           Price = 9.99M,
                           IsAvailable = true,
                           ImgUrl = "https://th.bing.com/th/id/OIP.h2v49-gy-RyOKhQ6ub2T4wAAAA?rs=1&pid=ImgDetMain"

                       },
                       new Book
                       {
                           Title = "The Kite Runner",
                           Authors = new List<Author> { context.Authors.FirstOrDefault(x => x.Name == "Khaled Hosseini") },
                           PublicationYear = 2003,
                           Genres = new List<Genre> { context.Genres.FirstOrDefault(x => x.Name == "Fiction") },
                           ISBN = "978-1594631931",
                           Publisher = context.Publishers.FirstOrDefault(x => x.Name == "Riverhead Books"),
                           PageCount = 371,
                           Price = 14.00M,
                           IsAvailable = true,
                           ImgUrl = "https://imgv2-2-f.scribdassets.com/img/word_document/250024294/original/582f636f24/1587740851?v=1"

                       },
                       new Book
                       {
                           Title = "Life of Pi",
                           Authors = new List<Author> { context.Authors.FirstOrDefault(x => x.Name == "Yann Martel") },
                           PublicationYear = 2001,
                           Genres = new List<Genre> { context.Genres.FirstOrDefault(x => x.Name == "Fiction") },
                           ISBN = "978-0156027328",
                           Publisher = context.Publishers.FirstOrDefault(x => x.Name == "Knopf Canada"),
                           PageCount = 326,
                           Price = 12.99M,
                           IsAvailable = true,
                           ImgUrl = "https://i.pinimg.com/originals/a3/71/56/a37156a4e8b8b1a403e18eb32ebdb97e.jpg"

                       },
                       new Book
                       {
                           Title = "The Book Thief",
                           Authors = new List<Author> { context.Authors.FirstOrDefault(x => x.Name == "Markus Zusak") },
                           PublicationYear = 2005,
                           Genres = new List<Genre> { context.Genres.FirstOrDefault(x => x.Name == "Historical Fiction") },
                           ISBN = "978-0375842207",
                           Publisher = context.Publishers.FirstOrDefault(x => x.Name == "Picador"),
                           PageCount = 584,
                           Price = 12.99M,
                           IsAvailable = true,
                           ImgUrl = "https://th.bing.com/th/id/OIP.QXfpwLb6of2QlgHwviXxogHaLj?rs=1&pid=ImgDetMain"

                       },
                       new Book
                       {
                           Title = "The Road",
                           Authors = new List<Author> { context.Authors.FirstOrDefault(x => x.Name == "Cormac McCarthy") },
                           PublicationYear = 2006,
                           Genres = new List<Genre> { context.Genres.FirstOrDefault(x => x.Name == "Post-Apocalyptic") },
                           ISBN = "978-0307387899",
                           Publisher = context.Publishers.FirstOrDefault(x => x.Name == "Alfred A. Knopf"),
                           PageCount = 287,
                           Price = 16.00M,
                           IsAvailable = true,
                           ImgUrl = "https://th.bing.com/th/id/OIP.b0ixz-7oXf23i_S0xgXpGQHaLO?rs=1&pid=ImgDetMain"

                       },
                       new Book
                       {
                           Title = "Fahrenheit 451",
                           Authors = new List<Author> { context.Authors.FirstOrDefault(x => x.Name == "Ray Bradbury") },
                           PublicationYear = 1953,
                           Genres = new List<Genre> { context.Genres.FirstOrDefault(x => x.Name == "Dystopian") },
                           ISBN = "978-1451673319",
                           Publisher = context.Publishers.FirstOrDefault(x => x.Name == "Ballantine Books"),
                           PageCount = 249,
                           Price = 8.99M,
                           IsAvailable = true,
                           ImgUrl = "https://th.bing.com/th/id/R.ef65387af1539c717421c16fdf69d4fc?rik=UGH8S1OlgvrkoQ&riu=http%3a%2f%2flitstack.com%2fwp-content%2fuploads%2f2013%2f09%2ffahrenheit-451-book-cover1.jpg&ehk=5jdfngLzYvSUA%2fChbZR5A7GmzIm6eCt72Z7QwkhYl9o%3d&risl=&pid=ImgRaw&r=0"

                       },
                       new Book
                       {
                           Title = "The Handmaid\u0027s Tale",
                           Authors = new List<Author> { context.Authors.FirstOrDefault(x => x.Name == "Margaret Atwood") },
                           PublicationYear = 1985,
                           Genres = new List<Genre> { context.Genres.FirstOrDefault(x => x.Name == "Dystopian") },
                           ISBN = "978-0385490818",
                           Publisher = context.Publishers.FirstOrDefault(x => x.Name == "McClelland \u0026 Stewart"),
                           PageCount = 311,
                           Price = 15.95M,
                           IsAvailable = true,
                           ImgUrl = "https://th.bing.com/th/id/OIP.Bot9b5KthqRuI80alqWmBgHaLb?rs=1&pid=ImgDetMain"

                       },
                       new Book
                       {
                           Title = "The Giver",
                           Authors = new List<Author> { context.Authors.FirstOrDefault(x => x.Name == "Lois Lowry") },
                           PublicationYear = 1993,
                           Genres = new List<Genre> { context.Genres.FirstOrDefault(x => x.Name == "Young Adult") },
                           ISBN = "978-0544336261",
                           Publisher = context.Publishers.FirstOrDefault(x => x.Name == "Houghton Mifflin"),
                           PageCount = 208,
                           Price = 8.99M,
                           IsAvailable = true,
                           ImgUrl = "https://cdn.kobo.com/book-images/16327bfc-5399-4b3d-be6d-e65873cc4c30/1200/1200/False/the-giver-harpercollins-children-s-modern-classics.jpg"

                       },
                       new Book
                       {
                           Title = "The Martian",
                           Authors = new List<Author> { context.Authors.FirstOrDefault(x => x.Name == "Andy Weir") },
                           PublicationYear = 2011,
                           Genres = new List<Genre> { context.Genres.FirstOrDefault(x => x.Name == "Science Fiction") },
                           ISBN = "978-0553418026",
                           Publisher = context.Publishers.FirstOrDefault(x => x.Name == "Crown Publishing Group"),
                           PageCount = 387,
                           Price = 15.00M,
                           IsAvailable = true,
                           ImgUrl = "https://th.bing.com/th/id/OIP.EQvB2pkuKdBk5ThLNRpV3AHaLQ?rs=1&pid=ImgDetMain"

                       },
                       new Book
                       {
                           Title = "Gone Girl",
                           Authors = new List<Author> { context.Authors.FirstOrDefault(x => x.Name == "Gillian Flynn") },
                           PublicationYear = 2012,
                           Genres = new List<Genre> { context.Genres.FirstOrDefault(x => x.Name == "Thriller") },
                           ISBN = "978-0307588371",
                           Publisher = context.Publishers.FirstOrDefault(x => x.Name == "Crown Publishing Group"),
                           PageCount = 432,
                           Price = 14.99M,
                           IsAvailable = true,
                           ImgUrl = "https://th.bing.com/th/id/R.979b1d14087153c5e264fef39aee3a96?rik=ADYwTpdpf0DeqQ&riu=http%3a%2f%2fimages6.fanpop.com%2fimage%2fphotos%2f37400000%2fGone-Girl-by-Gillian-Flynn-gone-girl-37441442-1181-1810.jpg&ehk=rtg5qz7Eh5WgcMzc%2bCEMz7DThMzsLhAX39DPt%2fl9MtM%3d&risl=&pid=ImgRaw&r=0"

                       },
                       new Book
                       {
                           Title = "The Fault in Our Stars",
                           Authors = new List<Author> { context.Authors.FirstOrDefault(x => x.Name == "John Green") },
                           PublicationYear = 2012,
                           Genres = new List<Genre> { context.Genres.FirstOrDefault(x => x.Name == "Young Adult") },
                           ISBN = "978-0142424179",
                           Publisher = context.Publishers.FirstOrDefault(x => x.Name == "Dutton Books"),
                           PageCount = 313,
                           Price = 12.99M,
                           IsAvailable = true,
                           ImgUrl = "https://th.bing.com/th/id/R.e795ce4ab4b4c1fe088529296b6ee94d?rik=JBP7Sjfuhhqp1Q&pid=ImgRaw&r=0"

                       },
                       new Book
                       {
                           Title = "The Hitchhiker\u0027s Guide to the Galaxy",
                           Authors = new List<Author> { context.Authors.FirstOrDefault(x => x.Name == "Douglas Adams") },
                           PublicationYear = 1979,
                           Genres = new List<Genre> { context.Genres.FirstOrDefault(x => x.Name == "Science Fiction") },
                           ISBN = "978-0345391803",
                           Publisher = context.Publishers.FirstOrDefault(x => x.Name == "Pan Books"),
                           PageCount = 224,
                           Price = 7.99M,
                           IsAvailable = true,
                           ImgUrl = "https://th.bing.com/th/id/OIP.6lE_KV_vkckc3Ey-TSvHuwHaLJ?rs=1&pid=ImgDetMain"

                       },
                       new Book
                       {
                           Title = "The Catch-22",
                           Authors = new List<Author> { context.Authors.FirstOrDefault(x => x.Name == "Joseph Heller") },
                           PublicationYear = 1961,
                           Genres = new List<Genre> { context.Genres.FirstOrDefault(x => x.Name == "Satire") },
                           ISBN = "978-1451626650",
                           Publisher = context.Publishers.FirstOrDefault(x => x.Name == "Simon \u0026 Schuster"),
                           PageCount = 453,
                           Price = 17.00M,
                           IsAvailable = true,
                           ImgUrl = "https://th.bing.com/th/id/OIP.8176UT2wXeN8km6wuq_imwAAAA?rs=1&pid=ImgDetMain"

                       },
                       new Book
                       {
                           Title = "Ready Player One",
                           Authors = new List<Author> { context.Authors.FirstOrDefault(x => x.Name == "Ernest Cline") },
                           PublicationYear = 2011,
                           Genres = new List<Genre> { context.Genres.FirstOrDefault(x => x.Name == "Science Fiction") },
                           ISBN = "978-0307887443",
                           Publisher = context.Publishers.FirstOrDefault(x => x.Name == "Crown Publishing Group"),
                           PageCount = 384,
                           Price = 16.00M,
                           IsAvailable = true,
                           ImgUrl = "https://th.bing.com/th/id/OIP.86Wh_7BdSgI8So5cKvXhgwHaLb?rs=1&pid=ImgDetMain"

                       },
                       new Book
                       {
                           Title = "The Name of the Wind",
                           Authors = new List<Author> { context.Authors.FirstOrDefault(x => x.Name == "Patrick Rothfuss") },
                           PublicationYear = 2007,
                           Genres = new List<Genre> { context.Genres.FirstOrDefault(x => x.Name == "Fantasy") },
                           ISBN = "978-0756404741",
                           Publisher = context.Publishers.FirstOrDefault(x => x.Name == "DAW Books"),
                           PageCount = 662,
                           Price = 9.99M,
                           IsAvailable = true,
                           ImgUrl = "https://i0.wp.com/www.attackofthebooks.com/wp-content/uploads/2017/04/The_Name_of_the_Wind_UK_cover.jpg?fit=1666%2C2560&ssl=1"

                       },
                       new Book
                       {
                           Title = "Dune",
                           Authors = new List<Author> { context.Authors.FirstOrDefault(x => x.Name == "Frank Herbert") },
                           PublicationYear = 1965,
                           Genres = new List<Genre> { context.Genres.FirstOrDefault(x => x.Name == "Science Fiction") },
                           ISBN = "978-0441172719",
                           Publisher = context.Publishers.FirstOrDefault(x => x.Name == "Chilton Books"),
                           PageCount = 412,
                           Price = 9.99M,
                           IsAvailable = true,
                           ImgUrl = "https://cdn11.bigcommerce.com/s-gibnfyxosi/images/stencil/2560w/products/130183/131921/41UZeCEKOBL__78384.1615569895.jpg?c=1"

                       },
                       new Book
                       {
                           Title = "The Road Less Traveled",
                           Authors = new List<Author> { context.Authors.FirstOrDefault(x => x.Name == "M. Scott Peck") },
                           PublicationYear = 1978,
                           Genres = new List<Genre> { context.Genres.FirstOrDefault(x => x.Name == "Self-help") },
                           ISBN = "978-0743243155",
                           Publisher = context.Publishers.FirstOrDefault(x => x.Name == "Simon \u0026 Schuster"),
                           PageCount = 320,
                           Price = 16.99M,
                           IsAvailable = true,
                           ImgUrl = "https://th.bing.com/th/id/R.135b0150a36bd91cbd74324561b4725b?rik=Ac6gyPMcaqoDHg&pid=ImgRaw&r=0"

                       },
                       new Book
                       {
                           Title = "Rich Dad Poor Dad",
                           Authors = new List<Author> { context.Authors.FirstOrDefault(x => x.Name == "Robert T. Kiyosaki") },
                           PublicationYear = 1997,
                           Genres = new List<Genre> { context.Genres.FirstOrDefault(x => x.Name == "Personal Finance") },
                           ISBN = "978-1612680194",
                           Publisher = context.Publishers.FirstOrDefault(x => x.Name == "Warner Books"),
                           PageCount = 336,
                           Price = 16.99M,
                           IsAvailable = true,
                           ImgUrl = "https://cdn.kobo.com/book-images/c81ea4de-cfb7-415d-8634-314aad041fdb/1200/1200/False/rich-dad-poor-dad-9.jpg"

                       },
                       new Book
                       {
                           Title = "How to Win Friends and Influence People",
                           Authors = new List<Author> { context.Authors.FirstOrDefault(x => x.Name == "Dale Carnegie") },
                           PublicationYear = 1936,
                           Genres = new List<Genre> { context.Genres.FirstOrDefault(x => x.Name == "Self-help") },
                           ISBN = "978-0671027032",
                           Publisher = context.Publishers.FirstOrDefault(x => x.Name == "Simon \u0026 Schuster"),
                           PageCount = 288,
                           Price = 16.00M,
                           IsAvailable = true,
                           ImgUrl = "https://d3fa68hw0m2vcc.cloudfront.net/f92/172976980.jpeg"

                       },
                       new Book
                       {
                           Title = "Thinking, Fast and Slow",
                           Authors = new List<Author> { context.Authors.FirstOrDefault(x => x.Name == "Daniel Kahneman") },
                           PublicationYear = 2011,
                           Genres = new List<Genre> { context.Genres.FirstOrDefault(x => x.Name == "Psychology") },
                           ISBN = "978-0374533557",
                           Publisher = context.Publishers.FirstOrDefault(x => x.Name == "Farrar, Straus and Giroux"),
                           PageCount = 499,
                           Price = 17.00M,
                           IsAvailable = true,
                           ImgUrl = "https://th.bing.com/th/id/OIP.c7sOT_zwzaWNafdGnO2PGwHaLx?rs=1&pid=ImgDetMain"

                       }
                   };
                await context.Books.AddRangeAsync(Books);
                await context.SaveChangesAsync();
            }

        }
    }
}
