using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BSG.ADInventory.Models.DataTable
{
    public class DataTableHelper
    {
        public static IEnumerable<T> FilterAndSort<T>(IEnumerable<T> data, JqDataTable filters)
        {
            // Apply global search
            if (!string.IsNullOrEmpty(filters.sSearch))
            {
                data = data.Where(item => item.GetType().GetProperties()
                                             .Any(prop => prop.GetValue(item, null)?.ToString().IndexOf(filters.sSearch, StringComparison.OrdinalIgnoreCase) >= 0));
            }

            // Apply column specific searches
            for (int i = 1; i <= 20; i++)
            {
                string searchValue = (string)filters.GetType().GetProperty($"sSearch_{i}")?.GetValue(filters);
                if (!string.IsNullOrEmpty(searchValue))
                {
                    //var property = typeof(T).GetProperties().ElementAtOrDefault(i - 1);
                    var property = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                   .OrderBy(p => p.DeclaringType == typeof(T)) 
                                   .ElementAtOrDefault(i - 1);

                    if (property != null)
                    {
                        data = data.Where(item => property.GetValue(item, null)?.ToString().IndexOf(searchValue, StringComparison.OrdinalIgnoreCase) >= 0);
                    }
                }
            }

            // Apply sorting
            if (filters.iSortCol_0 >= 0)
            {
                var sortProperty = typeof(T).GetProperties().ElementAtOrDefault(filters.iSortCol_0 - 1);
                if (sortProperty != null)
                {
                    data = filters.sSortDir_0 == "asc"
                        ? data.OrderBy(item => sortProperty.GetValue(item, null))
                        : data.OrderByDescending(item => sortProperty.GetValue(item, null));
                }
            }

            return data.ToList();
        }
    }
}
