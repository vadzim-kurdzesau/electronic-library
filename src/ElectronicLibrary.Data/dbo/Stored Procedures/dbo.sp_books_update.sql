CREATE PROCEDURE [dbo].[sp_books_update]
    @Id              AS INT,
	@Name            AS NVARCHAR(200),
	@Author          AS NVARCHAR(100),
    @PublicationDate AS DATE
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
