namespace Models;


public class Fine
{
    public int FineId { get; set; }

    public decimal Amount { get; set; }

    public bool IsPaid { get; set; } = false;

    public DateTime CreatedDate { get; set; }

    // Foreign Key
    public int BorrowingId { get; set; }

    // Navigation Property
    public Borrowing Borrowing { get; set; } = null!;

    public List<FinePayment> FinePayments { get; set; } = new List<FinePayment>();
}