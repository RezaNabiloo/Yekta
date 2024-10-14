alter proc [dbo].[BSG_RepMatUse](@MatId bigint, @ProjectId bigint, @MatGroupIds varchar(max))
as
begin

	select  m.Id,
			m.Code,
			m.Title,
			p.Title Project,			
			mu.Title MatUnit,
			mg.Title MatGroup,
			cast(sum(dd.RealAmount*dt.Sign) as float) Qty		
			 
	from InvDocDetails dd
	left join InvDocs d on d.Id=dd.InvDocId
	left join InvDocTypes dt on dt.Id=d.InvDocTypeId
	left join Projects p on p.Id=d.ProjectId
	left join Mats m on m.Id=dd.MatId
	left join MatGroups mg on mg.Id=m.MatGroupId
	left join MatUnits mu on mu.Id=m.MatUnitId
	where (dd.MatId=@MatId or @MatId=0)	and (d.ProjectId=@ProjectId or @ProjectId=0)
	and dt.Id>3
	and ( 
		exists (select top 1 1 from dbo.Fn_SplitString(@MatGroupIds,',') sp where sp.splitdata=cast(m.MatGroupId as varchar) ) 
		or @MatGroupIds=''
		)
	group by m.Id, m.Code, m.Title, p.Title, mu.Title, mg.Title
	



end
