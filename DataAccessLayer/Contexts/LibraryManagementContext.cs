using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccessLayer.Contexts;

public class LibraryManagementContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {   
        string connectionString = "Host=localhost;Port=5432;Database=LibrarayManagementDB;Username=postgres;Password=vijay";
        optionsBuilder.UseNpgsql(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    

    modelBuilder.Entity<Borrowing>()
        .HasOne(b => b.Fine)
        .WithOne(f => f.Borrowing)
        .HasForeignKey<Fine>(f => f.BorrowingId);
    
    // Email must be unique
    modelBuilder.Entity<Member>()
        .HasIndex(m => m.Email)
        .IsUnique();
    
    // Phonenumber must be unique
    modelBuilder.Entity<Member>()
        .HasIndex(m => m.Phone)
        .IsUnique();
    
    // CopyCode must be unique
    modelBuilder.Entity<BookCopy>()
        .HasIndex(c => c.CopyCode)
        .IsUnique();

    
    modelBuilder.Entity<BookCopy>()
        .Property(c => c.Status)
        .HasConversion<string>();

    modelBuilder.Entity<Borrowing>()
        .Property(b => b.Status)
        .HasConversion<string>();

    
    modelBuilder.Entity<Borrowing>()
        .HasOne(b => b.BookCopy)
        .WithMany(c => c.Borrowings)
        .HasForeignKey(b => b.BookCopyId)
        .OnDelete(DeleteBehavior.Restrict);

    modelBuilder.Entity<Borrowing>()
        .HasOne(b => b.Member)
        .WithMany(m => m.Borrowings)
        .HasForeignKey(b => b.MemberId)
        .OnDelete(DeleteBehavior.Restrict);
    
}
    
    public DbSet<Member> members { get; set; }
    public DbSet<MembershipType> membershipTypes { get; set; }
    public DbSet<Book> books { get; set; }
    public DbSet<BookCategory> bookCategories { get; set; }
    public DbSet<BookCopy> bookCopies { get; set; }
    public DbSet<Borrowing> borrowings { get; set; }
    public DbSet<Fine> fines { get; set; }
    public DbSet<FinePayment> finePayments { get; set; }
}