namespace BSG.ADInventory.Services
{
    using BSG.ADInventory.Data;
    using BSG.ADInventory.Entities;
    using BSG.ADInventory.Models.InvDocAttachment;
    using System.Collections.Generic;
    using System.Linq;
    using Zcf.Data;
    using Zcf.Data.Extensions;
    using Zcf.Paging;
    using Zcf.Services;
	
		  
    /// <summary>
    /// Create Service For InvDocAttachment" />
    /// </summary>
    public class InvDocAttachmentService : CrudService<InvDocAttachment>, IInvDocAttachmentService
    {
        private readonly IInvDocAttachmentRepository invDocAttachmentRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvDocAttachmentService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="InvDocAttachmentRepository"></param>
        public InvDocAttachmentService(IUnitOfWork unitOfWork, IInvDocAttachmentRepository invDocAttachmentRepository)
            : base(unitOfWork, invDocAttachmentRepository)
        {
            this.invDocAttachmentRepository = invDocAttachmentRepository;
        }

        /// <summary>
        /// Gets an <see cref="IPagedQueryable" /> instance according to the specified criteria.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public override IPagedQueryable<InvDocAttachment> GetItems(PagedQueryable criteria)
        {
            var data = this.invDocAttachmentRepository.Query;
            return data.ToPagedQueryable(criteria);
        }

        public IEnumerable<InvDocAttachmentDataModel> GetInvDocAttachments(long id)
        {
            var data = this.invDocAttachmentRepository.Query.Where(i=>i.InvDocId==id).Select(t => new InvDocAttachmentDataModel
            {
                Id = t.Id,
                InvDocId=t.InvDocId,
                Title = t.Title,
                FilePath = t.FilePath
            }).ToList();

            return (data);
        }
    }
}
