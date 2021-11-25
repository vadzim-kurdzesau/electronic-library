CREATE PROCEDURE [dbo].[sp_readers_read_by_name] (
	@FirstName	AS NVARCHAR(20),
	@LastName	AS NVARCHAR(20)
)
AS
BEGIN
	SELECT * 
	  FROM dbo.readers
	 WHERE first_name = @FirstName  
	   AND last_name = @LastName
END
