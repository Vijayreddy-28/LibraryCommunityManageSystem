using BusinessLayer.Services;
using Models;
using Models.Enums;

namespace PresentationLayer;

class Program
{
    static void Main(string[] args)
    {
        MemberService memberService = new MemberService();

        BookService bookService = new BookService();
        FineService fineService = new FineService();

        bool isRunning = true;

        while (isRunning)
        {
            Console.WriteLine("\n===== Library Management System =====");

            Console.WriteLine("1. Member Management");

            Console.WriteLine("2. Book Management");

            Console.WriteLine("3. Borrow Book");

            Console.WriteLine("4. Return Book");

            Console.WriteLine("5. Fine Management");

            Console.WriteLine("6. Reports");

            Console.WriteLine("7. Exit");

            Console.Write("\nEnter Choice : ");

            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {

            

                case 1:

                    Console.WriteLine("\n--- Member Management ---");

                    Console.WriteLine("1.Add a Member");

                    Console.WriteLine("2.View All Members");

                    Console.WriteLine("3.Search Member by Phone");

                    Console.WriteLine("4.Search Member by Email");

                    Console.WriteLine("5.Update Membership Status");

                    Console.WriteLine("6.Deactivate a Member");

                    Console.Write("\nEnter Choice : ");

                    int memberChoice =
                        Convert.ToInt32(Console.ReadLine());

                    switch (memberChoice)
                    {

                        case 1:

                            try
                            {
                                Member member = new Member();

                                Console.Write("Enter Full Name : ");

                                member.Name =
                                    Console.ReadLine();

                                Console.Write("Enter Email : ");

                                member.Email =
                                    Console.ReadLine();

                                Console.Write("Enter Phone : ");

                                member.Phone =
                                    Console.ReadLine();

                                Console.Write("Enter Membership Type Id : ");

                                member.MembershipTypeId =
                                    Convert.ToInt32(Console.ReadLine());

                                member.IsActive = true;

                                member.JoinedDate = DateTime.UtcNow;

                                var addedMember =
                                    memberService.AddMember(member);

                                Console.WriteLine("\nMember Added Successfully");

                                Console.WriteLine($"Member Id : {addedMember.MemberId}");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);

                                if (ex.InnerException != null)
                                {
                                    Console.WriteLine(ex.InnerException.Message);
                                }
                            }

                            break;



                        case 2:

                            var members =
                                memberService.GetAllMembers();

                            foreach (var member in members)
                            {
                                Console.WriteLine("\n------------------------");

                                Console.WriteLine($"Id : {member.MemberId}");

                                Console.WriteLine($"Name : {member.Name}");

                                Console.WriteLine($"Email : {member.Email}");

                                Console.WriteLine($"Phone : {member.Phone}");

                                Console.WriteLine($"Membership : {member.MembershipType.Name}");

                                Console.WriteLine($"Active : {member.IsActive}");
                            }

                            break;



                        case 3:

                            Console.Write("Enter Phone : ");

                            string phone =
                                Console.ReadLine();

                            var memberByPhone =
                                memberService.GetMemberByPhone(phone);

                            if (memberByPhone == null)
                            {
                                Console.WriteLine("Member Not Found");
                            }
                            else
                            {
                                Console.WriteLine($"Name : {memberByPhone.Name}");

                                Console.WriteLine($"Email : {memberByPhone.Email}");

                                Console.WriteLine($"Membership : {memberByPhone.MembershipType.Name}");
                            }

                            break;



                        case 4:

                            Console.Write("Enter Email : ");

                            string email =
                                Console.ReadLine();

                            var memberByEmail =
                                memberService.GetMemberByEmail(email);

                            if (memberByEmail == null)
                            {
                                Console.WriteLine("Member Not Found");
                            }
                            else
                            {
                                Console.WriteLine($"Name : {memberByEmail.Name}");

                                Console.WriteLine($"Phone : {memberByEmail.Phone}");

                                Console.WriteLine($"Membership : {memberByEmail.MembershipType.Name}");
                            }

                            break;



                        case 5:

                            try
                            {
                                Console.Write("Enter Member Id : ");

                                int memberId =
                                    Convert.ToInt32(Console.ReadLine());

                                Console.Write("Enter New Membership Type Id : ");

                                int membershipTypeId =
                                    Convert.ToInt32(Console.ReadLine());

                                var updatedMember =
                                    memberService.UpdateMembershipStatus(
                                        memberId,
                                        membershipTypeId);

                                Console.WriteLine("Membership Updated Successfully");

                                Console.WriteLine($"New Membership Type Id : {updatedMember.MembershipTypeId}");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            break;



                        case 6:

                            try
                            {
                                Console.Write("Enter Member Id : ");

                                int memberId =
                                    Convert.ToInt32(Console.ReadLine());

                                var deactivatedMember =
                                    memberService.DeactivateMember(memberId);

                                Console.WriteLine("Member Deactivated Successfully");

                                Console.WriteLine($"Member Name : {deactivatedMember.Name}");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            break;



                        default:

                            Console.WriteLine("Invalid Choice");

                            break;
                    }

                    break;
                

                case 2:

                    Console.WriteLine("\n--- Book Management ---");

                    Console.WriteLine("1.Add New Book");

                    Console.WriteLine("2.Add Book Copy");

                    Console.WriteLine("3.View Available Books");

                    Console.WriteLine("4.Search By Title");

                    Console.WriteLine("5.Search By Author");

                    Console.WriteLine("6.Search By Category");

                    Console.WriteLine("7.Mark Copy Damaged");

                    Console.Write("\nEnter Choice : ");

                    int bookChoice =
                        Convert.ToInt32(Console.ReadLine());

                    switch (bookChoice)
                    {

                        case 1:

                            try
                            {
                                Book book = new Book();

                                Console.Write("Enter Title : ");

                                book.Title = Console.ReadLine();

                                Console.Write("Enter Author : ");

                                book.Author = Console.ReadLine();

                                Console.WriteLine("=== Book Categories ===");
                                Console.WriteLine("1.Horror");
                                Console.WriteLine("2.Comedy");
                                Console.WriteLine("3.Fantasy");
                                

                                Console.Write("Enter Category Id : ");

                                book.BookCategoryId =
                                    Convert.ToInt32(Console.ReadLine());

                                var addedBook =
                                    bookService.AddBook(book);

                                Console.WriteLine("Book Added Successfully");

                                Console.WriteLine($"Book Id : {addedBook.BookId}");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.InnerException.Message);
                            }

                            break;



                        case 2:

                            try
                            {
                                BookCopy copy = new BookCopy();

                                Console.Write("Enter Book Id : ");

                                copy.BookId =
                                    Convert.ToInt32(Console.ReadLine());

                                Console.Write("Enter Copy Code : ");

                                copy.CopyCode =
                                    Console.ReadLine();

                                copy.Status =
                                    BookCopyStatusEnum.Available;

                                var addedCopy =
                                    bookService.AddBookCopy(copy);

                                Console.WriteLine("Book Copy Added Successfully");

                                Console.WriteLine($"Copy Id : {addedCopy.BookCopyId}");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            break;



                        case 3:

                            var availableBooks =
                                bookService.GetAvailableBooks();

                            foreach (var book in availableBooks)
                            {
                                Console.WriteLine("\n----------------");

                                Console.WriteLine($"Copy Id : {book.BookCopyId}");

                                Console.WriteLine($"Title : {book.Book.Title}");

                                Console.WriteLine($"Author : {book.Book.Author}");

                                Console.WriteLine($"Copy Code : {book.CopyCode}");

                                Console.WriteLine($"Status : {book.Status}");
                            }

                            break;



                        case 4:

                            Console.Write("Enter Title : ");

                            string title =
                                Console.ReadLine();

                            var booksByTitle =
                                bookService.GetBooksByTitle(title);

                            foreach (var book in booksByTitle)
                            {
                                Console.WriteLine($"\n{book.Title} - {book.Author}");
                            }

                            break;



                        case 5:

                            Console.Write("Enter Author : ");

                            string author =
                                Console.ReadLine();

                            var booksByAuthor =
                                bookService.GetBooksByAuthor(author);

                            foreach (var book in booksByAuthor)
                            {
                                Console.WriteLine($"\n{book.Title} - {book.Author}");
                            }

                            break;



                        case 6:
                            Console.WriteLine("=== Book Categories ===");
                            Console.WriteLine("1.Horror");
                            Console.WriteLine("2.Comedy");
                            Console.WriteLine("3.Fantasy");
                            Console.Write("Enter Category Id : ");

                            int categoryId =
                                Convert.ToInt32(Console.ReadLine());

                            var booksByCategory =
                                bookService.GetBooksByCategory(categoryId);

                            foreach (var book in booksByCategory)
                            {
                                Console.WriteLine($"\n{book.Title} - {book.Author}");
                            }

                            break;



                        case 7:

                            try
                            {
                                Console.Write("Enter Copy Id : ");

                                int copyId =
                                    Convert.ToInt32(Console.ReadLine());

                                var updatedCopy =
                                    bookService.UpdateBookCopyStatus(
                                        copyId,
                                        BookCopyStatusEnum.Damaged);

                                Console.WriteLine("Book Copy Marked Damaged");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            break;



                        default:

                            Console.WriteLine("Invalid Choice");

                            break;
                    }

                    break;

                

                case 3:

                    try
                    {
                        BorrowingService borrowingService =
                            new BorrowingService();

                        Console.WriteLine(
                            "\n--- Borrow Book ---");



                        Console.Write(
                            "Enter Member Id : ");

                        int memberId =
                            Convert.ToInt32(Console.ReadLine());



                        Console.Write(
                            "Enter Book Id : ");

                        int bookId =
                            Convert.ToInt32(Console.ReadLine());



                        borrowingService
                            .BorrowBook(memberId, bookId);



                        Console.WriteLine(
                            "Book Borrowed Successfully");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    break;



                case 4:

                    try
                    {
                        ReturnBorrowingService
                            returnBorrowingService =
                                new ReturnBorrowingService();



                        Console.WriteLine(
                            "\n--- Return Book ---");



                        Console.Write(
                            "Enter Borrowing Id : ");

                        int borrowingId =
                            Convert.ToInt32(
                                Console.ReadLine());



                        returnBorrowingService
                            .ReturnBook(borrowingId);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    break;



                case 5:

                    Console.WriteLine("\n--- Fine Management ---");

                    Console.WriteLine("1.View Pending Fines");

                    Console.WriteLine("2.Pay Fine");

                    Console.WriteLine("3.View Fine Payment History");

                    Console.Write("\nEnter Choice : ");

                    int fineChoice =
                        Convert.ToInt32(Console.ReadLine());

                    switch (fineChoice)
                    {
                        

                        case 1:

                            Console.Write(
                                "Enter Member Id : ");

                            int memberId =
                                Convert.ToInt32(Console.ReadLine());

                            var fines =
                                fineService
                                    .GetPendingFinesByMember(memberId);

                            if(fines.Count == 0)
                            {
                                Console.WriteLine(
                                    "No pending fines");
                            }
                            else
                            {

                                foreach (var fine in fines)
                                {
                                    Console.WriteLine(
                                        "\n----------------");

                                    Console.WriteLine(
                                        $"Fine Id : {fine.FineId}");

                                    Console.WriteLine(
                                        $"Borrowing Id : {fine.BorrowingId}");

                                    Console.WriteLine(
                                        $"Amount : ₹{fine.Amount}");

                                    Console.WriteLine(
                                        $"Paid : {fine.IsPaid}");
                                }

                                decimal total =
                                    fineService
                                        .GetPendingFineAmount(memberId);

                                Console.WriteLine(
                                    $"\nTotal Pending Fine : ₹{total}");
                            }

                            break;
                        

                        case 2:

                            try
                            {
                                Console.Write(
                                    "Enter Fine Id : ");

                                int fineId =
                                    Convert.ToInt32(Console.ReadLine());

                                Console.Write(
                                    "Enter Amount : ");

                                decimal amount =
                                    Convert.ToDecimal(Console.ReadLine());

                                var payment =
                                    fineService.PayFine(
                                        fineId,
                                        amount);

                                Console.WriteLine(
                                    "Fine Paid Successfully");

                                Console.WriteLine(
                                    $"Payment Id : {payment.FinePaymentId}");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            break;

                        case 3:

                            Console.Write(
                                "Enter Fine Id : ");

                            int historyFineId =
                                Convert.ToInt32(Console.ReadLine());

                            var payments =
                                fineService
                                    .GetFinePaymentHistory(
                                        historyFineId);

                            if(payments.Count == 0)
                            {
                                Console.WriteLine(
                                    "No payment history");
                            }
                            else
                            {
                                foreach (var payment in payments)
                                {
                                    Console.WriteLine(
                                        "\n----------------");

                                    Console.WriteLine(
                                        $"Payment Id : {payment.FinePaymentId}");

                                    Console.WriteLine(
                                        $"Amount Paid : ₹{payment.PaidAmount}");

                                    Console.WriteLine(
                                        $"Payment Date : {payment.PaymentDate}");
                                }
                            }

                            break;



                        default:

                            Console.WriteLine(
                                "Invalid Choice");

                            break;
                    }

                    break;



                case 6:

                        ReportService reportService =
                            new ReportService();

                        Console.WriteLine(
                            "\n--- Reports Module ---");

                        Console.WriteLine(
                            "1.Books Currently Borrowed");

                        Console.WriteLine(
                            "2.Overdue Books");

                        Console.WriteLine(
                            "3.Members With Pending Fines");

                        Console.WriteLine(
                            "4.Most Borrowed Books");

                        Console.WriteLine(
                            "5.Available Books By Category");

                        Console.WriteLine(
                            "6.Member Borrowing History");



                        Console.Write(
                            "\nEnter Choice : ");

                        int reportChoice =
                            Convert.ToInt32(Console.ReadLine());



                        switch (reportChoice)
                        {

                            

                            case 1:

                                reportService
                                    .GetCurrentlyBorrowedBooks();

                                break;



                            

                            case 2:

                                reportService
                                    .GetOverdueBooks();

                                break;



                            

                            case 3:

                                reportService
                                    .GetMembersWithPendingFines();

                                break;
                            

                            case 4:

                                reportService
                                    .GetMostBorrowedBooks();

                                break;
                            

                            case 5:

                                Console.Write(
                                    "Enter Category Id : ");

                                int categoryId =
                                    Convert.ToInt32(
                                        Console.ReadLine());

                                reportService
                                    .GetAvailableBooksByCategory(
                                        categoryId);

                                break;
                            

                            case 6:

                                Console.Write(
                                    "Enter Member Id : ");

                                int memberId =
                                    Convert.ToInt32(
                                        Console.ReadLine());

                                reportService
                                    .GetMemberBorrowingHistory(
                                        memberId);

                                break;



                            default:

                                Console.WriteLine(
                                    "Invalid Choice");

                                break;
                        }

                        break;



                case 7:

                    isRunning = false;

                    Console.WriteLine("Application Closed");

                    break;



                default:

                    Console.WriteLine("Invalid Choice");

                    break;
            }
        }
    }
}