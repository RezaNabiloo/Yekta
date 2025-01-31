USE [ADInventory]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetPurchaseDocDataList]    Script Date: 18/06/1402 02:52:18 ب.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[SP_GetPurchaseDocDataList](@Type int)
as
begin
	select	i.Id,			
            isnull(pr.Title,'') Provider,
            isnull(pr.Title,'') Project,
            i.IsAggregated,
            i.CreateTime,
			isnull(up.FirstName,'')+' '+isnull(up.LastName,'') CreateUser,
			dbo.Fn_GetSummaryPurchaseDocItems(i.Id) ItemsSummary,
            (select count(1) from PurchaseDocAttachments pa where pa.PurchaseDocId=i.Id) Attachments
			

			
	from PurchaseDocs i	
	left join Providers pr on pr.Id=i.ProviderId
	left join projects p on p.Id=i.ProjectId
	left join Users u on u.Id=i.CreateUserId
	left join People up on up.Id=u.PeopleId
	
	where (i.IsFinished = 0 and type = 1) or type = 0
	
end