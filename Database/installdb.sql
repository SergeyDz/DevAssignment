use master
IF db_id('AccountDB') IS NOT NULL
BEGIN 
	alter database [AccountDB] set single_user with rollback immediate
	DROP DATABASE [AccountDB]
END
CREATE DATABASE [AccountDB]


-- Checks if user table exists
USE [AccountDB]

GO
IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo'  AND  TABLE_NAME = 'User'))
BEGIN
	drop table dbo.[User]
END

-- Create dbo.Users table
GO 
CREATE TABLE [dbo].[User](
	[Id] [int] identity(1,1) PRIMARY KEY,
	[Login] [varchar] (50) not null,
	[Name] [varchar](50) NOT NULL,
	[IsActive] [bit] default (0), 
	[CreatedOn] [DateTime2] NOT NULL default GETDATE() 
	)
-- Add  constraint
GO
ALTER TABLE [dbo].[User] ADD CONSTRAINT AK_User_Login UNIQUE (Login); 

-- Account table

GO
IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo'  AND  TABLE_NAME = 'Account'))
BEGIN
	drop table dbo.Account
END

-- Create dbo.Account table
GO 
CREATE TABLE [dbo].[Account](
	[Id] [int] identity(1,1) PRIMARY KEY,
	[Code] [varchar] (16) not null,
	[Name] [varchar](50) NOT NULL,
	[IsActive] [bit] default (0), 
	[CreatedOn] [DateTime2] NOT NULL default GETDATE(), 
	[CreatedById] [int] NOT NULL,
	[UpdatedOn] [DateTime2],
	[UpdatedById] [int]
	)
-- Add constraint
GO
ALTER TABLE [dbo].[Account] ADD CONSTRAINT AK_Account_Code UNIQUE (Code); 
ALTER TABLE [dbo].[Account] ADD  FOREIGN KEY (CreatedById) REFERENCES [dbo].[User](Id)
ALTER TABLE [dbo].[Account] ADD  FOREIGN KEY (UpdatedById) REFERENCES [dbo].[User](Id)
GO

-- Order table

GO
IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo'  AND  TABLE_NAME = 'Order'))
BEGIN
	drop table dbo.[Order]
END

-- Create dbo.Order table
GO 
CREATE TABLE [dbo].[Order](
	[Id] [int] identity(1,1) PRIMARY KEY,
	[AccountId] [int] NOT NULL,
	[Description] [varchar] (50) NOT NULL,
	[Amount] [float] (53) NOT NULL,
	[CreatedOn] [DateTime2] NOT NULL default GETDATE(), 
	[CreatedById] [int] NOT NULL,
	[UpdatedOn] [DateTime2],
	[UpdatedById] [int]
	
	)
-- Add constraint
GO
ALTER TABLE [dbo].[Order] ADD  FOREIGN KEY (AccountId) REFERENCES [dbo].[Account](Id)
ALTER TABLE [dbo].[Order] ADD  FOREIGN KEY (CreatedById) REFERENCES [dbo].[User](Id)
ALTER TABLE [dbo].[Order] ADD  FOREIGN KEY (UpdatedById) REFERENCES [dbo].[User](Id)
GO

-- Insert test data
if not exists (select * from dbo.[User] where Login = 'admin')
begin
insert into dbo.[User] (Login, Name, IsActive) values ('admin', 'Administrator', 1)
insert into dbo.[User] (Login, Name, IsActive) values ('smith', 'Smith, John', 1)
insert into dbo.[User] (Login, Name, IsActive) values ('sergey', 'Dzyuban, Sergey', 1)
end

-- Generate Accounts scope 
DECLARE @i int = 0
DECLARE @j int = 0
DECLARE @accountUserId int
DECLARE @orderUserId int
DECLARE @accountId int


WHILE @i < 200 BEGIN
    SET @i = @i + 1
	SET @accountUserId = (select Id from (select Id, Name, [Login], ROW_NUMBER() over (order by Id) as [row] from [User]) as users where [row] = (@i % 3 + 1))

	insert into Account 
		(Code, Name, CreatedById, IsActive) 
		values 
		(
			'510111112222' + RIGHT('0000'+ CONVERT(VARCHAR,@i),4),
			'Test generated account #' + RIGHT('0000'+CONVERT(VARCHAR,@i),4),
			@accountUserId,
			1
		)


		set @accountId = SCOPE_IDENTITY()

		set @j = 0
		while @j < 1000
			begin
				set @j = @j + 1
				SET @orderUserId = (select Id from (select Id, Name, [Login], ROW_NUMBER() over (order by Id) as [row] from [User]) as users where [row] = (@i % 3 + 1))
				
				insert into [Order] 
				(AccountId, Description, Amount, CreatedById)
				values
				(
					@accountId,
					'Operation #' + CAST(@j as varchar(5)),
					cast(@j as varchar(5)) + '.' + cast(@i as varchar(5)),
					@orderUserId
				) 
			end
END

exec sp_spaceused