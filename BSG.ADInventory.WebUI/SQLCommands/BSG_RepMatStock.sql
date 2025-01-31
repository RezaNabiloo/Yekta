USE [ADInventory]
GO
/****** Object:  StoredProcedure [dbo].[BSG_RepMatStock]    Script Date: 21/05/1402 05:15:56 ب.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER  proc [dbo].[BSG_RepMatStock](@MatId bigint, @ProjectId bigint, @MatGroupIds varchar(max))
as
begin

	select  m.Id,
			cast(m.Id as varchar) MatCode,
			m.Title,
			p.Title Project,			
			mu.Title MatUnit,
			mg.Title MatGroup,
			cast(sum(case when dt.Sign=1 then dd.RealAmount else dd.Amount end *dt.Sign) as float) Qty		
	from InvDocDetails dd
	left join InvDocs d on d.Id=dd.InvDocId
	left join InvDocTypes dt on dt.Id=d.InvDocTypeId
	left join Projects p on p.Id=d.ProjectId
	left join Mats m on m.Id=dd.MatId
	left join MatGroups mg on mg.Id=m.MatGroupId
	left join MatUnits mu on mu.Id=m.MatUnitId
	where (dd.MatId=@MatId or @MatId=0)	and (d.ProjectId=@ProjectId or isnull(@ProjectId,0)=0) and ((dt.Sign=1 and d.InvDocStatus=2) or dt.Sign=-1)
	and ( 
		exists (select top 1 1 from dbo.Fn_SplitString(@MatGroupIds,',') sp where sp.splitdata=cast(m.MatGroupId as varchar) ) 
		or @MatGroupIds=''
		)
	group by m.Id, m.Code, m.Title, p.Title, mu.Title, mg.Title
	



end
