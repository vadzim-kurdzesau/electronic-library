CREATE PROCEDURE [dbo].[sp_readers_update]
    @Id         INT,
	@FirstName  NVARCHAR(20),
	@LastName   NVARCHAR(30),
    @Email      VARCHAR(320),
    @Phone      VARCHAR(20),
    @CityId     INT,
    @Address    NVARCHAR(100),
    @Zip        CHAR(6)
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