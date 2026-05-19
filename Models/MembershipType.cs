namespace Models;

public class MembershipType
{
    public int MembershipTypeId { get; set; }

    public string Name { get; set; } = string.Empty;

    public int MaxBooksAllowed { get; set; }

    public int MaxBorrowDays { get; set; }

    // Navigation Property
    public List<Member> Members { get; set; } = new List<Member>();
}