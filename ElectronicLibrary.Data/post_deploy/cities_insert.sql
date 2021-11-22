CREATE OR ALTER PROCEDURE dbo.sp_utils_insert_city (
    @id                 AS INT,
    @name          AS NVARCHAR(20)
)
AS
BEGIN
    IF (SELECT COUNT(1) FROM [dbo].[cities] WHERE [id] = @id) = 0 
    BEGIN
        INSERT INTO [dbo].[cities] ([id], [name])
        VALUES (@id, @name)
    END
    ELSE
    BEGIN
        UPDATE [dbo].[cities]
        SET [name] = @name
        WHERE [id] = @id AND [name] <> @name
    END
END

GO

-- Insert data blocks
EXEC dbo.sp_utils_insert_city 1, 'Minsk'
EXEC dbo.sp_utils_insert_city 2, 'Gomel'
EXEC dbo.sp_utils_insert_city 3, 'Brest'