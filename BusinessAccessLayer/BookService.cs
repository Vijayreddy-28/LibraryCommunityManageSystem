
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using Models;
using Models.Enums;

namespace BusinessLayer.Services;

public class BookService 
{
    private readonly IBookRepository _bookRepository;

    public BookService()
    {
        _bookRepository = new BookRepository();
    }

    public Book AddBook(Book book)
    {
        var existingBooks =
            _bookRepository.GetBooksByTitle(book.Title);
        
        return _bookRepository.AddBook(book);
    }

    public BookCopy AddBookCopy(BookCopy copy)
    {
        var books = _bookRepository.GetAllBooks();

        var existingBook =
            books.FirstOrDefault(b => b.BookId == copy.BookId);

        if (existingBook == null)
        {
            throw new Exception("Book does not exist");
        }

        return _bookRepository.AddBookCopy(copy);
    }

    public List<Book> GetAllBooks()
    {
        return _bookRepository.GetAllBooks();
    }

    public List<BookCopy> GetAvailableBooks()
    {
        return _bookRepository.GetAvailableBooks();
    }

    public List<Book> GetBooksByTitle(string title)
    {
        return _bookRepository.GetBooksByTitle(title);
    }

    public List<Book> GetBooksByAuthor(string author)
    {
        return _bookRepository.GetBooksByAuthor(author);
    }

    public List<Book> GetBooksByCategory(int categoryId)
    {
        return _bookRepository.GetBooksByCategory(categoryId);
    }

    public BookCopy UpdateBookCopyStatus(int copyId, BookCopyStatusEnum status)
    {
        var copy =
            _bookRepository.UpdateBookCopyStatus(copyId, status);

        if (copy == null)
        {
            throw new Exception("Book copy not found");
        }

        return copy;
    }
}