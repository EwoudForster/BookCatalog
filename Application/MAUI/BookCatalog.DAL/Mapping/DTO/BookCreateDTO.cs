using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookCatalog.DAL.DTO;

public class BookCreateDTO
{

    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    // Book properties
    // Giving the properties a display name and setting the required fields and
    // giving feedback to the user if the input is invalid
    [Required(ErrorMessage = "Please fill in the title")]
    [Display(Name = "Title")]
    [StringLength(50, ErrorMessage = "No longer than 50 characters")]
    public string Title { get; set; }

    [Display(Name = "Author")]
    [Required(ErrorMessage = "Please Select at least 1 Author")]
    [MaxLength(5, ErrorMessage = "No more than 5 authors")]
    public virtual List<Guid> AuthorIds { get; set; }
    [Required(ErrorMessage = "Please select at least 1 Picture")]
    [Display(Name = "Picture")]
    public virtual List<Guid> PictureIds { get; set; }

    [Required(ErrorMessage = "Please fill in the Publication year")]
    [Display(Name = "Year of Publication")]

    [YearRangeNow(1000)]
    public int PublicationYear { get; set; }

    [Display(Name = "Genre")]
    [Required(ErrorMessage = "Please Select at least 1 Genre")]

    [MaxLength(5, ErrorMessage = "No more than 5 Genres")]
    public virtual List<Guid> GenreIds { get; set; }

    [Required(ErrorMessage = "Please fill in the ISBN number")]
    [Display(Name = "ISBN")]
    [StringLength(50, ErrorMessage = "No longer than 50 characters")]
    public string ISBN { get; set; }

    [Required(ErrorMessage = "Please select at least 1 Publisher")]
    [Display(Name = "Publisher")]
    [ForeignKey("PublisherId")]
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

    [Display(Name = "Description")]
    public string? Description { get; set; }


    [Display(Name = "Image URL")]
    public List<Guid>? ImgUrlIds { get; set; }

    [Display(Name = "More Infos")]
    public List<Guid>? MoreInfoIds { get; set; }
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
