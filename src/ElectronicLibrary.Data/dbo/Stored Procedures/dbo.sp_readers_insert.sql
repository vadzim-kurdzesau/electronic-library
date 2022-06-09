CREATE PROCEDURE [dbo].[sp_readers_insert] (
	@FirstName	AS NVARCHAR(20),
	@LastName	AS NVARCHAR(20),
	@Email		AS NVARCHAR(320),
	@Phone      AS NVARCHAR(20),
	@CityId		AS NVARCHAR(20),
	@Address	AS NVARCHAR(100),
	@Zip		AS CHAR(6)
)
AS
BEGIN
	INSERT dbo.readers (
		first_name, 
		last_name, 
		email, 
		phone, 
		city_id, 
		address, 
		zip
	)
	-- todo: sg, inconsistent naming
	VALUES (
		@FirstName,
		@LastName,
		@Email,
		@Phone,
		@CityId,
		@Address,
		@Zip
	) 
END
