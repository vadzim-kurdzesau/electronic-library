CREATE PROCEDURE [dbo].[sp_books_read_by_name]
	@Name NVARCHAR(200)
AS
BEGIN
	SELECT *
      FROM dbo.books 
     WHERE dbo.books.name = @Name;
END