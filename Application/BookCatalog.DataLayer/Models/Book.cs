using BookCatalog.DataLayer;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BookCatalog.DataLayer
{
    public class Book : EntityBase
    {
        // Book properties
        [Required(ErrorMessage = "Please fill in the title")]
        [Display(Name = "Title")]
        [StringLength(50, ErrorMessage = "No longer than 50 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please fill in the Author")]
        [Display(Name = "Author")]
        [StringLength(50, ErrorMessage = "No longer than 50 characters")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Please fill in the Publication year")]
        [Display(Name = "Year of Publication")]
        
        [Range(1000, 2025, ErrorMessage = "Year Must be between 1000 and the current year")] 
        public int PublicationYear { get; set; }
        [Required(ErrorMessage = "Please fill in the Genre")]
        [Display(Name = "Genre")]

        [StringLength(50, ErrorMessage = "No longer than 50 characters")]
        public string Genre { get; set; }

        [Required(ErrorMessage = "Please fill in the ISBN number")]
        [Display(Name = "ISBN")]
        [StringLength(50, ErrorMessage = "No longer than 50 characters")]

        public string ISBN { get; set; }


        [Required(ErrorMessage = "Please fill in the Publisher")]
        [Display(Name = "Publisher")]
        [StringLength(50, ErrorMessage = "No longer than 50 characters")]
        public string Publisher { get; set; }


        [Required(ErrorMessage = "Please fill in the Page Count")]
        [Display(Name = "Page Count")]
        public int PageCount { get; set; }

        [Required(ErrorMessage = "Please fill in the Price")]
        [Display(Name = "Price")]
        [Precision(18, 2)]
        [Range(0, 100000, ErrorMessage = "Price Must be between 0 and the 10000")]
        public decimal Price { get; set; }

        [Display(Name = "Is the book available")]
        public bool IsAvailable { get; set; }

        [Display(Name = "Image URL")]
        public string? ImgUrl { get; set; }

        // Book ToString method
        public override string ToString() => $"{base.ToString()}" +
            $"\n\tTitle: {Title}" +
            $"\n\tAuthor: {Author}" +
            $"\n\tPublication Year: {PublicationYear}" +
            $"\n\tGenre: {Genre}" +
            $"\n\tISBN: {ISBN}" +
            $"\n\tPublisher: {Publisher}"+
            $"\n\tAmount of Pages: {PageCount}"+
            $"\n\tPrice: {Price}"+
            $"\n\tAvailable: {IsAvailable}\n";
    }
}
