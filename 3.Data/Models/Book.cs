namespace _3.Data.Models;

public class Book : BaseModel
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }
}