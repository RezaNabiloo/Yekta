namespace BSG.ADInventory.Services
{
    using BSG.ADInventory.Common;
    using BSG.ADInventory.Common.Enum;
    using BSG.ADInventory.Data;
    using BSG.ADInventory.Entities;
    using BSG.ADInventory.Models.People;
    using BSG.Models.People;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Zcf.Data;
    using Zcf.Data.Extensions;
    using Zcf.Paging;
    using Zcf.Services;
    using System.Data;
    using System.Data.SqlClient;
    
    /// <summary>
    /// Create Service For Person" />
    /// </summary>
    public class PeopleService : CrudService<People>, IPeopleService
    {
        private readonly IPeopleRepository peopleRepository;        

        /// <summary>
        /// Initializes a new instance of the <see cref="PeopleService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="PersonRepository"></param>
        public PeopleService(IUnitOfWork unitOfWork, IPeopleRepository peopleRepository)
            : base(unitOfWork, peopleRepository)
        {
            this.peopleRepository = peopleRepository;
        }

        /// <summary>
        /// Gets an <see cref="IPagedQueryable" /> instance according to the specified criteria.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public override IPagedQueryable<People> GetItems(PagedQueryable criteria)
        {
            var data = this.peopleRepository.Query;
            return data.ToPagedQueryable(criteria);
        }

        

        
        public long RemovePeople(long id, Guid? userId)
        {
            try
            {
                var model = this.peopleRepository.GetItemByKey(id);                
                this.peopleRepository.Update(model);
                this.UnitOfWork.Commit();
                return id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        

        public IEnumerable<People> GetAllEmployees()
        {
            return this.peopleRepository.GetAllItems().Where(p => p.PeopleType == PeopleType.Employee);
        }

        private string getPeopleType(PeopleType peopleType)
        {
            try
            {
                return EnumExtention<BSG.ADInventory.Common.Enum.PeopleType>.GetDisplayName(peopleType);
            }
            catch (Exception)
            {
                return string.Empty;
                //throw;
            }
        }

        public People GetPeopleByNationalCode(long? id, string nationalCode)
        {
            return this.peopleRepository.Query.Where(p => (p.Id != id || id == null) && p.NationalCode == nationalCode).FirstOrDefault();
        }

        public IEnumerable<PeopleListModel> GetDataList()
        {
            var data = (from p in this.peopleRepository.GetAllItems()
                        select new PeopleListModel
                        {
                            Id = p.Id,
                            IdNo = p.IdNo,
                            NationalCode = p.NationalCode,
                            EmployeeNumber = p.EmployeeNumber,
                            FullName = p.FullName,                            
                            FatherName = p.FatherName,                            
                            BirthDate = p.BirthDate==null?string.Empty:p.BirthDate.Value.ToShortDateString(),
                            ImageUrl = p.ImageUrl,
                            PhoneNumber = p.PhoneNumber,                            
                            Gender = p.Gender,
                            PeopleType=p.PeopleType,
                            Address=p.Address,
                            IsActive=p.IsActive
                           
                        }).ToList();
            return data;
        }

        
    }

}

