namespace BSG.ADInventory.Data
{
    using BSG.ADInventory.Entities;
    using System.Data;
    using System.Linq;
    using Zcf.Data;

    /// <summary>
    /// Initializes a new instance of the <see cref="CalendarYearRepository" /> class.
    /// </summary>
    /// <param name="dbFactory">The database factory.</param>
    public class CalendarYearRepository : EntityRepositoryBase<CalendarYear>, ICalendarYearRepository
	{        
		public CalendarYearRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        /// <summary>
        /// Gets the query.
        /// </summary>
        /// <value>
        /// The query.
        /// </value>
        public override IQueryable<CalendarYear> Query
        {
            get
            {
                return base.Query.OrderBy(p => p.Id);
            }
        } 
	}
}