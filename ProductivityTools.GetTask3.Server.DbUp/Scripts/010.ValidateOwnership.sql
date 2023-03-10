DROP FUNCTION [gt].[ValidateOwnershipWithRoot] 
go
CREATE FUNCTION [gt].[ValidateOwnershipWithRoot] (@TreeId INT,@RootId INT, @User VARCHAR(100))
    RETURNS BIT AS
    BEGIN
	    DECLARE @ParentId INT
		DECLARE @Name VARCHAR(100)
	    DECLARE @PartResult BIT
		DECLARE @RootId INT
		DECLARE @Type INT 

		exec xp_cmdshell 'echo "START">>d:\debug.txt'

		declare @log varchar(100)
		select @log = 'echo "Entered function with TreeId:'+CAST(@TreeId AS VARCHAR)+'">>d:\debug.txt'
		exec xp_cmdshell @log
		SELECT @RootId=ElementId FROM [gt].[Element] WHERE Name='Root'

		select @log = 'echo "Selected RootId:'+CAST(@RootId AS VARCHAR)+'">>d:\debug.txt'
		exec xp_cmdshell @log

        SELECT @ParentId=[ParentId],@Name=[Name],@Type=[Type]
        FROM [gt].[Element] where ElementId=@TreeId 

		If @ParentId IS NULL
			RETURN 0

		IF @ParentId = @RootId AND @Name=@User AND @Type=3
			RETURN 1
		ELSE
			BEGIN
				SELECT @PartResult = gt.[ValidateOwnership](@ParentId, @Name)
				RETURN @PartResult
			END
		
		RETURN 0
    END                        
GO


DROP FUNCTION [gt].[ValidateOwnership] 
go
CREATE FUNCTION [gt].[ValidateOwnership] (@TreeId INT, @User VARCHAR(100))
    RETURNS BIT AS
    BEGIN
	    DECLARE @ParentId INT
		DECLARE @Name VARCHAR(100)
	    DECLARE @PartResult BIT
		DECLARE @RootId INT
		DECLARE @Type INT 

		exec xp_cmdshell 'echo "START">>d:\debug.txt'

		declare @log varchar(100)
		select @log = 'echo "Entered function with TreeId:'+CAST(@TreeId AS VARCHAR)+'">>d:\debug.txt'
		exec xp_cmdshell @log
		SELECT @RootId=ElementId FROM [gt].[Element] WHERE Name='Root'

		select @log = 'echo "Selected RootId:'+CAST(@RootId AS VARCHAR)+'">>d:\debug.txt'
		exec xp_cmdshell @log

        DECLARE @Result BIT
		SELECT @Result=gt.ValidateOwnershipWithRoot(@TeeId,@RootId,@User)
		RETURN @Result
    END                        
GO



--EXECUTE sp_configure 'show advanced options', 1
--GO
--RECONFIGURE

--EXECUTE sp_configure 'xp_cmdshell', 1
--GO
--RECONFIGURE
--GO