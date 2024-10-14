namespace BSG.ADInventory.Services
{
    using BSG.ADInventory.Entities;
    using Data;
    using Zcf.Data;
    using Zcf.Data.Extensions;
    using Zcf.Paging;
    using Zcf.Services;
    using BSG.ADInventory.Models.MatGroupTechSpec;
    using System.Collections.Generic;
    using System.Linq;


    /// <summary>
    /// Create Service For Country" />
    /// </summary>
    public class MatGroupTechSpecService : CrudService<MatGroupTechSpec>, IMatGroupTechSpecService
    {
        private readonly IMatGroupTechSpecRepository matGroupTechSpecRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="MatGroupTechSpecService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="CountryRepository"></param>
        public MatGroupTechSpecService(IUnitOfWork unitOfWork, IMatGroupTechSpecRepository matGroupTechSpecRepository)
            : base(unitOfWork, matGroupTechSpecRepository)
        {
            this.matGroupTechSpecRepository = matGroupTechSpecRepository;
        }

        /// <summary>
        /// Gets an <see cref="IPagedQueryable" /> instance according to the specified criteria.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public override IPagedQueryable<MatGroupTechSpec> GetItems(PagedQueryable criteria)
        {
            var data = this.matGroupTechSpecRepository.Query;
            return data.ToPagedQueryable(criteria);
        }


        public IEnumerable<MatGroupTechSpec> GetMatGroupTechSpecs(long id)
        {
            var data = this.matGroupTechSpecRepository.Query.Where(g => g.MatGroupId == id).ToList();
            return data;
        }

        public void AddMatGroupTechSpec(MatGroupTechSpecModel techSpec)
        {

            try
            {
                MatGroupTechSpec ps = new MatGroupTechSpec
                {
                    FaTitle = techSpec.FaTitle,
                    MatGroupId = techSpec.MatGroupId,                    
                    TechSpecType = techSpec.TechSpecType,                    
                };

                this.matGroupTechSpecRepository.Add(ps);
               
                this.UnitOfWork.Commit();
            }
            catch (System.Exception)
            {

                //throw;
            }

        }
        public MatGroupTechSpecModel GetMatGroupTechSpec(long id)
        {
            MatGroupTechSpecModel data = new MatGroupTechSpecModel();

            var techSpec = this.matGroupTechSpecRepository.GetItemByKey(id);
            if (techSpec != null)
            {
                data.Id = techSpec.Id;
                data.MatGroupId = techSpec.MatGroupId;
                data.FaTitle = techSpec.FaTitle;                
                data.TechSpecType = techSpec.TechSpecType;
                
            }
            return data;
        }
        public bool UpdateMatGroupTechSpec(MatGroupTechSpecModel techSpec)
        {
            var data = false;

            var ts = this.matGroupTechSpecRepository.GetItemByKey(techSpec.Id);
            if (ts != null)
            {
                try
                {
                    ts.FaTitle = techSpec.FaTitle;
                    ts.MatGroupId = techSpec.MatGroupId;                    
                    ts.TechSpecType = techSpec.TechSpecType;                    
                    this.matGroupTechSpecRepository.Update(ts);
                    this.UnitOfWork.Commit();
                    data = true;
                }
                catch (System.Exception)
                {

                    //throw;
                }

            }

            return data;

        }

        //public bool UpdateMatGroupTechSpec(MatGroupTechSpecModel techSpec)
        //{
        //    var data = false;

        //    var ts = this.matGroupTechSpecRepository.GetItemByKey(techSpec.Id);
        //    if (ts != null)
        //    {
        //        //try
        //        //{
        //        //    ts.FaTitle = techSpec.FaTitle;
        //        //    ts.MatGroupId = techSpec.MatGroupId;
        //        //    ts.ViewOrder = techSpec.ViewOrder;
        //        //    ts.TechSpecType = techSpec.TechSpecType;
        //        //    ts.ShowValueLTR = techSpec.ShowValueLTR;
        //        //    this.matGroupTechSpecRepository.Update(ts);
        //        //    this.UnitOfWork.Commit();
        //        //    data = true;
        //        //}
        //        //catch (System.Exception)
        //        //{

        //        //    //throw;
        //        //}

        //    }

        //    return data;

        //}
    }
}

