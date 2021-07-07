CREATE TABLE [gt].[TomatoElement]
(
	[TomatoId] INT NOT NULL,
	[ElementId] INT NOT NULL,
    CONSTRAINT PK_TomatoElement PRIMARY KEY(TomatoId,ElementId),
	CONSTRAINT FK_TomatoItem_Tomato FOREIGN KEY (TomatoId) REFERENCES [gt].Tomato(TomatoId),
	CONSTRAINT FK_TomatoItem_ElementId FOREIGN KEY (ElementId) REFERENCES [gt].Element(ElementId)
)
