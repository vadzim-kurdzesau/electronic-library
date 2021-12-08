CREATE PROCEDURE [dbo].[sp_inventory_numbers_read_by_number]
	@Number INT
AS
BEGIN
	SELECT *
	  FROM dbo.inventory_numbers
	 WHERE number = @Number
END
