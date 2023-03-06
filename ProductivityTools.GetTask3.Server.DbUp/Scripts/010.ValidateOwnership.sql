USE [PTMeetings]
GO

/****** Object:  UserDefinedFunction [jl].[GetTreePath]    Script Date: 06.03.2023 18:26:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [gt].[ValidateOwnership] (@TreeId INT, @Name VARCHAR(100))
    RETURNS BOOL AS
    BEGIN
	    DECLARE @ParentId INT
	    
        SELECT [ElementId],[ParentId],[Name],[Type] 
        FROM [GetTask3].[gt].[Element] where ElementId=@TreeId

        


	                            RETURN TRUE
                            END
                            
GO


