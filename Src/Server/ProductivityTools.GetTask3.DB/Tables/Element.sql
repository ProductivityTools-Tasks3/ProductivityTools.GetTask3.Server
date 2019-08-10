CREATE TABLE [gt].[Element]
(
	[ElementId] INT NOT NULL IDENTITY(1,1),
	[ParentId] INT NULL,
	[Name] VARCHAR(100), 
	[Category] VARCHAR(100),
	[Person] varchar(20),
    [Type] INT NOT NULL, 
    [Status] INT NOT NULL, 
    [Created] DATETIME2 NOT NULL, 
	[Start] DATETIME2 NULL,
    [Finished] DATETIME2 NULL, 
	[Cleared] BIT DEFAULT(0) NOT NULL,

	[TaskIdMigrated] INT,
	[DefinedTaskIdMigrated] INT,
	[TomatoMigrated] INT,
	CONSTRAINT PK_Element PRIMARY KEY(ElementId),
	CONSTRAINT FK_ParentId FOREIGN KEY ([ParentId]) REFERENCES [gt].[Element](ElementId)
)
