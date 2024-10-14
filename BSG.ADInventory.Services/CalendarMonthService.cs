namespace BSG.ADInventory.Services
{
    using BSG.ADInventory.Data;
    using BSG.ADInventory.Entities;
    using System.Collections.Generic;
    using System.Linq;
    using Zcf.Data;
    using Zcf.Data.Extensions;
    using Zcf.Paging;
    using Zcf.Services;


    /// <summary>
    /// Create Service For CalendarMonth" />
    /// </summary>
    public class CalendarMonthService : CrudService<CalendarMonth>, ICalendarMonthService
    {
        private readonly ICalendarMonthRepository calendarMonthRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CalendarMonthService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="CalendarMonthRepository"></param>
        public CalendarMonthService(IUnitOfWork unitOfWork, ICalendarMonthRepository calendarMonthRepository)
            : base(unitOfWork, calendarMonthRepository)
        {
            this.calendarMonthRepository = calendarMonthRepository;
        }

        /// <summary>
        /// Gets an <see cref="IPagedQueryable" /> instance according to the specified criteria.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public override IPagedQueryable<CalendarMonth> GetItems(PagedQueryable criteria)
        {
            var data = this.calendarMonthRepository.Query;
            return data.ToPagedQueryable(criteria);
        }

        //public IEnumerable<CalendarMonthListModel> GetDataList()
        //{
        //    var data = this.calendarMonthRepository.GetAllItems().Select(t => new CalendarMonthListModel
        //    {
        //        Id = t.Id,
        //        Title = t.Title,
        //        TitleEn = t.TitleEn
        //    }).ToList();

        //    return (data);
        //}
    }
}
