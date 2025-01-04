using System.ComponentModel.DataAnnotations;
namespace MvcMovie.Models;

public class Movie
{
    public int Id { get; set; }
    public string? Title { get; set; } // The question mark (?) is a nullable annotation. It's used to indicate that the Title property can be null.
    [DataType(DataType.Date)] // The DataType attribute specifies the type of the data (Date). With this attribute, the user is able to pick a date from a calendar, but no specific time annotations... later see in DateTimeAnnotations.
    public DateTime ReleaseDate { get; set; }
    public string? Genre { get; set; }
    public decimal Price { get; set; }
}
