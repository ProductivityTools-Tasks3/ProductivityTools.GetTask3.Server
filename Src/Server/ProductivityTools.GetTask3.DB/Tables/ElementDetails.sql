CREATE TABLE [gt].[ElementDetails]
(
	[ElementDetailsId] INT IDENTITY(1,1) NOT NULL ,
	[ElementId] INT NOT NULL,
	[Details] VARCHAR(max),

	[DateAdded] DATETIME NULL, 
    CONSTRAINT PK_ElementDetails PRIMARY KEY ([ElementDetailsId]),
	CONSTRAINT FK_ElementDetails_Element FOREIGN KEY (ElementId) REFERENCES [gt].Element(ElementId)
)
