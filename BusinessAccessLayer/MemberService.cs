
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using Models;

namespace BusinessLayer.Services;

public class MemberService 
{
    private IMemberRepository _memberRepository;

    public MemberService()
    {
        _memberRepository = new MemberRepository();
    }

    public Member AddMember(Member member)
    {
        var existingEmail =
            _memberRepository.GetMemberByEmail(member.Email);

        if (existingEmail != null)
        {
            throw new Exception("Email already exists");
        }

        var existingPhone =
            _memberRepository.GetMemberByPhone(member.Phone);

        if (existingPhone != null)
        {
            throw new Exception("Phone number already exists");
        }

        return _memberRepository.AddMember(member);
    }

    public List<Member> GetAllMembers()
    {
        return _memberRepository.GetAllMembers();
    }

    public Member? GetMemberByEmail(string email)
    {
        return _memberRepository.GetMemberByEmail(email);
    }

    public Member? GetMemberByPhone(string phone)
    {
        return _memberRepository.GetMemberByPhone(phone);
    }

    public Member UpdateMembershipStatus(int memberId, int membershipTypeId)
    {
        var member =
            _memberRepository.UpdateMembershipStatus(memberId, membershipTypeId);

        if (member == null)
        {
            throw new Exception("Member not found");
        }

        return member;
    }

    public Member DeactivateMember(int memberId)
    {
        var member =
            _memberRepository.DeactivateMember(memberId);

        if (member == null)
        {
            throw new Exception("Member not found");
        }

        return member;
    }
}