CREATE OR REPLACE PROCEDURE return_book(
    p_borrow_id INT,
    p_return_date TIMESTAMPTZ,
    returned text
)
LANGUAGE plpgsql
AS
$$
BEGIN
    UPDATE "borrowings"
    SET "ReturnDate" = p_return_date
    WHERE "BorrowingId" = p_borrow_id;
END;
$$;


CREATE OR REPLACE PROCEDURE add_fine(
    p_borrowing_id INTEGER,
    p_amount DECIMAL
)
LANGUAGE plpgsql
AS
$$
BEGIN
    INSERT INTO "Fines"
        ("BorrowingId", "Amount", "IsPaid", "CreatedDate")
    VALUES
        (p_borrowing_id, p_amount, FALSE, NOW());
END;
$$;


CREATE OR REPLACE PROCEDURE update_book_copy_status(
    p_book_copy_id INTEGER,
    p_status TEXT
)
LANGUAGE plpgsql
AS
$$
BEGIN
    UPDATE "bookCopies"
    SET "Status" = p_status
    WHERE "BookCopyId" = p_book_copy_id;
END;
$$;