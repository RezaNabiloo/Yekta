namespace BSG.ADInventory.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// 
    /// </summary>
    public class CustomerPostalCodeEvaluator
    {
        static List<string> postalCodeBlackList;

        /// <summary>
        /// Initializes the <see cref="CustomerPostalCodeEvaluator" /> class.
        /// </summary>
        static CustomerPostalCodeEvaluator()
        {
            postalCodeBlackList = new List<string>() 
            {
                "0000000000",
                "1000000000",
                "1100000000",
                "0000000001",
                "0000000011",
                "0000011111",
                "1111100000",
                "1111111111",
                "2222222222",
                "3333333333",
                "4444444444",
                "5555555555",
                "6666666666",
                "7777777777",
                "8888888888",
                "9999999999",
                "1234567890",
                "0123456789",
                "1234567891",
                "1234512345",
                "5432154321",
                "54321123456"
            };

        }

        /// <summary>
        /// Determines whether [is in black list] [the specified postal code].
        /// </summary>
        /// <param name="postalCode">The postal code.</param>
        /// <returns>
        ///   <c>true</c> if [is in black list] [the specified postal code]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsInBlackList(string postalCode)
        {
            return postalCodeBlackList.Contains(postalCode);
        }

        //public static bool IsValidatePostCode(string postCode, long townshipId)
        //{

        //}

    }
}
