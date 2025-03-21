using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCatalog.DataLayer.DataBase.Seeder
{
    public static class DbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            BookCatalogDbContext context = applicationBuilder.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<BookCatalogDbContext>();

            if (!context.Books.Any())
            {
                context.Books.AddRange(
                   new List<Book> {
                       
                       new Book {
                            Title= "To Kill a Mockingbird",
                            Author= "Harper Lee",
                            PublicationYear= 1960,
                            Genre= "Fiction",
                            ISBN= "978-0061120084",
                            Publisher= "J. B. Lippincott \u0026 Co.",
                            PageCount= 336,
                            Price= 12.99M,
                            IsAvailable= true
                        },
                       new Book {
                           Title= "1984",
                           Author= "George Orwell",
                           PublicationYear= 1949,
                           Genre= "Science Fiction",
                           ISBN= "978-0451524935",
                           Publisher= "Secker \u0026 Warburg",
                           PageCount= 328,
                           Price= 9.99M,
                           IsAvailable= true
                       },
                       new Book {
                           Title= "The Great Gatsby",
                           Author= "F. Scott Fitzgerald",
                           PublicationYear= 1925,
                           Genre= "Fiction",
                           ISBN= "978-0743273565",
                           Publisher= "Charles Scribner\u0027s Sons",
                           PageCount= 180,
                           Price= 8.99M,
                           IsAvailable= true
                       },
                       new Book {
                           Title= "Pride and Prejudice",
                           Author= "Jane Austen",
                           PublicationYear= 1813,
                           Genre= "Romance",
                           ISBN= "978-0141439518",
                           Publisher= "T. Egerton",
                           PageCount= 432,
                           Price= 7.99M,
                           IsAvailable= true
                       },
                       new Book {
                           Title= "The Hobbit",
                           Author= "J.R.R. Tolkien",
                           PublicationYear= 1937,
                           Genre= "Fantasy",
                           ISBN= "978-0547928227",
                           Publisher= "George Allen \u0026 Unwin",
                           PageCount= 300,
                           Price= 14.99M,
                           IsAvailable= true
                       },
                       new Book {
                           Title= "Harry Potter and the Philosopher\u0027s Stone",
                           Author= "J.K. Rowling",
                           PublicationYear= 1997,
                           Genre= "Fantasy",
                           ISBN= "978-1408855652",
                           Publisher= "Bloomsbury",
                           PageCount= 223,
                           Price= 10.99M,
                           IsAvailable= true
                       },
                       new Book {
                           Title= "The Lord of the Rings",
                           Author= "J.R.R. Tolkien",
                           PublicationYear= 1954,
                           Genre= "Fantasy",
                           ISBN= "978-0618640157",
                           Publisher= "Allen \u0026 Unwin",
                           PageCount= 1178,
                           Price= 29.99M,
                           IsAvailable= true
                       },
                       new Book {
                           Title= "The Catcher in the Rye",
                           Author= "J.D. Salinger",
                           PublicationYear= 1951,
                           Genre= "Fiction",
                           ISBN= "978-0316769488",
                           Publisher= "Little, Brown and Company",
                           PageCount= 277,
                           Price= 8.99M,
                           IsAvailable= true
                       },
                       new Book {
                           Title= "Brave New World",
                           Author= "Aldous Huxley",
                           PublicationYear= 1932,
                           Genre= "Science Fiction",
                           ISBN= "978-0060850524",
                           Publisher= "Chatto \u0026 Windus",
                           PageCount= 288,
                           Price= 10.99M,
                           IsAvailable= true
                       },
                       new Book {
                           Title= "The Alchemist",
                           Author= "Paulo Coelho",
                           PublicationYear= 1988,
                           Genre= "Fiction",
                           ISBN= "978-0062315007",
                           Publisher= "HarperOne",
                           PageCount= 208,
                           Price= 11.99M,
                           IsAvailable= true
                       },
                       new Book {
                           Title= "The Hunger Games",
                           Author= "Suzanne Collins",
                           PublicationYear= 2008,
                           Genre= "Young Adult",
                           ISBN= "978-0439023481",
                           Publisher= "Scholastic Press",
                           PageCount= 374,
                           Price= 12.50M,
                           IsAvailable= true
  
                       },
                       new Book {
                           Title= "The Da Vinci Code",
                           Author= "Dan Brown",
                           PublicationYear= 2003,
                           Genre= "Mystery",
                           ISBN= "978-0307474278",
                           Publisher= "Doubleday",
                           PageCount= 489,
                           Price= 9.99M,
                           IsAvailable= true
       
                       },
                       new Book {
                           Title= "A Tale of Two Cities",
                           Author= "Charles Dickens",
                           PublicationYear= 1859,
                           Genre= "Historical Fiction",
                           ISBN= "978-0141439600",
                           Publisher= "Chapman \u0026 Hall",
                           PageCount= 489,
                           Price= 6.99M,
                           IsAvailable= true

                       },
                       new Book {
                           Title= "The Odyssey",
                           Author= "Homer",
                           PublicationYear= -800,
                           Genre= "Epic Poetry",
                           ISBN= "978-0199536788",
                           Publisher= "Oxford University Press",
                           PageCount= 416,
                           Price= 11.95M,
                           IsAvailable= true
               
                       },
                       new Book {
                           Title= "The Shining",
                           Author= "Stephen King",
                           PublicationYear= 1977,
                           Genre= "Horror",
                           ISBN= "978-0307743657",
                           Publisher= "Doubleday",
                           PageCount= 688,
                           Price= 9.99M,
                           IsAvailable= true
             
                       },
                       new Book {
                           Title= "The Grapes of Wrath",
                           Author= "John Steinbeck",
                           PublicationYear= 1939,
                           Genre= "Fiction",
                           ISBN= "978-0143039433",
                           Publisher= "The Viking Press",
                           PageCount= 464,
                           Price= 12.99M,
                           IsAvailable= true
                  
                       },
                       new Book {
                           Title= "Moby-Dick",
                           Author= "Herman Melville",
                           PublicationYear= 1851,
                           Genre= "Adventure",
                           ISBN= "978-0142437247",
                           Publisher= "Harper \u0026 Brothers",
                           PageCount= 720,
                           Price= 14.99M,
                           IsAvailable= true
                       
                       },
                       new Book {
                           Title= "One Hundred Years of Solitude",
                           Author= "Gabriel Garc\u00EDa M\u00E1rquez",
                           PublicationYear= 1967,
                           Genre= "Magical Realism",
                           ISBN= "978-0060883287",
                           Publisher= "Harper \u0026 Row",
                           PageCount= 448,
                           Price= 15.99M,
                           IsAvailable= true
                       
                       },
                       new Book {
                           Title= "Crime and Punishment",
                           Author= "Fyodor Dostoevsky",
                           PublicationYear= 1866,
                           Genre= "Literary Fiction",
                           ISBN= "978-0143058144",
                           Publisher= "The Russian Messenger",
                           PageCount= 671,
                           Price= 12.99M,
                           IsAvailable= true
             
                       },
                       new Book {
                           Title= "Wuthering Heights",
                           Author= "Emily Bront\u00EB",
                           PublicationYear= 1847,
                           Genre= "Gothic Fiction",
                           ISBN= "978-0141439556",
                           Publisher= "Thomas Cautley Newby",
                           PageCount= 416,
                           Price= 7.99M,
                           IsAvailable= true
                          
                       },
                       new Book {
                           Title= "The Picture of Dorian Gray",
                           Author= "Oscar Wilde",
                           PublicationYear= 1890,
                           Genre= "Gothic Fiction",
                           ISBN= "978-0199535989",
                           Publisher= "Lippincott\u0027s Monthly Magazine",
                           PageCount= 272,
                           Price= 7.95M,
                           IsAvailable= true
               
                       },
                       new Book {
                           Title= "The Count of Monte Cristo",
                           Author= "Alexandre Dumas",
                           PublicationYear= 1844,
                           Genre= "Adventure",
                           ISBN= "978-0140449266",
                           Publisher= "P\u00E9tion",
                           PageCount= 1276,
                           Price= 16.00M,
                           IsAvailable= true
                    
                       },
                       new Book {
                           Title= "The Silent Patient",
                           Author= "Alex Michaelides",
                           PublicationYear= 2019,
                           Genre= "Thriller",
                           ISBN= "978-1250301697",
                           Publisher= "Celadon Books",
                           PageCount= 336,
                           Price= 14.99M,
                           IsAvailable= true
                   
                       },
                       new Book {
                           Title= "Where the Crawdads Sing",
                           Author= "Delia Owens",
                           PublicationYear= 2018,
                           Genre= "Fiction",
                           ISBN= "978-0735219090",
                           Publisher= "G.P. Putnam\u0027s Sons",
                           PageCount= 384,
                           Price= 16.00M,
                           IsAvailable= true
                  
                       },
                       new Book {
                           Title= "Educated",
                           Author= "Tara Westover",
                           PublicationYear= 2018,
                           Genre= "Memoir",
                           ISBN= "978-0399590504",
                           Publisher= "Random House",
                           PageCount= 352,
                           Price= 15.95M,
                           IsAvailable= true
                    
                       },
                       new Book {
                           Title= "Sapiens: A Brief History of Humankind",
                           Author= "Yuval Noah Harari",
                           PublicationYear= 2011,
                           Genre= "Non-fiction",
                           ISBN= "978-0062316097",
                           Publisher= "Harper",
                           PageCount= 464,
                           Price= 24.99M,
                           IsAvailable= true
                       
                       },
                       new Book {
                           Title= "Becoming",
                           Author= "Michelle Obama",
                           PublicationYear= 2018,
                           Genre= "Autobiography",
                           ISBN= "978-1524763138",
                           Publisher= "Crown",
                           PageCount= 448,
                           Price= 32.50M,
                           IsAvailable= true
         
                       },
                       new Book {
                           Title= "The Girl on the Train",
                           Author= "Paula Hawkins",
                           PublicationYear= 2015,
                           Genre= "Mystery",
                           ISBN= "978-1594634024",
                           Publisher= "Riverhead Books",
                           PageCount= 336,
                           Price= 9.99M,
                           IsAvailable= true
                      
                       },
                       new Book {
                           Title= "A Game of Thrones",
                           Author= "George R.R. Martin",
                           PublicationYear= 1996,
                           Genre= "Fantasy",
                           ISBN= "978-0553593716",
                           Publisher= "Bantam Spectra",
                           PageCount= 835,
                           Price= 9.99M,
                           IsAvailable= true
                
                       },
                       new Book {
                           Title= "The Kite Runner",
                           Author= "Khaled Hosseini",
                           PublicationYear= 2003,
                           Genre= "Fiction",
                           ISBN= "978-1594631931",
                           Publisher= "Riverhead Books",
                           PageCount= 371,
                           Price= 14.00M,
                           IsAvailable= true
                         
                       },
                       new Book {
                           Title= "Life of Pi",
                           Author= "Yann Martel",
                           PublicationYear= 2001,
                           Genre= "Fiction",
                           ISBN= "978-0156027328",
                           Publisher= "Knopf Canada",
                           PageCount= 326,
                           Price= 12.99M,
                           IsAvailable= true
               
                       },
                       new Book {
                           Title= "The Book Thief",
                           Author= "Markus Zusak",
                           PublicationYear= 2005,
                           Genre= "Historical Fiction",
                           ISBN= "978-0375842207",
                           Publisher= "Picador",
                           PageCount= 584,
                           Price= 12.99M,
                           IsAvailable= true
                         
                       },
                       new Book {
                           Title= "The Road",
                           Author= "Cormac McCarthy",
                           PublicationYear= 2006,
                           Genre= "Post-Apocalyptic",
                           ISBN= "978-0307387899",
                           Publisher= "Alfred A. Knopf",
                           PageCount= 287,
                           Price= 16.00M,
                           IsAvailable= true
                  
                       },
                       new Book {
                           Title= "Fahrenheit 451",
                           Author= "Ray Bradbury",
                           PublicationYear= 1953,
                           Genre= "Dystopian",
                           ISBN= "978-1451673319",
                           Publisher= "Ballantine Books",
                           PageCount= 249,
                           Price= 8.99M,
                           IsAvailable= true
                
                       },
                       new Book {
                           Title= "The Handmaid\u0027s Tale",
                           Author= "Margaret Atwood",
                           PublicationYear= 1985,
                           Genre= "Dystopian",
                           ISBN= "978-0385490818",
                           Publisher= "McClelland \u0026 Stewart",
                           PageCount= 311,
                           Price= 15.95M,
                           IsAvailable= true
                     
                       },
                       new Book {
                           Title= "The Giver",
                           Author= "Lois Lowry",
                           PublicationYear= 1993,
                           Genre= "Young Adult",
                           ISBN= "978-0544336261",
                           Publisher= "Houghton Mifflin",
                           PageCount= 208,
                           Price= 8.99M,
                           IsAvailable= true
                          
                       },
                       new Book {
                           Title= "The Martian",
                           Author= "Andy Weir",
                           PublicationYear= 2011,
                           Genre= "Science Fiction",
                           ISBN= "978-0553418026",
                           Publisher= "Crown Publishing Group",
                           PageCount= 387,
                           Price= 15.00M,
                           IsAvailable= true
                     
                       },
                       new Book {
                           Title= "Gone Girl",
                           Author= "Gillian Flynn",
                           PublicationYear= 2012,
                           Genre= "Thriller",
                           ISBN= "978-0307588371",
                           Publisher= "Crown Publishing Group",
                           PageCount= 432,
                           Price= 14.99M,
                           IsAvailable= true
                       
                       },
                       new Book {
                           Title= "The Fault in Our Stars",
                           Author= "John Green",
                           PublicationYear= 2012,
                           Genre= "Young Adult",
                           ISBN= "978-0142424179",
                           Publisher= "Dutton Books",
                           PageCount= 313,
                           Price= 12.99M,
                           IsAvailable= true
                     
                       },
                       new Book {
                           Title= "The Hitchhiker\u0027s Guide to the Galaxy",
                           Author= "Douglas Adams",
                           PublicationYear= 1979,
                           Genre= "Science Fiction",
                           ISBN= "978-0345391803",
                           Publisher= "Pan Books",
                           PageCount= 224,
                           Price= 7.99M,
                           IsAvailable= true
          
                       },
                       new Book  {
                           Title= "The Catch-22",
                           Author= "Joseph Heller",
                           PublicationYear= 1961,
                           Genre= "Satire",
                           ISBN= "978-1451626650",
                           Publisher= "Simon \u0026 Schuster",
                           PageCount= 453,
                           Price= 17.00M,
                           IsAvailable= true
              
                       },
                       new Book {
                           Title= "Ready Player One",
                           Author= "Ernest Cline",
                           PublicationYear= 2011,
                           Genre= "Science Fiction",
                           ISBN= "978-0307887443",
                           Publisher= "Crown Publishing Group",
                           PageCount= 384,
                           Price= 16.00M,
                           IsAvailable= true
                       
                       },
                       new Book {
                           Title= "The Name of the Wind",
                           Author= "Patrick Rothfuss",
                           PublicationYear= 2007,
                           Genre= "Fantasy",
                           ISBN= "978-0756404741",
                           Publisher= "DAW Books",
                           PageCount= 662,
                           Price= 9.99M,
                           IsAvailable= true
                      
                       },
                       new Book {
                           Title= "Dune",
                           Author= "Frank Herbert",
                           PublicationYear= 1965,
                           Genre= "Science Fiction",
                           ISBN= "978-0441172719",
                           Publisher= "Chilton Books",
                           PageCount= 412,
                           Price= 9.99M,
                           IsAvailable= true
                       
                       },
                       new Book {
                           Title= "The Road Less Traveled",
                           Author= "M. Scott Peck",
                           PublicationYear= 1978,
                           Genre= "Self-help",
                           ISBN= "978-0743243155",
                           Publisher= "Simon \u0026 Schuster",
                           PageCount= 320,
                           Price= 16.99M,
                           IsAvailable= true
                       
                       },
                       new Book {
                           Title= "Rich Dad Poor Dad",
                           Author= "Robert T. Kiyosaki",
                           PublicationYear= 1997,
                           Genre= "Personal Finance",
                           ISBN= "978-1612680194",
                           Publisher= "Warner Books",
                           PageCount= 336,
                           Price= 16.99M,
                           IsAvailable= true
               
                       },
                       new Book {
                           Title= "How to Win Friends and Influence People",
                           Author= "Dale Carnegie",
                           PublicationYear= 1936,
                           Genre= "Self-help",
                           ISBN= "978-0671027032",
                           Publisher= "Simon \u0026 Schuster",
                           PageCount= 288,
                           Price= 16.00M,
                           IsAvailable= true
                     
                       },
                       new Book {
                           Title= "Thinking, Fast and Slow",
                           Author= "Daniel Kahneman",
                           PublicationYear= 2011,
                           Genre= "Psychology",
                           ISBN= "978-0374533557",
                           Publisher= "Farrar, Straus and Giroux",
                           PageCount= 499,
                           Price= 17.00M,
                           IsAvailable= true
                         
                       }
                   });
            }
            context.SaveChanges();

        }
    }
}
