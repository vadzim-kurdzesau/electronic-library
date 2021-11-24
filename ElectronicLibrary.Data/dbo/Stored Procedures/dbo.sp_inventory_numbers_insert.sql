CREATE PROCEDURE [dbo].[sp_inventory_numbers_insert] (
	@BookId		AS INT,
	@Number 	AS INT
)
AS
BEGIN
	INSERT dbo.inventory_numbers (
		book_id, 
		number
	)

	VALUES (
		@BookId,
		@Number
	) 
END
