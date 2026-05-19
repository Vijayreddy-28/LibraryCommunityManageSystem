namespace Models;


public class FinePayment
{
    public int FinePaymentId { get; set; }

    public decimal PaidAmount { get; set; }

    public DateTime PaymentDate { get; set; }

    public string PaymentMethod { get; set; } = string.Empty;

    // Foreign Key
    public int FineId { get; set; }

    // Navigation Property
    public Fine Fine { get; set; } = null!;
}