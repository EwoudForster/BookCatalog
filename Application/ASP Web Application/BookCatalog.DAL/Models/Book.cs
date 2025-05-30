using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Display(Name = "Author")]
        [MaxLength(5, ErrorMessage = "No more than 5 authors")]
        public virtual List<Author>? Authors { get; set; }

        [Required(ErrorMessage = "Please fill in the Publication year")]
        [Display(Name = "Year of Publication")]

        [YearRangeNow(1000)]
        public int PublicationYear { get; set; }

        [Display(Name = "Genre")]

        [MaxLength(5, ErrorMessage = "No more than 5 Genres")]
        public virtual List<Genre>? Genres { get; set; }

        [Required(ErrorMessage = "Please fill in the ISBN number")]
        [Display(Name = "ISBN")]
        [StringLength(50, ErrorMessage = "No longer than 50 characters")]

        public string ISBN { get; set; }


        [Display(Name = "Publisher")]
        [ForeignKey("PublisherId")]
        public virtual Publisher? Publisher { get; set; }
        public Guid PublisherId { get; set; }


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
    }

    
    // validating if the year is later then the currentyear
    public class YearRangeNowAttribute : ValidationAttribute
    {
        private readonly int _minYear;
        private readonly int _maxYear;

        public YearRangeNowAttribute(int minYear)
        {
            _minYear = minYear;
            _maxYear = DateTimeOffset.Now.Year;
            ErrorMessage = $"Year must be between {_minYear} and {_maxYear}.";
        }

        public override bool IsValid(object value)
        {
            if (value is int date)
            {
                return date >= _minYear && date <= _maxYear;
            }

            return false;
        }
    }
}
