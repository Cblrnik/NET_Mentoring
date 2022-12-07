CREATE VIEW [dbo].[EmployeeInfo] AS 
	SELECT emp.Id as EmployeeId, 
	CASE WHEN EmployeeName IS NOT NULL 
       THEN EmployeeName
       ELSE CONCAT(p.FirstName,' ', p.LastName)
	END as EmployeeFullName,
	CONCAT(adr.ZipCode, '_', adr.State, ',', adr.City, '-', adr.Street)
	as EmployeeFullAddress,
	CONCAT(emp.CompanyName, ' ', emp.Position)
	as EmployeeCompanyInfo
	FROM [Employee] as emp
	INNER JOIN Person as p ON emp.PersonId = p.Id
	INNER JOIN Address as adr ON emp.AddressId = adr.Id;
