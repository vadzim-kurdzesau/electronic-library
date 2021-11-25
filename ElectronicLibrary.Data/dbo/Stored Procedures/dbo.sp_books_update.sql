CREATE PROCEDURE [dbo].[sp_books_update]
    @Id              INT,
	@Name            NVARCHAR(200),
	@Author          NVARCHAR(100),
    @PublicationDate DATE
AS
BEGIN
    UPDATE dbo.books 
       SET 
           name             = @Name, 
           author           = @Author,
           publication_date = @PublicationDate
     WHERE 
           id = @Id
END
