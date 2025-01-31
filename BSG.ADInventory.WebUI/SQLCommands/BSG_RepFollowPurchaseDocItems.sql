USE [ADInventory]
GO
/****** Object:  StoredProcedure [dbo].[BSG_RepFolloePurchaseDoc]    Script Date: 22/05/1402 11:52:55 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[BSG_RepFolloePurchaseDoc](@PurchaseDocId bigint)
as
begin
 select pri.*,		
		isnull((select sum(id.RealAmount) from invdocs i left join InvDocDetails id on i.Id=id.InvDocId where i.PurchaseDocId=@PurchaseDocId and id.MatId=pri.MatId and i.InvDocTypeId=1),0) ReciveQty,
		isnull((select sum(id.RealAmount) from invdocs i left join InvDocDetails id on i.Id=id.InvDocId where i.PurchaseDocId=@PurchaseDocId and id.MatId=pri.MatId and i.InvDocTypeId=6),0) DistribQty,
		isnull((select sum(id.RealAmount) from invdocs i left join InvDocDetails id on i.Id=id.InvDocId where i.PurchaseDocId=@PurchaseDocId and id.MatId=pri.MatId and i.InvDocTypeId=7),0) ExitQty,
		m.Title MatTitle,
		mu.Title MatUnit
 
 from 
 (
	select p.Id PurchaseDocId, pr.Title Project, pd.MatId , cast(sum(pd.Amount) as real) PurchaseQty
	from PurchaseDocDetails pd
	left join PurchaseDocs p on p.Id=pd.PurchaseDocId
	left join Projects pr on pr.Id=p.ProjectId
	where p.Id=@PurchaseDocId
	group by p.Id, pr.Title, pd.MatId) pri
	left join Mats m on pri.MatId=m.Id
	left join MatUnits mu on mu.Id=m.MatUnitId

	
	
end