CREATE PROCEDURE [dbo].[sp_readers_delete]
	@Id INT
AS
BEGIN
	DELETE dbo.readers 
    WHERE dbo.readers.id = @Id;
END
