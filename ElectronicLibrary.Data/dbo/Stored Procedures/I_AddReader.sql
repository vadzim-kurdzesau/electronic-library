CREATE PROCEDURE I_AddReader
		@FirstName	AS NVARCHAR(20),
		@LastName	AS NVARCHAR(20),
		@Email		AS NVARCHAR(320),
		@Phone      AS NVARCHAR(20),
		@City		AS NVARCHAR(20),
		@Address	AS NVARCHAR(100),
		@Zip		AS CHAR(6)
	AS
	INSERT dbo.readers (first_name, last_name, email, phone, city_id, address, zip)
	VALUES
					   (@FirstName, @LastName, @Email, @Phone, (SELECT id FROM cities WHERE @City = cities.city), @Address, @Zip)