CREATE PROCEDURE [dbo].[sp_readers_read_by_phone] (
	@Phone AS VARCHAR(20)
)
AS
BEGIN
	SELECT * 
	FROM   dbo.readers
	WHERE  dbo.readers.phone = @Phone;
END
