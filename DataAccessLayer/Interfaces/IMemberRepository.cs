namespace DataAccessLayer.Interfaces;
using Models;
public interface IMemberRepository
{
    public Member AddMember(Member member);

    public List<Member>? GetAllMembers();

    public Member? GetMemberByEmail(string email);

    public Member? GetMemberByPhone(string phone);

    public Member UpdateMember(Member member);
    
    public Member? UpdateMembershipStatus(int memberId, int membershipTypeId);

    public Member? DeactivateMember(int memberId);

}