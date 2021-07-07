CREATE TABLE [gt].[DefinedElement]
(
	[DefinedElementId] INT NOT NULL IDENTITY(1,1), 
	DefinedElementGroupId INT NOT NULL, 
	[Name] VARCHAR(100), 
    
	CONSTRAINT PK_DefinedElement PRIMARY KEY ([DefinedElementId]),
    CONSTRAINT FK_DefinedElementGroup FOREIGN KEY (DefinedElementGroupId) REFERENCES [gt].DefinedElementGroup(DefinedElementGroupId),
)