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
                            IsAvailable= true,
                            ImgUrl="https://th.bing.com/th/id/OIP.W1ngiF2AkQaO78CY9yC1HQHaLd?rs=1&pid=ImgDetMain",
                            Id=Guid.Parse("b58bfc10-270a-4395-697d-08dd686bd604")
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
                           IsAvailable= true,
                           ImgUrl ="https://th.bing.com/th/id/R.e5f82111504d28779262bf2dca0fad56?rik=9etdGLc9mstPyw&pid=ImgRaw&r=0"
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
                           IsAvailable= true,
                           ImgUrl ="https://hachette.imgix.net/books/9780762498130.jpg?auto=compress,format"
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
                           IsAvailable= true,
                           ImgUrl="https://cdn2.penguin.com.au/covers/original/9780141330167.jpg"
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
                           IsAvailable= true,
                           ImgUrl ="https://th.bing.com/th/id/R.8947741487564891cf18c8b32752c2bf?rik=gsUGQLJSJMRUSQ&pid=ImgRaw&r=0"
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
                           IsAvailable= true,
                           ImgUrl="https://res.cloudinary.com/bloomsbury-atlas/image/upload/w_568,c_scale/jackets/9781408855652.jpg"
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
                           IsAvailable= true,
                           ImgUrl="https://cdn11.bigcommerce.com/s-gibnfyxosi/images/stencil/1920w/products/154740/156431/51eq24cRtRL__98083.1615576774.jpg?c=1"
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
                           IsAvailable= true,
                           ImgUrl="https://th.bing.com/th/id/OIP.7vHm4DvQa3iwZAoiOFcZtwHaKr?rs=1&pid=ImgDetMain"
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
                           IsAvailable= true,
                           ImgUrl="https://th.bing.com/th/id/R.9f4b5965d0bc99a4e4e0130b713285a0?rik=d5ac2urjW9GuEw&pid=ImgRaw&r=0"

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
                           IsAvailable= true,
                           ImgUrl="https://th.bing.com/th/id/OIP.9-v0mfum2BK2IFqSLAXf3gHaL7?rs=1&pid=ImgDetMain"
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
                           IsAvailable= true,
                           ImgUrl="https://th.bing.com/th/id/R.a7f9763386c793cbc02abec8fa1008bf?rik=5oDV0ZZtqiIVIA&riu=http%3a%2f%2fmedia.npr.org%2fassets%2fbakertaylor%2fcovers%2ft%2fthe-hunger-games%2f9780545425117_custom-c220dca852341153703cf3b610bef0687c0ed7e7-s6-c30.jpg&ehk=Cf60NCfoiX06dj0YkU9NOKP5RGiJ%2flamLC4r3qPxVKM%3d&risl=&pid=ImgRaw&r=0"

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
                           IsAvailable= true,
                           ImgUrl="https://th.bing.com/th/id/R.bd9481b3c05998fdbb7b1fc49ba1f3ef?rik=yUOPSX2aJliuAA&pid=ImgRaw&r=0"
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
                           IsAvailable= true,
                           ImgUrl="https://th.bing.com/th/id/OIP.B4wjpTB6Gg1o4O72nV6A7AHaJ4?rs=1&pid=ImgDetMain"

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
                           IsAvailable= true,
                           ImgUrl="https://th.bing.com/th/id/OIP._5vHs3lHPIM5CyLsVE-XUwHaMK?rs=1&pid=ImgDetMain"

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
                           IsAvailable= true,
                           ImgUrl="https://th.bing.com/th/id/OIP.SwfiX3jY9A5_pwTQ2IaZigHaLH?rs=1&pid=ImgDetMain"

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
                           IsAvailable= true,
                           ImgUrl="https://th.bing.com/th/id/OIP.ibj2AYzaoo9_OwOOTHHxYwHaLd?rs=1&pid=ImgDetMain"

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
                           IsAvailable= true,
                           ImgUrl="https://th.bing.com/th/id/OIP.WTbE7_qIyqfSbkS2CNdPRAHaJl?rs=1&pid=ImgDetMain"

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
                           IsAvailable= true,
                           ImgUrl="https://th.bing.com/th/id/R.8697cc74db5911afdc6e83680530401b?rik=1vDwtG3ci2QYLg&riu=http%3a%2f%2fdwcp78yw3i6ob.cloudfront.net%2fwp-content%2fuploads%2f2016%2f12%2f12162813%2f100_Years_First_Ed_Hi_Res-768x1153.jpg&ehk=%2b6OxuEL8iXKiqX9LZgGO6Tac3AslNULuU8MjLqgFkYU%3d&risl=&pid=ImgRaw&r=0"

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
                           IsAvailable= true,
                           ImgUrl="https://cdn2.penguin.com.au/covers/original/9781857150353.jpg"

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
                           IsAvailable= true,
                           ImgUrl="https://th.bing.com/th/id/R.e1d1b936c903c72d27b818a5c69b93b9?rik=47BZwvJmMrUBPQ&riu=http%3a%2f%2fassets.allenandunwin.com.s3.amazonaws.com%2fimages%2foriginal%2f9780571337118.jpg&ehk=nRjkVhb9omqg9ee5rtw3mvERugf1a29dwSSdI7kV5RY%3d&risl=&pid=ImgRaw&r=0"

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
                           IsAvailable= true,
                           ImgUrl="https://wordsworth-editions.com/wp-content/uploads/2017/07/9781853260155.jpg"

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
                           IsAvailable= true,
                           ImgUrl="https://cdn.kobo.com/book-images/f93d05eb-4306-48be-926b-8914b36bc9c3/1200/1200/False/the-count-of-monte-cristo-51.jpg"

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
                           IsAvailable= true,
                           ImgUrl="https://th.bing.com/th/id/OIP.MsbTboenjRO2EoLRDG_Q-wHaLM?rs=1&pid=ImgDetMain"

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
                           IsAvailable= true,
                           ImgUrl="https://th.bing.com/th/id/OIP.h1ZkrWQ1gu-ZDl-4T1ymBQHaLp?rs=1&pid=ImgDetMain"

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
                           IsAvailable= true,
                           ImgUrl="https://th.bing.com/th/id/OIP.Kn8v8LjGkvpgg8BanIjFnAHaLQ?rs=1&pid=ImgDetMain"

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
                           IsAvailable= true,
                           ImgUrl="https://th.bing.com/th/id/OIP.Wdw8knp_mCAsj_Y4hZ4MQwHaLX?rs=1&pid=ImgDetMain"

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
                           IsAvailable= true,
                           ImgUrl="https://th.bing.com/th/id/OIP.e__NEYvJpiqMPy4lAR_zHgHaLQ?rs=1&pid=ImgDetMain"

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
                           IsAvailable= true,
                           ImgUrl="https://i5.walmartimages.com/asr/a6513e10-23f9-4f18-9100-554fddcaa750_1.edd1069e46c70d49a412c4ec82f6dd21.jpeg"
                      
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
                           IsAvailable= true,
                           ImgUrl="https://th.bing.com/th/id/OIP.h2v49-gy-RyOKhQ6ub2T4wAAAA?rs=1&pid=ImgDetMain"

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
                           IsAvailable= true,
                           ImgUrl="https://imgv2-2-f.scribdassets.com/img/word_document/250024294/original/582f636f24/1587740851?v=1"

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
                           IsAvailable= true,
                           ImgUrl ="https://i.pinimg.com/originals/a3/71/56/a37156a4e8b8b1a403e18eb32ebdb97e.jpg"

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
                           IsAvailable= true,
                           ImgUrl="https://th.bing.com/th/id/OIP.QXfpwLb6of2QlgHwviXxogHaLj?rs=1&pid=ImgDetMain"

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
                           IsAvailable= true,
                           ImgUrl="https://th.bing.com/th/id/OIP.b0ixz-7oXf23i_S0xgXpGQHaLO?rs=1&pid=ImgDetMain"

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
                           IsAvailable= true,
                           ImgUrl="https://th.bing.com/th/id/R.ef65387af1539c717421c16fdf69d4fc?rik=UGH8S1OlgvrkoQ&riu=http%3a%2f%2flitstack.com%2fwp-content%2fuploads%2f2013%2f09%2ffahrenheit-451-book-cover1.jpg&ehk=5jdfngLzYvSUA%2fChbZR5A7GmzIm6eCt72Z7QwkhYl9o%3d&risl=&pid=ImgRaw&r=0"

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
                           IsAvailable= true,
                           ImgUrl="https://th.bing.com/th/id/OIP.Bot9b5KthqRuI80alqWmBgHaLb?rs=1&pid=ImgDetMain"

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
                           IsAvailable= true,
                           ImgUrl="https://cdn.kobo.com/book-images/16327bfc-5399-4b3d-be6d-e65873cc4c30/1200/1200/False/the-giver-harpercollins-children-s-modern-classics.jpg"

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
                           IsAvailable= true,
                           ImgUrl="https://th.bing.com/th/id/OIP.EQvB2pkuKdBk5ThLNRpV3AHaLQ?rs=1&pid=ImgDetMain"

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
                           IsAvailable= true,
                           ImgUrl="https://th.bing.com/th/id/R.979b1d14087153c5e264fef39aee3a96?rik=ADYwTpdpf0DeqQ&riu=http%3a%2f%2fimages6.fanpop.com%2fimage%2fphotos%2f37400000%2fGone-Girl-by-Gillian-Flynn-gone-girl-37441442-1181-1810.jpg&ehk=rtg5qz7Eh5WgcMzc%2bCEMz7DThMzsLhAX39DPt%2fl9MtM%3d&risl=&pid=ImgRaw&r=0"

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
                           IsAvailable= true,
                           ImgUrl="https://th.bing.com/th/id/R.e795ce4ab4b4c1fe088529296b6ee94d?rik=JBP7Sjfuhhqp1Q&pid=ImgRaw&r=0"

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
                           IsAvailable= true,
                           ImgUrl="https://th.bing.com/th/id/OIP.6lE_KV_vkckc3Ey-TSvHuwHaLJ?rs=1&pid=ImgDetMain"

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
                           IsAvailable= true,
                           ImgUrl="https://th.bing.com/th/id/OIP.8176UT2wXeN8km6wuq_imwAAAA?rs=1&pid=ImgDetMain"

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
                           IsAvailable= true,
                           ImgUrl="https://th.bing.com/th/id/OIP.86Wh_7BdSgI8So5cKvXhgwHaLb?rs=1&pid=ImgDetMain"

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
                           IsAvailable= true,
                           ImgUrl="https://i0.wp.com/www.attackofthebooks.com/wp-content/uploads/2017/04/The_Name_of_the_Wind_UK_cover.jpg?fit=1666%2C2560&ssl=1"

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
                           IsAvailable= true,
                           ImgUrl="https://cdn11.bigcommerce.com/s-gibnfyxosi/images/stencil/2560w/products/130183/131921/41UZeCEKOBL__78384.1615569895.jpg?c=1"

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
                           IsAvailable= true,
                           ImgUrl="https://th.bing.com/th/id/R.135b0150a36bd91cbd74324561b4725b?rik=Ac6gyPMcaqoDHg&pid=ImgRaw&r=0"

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
                           IsAvailable= true,
                           ImgUrl="https://cdn.kobo.com/book-images/c81ea4de-cfb7-415d-8634-314aad041fdb/1200/1200/False/rich-dad-poor-dad-9.jpg"

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
                           IsAvailable= true,
                           ImgUrl="https://d3fa68hw0m2vcc.cloudfront.net/f92/172976980.jpeg"

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
                           IsAvailable= true,
                           ImgUrl="https://th.bing.com/th/id/OIP.c7sOT_zwzaWNafdGnO2PGwHaLx?rs=1&pid=ImgDetMain"

                       }
                   });
            }
            context.SaveChanges();

        }
    }
}
