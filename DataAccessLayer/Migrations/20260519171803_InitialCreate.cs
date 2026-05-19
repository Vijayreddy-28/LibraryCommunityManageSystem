using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "bookCategories",
                columns: table => new
                {
                    BookCategoryId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CategoryName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bookCategories", x => x.BookCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "membershipTypes",
                columns: table => new
                {
                    MembershipTypeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    MaxBooksAllowed = table.Column<int>(type: "integer", nullable: false),
                    MaxBorrowDays = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_membershipTypes", x => x.MembershipTypeId);
                });

            migrationBuilder.CreateTable(
                name: "books",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Author = table.Column<string>(type: "text", nullable: false),
                    BookCategoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_books", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_books_bookCategories_BookCategoryId",
                        column: x => x.BookCategoryId,
                        principalTable: "bookCategories",
                        principalColumn: "BookCategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "members",
                columns: table => new
                {
                    MemberId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    JoinedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MembershipTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_members", x => x.MemberId);
                    table.ForeignKey(
                        name: "FK_members_membershipTypes_MembershipTypeId",
                        column: x => x.MembershipTypeId,
                        principalTable: "membershipTypes",
                        principalColumn: "MembershipTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "bookCopies",
                columns: table => new
                {
                    BookCopyId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CopyCode = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    BookId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bookCopies", x => x.BookCopyId);
                    table.ForeignKey(
                        name: "FK_bookCopies_books_BookId",
                        column: x => x.BookId,
                        principalTable: "books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "borrowings",
                columns: table => new
                {
                    BorrowingId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BorrowDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: false),
                    MemberId = table.Column<int>(type: "integer", nullable: false),
                    BookCopyId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_borrowings", x => x.BorrowingId);
                    table.ForeignKey(
                        name: "FK_borrowings_bookCopies_BookCopyId",
                        column: x => x.BookCopyId,
                        principalTable: "bookCopies",
                        principalColumn: "BookCopyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_borrowings_members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "members",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "fines",
                columns: table => new
                {
                    FineId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    IsPaid = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    BorrowingId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fines", x => x.FineId);
                    table.ForeignKey(
                        name: "FK_fines_borrowings_BorrowingId",
                        column: x => x.BorrowingId,
                        principalTable: "borrowings",
                        principalColumn: "BorrowingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "finePayments",
                columns: table => new
                {
                    FinePaymentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PaidAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PaymentMethod = table.Column<string>(type: "text", nullable: false),
                    FineId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_finePayments", x => x.FinePaymentId);
                    table.ForeignKey(
                        name: "FK_finePayments_fines_FineId",
                        column: x => x.FineId,
                        principalTable: "fines",
                        principalColumn: "FineId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bookCopies_BookId",
                table: "bookCopies",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_bookCopies_CopyCode",
                table: "bookCopies",
                column: "CopyCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_books_BookCategoryId",
                table: "books",
                column: "BookCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_borrowings_BookCopyId",
                table: "borrowings",
                column: "BookCopyId");

            migrationBuilder.CreateIndex(
                name: "IX_borrowings_MemberId",
                table: "borrowings",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_finePayments_FineId",
                table: "finePayments",
                column: "FineId");

            migrationBuilder.CreateIndex(
                name: "IX_fines_BorrowingId",
                table: "fines",
                column: "BorrowingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_members_Email",
                table: "members",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_members_MembershipTypeId",
                table: "members",
                column: "MembershipTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_members_Phone",
                table: "members",
                column: "Phone",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "finePayments");

            migrationBuilder.DropTable(
                name: "fines");

            migrationBuilder.DropTable(
                name: "borrowings");

            migrationBuilder.DropTable(
                name: "bookCopies");

            migrationBuilder.DropTable(
                name: "members");

            migrationBuilder.DropTable(
                name: "books");

            migrationBuilder.DropTable(
                name: "membershipTypes");

            migrationBuilder.DropTable(
                name: "bookCategories");
        }
    }
}
