CREATE PROCEDURE [dbo].[InsertEmployeeInfo]
	@EmployeeName nvarchar(100) = NULL,
	@FirstName nvarchar(50) = NULL,
	@LastName nvarchar(50) = NULL,
	@CompanyName nvarchar(20),
	@Position nvarchar(30) = NULL,
	@Street nvarchar(50),
	@City nvarchar(20) = NULL,
	@State nvarchar(50) = NULL,
	@ZipCode nvarchar(50) = NULL
AS
	begin
	IF TRIM(@EmployeeName) IS NOT NULL OR TRIM(@FirstName) IS NOT NULL OR TRIM(@LastName) IS NOT NULL
		begin
		IF len(@CompanyName) > 20
			SET @CompanyName = left(@CompanyName, 20) + '...'

			Insert into Address(Street,City,State,ZipCode) values (@Street,@City,@State, @ZipCode);

			DECLARE @AddressId INT;
			SELECT @AddressId = SCOPE_IDENTITY();

			Insert into Person(FirstName,LastName) values (@FirstName,@LastName);

			DECLARE @PersonId INT;
			SELECT @PersonId = SCOPE_IDENTITY();

			Insert into Employee(CompanyName,Position,EmployeeName, PersonId, AddressId) values (@CompanyName,@Position,@EmployeeName, @PersonId, @AddressId);
		end
	end
RETURN 0
