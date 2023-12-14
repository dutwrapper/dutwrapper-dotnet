using System;
using System.Collections.Generic;
using System.Text;

namespace DutWrapper.Model
{
    public class DutSchoolYearItem
    {
        int week;
        string schoolYear;
        int schoolYearVal;

        public DutSchoolYearItem(int week, string schoolYear, int schoolYearVal)
        {
            this.week = week;
            this.schoolYear = schoolYear;
            this.schoolYearVal = schoolYearVal;
        }


    }
}
