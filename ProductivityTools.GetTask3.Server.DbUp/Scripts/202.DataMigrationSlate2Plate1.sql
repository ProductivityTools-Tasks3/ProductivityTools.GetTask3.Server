﻿Update PTTasks3.[gt].[Element] set Details=REPLACE(Details,'"type":"link","href":','"type":"a","url":') 
Update PTTasks3.[gt].[Element] set Details=REPLACE(Details,'"type":"paragraph"','"type":"p"') 
Update PTTasks3.[gt].[Element] set Details=REPLACE(Details,'"type":"headingOne"','"type":"h1"') 
Update PTTasks3.[gt].[Element] set Details=REPLACE(Details,'"type":"headingTwo"','"type":"h2"')
Update PTTasks3.[gt].[Element] set Details=REPLACE(Details,'"type":"headingThree"','"type":"h3"')
Update PTTasks3.[gt].[Element] set Details=REPLACE(Details,'"type":"orderedList"','"type":"ul"')
Update PTTasks3.[gt].[Element] set Details=REPLACE(Details,'"type":"unorderedList"','"type":"ul"')
Update PTTasks3.[gt].[Element] set Details=REPLACE(Details,'"type":"table-row"','"type":"tr"') 
Update PTTasks3.[gt].[Element] set Details=REPLACE(Details,'"type":"table-cell"','"type":"td"') 


update PTTasks3.[gt].[Element] set Details=dbo.UpdateList(Details)
update PTTasks3.[gt].[Element] set Details=dbo.UpdateList(Details)-- where PageId=3145
update PTTasks3.[gt].[Element] set Details=dbo.UpdateList(Details)
update PTTasks3.[gt].[Element] set Details=dbo.UpdateList(Details)
update PTTasks3.[gt].[Element] set Details=dbo.UpdateList(Details)
update PTTasks3.[gt].[Element] set Details=dbo.UpdateList(Details)
update PTTasks3.[gt].[Element] set Details=dbo.UpdateList(Details)
update PTTasks3.[gt].[Element] set Details=dbo.UpdateList(Details)	
update PTTasks3.[gt].[Element] set Details=dbo.UpdateList(Details)
update PTTasks3.[gt].[Element] set Details=dbo.UpdateList(Details)
update PTTasks3.[gt].[Element] set Details=dbo.UpdateList(Details)
update PTTasks3.[gt].[Element] set Details=dbo.UpdateList(Details)
update PTTasks3.[gt].[Element] set Details=dbo.UpdateList(Details)
update PTTasks3.[gt].[Element] set Details=dbo.UpdateList(Details)
update PTTasks3.[gt].[Element] set Details=dbo.UpdateList(Details)	
update PTTasks3.[gt].[Element] set Details=dbo.UpdateList(Details)
