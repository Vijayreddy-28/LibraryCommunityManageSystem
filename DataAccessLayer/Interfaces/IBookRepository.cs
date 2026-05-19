namespace DataAccessLayer.Interfaces;

using Models;
using Models.Enums;


public interface IBookRepository
{
    public Book AddBook(Book book);

    public BookCopy AddBookCopy(BookCopy copy);

    public List<Book> GetAllBooks();

    public List<BookCopy> GetAvailableBooks();

    public List<Book> GetBooksByTitle(string title);

    public List<Book> GetBooksByAuthor(string author);

    public List<Book> GetBooksByCategory(int categoryId);

    public BookCopy? UpdateBookCopyStatus(int copyId, BookCopyStatusEnum status);
}