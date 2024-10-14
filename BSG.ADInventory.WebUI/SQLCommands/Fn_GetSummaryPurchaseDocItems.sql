create function [dbo].[Fn_GetSummaryPurchaseDocItems](@Id bigint)
returns varchar(max)
as
begin
	DECLARE @Items VARCHAR(Max) 
	SELECT @Items = COALESCE(@Items + ' | ', '') + MatTitle
	FROM (
	select distinct(m.Title) MatTitle
	from PurchaseDocDetails d	
	left join Mats m on m.Id=d.MatId
	where d.PurchaseDocId=@Id
	) t

	return @Items
end