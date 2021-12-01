CREATE PROCEDURE [dbo].[sp_inventory_numbers_read_not_borrowed]
    @Id AS INT
AS
BEGIN
	  SELECT i.*
        FROM dbo.inventory_numbers AS i
   LEFT JOIN dbo.borrow_history	   AS b
		  ON number = b.inventory_number_id
	   WHERE i.book_id = @Id 
		 AND b.borrow_date IS NULL;
END
