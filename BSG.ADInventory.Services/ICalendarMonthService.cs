namespace BSG.ADInventory.Services
{
    using BSG.ADInventory.Entities;
    using Zcf.Services;

    /// <summary>
    /// Create Interface for CalendarMonthService
    /// </summary>
    public interface ICalendarMonthService : ICrudService<CalendarMonth>
	{
        //IEnumerable<CalendarMonthListModel> GetDataList();
    }
}