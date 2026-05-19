using DataAccessLayer.Contexts;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using Models;
using Models.Enums;
using Microsoft.EntityFrameworkCore;
namespace BusinessLayer.Services;

public class BorrowingService
{
    
    private readonly BorrowingRepository
        _borrowingRepository;

    private readonly MemberRepository
        _memberRepository;

    private readonly BookRepository
        _bookRepository;

    private readonly FineRepository
        _fineRepository;

    public BorrowingService()
    {   
        
        _borrowingRepository =
            new BorrowingRepository();

        _memberRepository =
            new MemberRepository();

        _bookRepository =
            new BookRepository();

        _fineRepository =
            new FineRepository();
    }

    public void BorrowBook(
        int memberId,
        int bookId)
    {

        var member =
            _memberRepository
                .GetMemberById(memberId);

        if(member == null)
        {
            throw new Exception(
                "Member not found");
        }

        if(member.IsActive == false)
        {
            throw new Exception(
                "Membership inactive");
        }

        int borrowingLimit =
            member.MembershipType
                .MaxBooksAllowed;

        int activeBorrowings =
            _borrowingRepository
                .GetActiveBorrowings(memberId)
                .Count;

        if(activeBorrowings >= borrowingLimit)
        {
            throw new Exception(
                "Borrow limit reached");
        }

        

        decimal totalFine =
            _fineRepository
                .GetPendingFineAmount(memberId);

        if(totalFine > 500)
        {
            throw new Exception(
                "Unpaid fine above ₹500");
        }



        

        var availableCopy =
            _bookRepository
                .GetAvailableCopy(bookId);

        if(availableCopy == null)
        {
            throw new Exception(
                "No copies available");
        }
        

        var existingBorrow =
            _borrowingRepository
                .GetActiveBorrowingByBook(
                    memberId,
                    bookId);

        if(existingBorrow != null)
        {
            throw new Exception(
                "Book already borrowed");
        }


        

        Borrowing borrowing =
            new Borrowing();

        borrowing.MemberId = memberId;

        borrowing.BookCopyId =
            availableCopy.BookCopyId;

        borrowing.BorrowDate =
            DateTime.UtcNow;

        borrowing.DueDate =
            DateTime.UtcNow.AddDays(
                member.MembershipType
                    .MaxBorrowDays);

        borrowing.Status =
            BorrowingStatusEnum.Borrowed;



        _borrowingRepository
            .AddBorrowing(borrowing);

        availableCopy.Status =
            BookCopyStatusEnum.Borrowed;

        _bookRepository
            .UpdateBookCopy(availableCopy);

        Console.WriteLine(
            "Book borrowed successfully");
    }
}