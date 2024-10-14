namespace BSG.ADInventory.Services
{
    using BSG.ADInventory.Data;
    using BSG.ADInventory.Entities;
    using BSG.ADInventory.Models.InvDoc;
    using BSG.ADInventory.Resources;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Zcf.Data;
    using Zcf.Data.Extensions;
    using Zcf.Models;
    using Zcf.Paging;
    using Zcf.Services;


    /// <summary>
    /// Create Service For InvDoc" />
    /// </summary>
    public class InvDocService : CrudService<InvDoc>, IInvDocService
    {
        private readonly IInvDocRepository invDocRepository;
        private readonly IInvDocDetailRepository invDocDetailRepository;
        private readonly IInvDocTypeRepository invDocTypeRepository;
        private readonly IUserRepository userRepository;


        /// <summary>
        /// Initializes a new instance of the <see cref="InvDocService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="InvDocRepository"></param>
        public InvDocService(IUnitOfWork unitOfWork, IInvDocRepository invDocRepository, IInvDocDetailRepository invDocDetailRepository,
            IInvDocTypeRepository invDocTypeRepository,
            IUserRepository userRepository)
            : base(unitOfWork, invDocRepository)
        {
            this.invDocRepository = invDocRepository;
            this.invDocDetailRepository = invDocDetailRepository;
            this.invDocTypeRepository = invDocTypeRepository;
            this.userRepository = userRepository;
        }

        /// <summary>
        /// Gets an <see cref="IPagedQueryable" /> instance according to the specified criteria.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public override IPagedQueryable<InvDoc> GetItems(PagedQueryable criteria)
        {
            var data = this.invDocRepository.Query;
            return data.ToPagedQueryable(criteria);
        }

        public IEnumerable<InvDocListModel> GetDataList(long docType)
        {
            var userProjects = this.userRepository.GetCurrentUser().Projects.Select(p => p.Id).ToList();
            try
            {
                var data = this.invDocRepository.Query
                    .Where(d => d.InvDocTypeId == docType && userProjects.Contains(d.ProjectId))
                    .OrderByDescending(d => d.Id)
                    .Select(d => new InvDocListModel
                    {
                        Id = d.Id,
                        InvDocType = d.InvDocType.Title,
                        DocNo = d.DocNo,
                        InvDocStatus = d.InvDocStatus,
                        ReferenceInternalDocNo = d.ReferenceInternalDocNo,
                        CarType = (docType == 4 || docType == 5) ? string.Empty : (d.CarTypeId == null ? "" : d.CarType.Title),
                        CarPlateNumer = (docType == 4 || docType == 5) ? string.Empty : (d.PlateCharacter == null ? string.Empty : Resource.Iran + d.PlateSeries + "-" + d.PlateNumberPart2 + d.PlateCharacter.Title + d.PlateNumberPart1),
                        DriverName = d.DriverName,
                        Source = docType == 1 ? (d.Provider == null ? "" : d.Provider.Township.Title + "-" + d.Provider.Township.Province.Title) : (docType == 2 ? d.Project.Title : string.Empty),
                        Dest = docType == 6 ? (d.DestProjectId == null ? "" : d.DestProject.Township.Title + "-" + d.DestProject.Township.Province.Title) : (docType == 7 ? d.Provider.Township.Title + "-" + d.Provider.Township.Province.Title : string.Empty),
                        Provider = docType == 1 ? (d.Provider == null ? "" : d.Provider.Title) : string.Empty,
                        Operator = d.CreateUser == null ? string.Empty : d.CreateUser.People.FirstName + " " + d.CreateUser.People.LastName,
                        Reciver = (docType == 4 ? d.People.FirstName + " " + d.People.LastName
                                    : (docType == 6 ? (d.DestProjectId == null ? "" : d.DestProject.Title) : (docType == 7 ? d.Provider.Title : string.Empty))
                                    ),
                        CreateTime = d.CreateTime,
                        Attachments = d.InvDocAttachments.Count()

                    }).ToList();

                return data;

            }
            catch (System.Exception ex)
            {
                var err = ex;
                //throw;
                return null;
            }

        }

        public CustomResult<bool> SetEntranceDocData(EntranceDocDataModel dataModel)
        {
            CustomResult<bool> data = new CustomResult<bool>();
            InvDoc invDoc = new InvDoc();
            var invDocType = this.invDocTypeRepository.GetItemByKey(dataModel.InvDocTypeId);

            // Edit
            if (dataModel.Id > 0)
            {
                var oldDoc = this.invDocRepository.GetItemByKey(dataModel.Id);
                if (oldDoc.InvDocStatus == Common.Enum.InvDocStatus.Definitive)
                {
                    data.Result = false;
                    data.ErrorMessage = Resource.DocIsLockedCanNotChange;
                    return data;
                }

                //AutoMapper.Mapper.CreateMap<EntranceDocDataModel, InvDoc>();//.ForMember(x => x.CreateTime, opt => opt.Ignore()).ForMember(x => x.LastUpdateTime, opt => opt.Ignore());
                //oldDoc= AutoMapper.Mapper.Map<InvDoc>(dataModel);
                //invDoc = oldDoc;
                //invDoc.InvDocDetails.Clear();

                foreach (var item in oldDoc.InvDocDetails.ToList())
                {
                    this.invDocDetailRepository.Remove(item);
                }

                //invDoc.InvDocDetails.Clear();

                invDoc = oldDoc;

                invDoc.ProjectId = dataModel.ProjectId;
                invDoc.ProviderId = dataModel.ProviderId;
                invDoc.CarTypeId = dataModel.CarTypeId;
                invDoc.SendByOrgCar = dataModel.SendByOrgCar;
                invDoc.DriverName = dataModel.DriverName;
                invDoc.DriverPhone = dataModel.DriverPhone;
                invDoc.PlateSeries = dataModel.PlateSeries;
                invDoc.PlateCharacterId = dataModel.PlateCharacterId;
                invDoc.PlateNumberPart1 = dataModel.PlateNumberPart1;
                invDoc.PlateNumberPart2 = dataModel.PlateNumberPart2;
                invDoc.WeighbridgeNo = dataModel.WeighbridgeNo;
                invDoc.Description = dataModel.Description;


            }

            // Create
            else
            {
                var currentDate = DateTime.Now.ToString();
                var finYear = Int32.Parse(currentDate.Substring(0, 4));
                if (Int32.Parse(currentDate.Substring(5, 2)) >= 11)
                {
                    finYear++;
                }
                dataModel.FinanceYear = finYear;
                var doc = this.invDocRepository.Query.Where(d => d.FinanceYear == dataModel.FinanceYear).Max(d => d.DocNo);
                string MaxId = "";
                if (doc == null)
                {
                    MaxId = dataModel.FinanceYear.ToString() + "000001";
                }
                else
                {
                    MaxId = Common.Extention.Numberic.NextInvDocId(doc);
                }
                invDoc = new InvDoc
                {
                    InvDocTypeId = dataModel.InvDocTypeId,
                    FinanceYear = dataModel.FinanceYear,
                    DocNo = MaxId,
                    ProviderId = dataModel.ProviderId,
                    ProjectId = dataModel.ProjectId,
                    CarTypeId = dataModel.CarTypeId,
                    DriverName = dataModel.DriverName,
                    DriverPhone = dataModel.DriverPhone,
                    PlateCharacterId = dataModel.PlateCharacterId,
                    PlateSeries = dataModel.PlateSeries,
                    PlateNumberPart1 = dataModel.PlateNumberPart1,
                    PlateNumberPart2 = dataModel.PlateNumberPart2,
                    Description = dataModel.Description
                };

            }

            foreach (var item in dataModel.Mats)
            {
                if (invDocType.Sign < 0)
                {
                    if (item.RealAmount > GetMatInvQty(item.MatId, invDoc.ProjectId))
                    {
                        data.Result = false;
                        data.ErrorMessage = Resource.MatQtyIsNotEnough + "(" + Resource.MatCode + " : " + item.MatId.ToString() + ")";
                        return data;
                    }
                }

                InvDocDetail det = new InvDocDetail
                {
                    InvDoc = invDoc,
                    MatId = item.MatId,
                    Amount = item.Amount,
                    RealAmount = item.RealAmount
                };
                invDoc.InvDocDetails.Add(det);
            }


            try
            {
                if (dataModel.Id > 0)
                {
                    this.invDocRepository.Update(invDoc);
                }
                else
                {
                    this.invDocRepository.Add(invDoc);
                }
                this.UnitOfWork.Commit();
                data.Result = true;
                data.ErrorMessage = Resource.DataSavedSuccessfully;

            }
            catch (System.Exception ex)
            {
                var err = ex;
                //throw;

                data.Result = false;
                data.ErrorMessage = ex.Message;
            }

            return data;

        }

        public CustomResult<bool> SetInternalEntranceDocData(InternalEntranceDocDataModel dataModel)
        {
            CustomResult<bool> data = new CustomResult<bool>();
            InvDoc invDoc = new InvDoc();
            var invDocType = this.invDocTypeRepository.GetItemByKey(dataModel.InvDocTypeId);

            // Edit
            if (dataModel.Id > 0)
            {
                var oldDoc = this.invDocRepository.GetItemByKey(dataModel.Id);
                if (oldDoc.InvDocStatus == Common.Enum.InvDocStatus.Definitive)
                {
                    data.Result = false;
                    data.ErrorMessage = Resource.DocIsLockedCanNotChange;
                    return data;
                }


                foreach (var item in oldDoc.InvDocDetails.ToList())
                {
                    this.invDocDetailRepository.Remove(item);
                }

                //invDoc.InvDocDetails.Clear();

                invDoc = oldDoc;

                invDoc.ProjectId = dataModel.ProjectId;
                invDoc.ReferenceInternalDocNo = dataModel.ReferenceInternalDocNo;
                invDoc.SourceProjectId = dataModel.SourceProjectId;
                invDoc.CarTypeId = dataModel.CarTypeId;
                invDoc.SendByOrgCar = dataModel.SendByOrgCar;
                invDoc.DriverName = dataModel.DriverName;
                invDoc.DriverPhone = dataModel.DriverPhone;
                invDoc.PlateSeries = dataModel.PlateSeries;
                invDoc.PlateCharacterId = dataModel.PlateCharacterId;
                invDoc.PlateNumberPart1 = dataModel.PlateNumberPart1;
                invDoc.PlateNumberPart2 = dataModel.PlateNumberPart2;
                invDoc.WeighbridgeNo = dataModel.WeighbridgeNo;
                invDoc.Description = dataModel.Description;
            }

            // Create
            else
            {
                var refDoc = this.invDocRepository.Query.Where(i => i.DocNo == dataModel.ReferenceInternalDocNo && i.InvDocTypeId == 6 && i.DestProjectId == dataModel.ProjectId).FirstOrDefault();
                if (refDoc == null)
                {
                    data.Result = false;
                    data.ErrorMessage = "هیچ بارنامه داخلی با شماره :" + dataModel.ReferenceInternalDocNo + " در سامانه پیدا نشد.";
                    return data;
                }

                var currentDate = DateTime.Now.ToString();
                var finYear = Int32.Parse(currentDate.Substring(0, 4));
                if (Int32.Parse(currentDate.Substring(5, 2)) >= 11)
                {
                    finYear++;
                }
                dataModel.FinanceYear = finYear;
                var doc = this.invDocRepository.Query.Where(d => d.FinanceYear == dataModel.FinanceYear).Max(d => d.DocNo);
                string MaxId = "";
                if (doc == null)
                {
                    MaxId = dataModel.FinanceYear.ToString() + "000001";
                }
                else
                {
                    MaxId = Common.Extention.Numberic.NextInvDocId(doc);
                }
                invDoc = new InvDoc
                {
                    InvDocTypeId = dataModel.InvDocTypeId,
                    FinanceYear = dataModel.FinanceYear,
                    DocNo = MaxId,
                    ReferenceInternalDocNo = dataModel.ReferenceInternalDocNo,
                    SourceProjectId = dataModel.SourceProjectId,
                    ProjectId = dataModel.ProjectId,
                    CarTypeId = dataModel.CarTypeId,
                    DriverName = dataModel.DriverName,
                    DriverPhone = dataModel.DriverPhone,
                    PlateCharacterId = dataModel.PlateCharacterId,
                    PlateSeries = dataModel.PlateSeries,
                    PlateNumberPart1 = dataModel.PlateNumberPart1,
                    PlateNumberPart2 = dataModel.PlateNumberPart2,
                    WeighbridgeNo=dataModel.WeighbridgeNo,
                    Description = dataModel.Description
                };

            }




            foreach (var item in dataModel.Mats)
            {
                if (invDocType.Sign < 0)
                {
                    if (item.RealAmount > GetMatInvQty(item.MatId, invDoc.ProjectId))
                    {
                        data.Result = false;
                        data.ErrorMessage = Resource.MatQtyIsNotEnough + "(" + Resource.MatCode + " : " + item.MatId.ToString() + ")";
                        return data;
                    }
                }

                InvDocDetail det = new InvDocDetail
                {
                    InvDoc = invDoc,
                    MatId = item.MatId,
                    Amount = item.Amount,
                    RealAmount = item.RealAmount
                };
                invDoc.InvDocDetails.Add(det);
            }



            try
            {
                if (dataModel.Id > 0)
                {
                    this.invDocRepository.Update(invDoc);
                }
                else
                {
                    this.invDocRepository.Add(invDoc);
                }
                this.UnitOfWork.Commit();
                data.Result = true;
                data.ErrorMessage = Resource.DataSavedSuccessfully;


            }
            catch (System.Exception ex)
            {
                var err = ex;
                //throw;

                data.Result = false;
                data.ErrorMessage = ex.Message;
            }

            return data;

        }

        public CustomResult<bool> SetUseDocData(UseDocDataModel dataModel)
        {
            CustomResult<bool> data = new CustomResult<bool>();
            InvDoc invDoc = new InvDoc();
            var invDocType = this.invDocTypeRepository.GetItemByKey(dataModel.InvDocTypeId);

            // Edit
            if (dataModel.Id > 0)
            {
                var oldDoc = this.invDocRepository.GetItemByKey(dataModel.Id);
                if (oldDoc.InvDocStatus == Common.Enum.InvDocStatus.Definitive)
                {
                    data.Result = false;
                    data.ErrorMessage = Resource.DocIsLockedCanNotChange;
                    return data;
                }

                foreach (var item in oldDoc.InvDocDetails.ToList())
                {
                    this.invDocDetailRepository.Remove(item);
                }

                invDoc = oldDoc;

                invDoc.ProjectId = dataModel.ProjectId;
                invDoc.PeopleId = dataModel.PeopleId;
                invDoc.Description = dataModel.Description;


            }

            // Create
            else
            {
                var currentDate = DateTime.Now.ToString();
                var finYear = Int32.Parse(currentDate.Substring(0, 4));
                if (Int32.Parse(currentDate.Substring(5, 2)) >= 11)
                {
                    finYear++;
                }
                dataModel.FinanceYear = finYear;
                var doc = this.invDocRepository.Query.Where(d => d.FinanceYear == dataModel.FinanceYear).Max(d => d.DocNo);
                string MaxId = "";
                if (doc == null)
                {
                    MaxId = dataModel.FinanceYear.ToString() + "000001";
                }
                else
                {
                    MaxId = Common.Extention.Numberic.NextInvDocId(doc);
                }
                invDoc = new InvDoc
                {
                    InvDocTypeId = dataModel.InvDocTypeId,
                    FinanceYear = dataModel.FinanceYear,
                    DocNo = MaxId,
                    ProjectId = dataModel.ProjectId,
                    PeopleId = dataModel.PeopleId,
                    Description = dataModel.Description

                };

            }

            foreach (var item in dataModel.Mats)
            {

                if (item.RealAmount > GetMatInvQty(item.MatId, invDoc.ProjectId))
                {
                    data.Result = false;
                    data.ErrorMessage = Resource.MatQtyIsNotEnough + "(" + Resource.MatCode + " : " + item.MatId.ToString() + ")";
                    return data;
                }


                InvDocDetail det = new InvDocDetail
                {
                    InvDoc = invDoc,
                    MatId = item.MatId,
                    ProjectDetailId = item.ProjectDetailId,
                    Amount = item.Amount,
                    RealAmount = item.Amount
                };
                invDoc.InvDocDetails.Add(det);
            }



            try
            {
                if (dataModel.Id > 0)
                {
                    this.invDocRepository.Update(invDoc);
                }
                else
                {
                    this.invDocRepository.Add(invDoc);
                }
                this.UnitOfWork.Commit();
                data.Result = true;
                data.ErrorMessage = Resource.DataSavedSuccessfully;

            }
            catch (System.Exception ex)
            {
                var err = ex;
                //throw;

                data.Result = false;
                data.ErrorMessage = ex.Message;
            }

            return data;

        }

        public CustomResult<bool> SetReturnDocData(ReturnDocDataModel dataModel)
        {
            CustomResult<bool> data = new CustomResult<bool>();
            InvDoc invDoc = new InvDoc();
            var invDocType = this.invDocTypeRepository.GetItemByKey(dataModel.InvDocTypeId);

            // Edit
            if (dataModel.Id > 0)
            {
                var oldDoc = this.invDocRepository.GetItemByKey(dataModel.Id);
                if (oldDoc.InvDocStatus == Common.Enum.InvDocStatus.Definitive)
                {
                    data.Result = false;
                    data.ErrorMessage = Resource.DocIsLockedCanNotChange;
                    return data;
                }

                foreach (var item in oldDoc.InvDocDetails.ToList())
                {
                    this.invDocDetailRepository.Remove(item);
                }

                invDoc = oldDoc;

                invDoc.ProjectId = dataModel.ProjectId;
                invDoc.PeopleId = dataModel.PeopleId;
                invDoc.Description = dataModel.Description;


            }

            // Create
            else
            {
                var currentDate = DateTime.Now.ToString();
                var finYear = Int32.Parse(currentDate.Substring(0, 4));
                if (Int32.Parse(currentDate.Substring(5, 2)) >= 11)
                {
                    finYear++;
                }
                dataModel.FinanceYear = finYear;
                var doc = this.invDocRepository.Query.Where(d => d.FinanceYear == dataModel.FinanceYear).Max(d => d.DocNo);
                string MaxId = "";
                if (doc == null)
                {
                    MaxId = dataModel.FinanceYear.ToString() + "000001";
                }
                else
                {
                    MaxId = Common.Extention.Numberic.NextInvDocId(doc);
                }
                invDoc = new InvDoc
                {
                    InvDocTypeId = dataModel.InvDocTypeId,
                    FinanceYear = dataModel.FinanceYear,
                    DocNo = MaxId,
                    ProjectId = dataModel.ProjectId,
                    PeopleId = dataModel.PeopleId,
                    Description = dataModel.Description

                };

            }

            foreach (var item in dataModel.Mats)
            {

                if (item.RealAmount > GetMatInvQty(item.MatId, invDoc.ProjectId))
                {
                    data.Result = false;
                    data.ErrorMessage = Resource.MatQtyIsNotEnough + "(" + Resource.MatCode + " : " + item.MatId.ToString() + ")";
                    return data;
                }


                InvDocDetail det = new InvDocDetail
                {
                    InvDoc = invDoc,
                    MatId = item.MatId,
                    ProjectDetailId = item.ProjectDetailId,
                    Amount = item.Amount,
                    RealAmount = item.Amount
                };
                invDoc.InvDocDetails.Add(det);
            }

            try
            {
                if (dataModel.Id > 0)
                {
                    this.invDocRepository.Update(invDoc);
                }
                else
                {
                    this.invDocRepository.Add(invDoc);
                }
                this.UnitOfWork.Commit();
                data.Result = true;
                data.ErrorMessage = Resource.DataSavedSuccessfully;

            }
            catch (System.Exception ex)
            {
                var err = ex;
                //throw;

                data.Result = false;
                data.ErrorMessage = ex.Message;
            }

            return data;

        }

        public CustomResult<bool> SetExternalBOLDocData(ExternalBOLDocDataModel dataModel)
        {
            CustomResult<bool> data = new CustomResult<bool>();
            InvDoc invDoc = new InvDoc();
            var invDocType = this.invDocTypeRepository.GetItemByKey(dataModel.InvDocTypeId);

            // Edit
            if (dataModel.Id > 0)
            {
                var oldDoc = this.invDocRepository.GetItemByKey(dataModel.Id);
                if (oldDoc.InvDocStatus == Common.Enum.InvDocStatus.Definitive)
                {
                    data.Result = false;
                    data.ErrorMessage = Resource.DocIsLockedCanNotChange;
                    return data;
                }

                foreach (var item in oldDoc.InvDocDetails.ToList())
                {
                    this.invDocDetailRepository.Remove(item);
                }

                //invDoc.InvDocDetails.Clear();

                invDoc = oldDoc;

                invDoc.ProjectId = dataModel.ProjectId;
                invDoc.ProviderId = dataModel.ProviderId;
                invDoc.CarTypeId = dataModel.CarTypeId;
                invDoc.SendByOrgCar = dataModel.SendByOrgCar;
                invDoc.DriverName = dataModel.DriverName;
                invDoc.DriverPhone = dataModel.DriverPhone;
                invDoc.PlateSeries = dataModel.PlateSeries;
                invDoc.PlateCharacterId = dataModel.PlateCharacterId;
                invDoc.PlateNumberPart1 = dataModel.PlateNumberPart1;
                invDoc.PlateNumberPart2 = dataModel.PlateNumberPart2;
                invDoc.WeighbridgeNo = dataModel.WeighbridgeNo;
                invDoc.Description = dataModel.Description;


            }

            // Create
            else
            {
                var currentDate = DateTime.Now.ToString();
                var finYear = Int32.Parse(currentDate.Substring(0, 4));
                if (Int32.Parse(currentDate.Substring(5, 2)) >= 11)
                {
                    finYear++;
                }
                dataModel.FinanceYear = finYear;
                var doc = this.invDocRepository.Query.Where(d => d.FinanceYear == dataModel.FinanceYear).Max(d => d.DocNo);
                string MaxId = "";
                if (doc == null)
                {
                    MaxId = dataModel.FinanceYear.ToString() + "000001";
                }
                else
                {
                    MaxId = Common.Extention.Numberic.NextInvDocId(doc);
                }
                invDoc = new InvDoc
                {
                    InvDocTypeId = dataModel.InvDocTypeId,
                    FinanceYear = dataModel.FinanceYear,
                    DocNo = MaxId,
                    ProviderId = dataModel.ProviderId,
                    ProjectId = dataModel.ProjectId,
                    CarTypeId = dataModel.CarTypeId,
                    DriverName = dataModel.DriverName,
                    DriverPhone = dataModel.DriverPhone,
                    PlateCharacterId = dataModel.PlateCharacterId,
                    PlateSeries = dataModel.PlateSeries,
                    PlateNumberPart1 = dataModel.PlateNumberPart1,
                    PlateNumberPart2 = dataModel.PlateNumberPart2,
                    Description = dataModel.Description
                };

            }

            foreach (var item in dataModel.Mats)
            {
                if (invDocType.Sign < 0)
                {
                    if (item.RealAmount > GetMatInvQty(item.MatId, invDoc.ProjectId))
                    {
                        data.Result = false;
                        data.ErrorMessage = Resource.MatQtyIsNotEnough + "(" + Resource.MatCode + " : " + item.MatId.ToString() + ")";
                        return data;
                    }
                }

                InvDocDetail det = new InvDocDetail
                {
                    InvDoc = invDoc,
                    MatId = item.MatId,
                    Amount = item.Amount,
                    RealAmount = item.RealAmount
                };
                invDoc.InvDocDetails.Add(det);
            }


            try
            {
                if (dataModel.Id > 0)
                {
                    this.invDocRepository.Update(invDoc);
                }
                else
                {
                    this.invDocRepository.Add(invDoc);
                }
                this.UnitOfWork.Commit();
                data.Result = true;
                data.ErrorMessage = Resource.DataSavedSuccessfully;

            }
            catch (System.Exception ex)
            {
                var err = ex;
                //throw;

                data.Result = false;
                data.ErrorMessage = ex.Message;
            }

            return data;

        }

        public CustomResult<bool> SetInternalBOLDocData(InternalBOLDocDataModel dataModel)
        {
            CustomResult<bool> data = new CustomResult<bool>();
            InvDoc invDoc = new InvDoc();
            var invDocType = this.invDocTypeRepository.GetItemByKey(dataModel.InvDocTypeId);

            // Edit
            if (dataModel.Id > 0)
            {
                var oldDoc = this.invDocRepository.GetItemByKey(dataModel.Id);
                if (oldDoc.InvDocStatus == Common.Enum.InvDocStatus.Definitive)
                {
                    data.Result = false;
                    data.ErrorMessage = Resource.DocIsLockedCanNotChange;
                    return data;
                }

                foreach (var item in oldDoc.InvDocDetails.ToList())
                {
                    this.invDocDetailRepository.Remove(item);
                }


                invDoc = oldDoc;

                invDoc.ProjectId = dataModel.ProjectId;
                invDoc.DestProjectId = dataModel.DestProjectId;
                invDoc.CarTypeId = dataModel.CarTypeId;
                invDoc.SendByOrgCar = dataModel.SendByOrgCar;
                invDoc.DriverName = dataModel.DriverName;
                invDoc.DriverPhone = dataModel.DriverPhone;
                invDoc.PlateSeries = dataModel.PlateSeries;
                invDoc.PlateCharacterId = dataModel.PlateCharacterId;
                invDoc.PlateNumberPart1 = dataModel.PlateNumberPart1;
                invDoc.PlateNumberPart2 = dataModel.PlateNumberPart2;
                invDoc.WeighbridgeNo = dataModel.WeighbridgeNo;
                invDoc.Description = dataModel.Description;


            }

            // Create
            else
            {
                var currentDate = DateTime.Now.ToString();
                var finYear = Int32.Parse(currentDate.Substring(0, 4));
                if (Int32.Parse(currentDate.Substring(5, 2)) >= 11)
                {
                    finYear++;
                }
                dataModel.FinanceYear = finYear;
                var doc = this.invDocRepository.Query.Where(d => d.FinanceYear == dataModel.FinanceYear).Max(d => d.DocNo);
                string MaxId = "";
                if (doc == null)
                {
                    MaxId = dataModel.FinanceYear.ToString() + "000001";
                }
                else
                {
                    MaxId = Common.Extention.Numberic.NextInvDocId(doc);
                }
                invDoc = new InvDoc
                {
                    InvDocTypeId = dataModel.InvDocTypeId,
                    FinanceYear = dataModel.FinanceYear,
                    DocNo = MaxId,
                    DestProjectId = dataModel.DestProjectId,
                    ProjectId = dataModel.ProjectId,
                    CarTypeId = dataModel.CarTypeId,
                    DriverName = dataModel.DriverName,
                    DriverPhone = dataModel.DriverPhone,
                    PlateCharacterId = dataModel.PlateCharacterId,
                    PlateSeries = dataModel.PlateSeries,
                    PlateNumberPart1 = dataModel.PlateNumberPart1,
                    PlateNumberPart2 = dataModel.PlateNumberPart2,
                    Description = dataModel.Description
                };

            }

            foreach (var item in dataModel.Mats)
            {
                if (invDocType.Sign < 0)
                {
                    if (item.RealAmount > GetMatInvQty(item.MatId, invDoc.ProjectId))
                    {
                        data.Result = false;
                        data.ErrorMessage = Resource.MatQtyIsNotEnough + "(" + Resource.MatCode + " : " + item.MatId.ToString() + ")";
                        return data;
                    }
                }

                InvDocDetail det = new InvDocDetail
                {
                    InvDoc = invDoc,
                    MatId = item.MatId,
                    Amount = item.Amount,
                    RealAmount = item.RealAmount
                };
                invDoc.InvDocDetails.Add(det);
            }


            try
            {
                if (dataModel.Id > 0)
                {
                    this.invDocRepository.Update(invDoc);
                }
                else
                {
                    this.invDocRepository.Add(invDoc);
                }
                this.UnitOfWork.Commit();
                data.Result = true;
                data.ErrorMessage = Resource.DataSavedSuccessfully;

            }
            catch (System.Exception ex)
            {
                var err = ex;
                //throw;

                data.Result = false;
                data.ErrorMessage = ex.Message;
            }

            return data;

        }

        private float GetMatInvQty(long id, long projectId)
        {
            var data = this.invDocDetailRepository.Query
                .Where(d => d.InvDoc.ProjectId == projectId && d.MatId == id && ((d.InvDoc.InvDocType.Sign>0 && d.InvDoc.InvDocStatus== Common.Enum.InvDocStatus.Definitive) || (d.InvDoc.InvDocType.Sign<0)))
                .Select(d => d.RealAmount * d.InvDoc.InvDocType.Sign).DefaultIfEmpty(0).Sum();
            return data;
        }

        public InvDoc GetInvDocByDocNo(string docNo)
        {
            var data = this.invDocRepository.Query.Where(i => i.DocNo == docNo).FirstOrDefault();
            return data;
        }
    }
}
