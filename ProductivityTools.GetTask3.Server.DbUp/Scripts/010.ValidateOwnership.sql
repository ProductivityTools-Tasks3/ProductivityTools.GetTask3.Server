IF EXISTS (SELECT *
           FROM   sys.objects
           WHERE  object_id = OBJECT_ID(N'[gt].[ValidateOwnershipWithRoot]')
                  AND type IN ( N'FN', N'IF', N'TF', N'FS', N'FT' ))
DROP FUNCTION [gt].[ValidateOwnershipWithRoot] 
go
CREATE FUNCTION [gt].[ValidateOwnershipWithRoot] (@TreeId INT,@RootId INT, @User VARCHAR(100))
    RETURNS BIT AS
    BEGIN
	    DECLARE @ParentId INT
		DECLARE @Name VARCHAR(100)
	    DECLARE @PartResult BIT
		DECLARE @Type INT 

		exec xp_cmdshell 'echo "START">>d:\debug.txt'

		--declare @log varchar(100)
		--select @log = 'echo "Entered [ValidateOwnershipWithRoot1] function with TreeId:'+CAST(@TreeId AS VARCHAR)+'">>d:\debug.txt'
		--exec xp_cmdshell @log

		--declare @log3 varchar(100)
		--select @log3 = 'echo "Entered [ValidateOwnershipWithRoot1] function with User:'+@User+'">>d:\debug.txt'
		--exec xp_cmdshell @log3
	
        SELECT @ParentId=[ParentId],@Name=[Name],@Type=[Type]
        FROM [gt].[Element] where ElementId=@TreeId 

		
		--declare @log2 varchar(100)
		--select @log2 = 'echo "[ValidateOwnershipWithRoot] TreeId:'+CAST(@TreeId AS VARCHAR)+' ParentId'+CAST(@ParentId AS VARCHAR)+' RootId:'+CAST(@RootId AS VARCHAR)+',Name:'+CAST(@Name AS VARCHAR)+',Type:">>d:\debug.txt'
		--exec xp_cmdshell @log2

		If @ParentId IS NULL
			RETURN 0

		IF @ParentId = @RootId AND @Name=@User AND @Type=4
			RETURN 1
		ELSE
			BEGIN
				SELECT @PartResult = gt.[ValidateOwnershipWithRoot](@ParentId,@RootId, @User)
				RETURN @PartResult
			END
		
		RETURN 0
    END                        
GO

IF EXISTS (SELECT *
           FROM   sys.objects
           WHERE  object_id = OBJECT_ID(N'[gt].[ValidateOwnership]')
                  AND type IN ( N'FN', N'IF', N'TF', N'FS', N'FT' ))
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

		--exec xp_cmdshell 'echo "START">>d:\debug.txt'

		--declare @log varchar(100)
		--select @log = 'echo "Entered [ValidateOwnership] function with TreeId:'+CAST(@TreeId AS VARCHAR)+'">>d:\debug.txt'
		--exec xp_cmdshell @log
		SELECT @RootId=ElementId FROM [gt].[Element] WHERE Name='Root'

		--select @log = 'echo "Selected RootId:'+CAST(@RootId AS VARCHAR)+'">>d:\debug.txt'
		--exec xp_cmdshell @log

		
		--select @log = 'echo "User'+@User+'">>d:\debug.txt'
		--exec xp_cmdshell @log

        DECLARE @Result BIT
		SELECT @Result=gt.ValidateOwnershipWithRoot(@TreeId,@RootId,@User)
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