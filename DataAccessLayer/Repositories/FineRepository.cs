namespace DataAccessLayer.Repositories;

using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Contexts;
using DataAccessLayer.Interfaces;
using Models;


public class FineRepository : IFineRepository
{
    private readonly LibraryManagementContext _context;

    public FineRepository()
    {
        _context = new LibraryManagementContext();
    }

    public Fine AddFine(Fine fine)
    {
        _context.fines.Add(fine);

        _context.SaveChanges();

        return fine;
    }

    public Fine? GetFineById(int fineId)
    {
        return _context.fines
            .Include(f => f.Borrowing)
            .FirstOrDefault(f => f.FineId == fineId);
    }

    public List<Fine> GetPendingFinesByMember(int memberId)
    {
        return _context.fines
            .Include(f => f.Borrowing)
            .Where(f =>
                f.Borrowing.MemberId == memberId &&
                f.IsPaid == false)
            .ToList();
    }

    public decimal GetPendingFineAmount(int memberId)
    {
        return _context.fines
            .Include(f => f.Borrowing)
            .Where(f =>
                f.Borrowing.MemberId == memberId &&
                f.IsPaid == false)
            .Sum(f => f.Amount);
    }
    public List<Fine> GetAllFines()
    {
        return _context.fines
            .Include(f => f.Borrowing)
            .ThenInclude(b => b.Member)
            .ToList();
    }
    public Fine UpdateFine(Fine fine)
    {
        _context.fines.Update(fine);

        _context.SaveChanges();

        return fine;
    }

    public FinePayment AddFinePayment(FinePayment payment)
    {
        _context.finePayments.Add(payment);

        _context.SaveChanges();

        return payment;
    }

    public List<FinePayment> GetFinePaymentHistory(int fineId)
    {
        return _context.finePayments
            .Where(p => p.FineId == fineId)
            .ToList();
    }
}