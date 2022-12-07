/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

GO
DECLARE @VarDecimalSupported AS BIT;

SELECT @VarDecimalSupported = 0;

IF ((ServerProperty(N'EngineEdition') = 3)
    AND (((@@microsoftversion / power(2, 24) = 9)
          AND (@@microsoftversion & 0xffff >= 3024))
         OR ((@@microsoftversion / power(2, 24) = 10)
             AND (@@microsoftversion & 0xffff >= 1600))))
    SELECT @VarDecimalSupported = 1;

IF (@VarDecimalSupported > 0)
    BEGIN
        EXECUTE sp_db_vardecimal_storage_format N'$(DatabaseName)', 'ON';
    END

IF (NOT EXISTS(SELECT * FROM Address))  
BEGIN  
    Insert into Address(Street,City,State,ZipCode) values (N'Пушкина', N'Минск', N'Минск', N'100000');
    DECLARE @AddressId INT;
    SELECT @AddressId = SCOPE_IDENTITY();
END

IF (NOT EXISTS(SELECT * FROM Person))  
BEGIN  
    Insert into Person(FirstName,LastName) values (N'Иван', N'Иванов');
    DECLARE @PersonId INT;
    SELECT @PersonId = SCOPE_IDENTITY();
END

IF (NOT EXISTS(SELECT * FROM Employee))  
BEGIN  
    Insert into Employee(CompanyName,Position,EmployeeName, PersonId, AddressId) values ('EPAM', 'Developer', N'Иванов Иван', @PersonId, @AddressId);
END

IF (NOT EXISTS(SELECT * FROM Company))  
BEGIN  
    Insert into Company(Name, AddressId) values ('EPAM', @AddressId);
END





GO
PRINT N'Update complete.';


GO