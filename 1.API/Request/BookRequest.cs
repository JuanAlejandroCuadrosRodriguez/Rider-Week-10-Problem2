using Microsoft.Build.Framework;

namespace _1.API.Request;

public class BookRequest
{
    [Required]
    public string Title { get; set; }
    [Required]
    public string Author { get; set; }
    [Required]
    public int Year { get; set; }
}