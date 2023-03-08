CREATE FUNCTION [gt].[ValidateOwnership] (@TreeId INT, @User VARCHAR(100))
    RETURNS BIT AS
    BEGIN
	    DECLARE @ParentId INT
		DECLARE @Name VARCHAR(100)
	    DECLARE @PartResult BIT
		DECLARE @RootId INT

		exec xp_cmdshell 'echo "START">>d:\debug.txt'
		--PRINT @TreeId
		SELECT @RootId=ElementId FROM [gt].[Element] WHERE Name='Root'
		--PRINT 'ROOT SELECTED'

        SELECT @ParentId=[ParentId],@Name=[Name]
        FROM [GetTask3].[gt].[Element] where ElementId=@TreeId and Type=3

		If @ParentId IS NULL
			RETURN 0

		IF @ParentId = @RootId AND @Name=@User
			RETURN 1
		ELSE
			BEGIN
				SELECT @PartResult = gt.[ValidateOwnership](@ParentId, @Name)
				RETURN @PartResult
			END
		
		RETURN 0
    END                        
GO

--EXECUTE sp_configure 'show advanced options', 1
--GO
--RECONFIGURE

--EXECUTE sp_configure 'xp_cmdshell', 1
--GO
--RECONFIGURE
--GO