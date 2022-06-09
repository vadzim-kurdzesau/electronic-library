CREATE PROCEDURE [dbo].[sp_books_read_all_paged]
	@Page	INT,
	@Size	INT
AS
BEGIN
		SELECT	*
		  FROM	dbo.books
	  ORDER BY	(SELECT NULL)
		OFFSET	((@Page - 1) * @Size)	ROWS
	FETCH NEXT	@Size					ROWS ONLY
END