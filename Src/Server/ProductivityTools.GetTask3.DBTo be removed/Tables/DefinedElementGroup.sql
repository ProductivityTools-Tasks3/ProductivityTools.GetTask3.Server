CREATE TABLE [gt].[DefinedElementGroup]
(
	[DefinedElementGroupId] INT NOT NULL IDENTITY(1,1),
	[BagId] INT NOT NULL, 
	[Name] VARCHAR(100), 
    
	CONSTRAINT PK_DefinedElementGroup PRIMARY KEY ([DefinedElementGroupId]),
    CONSTRAINT FK_BagId FOREIGN KEY ([BagId]) REFERENCES [gt].[Element](ElementId),
)
