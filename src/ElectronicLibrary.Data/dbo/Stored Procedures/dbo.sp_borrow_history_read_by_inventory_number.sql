CREATE PROCEDURE [dbo].[sp_borrow_history_read_by_inventory_number]
	@InventoryNumberId NVARCHAR(30)
AS
BEGIN
	SELECT *
      FROM dbo.borrow_history 
     WHERE dbo.borrow_history.inventory_number_id = @InventoryNumberId;
END
