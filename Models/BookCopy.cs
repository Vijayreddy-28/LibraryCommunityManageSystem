namespace Models;
using Models.Enums;

public class BookCopy
{
    public int BookCopyId { get; set; }

    public string CopyCode { get; set; } = string.Empty;

    public BookCopyStatusEnum Status { get; set; }

    // Foreign Key
    public int BookId { get; set; }

    // Navigation Property
    public Book Book { get; set; } = null!;

    public List<Borrowing> Borrowings { get; set; } = new List<Borrowing>();
}