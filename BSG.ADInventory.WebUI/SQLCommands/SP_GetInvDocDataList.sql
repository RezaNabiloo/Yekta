USE [ADInventory]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetInvDocDataList]    Script Date: 18/06/1402 02:52:39 ب.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[SP_GetInvDocDataList](@DocType bigint)
as
begin
	select	i.Id,
			idt.Title InvDocType,
			i.DocNo,
			i.InvDocStatus,
			isnull(ct.Title,'') CarType,
			case	when i.InvDocTypeId in (4,5) then '' 
					when c.PlateCharacterId is null then ''
					else c.PlateSeries + '-' + c.PlateNumberPart2 + pc.Title + c.PlateNumberPart1 end CarPlateNumer,
			i.DriverName,
			p.Title Project,
			i.PurchaseDocId,
			case	when i.InvDocTypeId=6 then sp.Title
					when i.InvDocTypeId=1 then isnull(pr.Title,'')
					when i.InvDocTypeId=2 then p.Title
					else '' end Source,
			case	when i.InvDocTypeId=6 then dp.Title
					when i.InvDocTypeId=7 then isnull(pr.Title,'')
					else '' end Dest,
			case	when i.InvDocTypeId=1 then isnull(pr.Title,'')
					else '' end Provider,

			isnull(pu.FirstName,'')+' '+isnull(pu.LastName,'') Operator,
			case	when i.InvDocTypeId=4 then isnull(pl.FirstName,'')+' '+isnull(pl.LastName,'')
					when i.InvDocTypeId=6 then isnull(dp.Title,'')
					when i.InvDocTypeId=7 then isnull(pr.Title,'')
					else '' end Reciver,
			
			i.CreateTime,
			(select count(1) from InvDocAttachments ia where ia.InvDocId=i.Id) Attachments,
			dbo.Fn_GetSummaryInvDocItems(i.Id) ItemsSummary

			
	from InvDocs i
	left join InvDocTypes idt on idt.Id=i.InvDocTypeId
	left join CarTypes ct on ct.Id=i.CarTypeId
	left join Cars c on c.Id=i.CarId
	left join PlateCharacters pc on pc.Id=c.PlateCharacterId
	left join Projects p on p.Id=i.ProjectId
	left join Projects sp on sp.Id=i.SourceProjectId
	left join Projects dp on dp.Id=i.DestProjectId
	left join Providers pr on pr.Id=i.ProviderId
	left join Users u on u.Id=i.CreateUserId
	left join people pu on pu.Id=u.PeopleId
	left join People pl on pl.Id=u.PeopleId

	where i.InvDocTypeId = @DocType or @DocType=0
end