CREATE PROCEDURE [dbo].[sp_inventory_numbers_read_by_book_id]
	@Id AS INT
AS
BEGIN
    SELECT *
      FROM dbo.inventory_numbers
     WHERE dbo.inventory_numbers.book_id = @Id;
END
