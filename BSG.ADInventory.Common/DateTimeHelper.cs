namespace BSG.ADInventory.Common
{
    using System;
    using System.Globalization;

    /// <summary>
    /// the DateTimeHelper clas work with persian date create by ghaderian
    /// </summary>
    public static class DateTimeHelper
    {
        /// <summary>
        /// Determines whether the specified year is kabiseh.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <returns></returns>
        public static bool IsKabiseh(int year)
        {
            PersianCalendar pc = new PersianCalendar();
            DateTime dateTime = pc.ToDateTime(year, 1, 1, 0, 0, 0, 0);
            Persia.SolarDate sloardt = Persia.Calendar.ConvertToPersian(dateTime);
            bool resualt = sloardt.IsLeapYear;
            return resualt;
        }

        /// <summary>
        /// To the string date time.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static int GetmonthDayCount(int year, int month)
        {
            bool isKabiseh = IsKabiseh(year);

            int monthDayCount = (month == 1 || month == 2 || month == 3 || month == 4 || month == 5 || month == 6) ? 31 : ((isKabiseh) ? 30 : 29);
            return monthDayCount;
        }

        /// <summary>
        /// To the string date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static string ToStringDate(DateTime? date)
        {
            try
            {
                if (date.HasValue)
                {
                    PersianCalendar PCalendar = new PersianCalendar();
                    DateTime GDate = new DateTime();
                    GDate = (DateTime)date;

                    string Day = PCalendar.GetDayOfMonth(GDate).ToString();
                    Day = Day.Length == 1 ? "0" + Day : Day;
                    string Month = PCalendar.GetMonth(GDate).ToString();
                    Month = Month.Length == 1 ? "0" + Month : Month;
                    string Year = PCalendar.GetYear(GDate).ToString();
                    string PDate = Year + "/" + Month + "/" + Day;

                    return PDate;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
            }
        }
        /// <summary>
        /// To the string date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="seprator">The seprator.</param>
        /// <param name="direction">The direction.</param>
        /// <returns></returns>
        public static string ToStringDate(DateTime? date, string seprator, string direction)
        {
            try
            {
                if (date.HasValue)
                {
                    PersianCalendar PCalendar = new PersianCalendar();
                    DateTime GDate = new DateTime();
                    GDate = (DateTime)date;

                    string day = PCalendar.GetDayOfMonth(GDate).ToString();
                    day = day.Length == 1 ? "0" + day : day;
                    string month = PCalendar.GetMonth(GDate).ToString();
                    month = month.Length == 1 ? "0" + month : month;
                    string year = PCalendar.GetYear(GDate).ToString();

                    string persianDate = direction == "ltr" ? day + seprator + month + seprator + year : year + seprator + month + seprator + day;

                    return persianDate;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
            }
        }
        /// <summary>
        /// To the string date with time.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="seprator">The seprator.</param>
        /// <returns>System.String.</returns>
        public static string ToStringDateWithTime(DateTime? date, string seprator)
        {
            try
            {
                if (date.HasValue)
                {
                    PersianCalendar pCalendar = new PersianCalendar();
                    DateTime dateTime = new DateTime();
                    dateTime = (DateTime)date;

                    string day = pCalendar.GetDayOfMonth(dateTime).ToString();
                    day = day.Length == 1 ? "0" + day : day;
                    string month = pCalendar.GetMonth(dateTime).ToString();
                    month = month.Length == 1 ? "0" + month : month;
                    string year = pCalendar.GetYear(dateTime).ToString();
                    string persianDate = day + seprator + month + seprator + year;

                    return date.Value.Hour + ":" + date.Value.Minute + " " + persianDate;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
            }
        }

        /// <summary>
        /// Seprateshamsis the date without space.
        /// </summary>
        /// <param name="shamsiDateWithoutSpace">The shamsi date without space.</param>
        /// <param name="seprator">The seprator.</param>
        /// <returns></returns>
        public static string SeprateshamsiDateWithoutSpace(string shamsiDateWithoutSpace, string seprator)
        {
            try
            {
                if (string.IsNullOrEmpty(shamsiDateWithoutSpace))
                {
                    return string.Empty;
                }
                else
                {
                    string year = shamsiDateWithoutSpace.Substring(0, 4);
                    string month = shamsiDateWithoutSpace.Substring(4, 2);
                    string day = shamsiDateWithoutSpace.Substring(6, 2);
                    string hour = shamsiDateWithoutSpace.Substring(8, 2);
                    string minute = shamsiDateWithoutSpace.Substring(10, 2);
                    return string.Format("{5}:{6} {0}{1}{2}{3}{4}", year, seprator, month, seprator, day, hour, minute);
                }

            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
            }
        }



        /// <summary>
        /// To the string time.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static string ToStringTime(DateTime? date)
        {
            try
            {
                if (date.HasValue)
                {
                    return " " + date.Value.TimeOfDay.Hours + ":" + date.Value.TimeOfDay.Minutes;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
            }
        }

        /// <summary>
        /// To the date time.
        /// </summary>
        /// <param name="persianDate">The persian date.</param>
        /// <param name="hours">The hours.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="sec">The sec.</param>
        /// <param name="millisec">The millisec.</param>
        /// <returns></returns>
        public static System.DateTime ToDateTime(string persianDate, int hours = 0, int min = 0, int sec = 0,
            int millisec = 0)
        {
            DateTime dt = ToDateTime(persianDate);
            dt = dt.AddHours(hours).AddMinutes(min).AddSeconds(sec).AddMilliseconds(millisec);
            return dt;
        }

        /// <summary>
        /// To the date time.
        /// </summary>
        /// <param name="persianDate">The persian date.</param>
        /// <returns></returns>
        public static DateTime ToDateTime(string persianDate)
        {
            try
            {
                char seprator = persianDate.Contains("/") ? '/' : (persianDate.Contains("؍") ? '؍' : '-');
                DateTime dateTime = new DateTime(9999, 1, 1);

                if (string.IsNullOrEmpty(persianDate))
                    return DateTime.Now;

                string[] strDate = persianDate.Trim().Split(new char[] { seprator });

                if (strDate.Length != 3)
                    return dateTime;

                if (strDate[0].Trim().Length != 4 && strDate[2].Trim().Length != 4)
                {
                    return dateTime;
                }
                if (strDate[1].Trim().Length != 2)
                {
                    int month = Convert.ToInt32(strDate[1]);

                    if ((month < 10) && (month >= 0))
                        strDate[1] = "0" + month.ToString();
                    else
                        return dateTime;
                }
                if (strDate[2].Trim().Length != 2 && strDate[2].Trim().Length != 4)
                {
                    int day = Convert.ToInt32(strDate[2]);

                    if ((day < 10) && (day >= 0))
                        strDate[2] = "0" + day.ToString();
                    else
                        return dateTime;
                }
                if (strDate[0].Trim().Length != 2 && strDate[0].Trim().Length != 4)
                {
                    int day = Convert.ToInt32(strDate[2]);

                    if ((day < 10) && (day >= 0))
                        strDate[0] = "0" + day.ToString();
                    else
                        return dateTime;
                }

                PersianCalendar PCalendar = new PersianCalendar();

                try
                {
                    if (strDate[0].Trim().Length == 4)
                    {
                        dateTime = PCalendar.ToDateTime(Convert.ToInt32(strDate[0]), Convert.ToInt32(strDate[1]),
                            Convert.ToInt32(strDate[2]), 0, 0, 0, 0);
                    }
                    else if (strDate[2].Trim().Length == 4)
                    {
                        dateTime = PCalendar.ToDateTime(Convert.ToInt32(strDate[2]), Convert.ToInt32(strDate[1]),
                            Convert.ToInt32(strDate[0]), 0, 0, 0, 0);
                    }
                    else
                    {
                        return dateTime;
                    }
                }
                catch (Exception exception)
                {
                    return dateTime;
                }

                return dateTime;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
            }
        }
        public static DateTime? ToDateTimeNullIfIsEmpty(string persianDate)
        {
            try
            {
                if (String.IsNullOrEmpty(persianDate))
                    return null;
                char seprator = persianDate.Contains("/") ? '/' : (persianDate.Contains("؍") ? '؍' : '-');
                DateTime dateTime = new DateTime(9999, 1, 1);

                if (string.IsNullOrEmpty(persianDate))
                    return DateTime.Now;

                string[] strDate = persianDate.Trim().Split(new char[] { seprator });

                if (strDate.Length != 3)
                    return dateTime;

                if (strDate[0].Trim().Length != 4 && strDate[2].Trim().Length != 4)
                {
                    return dateTime;
                }
                if (strDate[1].Trim().Length != 2)
                {
                    int month = Convert.ToInt32(strDate[1]);

                    if ((month < 10) && (month >= 0))
                        strDate[1] = "0" + month.ToString();
                    else
                        return dateTime;
                }
                if (strDate[2].Trim().Length != 2 && strDate[2].Trim().Length != 4)
                {
                    int day = Convert.ToInt32(strDate[2]);

                    if ((day < 10) && (day >= 0))
                        strDate[2] = "0" + day.ToString();
                    else
                        return dateTime;
                }
                if (strDate[0].Trim().Length != 2 && strDate[0].Trim().Length != 4)
                {
                    int day = Convert.ToInt32(strDate[2]);

                    if ((day < 10) && (day >= 0))
                        strDate[0] = "0" + day.ToString();
                    else
                        return dateTime;
                }

                PersianCalendar PCalendar = new PersianCalendar();

                try
                {
                    if (strDate[0].Trim().Length == 4)
                    {
                        dateTime = PCalendar.ToDateTime(Convert.ToInt32(strDate[0]), Convert.ToInt32(strDate[1]),
                            Convert.ToInt32(strDate[2]), 0, 0, 0, 0);
                    }
                    else if (strDate[2].Trim().Length == 4)
                    {
                        dateTime = PCalendar.ToDateTime(Convert.ToInt32(strDate[2]), Convert.ToInt32(strDate[1]),
                            Convert.ToInt32(strDate[0]), 0, 0, 0, 0);
                    }
                    else
                    {
                        return dateTime;
                    }
                }
                catch (Exception exception)
                {
                    return dateTime;
                }

                return dateTime;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
            }
        }

        /// <summary>
        /// To the date time.
        /// </summary>
        /// <param name="persianDate">The persian date.</param>
        /// <param name="seprator">The seprator.</param>
        /// <returns></returns>
        public static DateTime ToDateTime(string persianDate, char seprator)
        {
            try
            {
                DateTime dateTime = new DateTime(9999, 1, 1);

                if (string.IsNullOrEmpty(persianDate))
                    return DateTime.Now;

                string[] strDate = persianDate.Trim().Split(new char[] { seprator });

                if (strDate.Length != 3)
                    return dateTime;

                if (strDate[0].Trim().Length != 4 && strDate[2].Trim().Length != 4)
                {
                    return dateTime;
                }
                if (strDate[1].Trim().Length != 2)
                {
                    int month = Convert.ToInt32(strDate[1]);

                    if ((month < 10) && (month >= 0))
                        strDate[1] = "0" + month.ToString();
                    else
                        return dateTime;
                }
                if (strDate[2].Trim().Length != 2 && strDate[2].Trim().Length != 4)
                {
                    int day = Convert.ToInt32(strDate[2]);

                    if ((day < 10) && (day >= 0))
                        strDate[2] = "0" + day.ToString();
                    else
                        return dateTime;
                }
                if (strDate[0].Trim().Length != 2 && strDate[0].Trim().Length != 4)
                {
                    int day = Convert.ToInt32(strDate[2]);

                    if ((day < 10) && (day >= 0))
                        strDate[0] = "0" + day.ToString();
                    else
                        return dateTime;
                }

                PersianCalendar PCalendar = new PersianCalendar();

                try
                {
                    if (strDate[0].Trim().Length == 4)
                    {
                        dateTime = PCalendar.ToDateTime(Convert.ToInt32(strDate[0]), Convert.ToInt32(strDate[1]),
                            Convert.ToInt32(strDate[2]), 0, 0, 0, 0);
                    }
                    else if (strDate[2].Trim().Length == 4)
                    {
                        dateTime = PCalendar.ToDateTime(Convert.ToInt32(strDate[2]), Convert.ToInt32(strDate[1]),
                            Convert.ToInt32(strDate[0]), 0, 0, 0, 0);
                    }
                    else
                    {
                        return dateTime;
                    }
                }
                catch (Exception exception)
                {
                    return dateTime;
                }

                return dateTime;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
            }
        }

        /// <summary>
        /// Gets the name of the month.
        /// </summary>
        /// <param name="monthNumber">The month number.</param>
        /// <returns></returns>
        public static string GetMonthName(int monthNumber)
        {

            switch (monthNumber)
            {
                case 1:
                    return "فروردین";
                //break;

                case 2:
                    return "اردیبهشت";
                //break;

                case 3:
                    return "خرداد";
                //break;

                case 4:
                    return "تیر";
                //break;

                case 5:
                    return "مرداد";
                //break;

                case 6:
                    return "شهریور";
                //break;

                case 7:
                    return "مهر";
                //break;

                case 8:
                    return "آبان";
                //break;

                case 9:
                    return "آذر";
                //break;

                case 10:
                    return "دی";
                //break;

                case 11:
                    return "بهمن";
                //break;

                case 12:
                    return "اسفند";
                    //break;
            }
            return "";
        }


        /// <summary>
        /// Gets the name of the week day.
        /// </summary>
        /// <param name="dayNumber">The day number.</param>
        /// <returns></returns>
        public static string GetWeekDayName(int dayNumber)
        {
            switch (dayNumber)
            {
                case 1:
                    return "شنبه";
                //break;

                case 2:
                    return "یکشنبه";
                // break;

                case 3:
                    return "دوشنبه";
                ///break;

                case 4:
                    return "سه شنبه";
                //break;

                case 5:
                    return "چهار شنبه";
                //break;

                case 6:
                    return "پنجشنبه";
                //break;

                case 7:
                    return "جمعه";
                    //break;

            }
            return "";
        }

        /// <summary>
        /// Persians the datestring to date time.
        /// </summary>
        /// <param name="persianDate">The persian date.</param>
        /// <param name="seprator">The seprator.</param>
        /// <returns></returns>
        public static DateTime PersianDatestringToDateTime(string persianDate, char seprator)
        {
            try
            {
                DateTime dateTime = new DateTime(9999, 1, 1);

                if (string.IsNullOrEmpty(persianDate))
                    return DateTime.Now;

                string[] strDate = persianDate.Trim().Split(new char[] { seprator });

                PersianCalendar PCalendar = new PersianCalendar();

                try
                {
                    if (strDate[2].Length == 4)
                    {
                        dateTime = PCalendar.ToDateTime(Convert.ToInt32(strDate[2]), Convert.ToInt32(strDate[1]),
                            Convert.ToInt32(strDate[0]), 0, 0, 0, 0);
                    }
                    else
                    {
                        dateTime = PCalendar.ToDateTime(Convert.ToInt32(strDate[0]), Convert.ToInt32(strDate[1]),
                            Convert.ToInt32(strDate[2]), 0, 0, 0, 0);
                    }
                }
                catch (Exception exception)
                {
                    return dateTime;
                }

                return dateTime;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
            }
        }

        /// <summary>
        /// Persians the datestring to date time.
        /// </summary>
        /// <param name="persianDate">The persian date.</param>
        /// <param name="time">The time.</param>
        /// <param name="seprator">The seprator.</param>
        /// <returns></returns>
        public static DateTime PersianDatestringToDateTime(string persianDate, string time, char seprator)
        {
            try
            {
                DateTime dateTime = new DateTime(9999, 1, 1);

                if (string.IsNullOrEmpty(persianDate))
                    return DateTime.Now;

                string[] strDate = persianDate.Trim().Split(new char[] { seprator });

                TimeSpan timeSpan = new TimeSpan(0, 0, 0, 0);
                if (!string.IsNullOrEmpty(time))
                {
                    string[] seprateTime = time.Split(':');
                    timeSpan = new TimeSpan(0, Convert.ToInt32(seprateTime[0]), Convert.ToInt32(seprateTime[1]), 0, 0);
                }
                PersianCalendar PCalendar = new PersianCalendar();

                try
                {
                    if (strDate[2].Length == 4)
                    {
                        dateTime = PCalendar.ToDateTime(Convert.ToInt32(strDate[2]), Convert.ToInt32(strDate[1]),
                            Convert.ToInt32(strDate[0]), 0, 0, 0, 0);
                    }
                    else
                    {
                        dateTime = PCalendar.ToDateTime(Convert.ToInt32(strDate[0]), Convert.ToInt32(strDate[1]),
                            Convert.ToInt32(strDate[2]), 0, 0, 0, 0);
                    }
                    dateTime += timeSpan;
                }
                catch (Exception exception)
                {
                    return dateTime;
                }

                return dateTime;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
            }
        }

        /// <summary>
        /// Gets the persian month number.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static int GetPersianMonthNumber(string date)
        {
            string[] data = date.Split('/');
            int month = Convert.ToInt32(data[1]);
            return month;
        }

        /// <summary>
        /// Gets the year of persian date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static int GetYearOfPersianDate(string date)
        {
            string[] data = date.Split('/');
            int year = Convert.ToInt32(data[0]);
            return year;
        }

        /// <summary>
        /// Gets the persian year of date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static int GetPersianYearOfDate(DateTime date)
        {
            string persianDate = ToStringDate(date);
            return GetYearOfPersianDate(persianDate);
        }

        public static string ShowAsTimeFormat(decimal time)
        {
            var hour = Math.Truncate(time);
            var min = ((time - hour).ToString().Replace(".", "").Replace("/", "") + "0").Substring(0, 2);

            return hour.ToString() + ":" + min;
        }

        public static decimal IntToTime(int time)
        {
            var hour = time / 60;
            var min = time % 60;

            return hour + (min / 100);
        }

    }
}