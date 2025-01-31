
ALTER function [dbo].[Fn_GetSummaryInvDocItems](@Id bigint)
returns varchar(max)
as
begin
	DECLARE @Items VARCHAR(Max) 
	SELECT @Items = COALESCE(@Items + ' | ', '') + MatTitle
	FROM (
	select distinct(m.Title) MatTitle
	from InvDocDetails d	
	left join Mats m on m.Id=d.MatId
	where d.InvDocId=@Id
	) t

	return @Items
end