﻿INSERT INTO [GetTask3].[gt].[Element] (Name,Type,Status,Created) VALUES ('root',2,0,GETDATE())
  INSERT INTO [GetTask3].[gt].[Element] (Name,Type,Status,Created,Initialization,ParentId) VALUES ('pwujczyk',2,10,GETDATE(),GETDATE(),(select top 1 elementId from gt.Element))