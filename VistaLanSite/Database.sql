-- Create user account to manipulate table with
CREATE LOGIN DBMASTER WITH PASSWORD = 'M07cwK%4'

IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'DBMASTER')
BEGIN
	CREATE USER [DBMASTER] FOR LOGIN [DBMASTER]
	EXEC sp_addrolemember N'db_owner', N'DBMASTER'
END;

-- Reset Participant table
DROP TABLE [Participants];

-- Empty Participant table
DELETE FROM [Participants];

-- Create Participant table
CREATE TABLE [dbo].[Participants](
	[Id] INT IDENTITY (1, 1),
	[FirstName] NVARCHAR(255),
	[LastName] NVARCHAR(255),
	[StudentNumber] NVARCHAR(255),
	[StudentClass] NVARCHAR(255),
	[PhoneNumber] NVARCHAR(255),
	[BringsConsole] BIT,
	[ConsoleDetails] NVARCHAR(MAX),
	[BringsComputer] BIT,
	[ComputerDetails] NVARCHAR(MAX),
	[HasPaid] BIT
);

-- Dummy data for testing
INSERT INTO [dbo].[Participants] SELECT 'Justin', 'Muris', 1234567890, 'A1', '06-45128159', 0, '', 1, 'Desktop, twee monitoren, headset, toetsenbord en muis.', 0, 0;

-- Select all for testing
SELECT * FROM [Participants];