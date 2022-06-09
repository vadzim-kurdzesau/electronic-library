CREATE PROCEDURE [dbo].[sp_readers_delete]
	@Id AS INT
AS
BEGIN
	DELETE dbo.readers 
    WHERE dbo.readers.id = @Id;
END
