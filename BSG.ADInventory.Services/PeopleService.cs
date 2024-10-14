namespace BSG.ADInventory.Services
{
    using BSG.ADInventory.Common;
    using BSG.ADInventory.Common.Enum;
    using BSG.ADInventory.Data;
    using BSG.ADInventory.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Zcf.Data;
    using Zcf.Data.Extensions;
    using Zcf.Paging;
    using Zcf.Services;

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

    }

}

