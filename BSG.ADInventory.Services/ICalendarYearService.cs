namespace BSG.ADInventory.Services
{
    using BSG.ADInventory.Entities;
    using BSG.ADInventory.Models.CalendarYear;
    using System.Collections.Generic;
    using Zcf.Services;

    /// <summary>
    /// Create Interface for CalendarYearService
    /// </summary>
    public interface ICalendarYearService : ICrudService<CalendarYear>
	{
        IEnumerable<CalendarYearListModel> GetDataList();
    }
}