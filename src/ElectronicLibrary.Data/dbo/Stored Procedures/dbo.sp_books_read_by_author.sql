CREATE PROCEDURE [dbo].[sp_books_read_by_author]
	@Author AS NVARCHAR(200)
AS
BEGIN
	SELECT *
      FROM dbo.books 
     WHERE dbo.books.author = @Author;
END
