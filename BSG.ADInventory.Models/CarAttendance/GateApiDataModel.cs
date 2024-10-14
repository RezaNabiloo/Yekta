using BSG.Seyhoon.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSG.Seyhoon.Models.PeopleAttendance
{
    public class GateApiDataModel
    {
        public long PeopleId { get; set; }

        public InOutType Type { get; set; }
    }
}
