namespace Models;

public class Member
{
    public int MemberId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime JoinedDate { get; set; }

    // Foreign Key
    public int MembershipTypeId { get; set; }

    // Navigation Property
    public MembershipType MembershipType { get; set; } = null!;

    public List<Borrowing> Borrowings { get; set; } = new List<Borrowing>();
}