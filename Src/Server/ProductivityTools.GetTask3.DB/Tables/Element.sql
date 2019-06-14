CREATE TABLE [gt].[Elements]
(
	[ElementId] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[BagId] INT NULL,
	[Name] VARCHAR(100), 
    [Type] INT NOT NULL, 
    [Status] INT NOT NULL, 
    [Created] DATETIME2 NOT NULL, 
    [Deadline] DATETIME2 NOT NULL, 
    [Finished] DATETIME2 NULL, 

)
