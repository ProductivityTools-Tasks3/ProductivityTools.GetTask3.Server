CREATE TABLE [gt].[TomatoItem]
(
	[TomatoItemId] INT NOT NULL IDENTITY(1,1),
	[TomatoId] INT NOT NULL,
	[ElementId] INT NOT NULL,
    CONSTRAINT PK_TomatoItem PRIMARY KEY(TomatoItemId),
	CONSTRAINT FK_TomatoItem_Tomato FOREIGN KEY (TomatoId) REFERENCES [gt].Tomato(TomatoId),
	CONSTRAINT FK_TomatoItem_ElementId FOREIGN KEY (ElementId) REFERENCES [gt].Element(ElementId)
)
