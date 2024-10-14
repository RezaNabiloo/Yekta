namespace BSG.ADInventory.Services
{
    using BSG.ADInventory.Data;
    using BSG.ADInventory.Entities;
    using BSG.ADInventory.Models.CalendarYear;
    using System.Collections.Generic;
    using System.Linq;
    using Zcf.Data;
    using Zcf.Data.Extensions;
    using Zcf.Paging;
    using Zcf.Services;


    /// <summary>
    /// Create Service For CalendarYear" />
    /// </summary>
    public class CalendarYearService : CrudService<CalendarYear>, ICalendarYearService
    {
        private readonly ICalendarYearRepository calendarYearRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CalendarYearService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="CalendarYearRepository"></param>
        public CalendarYearService(IUnitOfWork unitOfWork, ICalendarYearRepository calendarYearRepository)
            : base(unitOfWork, calendarYearRepository)
        {
            this.calendarYearRepository = calendarYearRepository;
        }

        /// <summary>
        /// Gets an <see cref="IPagedQueryable" /> instance according to the specified criteria.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public override IPagedQueryable<CalendarYear> GetItems(PagedQueryable criteria)
        {
            var data = this.calendarYearRepository.Query;
            return data.ToPagedQueryable(criteria);
        }

        public IEnumerable<CalendarYearListModel> GetDataList()
        {
            var data = this.calendarYearRepository.GetAllItems().Select(t => new CalendarYearListModel
            {
                Id = t.Id,
                YearNo = t.YearNo,
                Description = t.Description
            }).ToList();

            return (data);
        }
    }
}
