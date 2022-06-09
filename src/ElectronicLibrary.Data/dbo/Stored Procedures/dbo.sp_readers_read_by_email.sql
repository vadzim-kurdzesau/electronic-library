CREATE PROCEDURE [dbo].[sp_readers_read_by_email] (
	@Email AS VARCHAR(320)
)
AS
BEGIN
	SELECT * 
	  FROM dbo.readers
	 WHERE dbo.readers.email = @Email;
END
