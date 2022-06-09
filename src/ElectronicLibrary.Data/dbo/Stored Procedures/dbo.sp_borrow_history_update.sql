CREATE PROCEDURE [dbo].[sp_borrow_history_update]
    @ReaderId          AS INT, 
	@InventoryNumberId AS INT,
    @ReturnDate        AS DATE 
AS
BEGIN
    UPDATE dbo.borrow_history
       SET return_date         = @ReturnDate
     WHERE borrow_date         = (SELECT MAX(borrow_date)
                                    FROM borrow_history 
                                   WHERE reader_id = @ReaderId 
                                     AND inventory_number_id = @InventoryNumberId);
END
