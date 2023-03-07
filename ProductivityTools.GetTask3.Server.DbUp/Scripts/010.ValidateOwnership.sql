CREATE FUNCTION [gt].[ValidateOwnership] (@TreeId INT, @Name VARCHAR(100))
    RETURNS BOOL AS
    BEGIN
	    DECLARE @ParentId INT
	    DECLARE @PartResult BIT
		DECLARE @RootId INT

		SELECT @RootId=ElementId FROM [gt].[Element] WHERE Name='Root'

        SELECT [ElementId],@ParentId=[ParentId],[Name],[Type] 
        FROM [GetTask3].[gt].[Element] where ElementId=@TreeId

		IF @ParentId =@RootId
			RETURN TRUE
		ELSE
			@PartResult=gt.[ValidateOwnership](@ParentId, @Name)
		
     RETURN TRUE
    END                        
GO