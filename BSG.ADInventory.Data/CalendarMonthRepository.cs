namespace BSG.ADInventory.Data
{
    using BSG.ADInventory.Entities;
    using System.Data;
    using System.Linq;
    using Zcf.Data;

    /// <summary>
    /// Initializes a new instance of the <see cref="CalendarMonthRepository" /> class.
    /// </summary>
    /// <param name="dbFactory">The database factory.</param>
    public class CalendarMonthRepository : EntityRepositoryBase<CalendarMonth>, ICalendarMonthRepository
	{        
		public CalendarMonthRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        /// <summary>
        /// Gets the query.
        /// </summary>
        /// <value>
        /// The query.
        /// </value>
        public override IQueryable<CalendarMonth> Query
        {
            get
            {
                return base.Query.OrderBy(p => p.Id);
            }
        } 
	}
}