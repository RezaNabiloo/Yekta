USE [ADInventory]
GO
/****** Object:  StoredProcedure [dbo].[BSG_RepMatOnWay]    Script Date: 23/07/1402 08:59:10 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[BSG_RepMatOnWay]
as
begin

	select t.* from 
	(
		select  p.Id PurchaseDocId,
				p.IsAggregated,
				m.Id MatId,
				m.Title MatTitle,
				mu.Title MatUnit,
				cast(pd.Amount as float) PurchaseQty,
				pr.Title ProjectTitle,
				pro.Title ProviderName,
				p.CreateTime,
				isnull((select sum(cast(ind.RealAmount as float)) from InvDocs i
					left join InvDocDetails ind on ind.InvDocId = i.Id 
					where i.PurchaseDocId=p.Id and ind.MatId=pd.MatId), 0) as ReciveQty,
				mo.Id RequestId
				
				

		from PurchaseDocDetails pd
		left join PurchaseDocs p on p.Id=pd.PurchaseDocId
		left join Mats m on m.Id=pd.MatId
		left join MatUnits mu on mu.Id=m.MatUnitId
		left join Providers pro on pro.Id = p.ProviderId
		left join Projects pr on pr.Id = p.ProjectId
		left join MatOrderDetails modd on modd.Id = pd.MatOrderDetailId
		left join MatOrders mo on mo.Id = modd.MatOrderId
		where p.IsFinished = 0

	) t where t.PurchaseQty > t.ReciveQty
		
		

end
