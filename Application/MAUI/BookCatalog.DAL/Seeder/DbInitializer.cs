using BookCatalog.DAL.Data;
using BookCatalog.DAL.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BookCatalog.DAL.Storage.DataBase.Seeder;

public static class DbInitializer
{
    public async static Task Seed(IApplicationBuilder applicationBuilder)
    {
        BookCatalogDbContext context = applicationBuilder.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<BookCatalogDbContext>();
        if (!await context.Genres.AnyAsync())
        {
            List<Genre> Genres = new() {
                new Genre { Name = "Fiction", Description = "Narrative literature created from the imagination, encompassing novels and short stories." },
                new Genre { Name = "Science Fiction", Description = "Explores futuristic concepts, advanced technology, and space exploration." },
                new Genre { Name = "Romance", Description = "Centers on love stories and emotional relationships." },
                new Genre { Name = "Fantasy", Description = "Features magical elements and imaginary worlds." },
                new Genre { Name = "Young Adult", Description = "Targets teenage readers, often focusing on coming-of-age themes." },
                new Genre { Name = "Mystery", Description = "Involves solving crimes or uncovering secrets." },
                new Genre { Name = "Historical Fiction", Description = "Set in the past, blending historical facts with fictional narratives." },
                new Genre { Name = "Epic Poetry", Description = "Long narrative poems detailing heroic deeds and events." },
                new Genre { Name = "Horror", Description = "Aims to evoke fear through suspense and supernatural elements." },
                new Genre { Name = "Adventure", Description = "Focuses on exciting journeys and explorations." },
                new Genre { Name = "Magical Realism", Description = "Blends magical elements into realistic settings." },
                new Genre { Name = "Literary Fiction", Description = "Emphasizes character development and thematic depth." },
                new Genre { Name = "Gothic Fiction", Description = "Combines horror and romance in dark, mysterious settings." },
                new Genre { Name = "Thriller", Description = "Characterized by suspense, tension, and excitement." },
                new Genre { Name = "Memoir", Description = "A personal account focusing on specific experiences." },
                new Genre { Name = "Non-fiction", Description = "Based on factual information and real events." },
                new Genre { Name = "Autobiography", Description = "A self-written account of one's life." },
                new Genre { Name = "Post-Apocalyptic", Description = "Depicts life after a catastrophic event." },
                new Genre { Name = "Dystopian", Description = "Explores societies characterized by oppression and control." },
                new Genre { Name = "Satire", Description = "Uses humor and irony to critique society." },
                new Genre { Name = "Self-help", Description = "Provides guidance for personal improvement." },
                new Genre { Name = "Personal Finance", Description = "Offers advice on managing money and investments." },
                new Genre { Name = "Psychology", Description = "Studies mental processes and behavior." }
                };
            await context.Genres.AddRangeAsync(Genres);
            await context.SaveChangesAsync();

        }

        if (!await context.Authors.AnyAsync())
        {
            List<Author> Authors = new(){
                    new Author { Name = "Harper Lee", Description = "American author known for 'To Kill a Mockingbird,' addressing racial injustice." },
                    new Author { Name = "George Orwell", Description = "British writer of dystopian novels like '1984' and 'Animal Farm.'" },
                    new Author { Name = "F. Scott Fitzgerald", Description = "American novelist famous for 'The Great Gatsby,' depicting the Jazz Age." },
                    new Author { Name = "Jane Austen", Description = "English novelist known for romantic fiction like 'Pride and Prejudice.'" },
                    new Author { Name = "J.R.R. Tolkien", Description = "British author of high fantasy works, including 'The Lord of the Rings.'" },
                    new Author { Name = "J.K. Rowling", Description = "British writer of the 'Harry Potter' series, blending fantasy and adventure." },
                    new Author { Name = "J.D. Salinger", Description = "American author best known for 'The Catcher in the Rye.'" },
                    new Author { Name = "Aldous Huxley", Description = "British writer of 'Brave New World,' exploring dystopian themes." },
                    new Author { Name = "Paulo Coelho", Description = "Brazilian author of 'The Alchemist,' focusing on spiritual journeys." },
                    new Author { Name = "Suzanne Collins", Description = "American writer of 'The Hunger Games' trilogy." },
                    new Author { Name = "Dan Brown", Description = "American author known for thrillers like 'The Da Vinci Code.'" },
                    new Author { Name = "Charles Dickens", Description = "English novelist of classics like 'Oliver Twist' and 'A Christmas Carol.'" },
                    new Author { Name = "Homer", Description = "Ancient Greek poet attributed with 'The Iliad' and 'The Odyssey.'" },
                    new Author { Name = "Stephen King", Description = "Prolific American author of horror and supernatural fiction." },
                    new Author { Name = "John Steinbeck", Description = "American writer of 'The Grapes of Wrath,' focusing on social issues." },
                    new Author { Name = "Herman Melville", Description = "American novelist best known for 'Moby-Dick.'" },
                    new Author { Name = "Gabriel García Márquez", Description = "Colombian author of 'One Hundred Years of Solitude,' a key figure in magical realism." },
                    new Author { Name = "Fyodor Dostoevsky", Description = "Russian novelist of psychological works like 'Crime and Punishment.'" },
                    new Author { Name = "Emily Brontë", Description = "English author of 'Wuthering Heights,' a classic of Gothic fiction." },
                    new Author { Name = "Oscar Wilde", Description = "Irish writer known for wit and works like 'The Picture of Dorian Gray.'" },
                    new Author { Name = "Alexandre Dumas", Description = "French author of adventure novels like 'The Three Musketeers.'" },
                    new Author { Name = "Alex Michaelides", Description = "British-Cypriot author of psychological thriller 'The Silent Patient.'" },
                    new Author { Name = "Delia Owens", Description = "American author of 'Where the Crawdads Sing,' blending mystery and nature." },
                    new Author { Name = "Tara Westover", Description = "American memoirist of 'Educated,' detailing her journey from isolation to academia." },
                    new Author { Name = "Yuval Noah Harari", Description = "Israeli historian and author of 'Sapiens,' exploring human history." },
                    new Author { Name = "Michelle Obama", Description = "Former First Lady of the U.S. and author of memoir 'Becoming.'" },
                    new Author { Name = "Paula Hawkins", Description = "British author of psychological thriller 'The Girl on the Train.'" },
                    new Author { Name = "George R.R. Martin", Description = "American writer of the epic fantasy series 'A Song of Ice and Fire.'" },
                    new Author { Name = "Khaled Hosseini", Description = "Afghan-American author of 'The Kite Runner,' focusing on Afghan history." },
                    new Author { Name = "Yann Martel", Description = "Canadian author of 'Life of Pi,' blending adventure and spirituality." },
                    new Author { Name = "Markus Zusak", Description = "Australian writer of 'The Book Thief,' set in Nazi Germany." },
                    new Author { Name = "Cormac McCarthy", Description = "American novelist known for 'The Road,' depicting post-apocalyptic survival." },
                    new Author { Name = "Ray Bradbury", Description = "American author of 'Fahrenheit 451,' exploring censorship and dystopia." },
                    new Author { Name = "Margaret Atwood", Description = "Canadian writer of 'The Handmaid's Tale,' focusing on feminist dystopia." },
                    new Author { Name = "Lois Lowry", Description = "American author of 'The Giver,' a seminal work in young adult dystopian fiction." },
                    new Author { Name = "Andy Weir", Description = "American writer of science fiction novel 'The Martian.'" },
                    new Author { Name = "Gillian Flynn", Description = "American author of psychological thriller 'Gone Girl.'" },
                    new Author { Name = "John Green", Description = "American writer of young adult novels like 'The Fault in Our Stars.'" },
                    new Author { Name = "Douglas Adams", Description = "British author of the comedic science fiction series 'The Hitchhiker's Guide to the Galaxy.'" },
                    new Author { Name = "Joseph Heller", Description = "American writer of satirical novel 'Catch-22.'" },
                    new Author { Name = "Ernest Cline", Description = "American author of 'Ready Player One,' blending science fiction and pop culture." },
                    new Author { Name = "Patrick Rothfuss", Description = "American fantasy writer known for 'The Kingkiller Chronicle' series." },
                    new Author { Name = "Frank Herbert", Description = "American author of the science fiction epic 'Dune.'" },
                    new Author { Name = "M. Scott Peck", Description = "American psychiatrist and author of self-help book 'The Road Less Traveled.'" },
                    new Author { Name = "Robert T. Kiyosaki", Description = "American entrepreneur and author of 'Rich Dad Poor Dad,' focusing on financial education." },
                    new Author { Name = "Dale Carnegie", Description = "American writer known for 'How to Win Friends and Influence People,' a classic in self-improvement." },
                    new Author { Name = "Daniel Kahneman", Description = "Israeli-American psychologist and Nobel laureate, author of 'Thinking, Fast and Slow,' exploring decision-making processes." }
                };
            await context.Authors.AddRangeAsync(Authors);
            await context.SaveChangesAsync();
        }

        if (!await context.Publishers.AnyAsync())
        {
            List<Publisher> Publishers = new()
            {
                new Publisher { Name = "J. B. Lippincott \u0026 Co.", Description = "Founded in 1836 in Philadelphia, known for publishing 'To Kill a Mockingbird' and medical literature." },
                new Publisher { Name = "Secker \u0026 Warburg", Description = "British publisher renowned for politically charged works like George Orwell’s '1984' and 'Animal Farm'." },
                new Publisher { Name = "Charles Scribner\u0027s Sons", Description = "American publishing house famous for publishing F. Scott Fitzgerald and Ernest Hemingway." },
                new Publisher { Name = "T. Egerton", Description = "Historical London-based publisher known for releasing Jane Austen’s first novel, 'Sense and Sensibility'." },
                new Publisher { Name = "George Allen \u0026 Unwin", Description = "British publisher that brought J.R.R. Tolkien’s 'The Hobbit' and 'The Lord of the Rings' to print." },
                new Publisher { Name = "Bloomsbury", Description = "London-based publisher best known for publishing the 'Harry Potter' series." },
                new Publisher { Name = "Allen \u0026 Unwin", Description = "Continued as an Australian publisher of literary fiction and nonfiction." },
                new Publisher { Name = "Little, Brown and Company", Description = "American publisher behind works like J.D. Salinger’s 'The Catcher in the Rye'." },
                new Publisher { Name = "Chatto \u0026 Windus", Description = "British literary publishing house that published Aldous Huxley’s 'Brave New World'." },
                new Publisher { Name = "HarperOne", Description = "An imprint of HarperCollins focusing on spiritual and self-help literature, including Paulo Coelho’s works." },
                new Publisher { Name = "Scholastic Press", Description = "Publisher of children’s and young adult books, including Suzanne Collins’ 'The Hunger Games' series." },
                new Publisher { Name = "Doubleday", Description = "Major American publisher of bestselling fiction, including Dan Brown’s thrillers." },
                new Publisher { Name = "Chapman \u0026 Hall", Description = "19th-century British publisher known for publishing Charles Dickens’ novels." },
                new Publisher { Name = "Oxford University Press", Description = "World’s largest university press, publishing academic and classic literary works." },
                new Publisher { Name = "The Viking Press", Description = "Also published John Steinbeck’s major works such as 'The Grapes of Wrath'." },
                new Publisher { Name = "Harper \u0026 Brothers", Description = "One of the oldest American publishers, known for Herman Melville’s 'Moby-Dick'." },
                new Publisher { Name = "Harper \u0026 Row", Description = "Result of a merger between Harper \u0026 Brothers and Row, Peterson \u0026 Company; later became HarperCollins." },
                new Publisher { Name = "The Russian Messenger", Description = "19th-century literary magazine that first serialized Dostoevsky’s 'Crime and Punishment'." },
                new Publisher { Name = "Thomas Cautley Newby", Description = "Published Emily Brontë’s 'Wuthering Heights' in the mid-1800s." },
                new Publisher { Name = "Lippincott's Monthly Magazine", Description = "Published serialized fiction including early works of Oscar Wilde and other literary figures." },
                new Publisher { Name = "P\u00E9tion", Description = "French publisher associated with political writings during the French Revolution." },
                new Publisher { Name = "Celadon Books", Description = "Modern imprint known for publishing Alex Michaelides’ 'The Silent Patient'." },
                new Publisher { Name = "G.P. Putnam's Sons", Description = "American publishing house behind Delia Owens' bestselling novel 'Where the Crawdads Sing'." },
                new Publisher { Name = "Random House", Description = "One of the largest global publishers, released Tara Westover’s memoir 'Educated'." },
                new Publisher { Name = "Harper", Description = "Part of HarperCollins, known for a broad range of literary and commercial titles." },
                new Publisher { Name = "Crown", Description = "Imprint of Penguin Random House that published Michelle Obama’s 'Becoming' and Andy Weir’s 'The Martian'." },
                new Publisher { Name = "Riverhead Books", Description = "Imprint known for bestselling novels such as 'The Kite Runner' and 'The Girl on the Train'." },
                new Publisher { Name = "Bantam Spectra", Description = "Sci-fi/fantasy imprint of Bantam Books, published early works of George R.R. Martin." },
                new Publisher { Name = "Knopf Canada", Description = "Published Yann Martel’s 'Life of Pi,' a major international bestseller." },
                new Publisher { Name = "Picador", Description = "Imprint known for literary fiction like Markus Zusak’s 'The Book Thief'." },
                new Publisher { Name = "Alfred A. Knopf", Description = "Prestigious American publisher of literary fiction, including works by Cormac McCarthy." },
                new Publisher { Name = "Ballantine Books", Description = "American publisher of Ray Bradbury’s 'Fahrenheit 451' and other genre fiction." },
                new Publisher { Name = "McClelland \u0026 Stewart", Description = "Canadian publisher of Margaret Atwood’s works, including 'The Handmaid’s Tale'." },
                new Publisher { Name = "Houghton Mifflin", Description = "Publisher of Lois Lowry’s acclaimed young adult novels like 'The Giver'." },
                new Publisher { Name = "Crown Publishing Group", Description = "Division of Random House known for nonfiction bestsellers and modern fiction hits." },
                new Publisher { Name = "Dutton Books", Description = "Imprint that released John Green’s popular YA novels." },
                new Publisher { Name = "Pan Books", Description = "UK-based publisher of Douglas Adams’ 'The Hitchhiker’s Guide to the Galaxy'." },
                new Publisher { Name = "Simon \u0026 Schuster", Description = "Published Joseph Heller’s 'Catch-22' and other modern classics." },
                new Publisher { Name = "DAW Books", Description = "Science fiction and fantasy publisher of Patrick Rothfuss’ works." },
                new Publisher { Name = "Chilton Books", Description = "Original publisher of Frank Herbert’s science fiction classic 'Dune'." },
                new Publisher { Name = "Warner Books", Description = "Published popular fiction, later merged into Hachette Book Group." },
                new Publisher { Name = "Farrar, Straus and Giroux", Description = "Publisher of Daniel Kahneman’s 'Thinking, Fast and Slow,' on human decision-making." }
            };

            await context.Publishers.AddRangeAsync(Publishers);
            await context.SaveChangesAsync();
        }


        if (!await context.Pictures.AnyAsync())
        {
            List<Picture> Images = new()
            {
                new Picture { ImgUrl = "https://th.bing.com/th/id/OIP.W1ngiF2AkQaO78CY9yC1HQHaLd?rs=1&pid=ImgDetMain" },
                new Picture { ImgUrl = "https://th.bing.com/th/id/R.e5f82111504d28779262bf2dca0fad56?rik=9etdGLc9mstPyw&pid=ImgRaw&r=0" },
                new Picture { ImgUrl = "https://hachette.imgix.net/books/9780762498130.jpg?auto=compress,format" },
                new Picture { ImgUrl = "https://cdn2.penguin.com.au/covers/original/9780141330167.jpg" },
                new Picture { ImgUrl = "https://th.bing.com/th/id/R.8947741487564891cf18c8b32752c2bf?rik=gsUGQLJSJMRUSQ&pid=ImgRaw&r=0" },
                new Picture { ImgUrl = "https://res.cloudinary.com/bloomsbury-atlas/image/upload/w_568,c_scale/jackets/9781408855652.jpg" },
                new Picture { ImgUrl = "https://cdn11.bigcommerce.com/s-gibnfyxosi/images/stencil/1920w/products/154740/156431/51eq24cRtRL__98083.1615576774.jpg?c=1" },
                new Picture { ImgUrl = "https://th.bing.com/th/id/OIP.7vHm4DvQa3iwZAoiOFcZtwHaKr?rs=1&pid=ImgDetMain" },
                new Picture { ImgUrl = "https://th.bing.com/th/id/OIP.7vHm4DvQa3iwZAoiOFcZtwHaKr?rs=1&pid=ImgDetMain" },
                new Picture { ImgUrl = "https://th.bing.com/th/id/R.9f4b5965d0bc99a4e4e0130b713285a0?rik=d5ac2urjW9GuEw&pid=ImgRaw&r=0" },
                new Picture { ImgUrl = "https://th.bing.com/th/id/OIP.9-v0mfum2BK2IFqSLAXf3gHaL7?rs=1&pid=ImgDetMain" },
                new Picture { ImgUrl = "https://th.bing.com/th/id/OIP.QdhjN4JxwcYPvlvKjwPKygHaLH?rs=1&pid=ImgDetMain" },
                new Picture { ImgUrl = "https://th.bing.com/th/id/R.bd9481b3c05998fdbb7b1fc49ba1f3ef?rik=yUOPSX2aJliuAA&pid=ImgRaw&r=0" },
                new Picture { ImgUrl = "https://th.bing.com/th/id/OIP.B4wjpTB6Gg1o4O72nV6A7AHaJ4?rs=1&pid=ImgDetMain" },
                new Picture { ImgUrl = "https://th.bing.com/th/id/OIP._5vHs3lHPIM5CyLsVE-XUwHaMK?rs=1&pid=ImgDetMain" },
                new Picture { ImgUrl = "https://th.bing.com/th/id/OIP.SwfiX3jY9A5_pwTQ2IaZigHaLH?rs=1&pid=ImgDetMain" },
                new Picture { ImgUrl = "https://th.bing.com/th/id/OIP.ibj2AYzaoo9_OwOOTHHxYwHaLd?rs=1&pid=ImgDetMain" },
                new Picture { ImgUrl = "https://th.bing.com/th/id/OIP.WTbE7_qIyqfSbkS2CNdPRAHaJl?rs=1&pid=ImgDetMain" },
                new Picture { ImgUrl = "https://th.bing.com/th/id/R.8697cc74db5911afdc6e83680530401b?rik=1vDwtG3ci2QYLg&riu=http%3a%2f%2fdwcp78yw3i6ob.cloudfront.net%2fwp-content%2fuploads%2f2016%2f12%2f12162813%2f100_Years_FirstOrDefault_Ed_Hi_Res-768x1153.jpg&ehk=%2b6OxuEL8iXKiqX9LZgGO6Tac3AslNULuU8MjLqgFkYU%3d&risl=&pid=ImgRaw&r=0" },
                new Picture { ImgUrl = "https://cdn2.penguin.com.au/covers/original/9781857150353.jpg" },
                new Picture { ImgUrl = "https://th.bing.com/th/id/R.e1d1b936c903c72d27b818a5c69b93b9?rik=47BZwvJmMrUBPQ&riu=http%3a%2f%2fassets.allenandunwin.com.s3.amazonaws.com%2fimages%2foriginal%2f9780571337118.jpg&ehk=nRjkVhb9omqg9ee5rtw3mvERugf1a29dwSSdI7kV5RY%3d&risl=&pid=ImgRaw&r=0" },
                new Picture { ImgUrl = "https://wordsworth-editions.com/wp-content/uploads/2017/07/9781853260155.jpg" },
                new Picture { ImgUrl = "https://cdn.kobo.com/book-images/f93d05eb-4306-48be-926b-8914b36bc9c3/1200/1200/False/the-count-of-monte-cristo-51.jpg" },
                new Picture { ImgUrl = "https://th.bing.com/th/id/OIP.MsbTboenjRO2EoLRDG_Q-wHaLM?rs=1&pid=ImgDetMain" },
                new Picture { ImgUrl = "https://th.bing.com/th/id/OIP.h1ZkrWQ1gu-ZDl-4T1ymBQHaLp?rs=1&pid=ImgDetMain" },
                new Picture { ImgUrl = "https://th.bing.com/th/id/OIP.Kn8v8LjGkvpgg8BanIjFnAHaLQ?rs=1&pid=ImgDetMain" },
                new Picture { ImgUrl = "https://th.bing.com/th/id/OIP.Wdw8knp_mCAsj_Y4hZ4MQwHaLX?rs=1&pid=ImgDetMain" },
                new Picture { ImgUrl = "https://th.bing.com/th/id/OIP.e__NEYvJpiqMPy4lAR_zHgHaLQ?rs=1&pid=ImgDetMain" },
                new Picture { ImgUrl = "https://i5.walmartimages.com/asr/a6513e10-23f9-4f18-9100-554fddcaa750_1.edd1069e46c70d49a412c4ec82f6dd21.jpeg" },
                new Picture { ImgUrl = "https://th.bing.com/th/id/OIP.h2v49-gy-RyOKhQ6ub2T4wAAAA?rs=1&pid=ImgDetMain" },
                new Picture { ImgUrl = "https://imgv2-2-f.scribdassets.com/img/word_document/250024294/original/582f636f24/1587740851?v=1" },
                new Picture { ImgUrl = "https://i.pinimg.com/originals/a3/71/56/a37156a4e8b8b1a403e18eb32ebdb97e.jpg" },
                new Picture { ImgUrl = "https://th.bing.com/th/id/OIP.QXfpwLb6of2QlgHwviXxogHaLj?rs=1&pid=ImgDetMain" },
                new Picture { ImgUrl = "https://th.bing.com/th/id/OIP.b0ixz-7oXf23i_S0xgXpGQHaLO?rs=1&pid=ImgDetMain" },
                new Picture { ImgUrl = "https://th.bing.com/th/id/R.ef65387af1539c717421c16fdf69d4fc?rik=UGH8S1OlgvrkoQ&riu=http%3a%2f%2flitstack.com%2fwp-content%2fuploads%2f2013%2f09%2ffahrenheit-451-book-cover1.jpg&ehk=5jdfngLzYvSUA%2fChbZR5A7GmzIm6eCt72Z7QwkhYl9o%3d&risl=&pid=ImgRaw&r=0" },
                new Picture { ImgUrl = "https://th.bing.com/th/id/OIP.Bot9b5KthqRuI80alqWmBgHaLb?rs=1&pid=ImgDetMain" },
                new Picture { ImgUrl = "https://cdn.kobo.com/book-images/16327bfc-5399-4b3d-be6d-e65873cc4c30/1200/1200/False/the-giver-harpercollins-children-s-modern-classics.jpg" },
                new Picture { ImgUrl = "https://th.bing.com/th/id/OIP.EQvB2pkuKdBk5ThLNRpV3AHaLQ?rs=1&pid=ImgDetMain" },
                new Picture { ImgUrl = "https://th.bing.com/th/id/R.979b1d14087153c5e264fef39aee3a96?rik=ADYwTpdpf0DeqQ&riu=http%3a%2f%2fimages6.fanpop.com%2fimage%2fphotos%2f37400000%2fGone-Girl-by-Gillian-Flynn-gone-girl-37441442-1181-1810.jpg&ehk=rtg5qz7Eh5WgcMzc%2bCEMz7DThMzsLhAX39DPt%2fl9MtM%3d&risl=&pid=ImgRaw&r=0" },
                new Picture { ImgUrl = "https://th.bing.com/th/id/R.e795ce4ab4b4c1fe088529296b6ee94d?rik=JBP7Sjfuhhqp1Q&pid=ImgRaw&r=0" },
                new Picture { ImgUrl = "https://th.bing.com/th/id/OIP.6lE_KV_vkckc3Ey-TSvHuwHaLJ?rs=1&pid=ImgDetMain" },
                new Picture { ImgUrl = "https://i0.wp.com/www.attackofthebooks.com/wp-content/uploads/2017/04/The_Name_of_the_Wind_UK_cover.jpg?fit=1666%2C2560&ssl=1" },
                new Picture { ImgUrl = "https://th.bing.com/th/id/OIP.8176UT2wXeN8km6wuq_imwAAAA?rs=1&pid=ImgDetMain" },
                new Picture { ImgUrl = "https://th.bing.com/th/id/OIP.86Wh_7BdSgI8So5cKvXhgwHaLb?rs=1&pid=ImgDetMain" },
                new Picture { ImgUrl = "https://cdn11.bigcommerce.com/s-gibnfyxosi/images/stencil/2560w/products/130183/131921/41UZeCEKOBL__78384.1615569895.jpg?c=1" },
                new Picture { ImgUrl = "https://th.bing.com/th/id/R.135b0150a36bd91cbd74324561b4725b?rik=Ac6gyPMcaqoDHg&pid=ImgRaw&r=0" },
                new Picture { ImgUrl = "https://cdn.kobo.com/book-images/c81ea4de-cfb7-415d-8634-314aad041fdb/1200/1200/False/rich-dad-poor-dad-9.jpg" },
                new Picture { ImgUrl = "https://d3fa68hw0m2vcc.cloudfront.net/f92/172976980.jpeg" },
                new Picture { ImgUrl = "https://th.bing.com/th/id/OIP.c7sOT_zwzaWNafdGnO2PGwHaLx?rs=1&pid=ImgDetMain" },
                new Picture { ImgUrl = "https://images.pexels.com/photos/626986/pexels-photo-626986.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2" },
                new Picture { ImgUrl = "https://images.pexels.com/photos/3952078/pexels-photo-3952078.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2" },
                new Picture { ImgUrl = "https://images.pexels.com/photos/3952095/pexels-photo-3952095.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2" },
                new Picture { ImgUrl = "https://media-cdn.tripadvisor.com/media/photo-o/17/26/36/12/john-sandoe-books.jpg" },
                new Picture { ImgUrl = "https://lp-cms-production.imgix.net/2021-12/GettyImages-1286769946.jpg?auto=format&q=40&w=870&dpr=4" },
                new Picture { ImgUrl = "https://i0.wp.com/littlebookstores.gr/wp-content/uploads/2016/09/IMG_1827-1.jpg?fit=3564%2C1909&ssl=1" },
                new Picture { ImgUrl = "https://drsw10gc90t0z.cloudfront.net/AcuCustom/Sitename/DAM/483/Housmans_Bookshop.jpg" },
                new Picture { ImgUrl = "https://www.boncado.be/img/uploads/9830/5fca60ab9cf93.jpg" },
                new Picture { ImgUrl = "https://live.staticflickr.com/86/248172816_79cd2f2b9b_b.jpg" },
            };
            await context.Pictures.AddRangeAsync(Images);
            await context.SaveChangesAsync();
        }


        if (!await context.MoreInfos.AnyAsync())
        {
            List<MoreInfo> moreInfos = new()
    {
        new MoreInfo
        {
            Id = Guid.NewGuid(),
            Name = "Pulitzer Prize Winner",
            Description = "Winner of the prestigious Pulitzer Prize for Fiction, recognizing excellence in literature."
        },
        new MoreInfo
        {
            Id = Guid.NewGuid(),
            Name = "Nobel Prize in Literature",
            Description = "Author received the Nobel Prize in Literature for their outstanding contribution to world literature."
        },
        new MoreInfo
        {
            Id = Guid.NewGuid(),
            Name = "Hugo Award Winner",
            Description = "Winner of the Hugo Award for Best Novel, the premier award for science fiction and fantasy literature."
        },
        new MoreInfo
        {
            Id = Guid.NewGuid(),
            Name = "Newbery Medal Winner",
            Description = "Recipient of the Newbery Medal, awarded annually for the most distinguished American children's book."
        },
        new MoreInfo
        {
            Id = Guid.NewGuid(),
            Name = "Man Booker Prize Winner",
            Description = "Winner of the prestigious Man Booker Prize for Fiction, celebrating the finest in contemporary literature."
        },
        new MoreInfo
        {
            Id = Guid.NewGuid(),
            Name = "National Book Award Winner",
            Description = "Recipient of the National Book Award, recognizing outstanding literary work by American authors."
        },
        new MoreInfo
        {
            Id = Guid.NewGuid(),
            Name = "Edgar Award Winner",
            Description = "Winner of the Edgar Award for Best Novel, recognizing excellence in mystery and crime writing."
        },

        new MoreInfo
        {
            Id = Guid.NewGuid(),
            Name = "Major Motion Picture",
            Description = "Adapted into a successful major motion picture that received critical acclaim and commercial success."
        },
        new MoreInfo
        {
            Id = Guid.NewGuid(),
            Name = "Television Series Adaptation",
            Description = "Adapted into a popular television series that has garnered international recognition."
        },
        new MoreInfo
        {
            Id = Guid.NewGuid(),
            Name = "Broadway Musical",
            Description = "Adapted into a successful Broadway musical production."
        },
        new MoreInfo
        {
            Id = Guid.NewGuid(),
            Name = "Graphic Novel Adaptation",
            Description = "Successfully adapted into graphic novel format, bringing visual storytelling to the narrative."
        },
        new MoreInfo
        {
            Id = Guid.NewGuid(),
            Name = "Video Game Adaptation",
            Description = "Inspired video game adaptations that allow readers to experience the story interactively."
        },

        new MoreInfo
        {
            Id = Guid.NewGuid(),
            Name = "New York Times Bestseller",
            Description = "Appeared on The New York Times Best Seller list, indicating exceptional sales and popularity."
        },
        new MoreInfo
        {
            Id = Guid.NewGuid(),
            Name = "International Bestseller",
            Description = "Achieved bestseller status in multiple countries and has been translated into numerous languages."
        },
        new MoreInfo
        {
            Id = Guid.NewGuid(),
            Name = "Over 50 Million Copies Sold",
            Description = "Has sold over 50 million copies worldwide, demonstrating its enduring appeal and impact."
        },
        new MoreInfo
        {
            Id = Guid.NewGuid(),
            Name = "Translated into 100+ Languages",
            Description = "Translated into over 100 languages, making it accessible to readers worldwide."
        },

        new MoreInfo
        {
            Id = Guid.NewGuid(),
            Name = "High School Curriculum",
            Description = "Widely included in high school English curricula as required reading for students."
        },
        new MoreInfo
        {
            Id = Guid.NewGuid(),
            Name = "University Study Subject",
            Description = "Frequently studied in university literature courses and used as subject for academic research."
        },
        new MoreInfo
        {
            Id = Guid.NewGuid(),
            Name = "Cultural Phenomenon",
            Description = "Became a cultural phenomenon that influenced popular culture and spawned numerous references in other media."
        },
        new MoreInfo
        {
            Id = Guid.NewGuid(),
            Name = "Banned Book",
            Description = "Has been subject to censorship and banning in some regions due to controversial themes, highlighting its impact."
        },

        new MoreInfo
        {
            Id = Guid.NewGuid(),
            Name = "Genre Defining Work",
            Description = "Considered a defining work of its genre that influenced countless subsequent authors and works."
        },
        new MoreInfo
        {
            Id = Guid.NewGuid(),
            Name = "Literary Classic",
            Description = "Recognized as a literary classic that continues to be relevant and influential decades after publication."
        },
        new MoreInfo
        {
            Id = Guid.NewGuid(),
            Name = "Debut Novel",
            Description = "Remarkable debut novel that launched the author's successful literary career."
        },
        new MoreInfo
        {
            Id = Guid.NewGuid(),
            Name = "Final Work",
            Description = "The author's final completed work, representing the culmination of their literary journey."
        },

        new MoreInfo
        {
            Id = Guid.NewGuid(),
            Name = "Collector's Edition Available",
            Description = "Special collector's editions with unique cover art, illustrations, and additional content are available."
        },
        new MoreInfo
        {
            Id = Guid.NewGuid(),
            Name = "Annotated Edition",
            Description = "Annotated editions available with scholarly commentary and historical context."
        },
        new MoreInfo
        {
            Id = Guid.NewGuid(),
            Name = "Illustrated Edition",
            Description = "Beautiful illustrated editions featuring artwork that complements the narrative."
        },

        new MoreInfo
        {
            Id = Guid.NewGuid(),
            Name = "Posthumous Publication",
            Description = "Published after the author's death, representing their final literary contribution."
        },
        new MoreInfo
        {
            Id = Guid.NewGuid(),
            Name = "Pseudonym Work",
            Description = "Originally published under a pseudonym, adding mystery to the work's origins."
        },
        new MoreInfo
        {
            Id = Guid.NewGuid(),
            Name = "Biographical Elements",
            Description = "Contains significant autobiographical elements, offering insights into the author's life and experiences."
        },

        new MoreInfo
        {
            Id = Guid.NewGuid(),
            Name = "Quick Read",
            Description = "Can be comfortably read in a single sitting, making it perfect for busy readers."
        },
        new MoreInfo
        {
            Id = Guid.NewGuid(),
            Name = "Epic Length",
            Description = "An epic-length novel that provides an immersive reading experience spanning many hours."
        },
        new MoreInfo
        {
            Id = Guid.NewGuid(),
            Name = "Multiple Perspectives",
            Description = "Told from multiple character perspectives, offering a rich and complex narrative structure."
        },
        new MoreInfo
        {
            Id = Guid.NewGuid(),
            Name = "Unreliable Narrator",
            Description = "Features an unreliable narrator, adding layers of complexity and intrigue to the storytelling."
        },

        new MoreInfo
        {
            Id = Guid.NewGuid(),
            Name = "First in Series",
            Description = "The first book in a beloved series that continues the story across multiple volumes."
        },
        new MoreInfo
        {
            Id = Guid.NewGuid(),
            Name = "Standalone Novel",
            Description = "A complete standalone story that doesn't require reading other books in the series."
        },
        new MoreInfo
        {
            Id = Guid.NewGuid(),
            Name = "Series Finale",
            Description = "The concluding volume of a popular series, bringing beloved characters' stories to a close."
        }
    };

            await context.MoreInfos.AddRangeAsync(moreInfos);
            await context.SaveChangesAsync();

            var books = await context.Books.Include(b => b.MoreInfos).ToListAsync();
            var allMoreInfos = await context.MoreInfos.ToListAsync();

            MoreInfo GetMoreInfo(string name) => allMoreInfos.First(mi => mi.Name == name);

            var toKillAMockingbird = books.First(b => b.Title == "To Kill a Mockingbird");
            toKillAMockingbird.MoreInfos = new List<MoreInfo>
    {
        GetMoreInfo("Pulitzer Prize Winner"),
        GetMoreInfo("Major Motion Picture"),
        GetMoreInfo("High School Curriculum"),
        GetMoreInfo("Literary Classic"),
        GetMoreInfo("Banned Book")
    };

            var nineteenEightyFour = books.First(b => b.Title == "1984");
            nineteenEightyFour.MoreInfos = new List<MoreInfo>
    {
        GetMoreInfo("Literary Classic"),
        GetMoreInfo("High School Curriculum"),
        GetMoreInfo("Cultural Phenomenon"),
        GetMoreInfo("Banned Book"),
        GetMoreInfo("Television Series Adaptation")
    };

            var greatGatsby = books.First(b => b.Title == "The Great Gatsby");
            greatGatsby.MoreInfos = new List<MoreInfo>
    {
        GetMoreInfo("Literary Classic"),
        GetMoreInfo("Major Motion Picture"),
        GetMoreInfo("High School Curriculum"),
        GetMoreInfo("University Study Subject")
    };

            var prideAndPrejudice = books.First(b => b.Title == "Pride and Prejudice");
            prideAndPrejudice.MoreInfos = new List<MoreInfo>
    {
        GetMoreInfo("Literary Classic"),
        GetMoreInfo("Major Motion Picture"),
        GetMoreInfo("Television Series Adaptation"),
        GetMoreInfo("University Study Subject")
    };

            var theHobbit = books.First(b => b.Title == "The Hobbit");
            theHobbit.MoreInfos = new List<MoreInfo>
    {
        GetMoreInfo("Major Motion Picture"),
        GetMoreInfo("International Bestseller"),
        GetMoreInfo("Translated into 100+ Languages"),
        GetMoreInfo("First in Series")
    };

            var harryPotter = books.First(b => b.Title == "Harry Potter and the Philosopher's Stone");
            harryPotter.MoreInfos = new List<MoreInfo>
    {
        GetMoreInfo("Major Motion Picture"),
        GetMoreInfo("International Bestseller"),
        GetMoreInfo("Over 50 Million Copies Sold"),
        GetMoreInfo("Cultural Phenomenon"),
        GetMoreInfo("First in Series"),
        GetMoreInfo("Debut Novel")
    };

            var lordOfRings = books.First(b => b.Title == "The Lord of the Rings");
            lordOfRings.MoreInfos = new List<MoreInfo>
    {
        GetMoreInfo("Major Motion Picture"),
        GetMoreInfo("International Bestseller"),
        GetMoreInfo("Epic Length"),
        GetMoreInfo("Genre Defining Work"),
        GetMoreInfo("Translated into 100+ Languages")
    };

            var catcherInRye = books.First(b => b.Title == "The Catcher in the Rye");
            catcherInRye.MoreInfos = new List<MoreInfo>
    {
        GetMoreInfo("Literary Classic"),
        GetMoreInfo("High School Curriculum"),
        GetMoreInfo("Banned Book"),
        GetMoreInfo("Cultural Phenomenon")
    };

            var braveNewWorld = books.First(b => b.Title == "Brave New World");
            braveNewWorld.MoreInfos = new List<MoreInfo>
    {
        GetMoreInfo("Literary Classic"),
        GetMoreInfo("High School Curriculum"),
        GetMoreInfo("Genre Defining Work"),
        GetMoreInfo("University Study Subject")
    };

            var theAlchemist = books.First(b => b.Title == "The Alchemist");
            theAlchemist.MoreInfos = new List<MoreInfo>
    {
        GetMoreInfo("International Bestseller"),
        GetMoreInfo("Translated into 100+ Languages"),
        GetMoreInfo("Over 50 Million Copies Sold"),
        GetMoreInfo("Quick Read")
    };

            var hungerGames = books.First(b => b.Title == "The Hunger Games");
            hungerGames.MoreInfos = new List<MoreInfo>
    {
        GetMoreInfo("Major Motion Picture"),
        GetMoreInfo("New York Times Bestseller"),
        GetMoreInfo("Cultural Phenomenon"),
        GetMoreInfo("First in Series")
    };

            var daVinciCode = books.First(b => b.Title == "The Da Vinci Code");
            daVinciCode.MoreInfos = new List<MoreInfo>
    {
        GetMoreInfo("Major Motion Picture"),
        GetMoreInfo("International Bestseller"),
        GetMoreInfo("New York Times Bestseller"),
        GetMoreInfo("Cultural Phenomenon")
    };

            var taleOfTwoCities = books.First(b => b.Title == "A Tale of Two Cities");
            taleOfTwoCities.MoreInfos = new List<MoreInfo>
    {
        GetMoreInfo("Literary Classic"),
        GetMoreInfo("High School Curriculum"),
        GetMoreInfo("University Study Subject"),
        GetMoreInfo("Multiple Perspectives")
    };

            var theOdyssey = books.First(b => b.Title == "The Odyssey");
            theOdyssey.MoreInfos = new List<MoreInfo>
    {
        GetMoreInfo("Literary Classic"),
        GetMoreInfo("University Study Subject"),
        GetMoreInfo("Epic Length"),
        GetMoreInfo("Genre Defining Work")
    };

            var theShining = books.First(b => b.Title == "The Shining");
            theShining.MoreInfos = new List<MoreInfo>
    {
        GetMoreInfo("Major Motion Picture"),
        GetMoreInfo("Cultural Phenomenon"),
        GetMoreInfo("Genre Defining Work")
    };

            var grapesOfWrath = books.First(b => b.Title == "The Grapes of Wrath");
            grapesOfWrath.MoreInfos = new List<MoreInfo>
    {
        GetMoreInfo("Pulitzer Prize Winner"),
        GetMoreInfo("Nobel Prize in Literature"),
        GetMoreInfo("Major Motion Picture"),
        GetMoreInfo("Literary Classic")
    };

            var mobyDick = books.First(b => b.Title == "Moby-Dick");
            mobyDick.MoreInfos = new List<MoreInfo>
    {
        GetMoreInfo("Literary Classic"),
        GetMoreInfo("University Study Subject"),
        GetMoreInfo("Epic Length"),
        GetMoreInfo("Genre Defining Work")
    };

            var oneHundredYears = books.First(b => b.Title == "One Hundred Years of Solitude");
            oneHundredYears.MoreInfos = new List<MoreInfo>
    {
        GetMoreInfo("Nobel Prize in Literature"),
        GetMoreInfo("Literary Classic"),
        GetMoreInfo("International Bestseller"),
        GetMoreInfo("Genre Defining Work")
    };

            var crimeAndPunishment = books.First(b => b.Title == "Crime and Punishment");
            crimeAndPunishment.MoreInfos = new List<MoreInfo>
    {
        GetMoreInfo("Literary Classic"),
        GetMoreInfo("University Study Subject"),
        GetMoreInfo("International Bestseller"),
        GetMoreInfo("Epic Length")
    };

            var wutheringHeights = books.First(b => b.Title == "Wuthering Heights");
            wutheringHeights.MoreInfos = new List<MoreInfo>
    {
        GetMoreInfo("Literary Classic"),
        GetMoreInfo("University Study Subject"),
        GetMoreInfo("Debut Novel"),
        GetMoreInfo("Multiple Perspectives")
    };

            var pictureOfDorian = books.First(b => b.Title == "The Picture of Dorian Gray");
            pictureOfDorian.MoreInfos = new List<MoreInfo>
    {
        GetMoreInfo("Literary Classic"),
        GetMoreInfo("Major Motion Picture"),
        GetMoreInfo("University Study Subject")
    };

            var countOfMonteCristo = books.First(b => b.Title == "The Count of Monte Cristo");
            countOfMonteCristo.MoreInfos = new List<MoreInfo>
    {
        GetMoreInfo("Literary Classic"),
        GetMoreInfo("Major Motion Picture"),
        GetMoreInfo("Epic Length"),
        GetMoreInfo("International Bestseller")
    };

            var silentPatient = books.First(b => b.Title == "The Silent Patient");
            silentPatient.MoreInfos = new List<MoreInfo>
    {
        GetMoreInfo("New York Times Bestseller"),
        GetMoreInfo("International Bestseller"),
        GetMoreInfo("Debut Novel"),
        GetMoreInfo("Unreliable Narrator")
    };

            var crawdadsSing = books.First(b => b.Title == "Where the Crawdads Sing");
            crawdadsSing.MoreInfos = new List<MoreInfo>
    {
        GetMoreInfo("New York Times Bestseller"),
        GetMoreInfo("Major Motion Picture"),
        GetMoreInfo("International Bestseller"),
        GetMoreInfo("Debut Novel")
    };

            var educated = books.First(b => b.Title == "Educated");
            educated.MoreInfos = new List<MoreInfo>
    {
        GetMoreInfo("New York Times Bestseller"),
        GetMoreInfo("International Bestseller"),
        GetMoreInfo("Biographical Elements"),
        GetMoreInfo("Debut Novel")
    };

            var sapiens = books.First(b => b.Title == "Sapiens: A Brief History of Humankind");
            sapiens.MoreInfos = new List<MoreInfo>
    {
        GetMoreInfo("International Bestseller"),
        GetMoreInfo("New York Times Bestseller"),
        GetMoreInfo("Translated into 100+ Languages"),
        GetMoreInfo("University Study Subject")
    };

            var becoming = books.First(b => b.Title == "Becoming");
            becoming.MoreInfos = new List<MoreInfo>
    {
        GetMoreInfo("New York Times Bestseller"),
        GetMoreInfo("International Bestseller"),
        GetMoreInfo("Biographical Elements"),
        GetMoreInfo("Cultural Phenomenon")
    };

            var girlOnTrain = books.First(b => b.Title == "The Girl on the Train");
            girlOnTrain.MoreInfos = new List<MoreInfo>
    {
        GetMoreInfo("New York Times Bestseller"),
        GetMoreInfo("Major Motion Picture"),
        GetMoreInfo("International Bestseller"),
        GetMoreInfo("Unreliable Narrator")
    };

            var gameOfThrones = books.First(b => b.Title == "A Game of Thrones");
            gameOfThrones.MoreInfos = new List<MoreInfo>
    {
        GetMoreInfo("Television Series Adaptation"),
        GetMoreInfo("International Bestseller"),
        GetMoreInfo("Cultural Phenomenon"),
        GetMoreInfo("First in Series"),
        GetMoreInfo("Epic Length")
    };

            var kiteRunner = books.First(b => b.Title == "The Kite Runner");
            kiteRunner.MoreInfos = new List<MoreInfo>
    {
        GetMoreInfo("New York Times Bestseller"),
        GetMoreInfo("Major Motion Picture"),
        GetMoreInfo("International Bestseller"),
        GetMoreInfo("Debut Novel")
    };

            var lifeOfPi = books.First(b => b.Title == "Life of Pi");
            lifeOfPi.MoreInfos = new List<MoreInfo>
    {
        GetMoreInfo("Man Booker Prize Winner"),
        GetMoreInfo("Major Motion Picture"),
        GetMoreInfo("International Bestseller")
    };

            var bookThief = books.First(b => b.Title == "The Book Thief");
            bookThief.MoreInfos = new List<MoreInfo>
    {
        GetMoreInfo("International Bestseller"),
        GetMoreInfo("Major Motion Picture"),
        GetMoreInfo("Multiple Perspectives"),
        GetMoreInfo("High School Curriculum")
    };

            var theRoad = books.First(b => b.Title == "The Road");
            theRoad.MoreInfos = new List<MoreInfo>
    {
        GetMoreInfo("Pulitzer Prize Winner"),
        GetMoreInfo("Major Motion Picture"),
        GetMoreInfo("Literary Classic")
    };

            var fahrenheit451 = books.First(b => b.Title == "Fahrenheit 451");
            fahrenheit451.MoreInfos = new List<MoreInfo>
    {
        GetMoreInfo("Literary Classic"),
        GetMoreInfo("High School Curriculum"),
        GetMoreInfo("Cultural Phenomenon"),
        GetMoreInfo("Genre Defining Work")
    };

            var handmaidsTale = books.First(b => b.Title == "The Handmaid's Tale");
            handmaidsTale.MoreInfos = new List<MoreInfo>
    {
        GetMoreInfo("Television Series Adaptation"),
        GetMoreInfo("Literary Classic"),
        GetMoreInfo("High School Curriculum"),
        GetMoreInfo("Cultural Phenomenon")
    };

            var theGiver = books.First(b => b.Title == "The Giver");
            theGiver.MoreInfos = new List<MoreInfo>
    {
        GetMoreInfo("Newbery Medal Winner"),
        GetMoreInfo("Major Motion Picture"),
        GetMoreInfo("High School Curriculum"),
        GetMoreInfo("First in Series")
    };

            var theMartian = books.First(b => b.Title == "The Martian");
            theMartian.MoreInfos = new List<MoreInfo>
    {
        GetMoreInfo("Major Motion Picture"),
        GetMoreInfo("New York Times Bestseller"),
        GetMoreInfo("Hugo Award Winner"),
        GetMoreInfo("Debut Novel")
    };

            var goneGirl = books.First(b => b.Title == "Gone Girl");
            goneGirl.MoreInfos = new List<MoreInfo>
    {
        GetMoreInfo("New York Times Bestseller"),
        GetMoreInfo("Major Motion Picture"),
        GetMoreInfo("International Bestseller"),
        GetMoreInfo("Unreliable Narrator")
    };

            var faultInStars = books.First(b => b.Title == "The Fault in Our Stars");
            faultInStars.MoreInfos = new List<MoreInfo>
    {
        GetMoreInfo("New York Times Bestseller"),
        GetMoreInfo("Major Motion Picture"),
        GetMoreInfo("International Bestseller"),
        GetMoreInfo("Cultural Phenomenon")
    };

            var hitchhikersGuide = books.First(b => b.Title == "The Hitchhiker's Guide to the Galaxy");
            hitchhikersGuide.MoreInfos = new List<MoreInfo>
    {
        GetMoreInfo("Cultural Phenomenon"),
        GetMoreInfo("Genre Defining Work"),
        GetMoreInfo("International Bestseller"),
        GetMoreInfo("First in Series")
    };

            var catch22 = books.First(b => b.Title == "The Catch-22");
            catch22.MoreInfos = new List<MoreInfo>
    {
        GetMoreInfo("Literary Classic"),
        GetMoreInfo("University Study Subject"),
        GetMoreInfo("Cultural Phenomenon")
    };

            var readyPlayerOne = books.First(b => b.Title == "Ready Player One");
            readyPlayerOne.MoreInfos = new List<MoreInfo>
    {
        GetMoreInfo("New York Times Bestseller"),
        GetMoreInfo("Major Motion Picture"),
        GetMoreInfo("Cultural Phenomenon"),
        GetMoreInfo("Debut Novel")
    };

            var nameOfWind = books.First(b => b.Title == "The Name of the Wind");
            nameOfWind.MoreInfos = new List<MoreInfo>
    {
        GetMoreInfo("International Bestseller"),
        GetMoreInfo("First in Series"),
        GetMoreInfo("Debut Novel"),
        GetMoreInfo("Epic Length")
    };

            var dune = books.First(b => b.Title == "Dune");
            dune.MoreInfos = new List<MoreInfo>
    {
        GetMoreInfo("Hugo Award Winner"),
        GetMoreInfo("Major Motion Picture"),
        GetMoreInfo("Genre Defining Work"),
        GetMoreInfo("First in Series")
    };

            var roadLessTraveled = books.First(b => b.Title == "The Road Less Traveled");
            roadLessTraveled.MoreInfos = new List<MoreInfo>
    {
        GetMoreInfo("New York Times Bestseller"),
        GetMoreInfo("International Bestseller"),
        GetMoreInfo("Over 50 Million Copies Sold")
    };

            var richDadPoorDad = books.First(b => b.Title == "Rich Dad Poor Dad");
            richDadPoorDad.MoreInfos = new List<MoreInfo>
    {
        GetMoreInfo("New York Times Bestseller"),
        GetMoreInfo("International Bestseller"),
        GetMoreInfo("Over 50 Million Copies Sold"),
        GetMoreInfo("Cultural Phenomenon")
    };

            var howToWinFriends = books.First(b => b.Title == "How to Win Friends and Influence People");
            howToWinFriends.MoreInfos = new List<MoreInfo>
    {
        GetMoreInfo("International Bestseller"),
        GetMoreInfo("Over 50 Million Copies Sold"),
        GetMoreInfo("Cultural Phenomenon")
    };

            var thinkingFastSlow = books.First(b => b.Title == "Thinking, Fast and Slow");
            thinkingFastSlow.MoreInfos = new List<MoreInfo>
    {
        GetMoreInfo("New York Times Bestseller"),
        GetMoreInfo("International Bestseller"),
        GetMoreInfo("Nobel Prize in Literature"),
        GetMoreInfo("University Study Subject")
    };

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
                    Pictures =new List<Picture>{ context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://th.bing.com/th/id/OIP.W1ngiF2AkQaO78CY9yC1HQHaLd?rs=1&pid=ImgDetMain") } ,
                    Id = Guid.Parse("b58bfc10-270a-4395-697d-08dd686bd604"),
                    Reviews = new List<Review>(){},
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
                       Pictures =new List<Picture>{ context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://th.bing.com/th/id/R.e5f82111504d28779262bf2dca0fad56?rik=9etdGLc9mstPyw&pid=ImgRaw&r=0") },
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
                       Pictures =new List<Picture>{ context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://hachette.imgix.net/books/9780762498130.jpg?auto=compress,format") },
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
                       Pictures =new List<Picture>{ context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://cdn2.penguin.com.au/covers/original/9780141330167.jpg") },
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
                       Pictures =new List<Picture>{ context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://th.bing.com/th/id/R.8947741487564891cf18c8b32752c2bf?rik=gsUGQLJSJMRUSQ&pid=ImgRaw&r=0") },
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
                       Pictures =new List<Picture>{ context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://res.cloudinary.com/bloomsbury-atlas/image/upload/w_568,c_scale/jackets/9781408855652.jpg") },
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
                       Pictures =new List<Picture>{ context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://cdn11.bigcommerce.com/s-gibnfyxosi/images/stencil/1920w/products/154740/156431/51eq24cRtRL__98083.1615576774.jpg?c=1") },
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
                       Pictures =new List<Picture>{ context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://th.bing.com/th/id/OIP.7vHm4DvQa3iwZAoiOFcZtwHaKr?rs=1&pid=ImgDetMain") },
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
                       Pictures =new List<Picture>{ context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://th.bing.com/th/id/R.9f4b5965d0bc99a4e4e0130b713285a0?rik=d5ac2urjW9GuEw&pid=ImgRaw&r=0") },

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
                       Pictures =new List<Picture>{ context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://th.bing.com/th/id/OIP.9-v0mfum2BK2IFqSLAXf3gHaL7?rs=1&pid=ImgDetMain") },
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
                      Pictures =new List<Picture>{ context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://th.bing.com/th/id/OIP.QdhjN4JxwcYPvlvKjwPKygHaLH?rs=1&pid=ImgDetMain") },

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
                       Pictures =new List<Picture>{ context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://th.bing.com/th/id/R.bd9481b3c05998fdbb7b1fc49ba1f3ef?rik=yUOPSX2aJliuAA&pid=ImgRaw&r=0") },
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
                      Pictures =new List<Picture>{ context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://th.bing.com/th/id/OIP.B4wjpTB6Gg1o4O72nV6A7AHaJ4?rs=1&pid=ImgDetMain") },

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
                       Pictures =new List<Picture>{ context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://th.bing.com/th/id/OIP._5vHs3lHPIM5CyLsVE-XUwHaMK?rs=1&pid=ImgDetMain") },

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
                      Pictures =new List<Picture>{ context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://th.bing.com/th/id/OIP.SwfiX3jY9A5_pwTQ2IaZigHaLH?rs=1&pid=ImgDetMain") },

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
                      Pictures =new List<Picture>{ context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://th.bing.com/th/id/OIP.ibj2AYzaoo9_OwOOTHHxYwHaLd?rs=1&pid=ImgDetMain") },

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
                       Pictures =new List<Picture>{ context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://th.bing.com/th/id/OIP.WTbE7_qIyqfSbkS2CNdPRAHaJl?rs=1&pid=ImgDetMain") },

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
                       Pictures =new List<Picture>{ context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://th.bing.com/th/id/R.8697cc74db5911afdc6e83680530401b?rik=1vDwtG3ci2QYLg&riu=http%3a%2f%2fdwcp78yw3i6ob.cloudfront.net%2fwp-content%2fuploads%2f2016%2f12%2f12162813%2f100_Years_FirstOrDefault_Ed_Hi_Res-768x1153.jpg&ehk=%2b6OxuEL8iXKiqX9LZgGO6Tac3AslNULuU8MjLqgFkYU%3d&risl=&pid=ImgRaw&r=0") },

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
                       Pictures =new List<Picture>{ context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://cdn2.penguin.com.au/covers/original/9781857150353.jpg") },

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
                       Pictures =new List<Picture>{ context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://th.bing.com/th/id/R.e1d1b936c903c72d27b818a5c69b93b9?rik=47BZwvJmMrUBPQ&riu=http%3a%2f%2fassets.allenandunwin.com.s3.amazonaws.com%2fimages%2foriginal%2f9780571337118.jpg&ehk=nRjkVhb9omqg9ee5rtw3mvERugf1a29dwSSdI7kV5RY%3d&risl=&pid=ImgRaw&r=0") },

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
                      Pictures =new List<Picture>{ context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://wordsworth-editions.com/wp-content/uploads/2017/07/9781853260155.jpg") },

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
                       Pictures =new List<Picture>{ context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://cdn.kobo.com/book-images/f93d05eb-4306-48be-926b-8914b36bc9c3/1200/1200/False/the-count-of-monte-cristo-51.jpg") },

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
                       Pictures =new List<Picture>{ context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://th.bing.com/th/id/OIP.MsbTboenjRO2EoLRDG_Q-wHaLM?rs=1&pid=ImgDetMain") },

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
                       Pictures =new List<Picture>{ context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://th.bing.com/th/id/OIP.h1ZkrWQ1gu-ZDl-4T1ymBQHaLp?rs=1&pid=ImgDetMain") },

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
                       Pictures =new List<Picture>{ context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://th.bing.com/th/id/OIP.Kn8v8LjGkvpgg8BanIjFnAHaLQ?rs=1&pid=ImgDetMain") },

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
                      Pictures =new List<Picture>{ context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://th.bing.com/th/id/OIP.Wdw8knp_mCAsj_Y4hZ4MQwHaLX?rs=1&pid=ImgDetMain") },

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
                      Pictures =new List<Picture>{ context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://th.bing.com/th/id/OIP.e__NEYvJpiqMPy4lAR_zHgHaLQ?rs=1&pid=ImgDetMain") },

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
                       Pictures =new List<Picture>{ context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://i5.walmartimages.com/asr/a6513e10-23f9-4f18-9100-554fddcaa750_1.edd1069e46c70d49a412c4ec82f6dd21.jpeg") },

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
                       Pictures =new List<Picture>{ context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://th.bing.com/th/id/OIP.h2v49-gy-RyOKhQ6ub2T4wAAAA?rs=1&pid=ImgDetMain") },

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
                      Pictures =new List<Picture>{ context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://imgv2-2-f.scribdassets.com/img/word_document/250024294/original/582f636f24/1587740851?v=1") },

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
                        Pictures =new List<Picture>{ context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://i.pinimg.com/originals/a3/71/56/a37156a4e8b8b1a403e18eb32ebdb97e.jpg") },

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
                        Pictures =new List<Picture>{ context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://th.bing.com/th/id/OIP.QXfpwLb6of2QlgHwviXxogHaLj?rs=1&pid=ImgDetMain") },

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
                        Pictures =new List<Picture>{ context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://th.bing.com/th/id/OIP.b0ixz-7oXf23i_S0xgXpGQHaLO?rs=1&pid=ImgDetMain") },

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
                        Pictures =new List<Picture>{ context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://th.bing.com/th/id/R.ef65387af1539c717421c16fdf69d4fc?rik=UGH8S1OlgvrkoQ&riu=http%3a%2f%2flitstack.com%2fwp-content%2fuploads%2f2013%2f09%2ffahrenheit-451-book-cover1.jpg&ehk=5jdfngLzYvSUA%2fChbZR5A7GmzIm6eCt72Z7QwkhYl9o%3d&risl=&pid=ImgRaw&r=0") },

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
                        Pictures =new List<Picture>{ context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://th.bing.com/th/id/OIP.Bot9b5KthqRuI80alqWmBgHaLb?rs=1&pid=ImgDetMain") },

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
                        Pictures =new List<Picture>{ context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://cdn.kobo.com/book-images/16327bfc-5399-4b3d-be6d-e65873cc4c30/1200/1200/False/the-giver-harpercollins-children-s-modern-classics.jpg") },

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
                        Pictures =new List<Picture>{ context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://th.bing.com/th/id/OIP.EQvB2pkuKdBk5ThLNRpV3AHaLQ?rs=1&pid=ImgDetMain") },

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
                        Pictures =new List<Picture>{ context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://th.bing.com/th/id/R.979b1d14087153c5e264fef39aee3a96?rik=ADYwTpdpf0DeqQ&riu=http%3a%2f%2fimages6.fanpop.com%2fimage%2fphotos%2f37400000%2fGone-Girl-by-Gillian-Flynn-gone-girl-37441442-1181-1810.jpg&ehk=rtg5qz7Eh5WgcMzc%2bCEMz7DThMzsLhAX39DPt%2fl9MtM%3d&risl=&pid=ImgRaw&r=0") },

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
                        Pictures =new List<Picture>{ context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://th.bing.com/th/id/R.e795ce4ab4b4c1fe088529296b6ee94d?rik=JBP7Sjfuhhqp1Q&pid=ImgRaw&r=0") },

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
                       Pictures =new List<Picture>{ context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://th.bing.com/th/id/OIP.6lE_KV_vkckc3Ey-TSvHuwHaLJ?rs=1&pid=ImgDetMain") },

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
                        Pictures =new List<Picture>{ context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://th.bing.com/th/id/OIP.8176UT2wXeN8km6wuq_imwAAAA?rs=1&pid=ImgDetMain") },

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
                        Pictures =new List<Picture>{ context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://th.bing.com/th/id/OIP.86Wh_7BdSgI8So5cKvXhgwHaLb?rs=1&pid=ImgDetMain") },

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
                        Pictures =new List<Picture>{ context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://i0.wp.com/www.attackofthebooks.com/wp-content/uploads/2017/04/The_Name_of_the_Wind_UK_cover.jpg?fit=1666%2C2560&ssl=1") },

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
                       Pictures =new List<Picture>{ context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://cdn11.bigcommerce.com/s-gibnfyxosi/images/stencil/2560w/products/130183/131921/41UZeCEKOBL__78384.1615569895.jpg?c=1") },

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
                       Pictures =new List<Picture>{ context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://th.bing.com/th/id/R.135b0150a36bd91cbd74324561b4725b?rik=Ac6gyPMcaqoDHg&pid=ImgRaw&r=0") },

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
                      Pictures =new List<Picture>{ context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://cdn.kobo.com/book-images/c81ea4de-cfb7-415d-8634-314aad041fdb/1200/1200/False/rich-dad-poor-dad-9.jpg") },

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
                       Pictures =new List<Picture>{ context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://d3fa68hw0m2vcc.cloudfront.net/f92/172976980.jpeg") },

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
                       Pictures =new List<Picture>{ context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://th.bing.com/th/id/OIP.c7sOT_zwzaWNafdGnO2PGwHaLx?rs=1&pid=ImgDetMain") },

                   }
               };
            await context.Books.AddRangeAsync(Books);
            await context.SaveChangesAsync();
        }

        if (!await context.BookStores.AnyAsync())
        {
            List<BookStore> bookStores = new()
            {
                new BookStore
                {
                    Name = "John Sandoe Books LTD",
                    Address = "10 Blacklands Terrace, London SW3 2SP, United Kingdom",
                    PhoneNumber = "+44 20 7589 9473",
                    Latitude= 51.4912989,
                    Longitude= -0.1611118,
                    Email = "Johnsandoe@booksltd.com",
                    Pictures = new List<Picture>{ context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://images.pexels.com/photos/626986/pexels-photo-626986.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2"),
                        context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://media-cdn.tripadvisor.com/media/photo-o/17/26/36/12/john-sandoe-books.jpg"),
                        context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://lp-cms-production.imgix.net/2021-12/GettyImages-1286769946.jpg?auto=format&q=40&w=870&dpr=4") },
                },

                new BookStore
                {
                    Name = "Little Book Origin",
                    Address = "Crosstown Julie Brown, 1314 Washington Street\r\nWatertown, New York 13601",
                    PhoneNumber = "(315) 740-8715",
                    Latitude= 44.9709,
                    Longitude= -75.9104,
                    Email = "minion@littlebookstoreclayton.com",
                    Pictures = new List<Picture>{context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://images.pexels.com/photos/3952078/pexels-photo-3952078.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2"),
                    context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://i0.wp.com/littlebookstores.gr/wp-content/uploads/2016/09/IMG_1827-1.jpg?fit=3564%2C1909&ssl=1"),
                    context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://drsw10gc90t0z.cloudfront.net/AcuCustom/Sitename/DAM/483/Housmans_Bookshop.jpg") },
                },

                new BookStore
                {
                    Name = "Tropismes",
                    Address = "Galerie des Princes 11, 1000 Brussels, Belgium",
                    PhoneNumber = "+32 2 512 88 52",
                    Email = "Info@Tropismes.com",
                    Latitude= 50.84817,
                    Longitude= 4.35484,
                    Pictures = new List<Picture>{context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://images.pexels.com/photos/3952095/pexels-photo-3952095.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2"),
                    context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://www.boncado.be/img/uploads/9830/5fca60ab9cf93.jpg"),
                    context.Pictures.FirstOrDefault(x => x.ImgUrl == "https://live.staticflickr.com/86/248172816_79cd2f2b9b_b.jpg") },
                }
            };
            await context.BookStores.AddRangeAsync(bookStores);
            await context.SaveChangesAsync();
        }
        ;


        // Check if there are any reviews in the database

        if (!await context.Reviews.AnyAsync())
        {
            Random random = new Random();
            var users = await context.Users.ToListAsync();
            var books = await context.Books.ToListAsync();

            List<Review> reviewsToSeed = new List<Review>();

            // Arrays of varied phrases for more personal reviews
            string[] fiveStarTitles = {
        "Absolutely unforgettable!",
        "Pure magic from cover to cover!",
        "Couldn't put it down – truly a masterpiece!",
        "Phenomenal! A new favorite.",
        "Captivating and brilliant!"
    };
            string[] fiveStarContents = {
        "I was completely absorbed by this book. The characters felt incredibly real, and the storyline was so rich and layered. It truly left an impression on me.",
        "From the first page, I knew this was something special. The author's writing style is simply beautiful, and the plot twists had me on the edge of my seat. A truly immersive experience.",
        "Every single element of this book was perfect – the pacing, the world-building, the emotional depth. It's rare to find a book that resonates this deeply. Absolutely loved it!",
        "This is why I read! '{book.Title}' delivered on every promise and then some. The narrative flowed effortlessly, and I found myself thinking about it long after I finished the last page.",
        "If you're looking for a book that will transport you, this is it. I felt every emotion alongside the characters, and the journey was just incredible. A truly exceptional read."
    };

            string[] fourStarTitles = {
        "Really enjoyed it!",
        "A strong, engaging read.",
        "Solid and compelling.",
        "Highly recommended!",
        "Definitely worth reading."
    };
            string[] fourStarContents = {
        "I thoroughly enjoyed '{book.Title}'. It had a compelling plot and well-developed characters. While not perfect, it was a very satisfying read that I'd recommend.",
        "This book was a great journey. I found myself thinking about it often even when I wasn't reading. There were a few minor things, but overall, a very strong effort.",
        "A very well-crafted story. The writing was sharp, and the narrative kept me hooked. It didn't quite reach the 'masterpiece' level for me, but it came close!",
        "I picked up '{book.Title}' on a whim and was pleasantly surprised. It delivered on its premise and kept me engaged throughout. A really good choice for anyone interested in this genre.",
        "Good story, good characters, good writing. It ticked all the boxes for me. It might not be groundbreaking, but it's a very enjoyable and worthwhile read."
    };

            string[] threeStarTitles = {
        "It was okay.",
        "Decent, but not amazing.",
        "Mixed feelings on this one.",
        "Worth a read, but temper expectations.",
        "Pretty average."
    };
            string[] threeStarContents = {
        "'{book.Title}' was... fine. It had some interesting ideas and moments, but it didn't quite grab me as much as I hoped. I probably won't revisit it.",
        "I finished it, which is always a good sign! However, some parts felt a bit slow, and I struggled to connect with certain characters. It's an alright read if you have nothing else.",
        "It had its ups and downs. There were elements I really liked, but then other parts felt a bit flat or predictable. Not bad, but not memorable either.",
        "I wanted to love this one, but it just fell a little short. The premise was intriguing, but the execution sometimes felt a bit uneven. It passed the time, though.",
        "Pretty middle-of-the-road for me. I didn't dislike it, but I also didn't feel particularly excited by it. If you're looking for something light and undemanding, it might work."
    };

            string[] twoStarTitles = {
        "Disappointing.",
        "Struggled to finish.",
        "Not for me, unfortunately.",
        "Couldn't get into it.",
        "A bit of a letdown."
    };
            string[] twoStarContents = {
        "I honestly found '{book.Title}' quite a struggle. The plot felt convoluted, and I couldn't invest in any of the characters. I kept hoping it would get better, but it didn't.",
        "This one just didn't click for me. The pacing felt off, and I found my attention wandering frequently. I really tried to like it, but it wasn't enjoyable.",
        "Unfortunately, '{book.Title}' was a real disappointment. The premise sounded promising, but the execution left a lot to be desired. I wouldn't recommend it.",
        "I rarely give low ratings, but this one was tough. The writing felt clunky, and the story just dragged on. I regret spending time on it, to be honest.",
        "It's rare that I consider not finishing a book, but '{book.Title}' pushed me close. I just couldn't find anything to genuinely enjoy or connect with."
    };

            string[] oneStarTitles = {
        "Awful. Just awful.",
        "Did not finish – a waste of time.",
        "Terrible and frustrating.",
        "Steer clear!",
        "Beyond disappointing."
    };
            string[] oneStarContents = {
        "I genuinely disliked '{book.Title}'. The writing was poor, the characters were unlikable, and the plot made no sense. A truly frustrating reading experience.",
        "I tried, I really did, but I couldn't get past the first few chapters of '{book.Title}'. It was boring, confusing, and frankly, a chore to read. Don't bother.",
        "An absolute mess. This book had no redeeming qualities for me. I felt like I was wasting my time with every page. Truly one of the worst I've read.",
        "I have no idea how this book got published. The plot was nonsensical, and the dialogue was atrocious. I couldn't find a single positive thing to say about '{book.Title}'.",
        "A truly painful read. Everything about '{book.Title}' felt forced and poorly executed. I wish I could get my time back after reading this."
    };


            foreach (var book in books)
            {
                // Still generating 1, 2, or 3 reviews per book for variety
                int numberOfReviewsForThisBook = random.Next(1, 4);

                for (int i = 0; i < numberOfReviewsForThisBook; i++)
                {
                    User randomUser = users[random.Next(users.Count - 1)];
                    int randomRating = random.Next(1, 6);

                    string reviewTitle = "";
                    string reviewContent = "";

                    // Select random phrases based on rating
                    switch (randomRating)
                    {
                        case 5:
                            reviewTitle = fiveStarTitles[random.Next(fiveStarTitles.Length)];
                            reviewContent = fiveStarContents[random.Next(fiveStarContents.Length)].Replace("{book.Title}", book.Title);
                            break;
                        case 4:
                            reviewTitle = fourStarTitles[random.Next(fourStarTitles.Length)];
                            reviewContent = fourStarContents[random.Next(fourStarContents.Length)].Replace("{book.Title}", book.Title);
                            break;
                        case 3:
                            reviewTitle = threeStarTitles[random.Next(threeStarTitles.Length)];
                            reviewContent = threeStarContents[random.Next(threeStarContents.Length)].Replace("{book.Title}", book.Title);
                            break;
                        case 2:
                            reviewTitle = twoStarTitles[random.Next(twoStarTitles.Length)];
                            reviewContent = twoStarContents[random.Next(twoStarContents.Length)].Replace("{book.Title}", book.Title);
                            break;
                        case 1:
                            reviewTitle = oneStarTitles[random.Next(oneStarTitles.Length)];
                            reviewContent = oneStarContents[random.Next(oneStarContents.Length)].Replace("{book.Title}", book.Title);
                            break;
                    }

                    reviewsToSeed.Add(new Review
                    {
                        BookId = book.Id,
                        UserId = randomUser.Id,
                        Title = reviewTitle,
                        Content = reviewContent,
                        Rating = randomRating
                    });
                }
            }
            await context.Reviews.AddRangeAsync(reviewsToSeed);
            await context.SaveChangesAsync();
        }
    }




}
