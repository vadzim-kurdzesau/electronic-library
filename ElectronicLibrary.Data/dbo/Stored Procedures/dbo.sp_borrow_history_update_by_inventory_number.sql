CREATE PROCEDURE [dbo].[sp_borrow_history_update_by_inventory_number]
	@InventoryNumberId AS INT,
    @ReturnDate        AS DATE 
AS
BEGIN
    UPDATE dbo.borrow_history
       SET return_date         = @ReturnDate
     WHERE inventory_number_id = @InventoryNumberId;
END
