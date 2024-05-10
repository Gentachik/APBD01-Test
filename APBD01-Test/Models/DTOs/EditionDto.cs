using System.ComponentModel.DataAnnotations;

namespace APBD01_Test.Models.DTOs;

public class EditionDto
{
    [Required]
    public int Id { get; set; }
    [Required]
    [MinLength(1)]
    public string BookTitle { get; set; }
    [Required]
    [MinLength(1)]
    public string EditionTitle { get; set; }
    [Required]
    public string PublishingHouseName { get; set; }
    [Required]
    public DateTime ReleaseDate { get; set; }
}

public class BookDto
{
    [Required]
    [MinLength(1)]
    public string BookTitle { get; set; }
    [Required]
    [MinLength(1)]
    public string EditionTitle { get; set; }
    [Required]
    public int publishingHouseId { get; set; }
    [Required]
    public DateTime releaseDate { get; set; }
}
