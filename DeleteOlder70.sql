DELETE FROM [TestApp].[dbo].[Employees]
WHERE DATEDIFF(YEAR, GETDATE(), [DateBirth]) < -70