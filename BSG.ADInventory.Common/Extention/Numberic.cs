using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Common.Extention
{
    public static class Numberic
    {
        public static string DoubleToTimeView(double? value)
        {
            if (value==null)
            {
                return "00:00";
            }

            return RightString("00" + ((int)value).ToString(), 2)+":"+RightString("00"+(value - Math.Truncate(value.Value)).ToString(),2);

        }

        public static string RightString(this string input, int count)
        {
            return input.Substring(Math.Max(input.Length - count, 0), Math.Min(count, input.Length));
         
        }

        public static string LeftString(this string input, int count)
        {
            return input.Substring(0, Math.Min(input.Length, count));
        }

        public static string NextInvDocId (string id)
        {
            var id1 = id.Substring(5, id.Length - 5);
            var prefix = id.Substring(0, 5);
            var id2 = Int32.Parse(id1) + 1;

            var id3 = prefix + Right("000000" + id2.ToString(), 6);
            return id3;
        }
        public static string Right(this string value, int length)
        {
            if (String.IsNullOrEmpty(value)) return string.Empty;

            return value.Length <= length ? value : value.Substring(value.Length - length);
        }
    }
}
