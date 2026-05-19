using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Contexts;
using Models;
using Models.Enums;

namespace DataAccessLayer.Repositories;

public class BorrowingRepository
{
    private readonly LibraryManagementContext _context;

    public BorrowingRepository()
    {
        _context = new LibraryManagementContext();
    }


    public Borrowing AddBorrowing(Borrowing borrowing)
    {
        _context.borrowings.Add(borrowing);

        _context.SaveChanges();

        return borrowing;
    }
    
    public List<Borrowing> GetAllBorrowings()
    {
        return _context.borrowings
            .Include(b => b.Member)
            .Include(b => b.BookCopy)
            .ThenInclude(c => c.Book)
            .ToList();
    }





    public Borrowing? GetBorrowingById(int borrowingId)
    {
        return _context.borrowings
            .Include(b => b.Member)
            .Include(b => b.BookCopy)
            .FirstOrDefault(
                b => b.BorrowingId == borrowingId);
    }

    
    public List<Borrowing> GetBorrowingsByMember(
        int memberId)
    {
        return _context.borrowings
            .Include(b => b.BookCopy)
            .ThenInclude(c => c.Book)
            .Where(b => b.MemberId == memberId)
            .ToList();
    }
    

    public List<Borrowing> GetActiveBorrowings(
        int memberId)
    {
        return _context.borrowings
            .Where(b =>
                b.MemberId == memberId &&
                b.Status ==
                BorrowingStatusEnum.Borrowed)
            .ToList();
    }



    public Borrowing? GetActiveBorrowingByBook(
        int memberId,
        int bookId)
    {
        return _context.borrowings
            .Include(b => b.BookCopy)
            .FirstOrDefault(b =>
                b.MemberId == memberId &&
                b.BookCopy.BookId == bookId &&
                b.Status ==
                BorrowingStatusEnum.Borrowed);
    }
}