USE [ADInventory]
GO
/****** Object:  StoredProcedure [dbo].[BSG_RepInvDoc]    Script Date: 17/08/1401 04:44:35 ب.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[BSG_RepInvDoc](@Id bigint)
as
begin

	select d.Id,
			d.InvDocTypeId,
			dt.Title InvDocTypeTitle,
			d.DocNo,
			d.WeighbridgeNo,
			d.ReferenceInternalDocNo,
			p.Title Project,
			pr.Title Provider,
			--i.Title Inventory,
			isnull(p.Title,'')+' '+isnull(t.Title,'') SourcePlace,
			d.DriverName,
			d.DriverPhone,
			d.CreateTime,
			isnull(po.FirstName,'') +' '+isnull(po.LastName,'') Operator,
			case when d.PlateCharacterId is not null 
				 and d.PlateSeries<>''
				 and d.PlateNumberPart1<>''
				 and d.PlateNumberPart2<>''
				 then 'ایران' +d.PlateSeries+'-'+PlateNumberPart2+pc.Title+d.PlateNumberPart1
				 else '' end CarPlateNumber,	
           

		    pro.Title Province,
		    t.Title Township,
			m.Id MatId,
			m.Code MatCode,
			m.Title MatTitle,
			m.Dimention,
			m.TechnicalSpec,
			mu.Title MatUnit,
			dd.Amount,
			dd.RealAmount,
			isnull(ct.Title,'') CarType,
			d.Description,
			isnull(pu.FirstName,'') +' '+isnull(pu.LastName,'') PeopleInfo,
			isnull(pd.Title,'') ProjectDetailTitle,
			case when d.InvDocTypeId=6 then isnull(dp.Title,'') else '' end DestProject,
			case when d.InvDocTypeId=6 then dp.Title +' '+ dp.Address
				 when d.InvDocTypeId=7 then isnull(pr.Title,'') +' '+ isnull(ppr.Title,'')+' ' + isnull(tt.Title,'') +' '+ isnull(pr.Address,'') else '' end Reciver,
			case when d.InvDocTypeId=7 then isnull(pr.Tell,'') else '' end ReciverTell
			
			
	from InvDocs d
	left join InvDocTypes dt on dt.Id=d.InvDocTypeId
	left join InvDocDetails dd on dd.InvDocId=d.Id
	left join Mats m on m.Id = dd.MatId
	left join MatUnits mu on mu.Id=m.MatUnitId
	left join Projects p on p.Id=d.ProjectId
	left join CarTypes ct on ct.id=d.CarTypeId
	left join Providers pr on pr.Id=d.ProviderId
	left join Townships t on t.Id=p.TownshipId
	left join Provinces pro on pro.Id=t.ProvinceId
	left join Users u on u.Id=d.CreateUserId
	left join people po on po.id=u.PeopleId
	left join PlateCharacters pc on pc.id=d.PlateCharacterId
	left join people pu on po.id=d.PeopleId
	left join ProjectDetails pd on pd.Id=dd.ProjectDetailId
	left join projects dp on dp.Id=d.DestProjectid
	left join Townships tt on tt.Id=pr.TownshipId
	left join Provinces ppr on ppr.id=tt.ProvinceId
	where d.Id=@Id

end
