using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF
{
    public class PersianDate
    {

        private readonly string[] Month = {"null","فروردین","اردیبهشت","خرداد",
        "تیر","مرداد","شهریور", "مهر","آبان","آذر","دی","بهمن","اسفند"};
        private readonly string[] Day = { "یکشنبه", "دوشنبه", "سه شنبه", "چهارشنبه",
            "پنجشنبه", "جمعه", "شنبه"};

        private string _date;
        public string Date
        {
            set
            {
                _date = value;
            }
            get
            {
                return _date;
            }
        }


        private void MyFunction_()
        {
            DateTime d = DateTime.Now.ToLocalTime();
            bool checkKabiseh = false;
            if ((d.Year % 100 != 0) &&
                (d.Year % 4 == 0))
            {
                checkKabiseh = true;
            } // end if

            int dayTemp = 0;
            if (d.DayOfYear > 79)
            {
                dayTemp = d.DayOfYear - 79;

                //first 6 month
                if (dayTemp <= 186)
                {
                    if (dayTemp % 31 == 0)
                    {
                        Date = Day[Convert.ToInt32(d.DayOfWeek)] + " " + "31" + " " + Month[dayTemp / 31];
                    } // end if
                    else
                    {
                        Date = Day[Convert.ToInt32(d.DayOfWeek)] + " " + (dayTemp % 31).ToString() + " " + Month[(dayTemp / 31) + 1];
                    } // end else
                } // end if

                //second 6 month
                else
                {
                    dayTemp = dayTemp - 186;
                    if (dayTemp % 30 == 0)
                    {
                        Date = Day[Convert.ToInt32(d.DayOfWeek)] + " " + "30" + " " + Month[(dayTemp / 30) + 6];
                    } // end if
                    else
                    {
                        Date = Day[Convert.ToInt32(d.DayOfWeek)] + " " + (dayTemp % 30).ToString() + " " + Month[(dayTemp / 30) + 7];
                    } // end else
                } // end else
            } // end if
            else
            {
                if (checkKabiseh)
                {
                    dayTemp = d.DayOfYear + 11;
                } // end if
                else
                {
                    dayTemp = d.DayOfYear + 10;
                } // end else

                if (dayTemp % 30 == 0)
                {
                    Date = Day[Convert.ToInt32(d.DayOfWeek)] + " " + "31" + " " + Month[(dayTemp / 30) + 9];

                } // end if
                else
                {
                    Date = Day[Convert.ToInt32(d.DayOfWeek)] + " " + (dayTemp % 30).ToString() + " " + Month[(dayTemp / 30) + 10];
                } // end else
            } // end else
        } // end method 
    } // end class
}
