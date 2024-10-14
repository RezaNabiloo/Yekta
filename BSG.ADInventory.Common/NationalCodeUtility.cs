namespace BSG.ADInventory.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class NationalCodeUtility
    {
        public static bool IsValidIranianNationalCode(string nationalCode)
        {
            if (nationalCode.Length == 10)
            {
                if (nationalCode == "0000000000" ||
                    nationalCode == "1111111111" ||
                    nationalCode == "2222222222" ||
                    nationalCode == "3333333333" ||
                    nationalCode == "4444444444" ||
                    nationalCode == "5555555555" ||
                    nationalCode == "6666666666" ||
                    nationalCode == "7777777777" ||
                    nationalCode == "8888888888" ||
                    nationalCode == "9999999999")
                {
                    return false;
                }
                else
                {
                    var nationalCode9 = Int32.Parse(nationalCode[9].ToString());

                    var nationalCodeCell = (Int32.Parse(nationalCode[0].ToString()) * 10) +
                            (Int32.Parse(nationalCode[1].ToString()) * 9) +
                            (Int32.Parse(nationalCode[2].ToString()) * 8) +
                            (Int32.Parse(nationalCode[3].ToString()) * 7) +
                            (Int32.Parse(nationalCode[4].ToString()) * 6) +
                            (Int32.Parse(nationalCode[5].ToString()) * 5) +
                            (Int32.Parse(nationalCode[6].ToString()) * 4) +
                            (Int32.Parse(nationalCode[7].ToString()) * 3) +
                            (Int32.Parse(nationalCode[8].ToString()) * 2);

                    int nationalCodeCheck = nationalCodeCell - Convert.ToInt32(nationalCodeCell / 11) * 11;

                    if ((nationalCodeCheck == 0 && nationalCodeCheck == nationalCode9) ||
                        (nationalCodeCheck == 1 && nationalCode9 == 1) ||
                        (nationalCodeCheck > 1 && nationalCode9 == 11 - nationalCodeCheck))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }
    }
}
