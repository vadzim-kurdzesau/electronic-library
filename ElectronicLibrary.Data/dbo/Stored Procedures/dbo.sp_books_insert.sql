CREATE PROCEDURE [dbo].[sp_books_insert] (
	@Name				AS NVARCHAR(200),
	@Author				AS NVARCHAR(100),
	@PublicationDate	AS DATE
)
AS
BEGIN
	INSERT dbo.books(
		name, 
		author, 
		publication_date
	)
	VALUES (
		@Name,
		@Author,
		@PublicationDate
	) 
END
