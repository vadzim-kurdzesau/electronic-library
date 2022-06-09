CREATE PROCEDURE [dbo].[sp_readers_update]
    @Id         AS INT,
	@FirstName  AS NVARCHAR(20),
	@LastName   AS NVARCHAR(30),
    @Email      AS VARCHAR(320),
    @Phone      AS VARCHAR(20),
    @CityId     AS INT,
    @Address    AS NVARCHAR(100),
    @Zip        AS CHAR(6)
AS
BEGIN
    UPDATE dbo.readers
      SET 
        first_name = @FirstName, 
        last_name  = @LastName,
        email      = @Email,
        phone      = @Phone, 
        city_id    = @CityId,
        address    = @Address,
        zip        = @Zip
    WHERE 
        id         = @Id
END