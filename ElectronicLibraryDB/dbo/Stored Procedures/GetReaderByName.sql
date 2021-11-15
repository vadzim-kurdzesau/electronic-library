CREATE PROCEDURE GetReaderByName 
			@FirstName	NVARCHAR(20),
			@LastName	NVARCHAR(20)
	AS
	SELECT * FROM dbo.readers
	WHERE	first_name = @FirstName
	AND		last_name = @LastName