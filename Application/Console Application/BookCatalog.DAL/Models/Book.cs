using BookCatalog.DataLayer;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BookCatalog.DAL
{
    public class Book : EntityBase
    {
        // Book properties
        // Giving the properties a display name and setting the required fields and
        // giving feedback to the user if the input is invalid
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

        [YearRange(1000, 9999)]
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
        [Range(0, 100000, ErrorMessage = "Price Must be between 0 and the 100000")]
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
            $"\n\tPublisher: {Publisher}" +
            $"\n\tAmount of Pages: {PageCount}" +
            $"\n\tPrice: {Price}" +
            $"\n\tAvailable: {IsAvailable}\n";
    }


    // validating if the year is later then the currentyear
    public class YearRangeAttribute : ValidationAttribute
    {
        private readonly int _minYear;
        private readonly int _maxYear;

        public YearRangeAttribute(int minYear, int maxYear)
        {
            _minYear = minYear;
            _maxYear = maxYear;
            ErrorMessage = $"Year must be between {_minYear} and {_maxYear}.";
        }

        public override bool IsValid(object value)
        {
            if (value is DateTimeOffset date)
            {
                int year = date.Year;
                return year >= _minYear && year <= _maxYear;
            }

            return false;
        }
    }
}
