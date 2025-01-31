USE [ADInventory]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetPurchaseDocDataList]    Script Date: 10/07/1403 10:21:21 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[SP_GetPurchaseDocDataList](@Type bigint)
as
begin
	select	i.Id,			
            isnull(pr.Title,'') Provider,
            isnull(pr.Title,'') Project,
            i.IsAggregated,
            i.CreateTime,
			isnull(up.FirstName,'')+' '+isnull(up.LastName,'') CreateUser,
			isnull(aup.FirstName,'')+' '+isnull(aup.LastName,'') AgentUser,
			dbo.Fn_GetSummaryPurchaseDocItems(i.Id) ItemsSummary,
            (select count(1) from PurchaseDocAttachments pa where pa.PurchaseDocId=i.Id) Attachments,
			i.IsFinished,
			@type type
			

			
	from PurchaseDocs i	
	left join Providers pr on pr.Id=i.ProviderId
	left join projects p on p.Id=i.ProjectId
	left join Users u on u.Id=i.CreateUserId
	left join People up on up.Id=u.PeopleId

	left join Users au on au.Id=i.AgentUserId
	left join People aup on aup.Id=au.PeopleId
	
	where (i.IsFinished = 0 and @Type = 1) or @Type = 0
	
end