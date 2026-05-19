namespace Models;
using Models.Enums;

public class Borrowing
{
    public int BorrowingId { get; set; }

    public DateTime BorrowDate { get; set; }

    public DateTime DueDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public BorrowingStatusEnum Status { get; set; }

    // Foreign Keys
    public int MemberId { get; set; }

    public int BookCopyId { get; set; }

    // Navigation Properties
    public Member Member { get; set; } = null!;

    public BookCopy BookCopy { get; set; } = null!;

    public Fine? Fine { get; set; }
}