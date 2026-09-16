IF NOT EXISTS (SELECT 1 FROM [gt].[Element] WHERE [Name] = 'malgorzata.wujczyk@gmail.com' AND [Type] = 4)
BEGIN
    INSERT INTO [gt].[Element] ([Name], [Type], [Status], [Created], [Initialization], [ParentId], [Cleared])
    VALUES (
        'malgorzata.wujczyk@gmail.com',
        4,
        10,
        GETDATE(),
        GETDATE(),
        (SELECT TOP 1 [ElementId] FROM [gt].[Element] WHERE [Name] = 'root' AND [ParentId] IS NULL),
        0
    )
END
