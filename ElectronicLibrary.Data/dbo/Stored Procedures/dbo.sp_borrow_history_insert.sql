CREATE PROCEDURE [dbo].[sp_borrow_history_insert]
	@ReaderId          AS INT,
	@InventoryNumberId AS INT,
    @BorrowDate        AS DATE 
AS
BEGIN
	INSERT dbo.borrow_history(
        reader_id,
        inventory_number_id,
        borrow_date
	)
	VALUES (
		@ReaderId,
		@InventoryNumberId,
		@BorrowDate
	)
END