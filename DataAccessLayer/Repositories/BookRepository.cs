using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Contexts;
using DataAccessLayer.Interfaces;
using Models;
using Models.Enums;

namespace DataAccessLayer.Repositories;

public class BookRepository : IBookRepository
{
    private LibraryManagementContext _context;

    public BookRepository()
    {
        _context = new LibraryManagementContext();
    }

    public Book AddBook(Book book)
    {
        _context.books.Add(book);

        _context.SaveChanges();

        return book;
    }

    public BookCopy AddBookCopy(BookCopy copy)
    {
        _context.bookCopies.Add(copy);

        _context.SaveChanges();

        return copy;
    }
    
    public BookCopy? GetAvailableCopy(int bookId)
    {
        return _context.bookCopies
            .FirstOrDefault(c =>
                c.BookId == bookId &&
                c.Status ==
                BookCopyStatusEnum.Available);
    }



    public BookCopy UpdateBookCopy(BookCopy copy)
    {
        _context.bookCopies.Update(copy);

        _context.SaveChanges();

        return copy;
    }

    public List<Book> GetAllBooks()
    {
        return _context.books
            .Include(b => b.BookCategory)
            .Include(b => b.BookCopies)
            .ToList();
    }

    public List<BookCopy> GetAvailableBooks()
    {
        return _context.bookCopies
            .Include(c => c.Book)
            .Where(c => c.Status == BookCopyStatusEnum.Available)
            .ToList();
    }

    public List<Book> GetBooksByTitle(string title)
    {
        return _context.books
            .Include(b => b.BookCategory)
            .Where(b => b.Title.ToLower().Contains(title.ToLower()))
            .ToList();
    }

    public List<Book> GetBooksByAuthor(string author)
    {
        return _context.books
            .Include(b => b.BookCategory)
            .Where(b => b.Author.ToLower().Contains(author.ToLower()))
            .ToList();
    }

    public List<Book> GetBooksByCategory(int categoryId)
    {
        return _context.books
            .Include(b => b.BookCategory)
            .Where(b => b.BookCategoryId == categoryId)
            .ToList();
    }

    public BookCopy? UpdateBookCopyStatus(int copyId, BookCopyStatusEnum status)
    {
        var copy = _context.bookCopies
            .Include(c => c.Book)
            .FirstOrDefault(c => c.BookCopyId == copyId);

        if (copy == null)
        {
            return null;
        }

        copy.Status = status;

        _context.SaveChanges();

        return copy;
    }
}