using DataAccessLayer.Repositories;
using Models.Enums;

namespace BusinessLayer.Services;

public class ReportService
{
    private readonly BorrowingRepository
        _borrowingRepository;

    private readonly FineRepository
        _fineRepository;

    private readonly BookRepository
        _bookRepository;

    public ReportService()
    {
        _borrowingRepository =
            new BorrowingRepository();

        _fineRepository =
            new FineRepository();

        _bookRepository =
            new BookRepository();
    }
    

    public void GetCurrentlyBorrowedBooks()
    {
        var books =
            _borrowingRepository
                .GetAllBorrowings()
                .Where(b =>
                    b.Status ==
                    BorrowingStatusEnum.Borrowed)
                .ToList();

        foreach (var borrowing in books)
        {
            Console.WriteLine("\n----------------");

            Console.WriteLine(
                $"Borrowing Id : {borrowing.BorrowingId}");

            Console.WriteLine(
                $"Member : {borrowing.Member.Name}");

            Console.WriteLine(
                $"Book : {borrowing.BookCopy.Book.Title}");

            Console.WriteLine(
                $"Borrow Date : {borrowing.BorrowDate}");

            Console.WriteLine(
                $"Due Date : {borrowing.DueDate}");
        }
    }
    

    public void GetOverdueBooks()
    {
        var overdueBooks =
            _borrowingRepository
                .GetAllBorrowings()
                .Where(b =>
                    b.Status ==
                    BorrowingStatusEnum.Borrowed &&
                    b.DueDate.Date <
                    DateTime.UtcNow.Date)
                .ToList();

        foreach (var borrowing in overdueBooks)
        {
            Console.WriteLine("\n----------------");

            Console.WriteLine(
                $"Member : {borrowing.Member.Name}");

            Console.WriteLine(
                $"Book : {borrowing.BookCopy.Book.Title}");

            Console.WriteLine(
                $"Due Date : {borrowing.DueDate}");
        }
    }
    

    public void GetMembersWithPendingFines()
    {
        var fines =
            _fineRepository
                .GetAllFines()
                .Where(f => f.IsPaid == false)
                .ToList();

        foreach (var fine in fines)
        {
            Console.WriteLine("\n----------------");

            Console.WriteLine(
                $"Member : {fine.Borrowing.Member.Name}");

            Console.WriteLine(
                $"Amount : ₹{fine.Amount}");
        }
    }
    

    public void GetMostBorrowedBooks()
    {
        var books =
            _borrowingRepository
                .GetAllBorrowings()
                .GroupBy(b =>
                    b.BookCopy.Book.Title)
                .Select(g => new
                {
                    Title = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(g => g.Count)
                .ToList();

        foreach (var book in books)
        {
            Console.WriteLine(
                $"{book.Title} - Borrowed {book.Count} times");
        }
    }
    

    public void GetAvailableBooksByCategory(
        int categoryId)
    {
        var books =
            _bookRepository
                .GetAvailableBooks()
                .Where(c =>
                    c.Book.BookCategoryId ==
                    categoryId)
                .ToList();

        foreach (var copy in books)
        {
            Console.WriteLine("\n----------------");

            Console.WriteLine(
                $"Title : {copy.Book.Title}");

            Console.WriteLine(
                $"Author : {copy.Book.Author}");

            Console.WriteLine(
                $"Copy Code : {copy.CopyCode}");
        }
    }

    

    public void GetMemberBorrowingHistory(
        int memberId)
    {
        var borrowings =
            _borrowingRepository
                .GetBorrowingsByMember(memberId);

        foreach (var borrowing in borrowings)
        {
            Console.WriteLine("\n----------------");

            Console.WriteLine(
                $"Book : {borrowing.BookCopy.Book.Title}");

            Console.WriteLine(
                $"Borrow Date : {borrowing.BorrowDate}");

            Console.WriteLine(
                $"Return Date : {borrowing.ReturnDate}");

            Console.WriteLine(
                $"Status : {borrowing.Status}");
        }
    }
}