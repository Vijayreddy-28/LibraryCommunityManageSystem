namespace Models;

public class BookCategory
{
    public int BookCategoryId { get; set; }

    public string CategoryName { get; set; } = string.Empty;

    public List<Book> Books { get; set; } = new List<Book>();
}