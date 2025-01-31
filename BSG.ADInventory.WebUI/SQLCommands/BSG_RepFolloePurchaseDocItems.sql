USE [ADInventory]
GO
/****** Object:  StoredProcedure [dbo].[BSG_RepFollowPurchaseDoc]    Script Date: 23/05/1402 11:28:23 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[BSG_RepFollowPurchaseDoc](@StartDate varchar(10), @EndDate varchar(10), @PurchaseDocId bigint, @MatId bigint, @RepType int)
as
begin
	
	declare @JStartDate date = case when dbo.s2j(@StartDate)='' then null else dbo.s2j(@StartDate) end
	declare @JEndDate date = case when dbo.s2j(@EndDate)='' then null else dbo.s2j(@EndDate) end
	
	 --=========================================
	 -- Follow PurchaseDoc
	 --=========================================
	 if @RepType = 1
	 begin
	 select pri.*,
			cast(isnull((select sum(id.RealAmount) from invdocs i left join InvDocDetails id on i.Id=id.InvDocId where i.PurchaseDocId=@PurchaseDocId and id.MatId=pri.MatId and i.InvDocTypeId=1),0) as real) ReciveQty,
			cast(isnull((select sum(id.RealAmount) from invdocs i left join InvDocDetails id on i.Id=id.InvDocId where i.PurchaseDocId=@PurchaseDocId and id.MatId=pri.MatId and i.InvDocTypeId=6),0) as real) DistribQty,
			cast(isnull((select sum(id.RealAmount) from invdocs i left join InvDocDetails id on i.Id=id.InvDocId where i.PurchaseDocId=@PurchaseDocId and id.MatId=pri.MatId and i.InvDocTypeId=7),0) as real) ExitQty,
			m.Title MatTitle,
			mu.Title MatUnit			
 
	 from 
	 (
		select p.Id PurchaseDocId, p.IsAggregated, p.CreateTime, pr.Title Project, pd.MatId , cast(sum(pd.Amount) as real) PurchaseQty
		from PurchaseDocDetails pd
		left join PurchaseDocs p on p.Id=pd.PurchaseDocId
		left join Projects pr on pr.Id=p.ProjectId
		where (p.Id=@PurchaseDocId or @PurchaseDocId=0)
		and (pd.MatId=@MatId or @MatId=0)
		and ((cast(p.CreateTime as date) between @JStartDate and @JEndDate) or (@StartDate='' and @EndDate=''))
		group by p.Id, p.IsAggregated, p.CreateTime, pr.Title, pd.MatId		
		) pri
		left join Mats m on pri.MatId=m.Id
		left join MatUnits mu on mu.Id=m.MatUnitId

		order by pri.CreateTime desc

	end

	 --=========================================
	 -- PurchaseDoc Details
	 --=========================================
	 if @RepType = 2
	 begin	 
		select	p.Id PurchaseDocId, 
				ppr.Title Project,
				p.IsAggregated,
				pd.MatId , 
				m.Title MatTitle, 
				mu.Title MatUnit,
				pr.Title RequestProject,
				mr.Id MatOrderId,				
				cast(mrd.RequiredAmount as real) RequestedAmount,
				cast(mrd.ConfirmedAmount as real) ConfirmedAmount,
				cast(pd.Amount as real) PurchaseAmount,
				mr.RequiredDate RequiredDate
		from PurchaseDocs p
		left join PurchaseDocDetails pd on p.Id=pd.PurchaseDocId
		left join MatOrderDetails mrd on mrd.Id=pd.MatOrderDetailId
		left join MatOrders mr on mr.Id=mrd.MatOrderId
		left join Projects pr on pr.Id=mr.ProjectId
		left join Projects ppr on ppr.Id=p.ProjectId
		left join Mats m on pd.MatId=m.Id
		left join MatUnits mu on mu.Id=m.MatUnitId
		where (p.Id=@PurchaseDocId or @PurchaseDocId=0)
		and ((cast(p.CreateTime as date) between @JStartDate and @JEndDate) or (@StartDate='' and @EndDate=''))
		and (pd.MatId=@MatId or @MatId=0)
	end

	 --=========================================
	 -- Inv Docs For PurchaseDoc
	 --=========================================
	 if @RepType = 3
	 begin	 
		select	i.DocNo DocNo, 
				i.PurchaseDocId,
				idt.Title InvDocType,
				i.InvDocStatus,
				pr.Title Project,				
				ind.MatId, 
				m.Title MatTitle, 
				mu.Title MatUnit,
				pr.Title Project,
				isnull(ps.Title, isnull(pp.Title,'')) Source,
				isnull(pd.Title, isnull(pp.Title,'')) Dest,				
				cast(ind.RealAmount as real) Amount,
				i.CreateTime,
				case when i.InvDocStatus=1 then null else i.LastUpdateTime end ConfirmTime
		from InvDocs i
		left join InvDocDetails ind on ind.InvDocId=i.Id		
		left join InvDocTypes idt on idt.Id=i.InvDocTypeId
		left join Projects pr on pr.Id=i.ProjectId
		left join Projects ps on ps.Id=i.SourceProjectId
		left join Projects pd on pd.Id=i.DestProjectId
		left join Mats m on ind.MatId=m.Id
		left join MatUnits mu on mu.Id=m.MatUnitId
		left join Providers pp on pp.Id=i.ProviderId
		where (i.PurchaseDocId=@PurchaseDocId or @PurchaseDocId=0) and i.PurchaseDocId is not null
		and (ind.MatId=@MatId or @MatId=0)
		and ((cast(i.CreateTime as date) between @JStartDate and @JEndDate) or (@StartDate='' and @EndDate=''))
		order by i.CreateTime
	end

	 --=========================================
	 -- Entrance Docs For PurchaseDoc
	 --=========================================
	 if @RepType = 4
	 begin	 
		select	i.DocNo DocNo, 
				i.PurchaseDocId,
				idt.Title InvDocType,
				i.InvDocStatus,
				pr.Title Project,				
				ind.MatId, 
				m.Title MatTitle, 
				mu.Title MatUnit,
				pr.Title Project,
				isnull(ps.Title, isnull(pp.Title,'')) Source,
				isnull(pd.Title, isnull(pp.Title,'')) Dest,				
				cast(ind.RealAmount as real) Amount,
				i.CreateTime,
				case when i.InvDocStatus=1 then null else i.LastUpdateTime end ConfirmTime
		from InvDocs i
		left join InvDocDetails ind on ind.InvDocId=i.Id		
		left join InvDocTypes idt on idt.Id=i.InvDocTypeId
		left join Projects pr on pr.Id=i.ProjectId
		left join Projects ps on ps.Id=i.SourceProjectId
		left join Projects pd on pd.Id=i.DestProjectId
		left join Mats m on ind.MatId=m.Id
		left join MatUnits mu on mu.Id=m.MatUnitId
		left join Providers pp on pp.Id=i.ProviderId		
		where (i.PurchaseDocId=@PurchaseDocId or @PurchaseDocId=0) and i.PurchaseDocId is not null
		and (ind.MatId=@MatId or @MatId=0)
		and i.InvDocTypeId in (1,2,5)
		and ((cast(i.CreateTime as date) between @JStartDate and @JEndDate) or (@StartDate='' and @EndDate=''))
		order by i.CreateTime
	end

	
	 --=========================================
	 -- Distrib Docs For PurchaseDoc
	 --=========================================
	 if @RepType = 5
	 begin	 
		select	i.DocNo DocNo, 
				i.PurchaseDocId,
				idt.Title InvDocType,
				i.InvDocStatus,
				pr.Title Project,				
				ind.MatId, 
				m.Title MatTitle, 
				mu.Title MatUnit,
				pr.Title Project,
				isnull(ps.Title, isnull(pp.Title,'')) Source,
				isnull(pd.Title, isnull(pp.Title,'')) Dest,				
				cast(ind.RealAmount as real) Amount,
				i.CreateTime,
				case when i.InvDocStatus=1 then null else i.LastUpdateTime end ConfirmTime
		from InvDocs i
		left join InvDocDetails ind on ind.InvDocId=i.Id		
		left join InvDocTypes idt on idt.Id=i.InvDocTypeId
		left join Projects pr on pr.Id=i.ProjectId
		left join Projects ps on ps.Id=i.SourceProjectId
		left join Projects pd on pd.Id=i.DestProjectId
		left join Mats m on ind.MatId=m.Id
		left join MatUnits mu on mu.Id=m.MatUnitId
		left join Providers pp on pp.Id=i.ProviderId
		where (i.PurchaseDocId=@PurchaseDocId or @PurchaseDocId=0) and i.PurchaseDocId is not null
		and (ind.MatId=@MatId or @MatId=0)
		and i.InvDocTypeId=6
		and ((cast(i.CreateTime as date) between @JStartDate and @JEndDate) or (@StartDate='' and @EndDate=''))
		order by i.CreateTime
	end

	 --=========================================
	 -- ExternalBOL Docs For PurchaseDoc
	 --=========================================
	 if @RepType = 6
	 begin	 
		select	i.DocNo DocNo, 
				i.PurchaseDocId,
				idt.Title InvDocType,
				i.InvDocStatus,
				pr.Title Project,				
				ind.MatId, 
				m.Title MatTitle, 
				mu.Title MatUnit,
				pr.Title Project,
				isnull(ps.Title, isnull(pp.Title,'')) Source,
				isnull(pd.Title, isnull(pp.Title,'')) Dest,				
				cast(ind.RealAmount as real) Amount,
				i.CreateTime,
				case when i.InvDocStatus=1 then null else i.LastUpdateTime end ConfirmTime
		from InvDocs i
		left join InvDocDetails ind on ind.InvDocId=i.Id		
		left join InvDocTypes idt on idt.Id=i.InvDocTypeId
		left join Projects pr on pr.Id=i.ProjectId
		left join Projects ps on ps.Id=i.SourceProjectId
		left join Projects pd on pd.Id=i.DestProjectId
		left join Mats m on ind.MatId=m.Id
		left join MatUnits mu on mu.Id=m.MatUnitId
		left join Providers pp on pp.Id=i.ProviderId
		where (i.PurchaseDocId=@PurchaseDocId or @PurchaseDocId=0) and i.PurchaseDocId is not null
		and (ind.MatId=@MatId or @MatId=0)
		and InvDocTypeId=7
		and ((cast(i.CreateTime as date) between @JStartDate and @JEndDate) or (@StartDate='' and @EndDate=''))
		order by i.CreateTime
	end
end