CREATE TABLE [gt].[TomatoElement](
	[TomatoId] [int] NOT NULL,
	[ElementId] [int] NOT NULL,
 CONSTRAINT [PK_TomatoElement] PRIMARY KEY CLUSTERED 
(
	[TomatoId] ASC,
	[ElementId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [gt].[TomatoElement]  WITH CHECK ADD  CONSTRAINT [FK_TomatoItem_ElementId] FOREIGN KEY([ElementId])
REFERENCES [gt].[Element] ([ElementId])
GO

ALTER TABLE [gt].[TomatoElement] CHECK CONSTRAINT [FK_TomatoItem_ElementId]
GO

ALTER TABLE [gt].[TomatoElement]  WITH CHECK ADD  CONSTRAINT [FK_TomatoItem_Tomato] FOREIGN KEY([TomatoId])
REFERENCES [gt].[Tomato] ([TomatoId])
GO

ALTER TABLE [gt].[TomatoElement] CHECK CONSTRAINT [FK_TomatoItem_Tomato]
GO
