using DataAccessLayer.Contexts;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using Models.Enums;

namespace BusinessLayer.Services;

public class ReturnBorrowingService
{
    private readonly LibraryManagementContext _context;
    private readonly BorrowingRepository _borrowingRepository;

    public ReturnBorrowingService()
    {
        _context = new LibraryManagementContext();
        _borrowingRepository = new BorrowingRepository();
    }

    public void ReturnBook(int borrowingId)
    {
        var borrowing =
            _borrowingRepository.GetBorrowingById(borrowingId);

        if (borrowing == null)
            throw new Exception("Borrowing not found");

        if (borrowing.Status == BorrowingStatusEnum.Returned)
            throw new Exception("Book already returned");

        DateTime returnDate = DateTime.UtcNow;

        int delayedDays =
            (returnDate.Date - borrowing.DueDate.Date).Days;

        decimal fineAmount = delayedDays > 0 ? delayedDays * 10 : 0;

        using var transaction = _context.Database.BeginTransaction();

        try
        {
            // ✅ 1. Update borrowing record
            _context.Database.ExecuteSqlInterpolated(
                $"CALL return_book({borrowingId}, {returnDate}, {"Returned"})"
            );

            // ✅ 2. Update book copy status
            _context.Database.ExecuteSqlInterpolated(
                $"CALL update_book_copy_status({borrowing.BookCopyId}, {"Available"})"
            );

            // ✅ 3. Add fine if applicable
            if (fineAmount > 0)
            {
                _context.Database.ExecuteSqlInterpolated(
                    $"CALL add_fine({borrowingId}, {fineAmount})"
                );
            }

            transaction.Commit();

            Console.WriteLine("Book returned successfully");

            if (fineAmount > 0)
            {
                Console.WriteLine($"Fine Generated : ₹{fineAmount}");
            }
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            Console.WriteLine(ex.Message);
        }
    }
}