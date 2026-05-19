using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Contexts;
using Models;
    
    
public class MemberRepository : IMemberRepository
{
    private LibraryManagementContext _context;

    public MemberRepository()
    {
        _context = new LibraryManagementContext();
    }

    public Member AddMember(Member member)
    {
        _context.members.Add(member);

        _context.SaveChanges();
        return member;
    }

    public List<Member>? GetAllMembers()
    {
        return _context.members
            .Include(m => m.MembershipType)
            .ToList();
    }

    public Member? GetMemberByEmail(string email)
    {
        return _context.members
            .Include(m => m.MembershipType)
            .FirstOrDefault(m => m.Email == email);
    }

    public  Member? GetMemberByPhone(string Phone)
    {
        return _context.members
            .Include(m => m.MembershipType)
            .FirstOrDefault(m => m.Phone == Phone);
    }
    
    public Member?  GetMemberById(int memberId)
    {
        var member = _context.members
            .Include(m => m.MembershipType)
            .FirstOrDefault(m => m.MemberId == memberId);
        if (member == null)
        {
            return null;
        }

        return member;
    }

    public Member? UpdateMember(Member member)
    {
        _context.members.Update(member);
        _context.SaveChanges();
        return  member;
    }

    public Member? UpdateMembershipStatus(int memberId, int membershipTypeId)
    {
        var user = _context.members.FirstOrDefault(m => m.MemberId == memberId);
        user.MembershipTypeId = membershipTypeId;
        _context.SaveChanges();
        return user;
    }

    public Member? DeactivateMember(int memberId)
    {
        var member = _context.members
            .FirstOrDefault(m => m.MemberId == memberId);

        member.IsActive = false;

        _context.SaveChanges();

        return member;
    }
}