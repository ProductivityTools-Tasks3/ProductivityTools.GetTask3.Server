CREATE UNIQUE INDEX [ElementSingleRoot]	ON [gt].[Element]([ParentId]) WHERE [ParentId] IS NULL;
