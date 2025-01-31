USE [ADInventory]
GO
/****** Object:  StoredProcedure [dbo].[BSG_RepPurchaseDoc]    Script Date: 22/07/1402 06:09:53 ب.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[BSG_RepPurchaseDoc](@Id bigint)
as
begin

	select	p.Id,
			p.IsAggregated,
			pr.Title ProviderName,
			m.Id MatCode,
			m.Title+ ' ' + isnull(m.TechnicalSpec,'') MatTitle,
			mu.Title MatUnit,
			pd.Amount,
			mo.Id MatOrderId,
			pro.Title ProjectTitle,
			dbo.j2s(p.CreateTime) CreateTime,			
			p.Description

	from PurchaseDocs p
	left join PurchaseDocDetails pd on pd.PurchaseDocId =p.Id
	left join Providers pr on pr.Id=p.ProviderId
	left join Mats m on m.Id=pd.MatId
	left join MatUnits mu on mu.Id=m.MatUnitId
	left join MatOrderDetails modd on modd.Id = pd.MatOrderDetailId
	left join MatOrders mo on mo.Id = modd.MatOrderId
	left join Projects pro on pro.Id =p.ProjectId


	where p.Id=@Id

end
