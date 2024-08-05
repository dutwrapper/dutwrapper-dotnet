using System;
using System.Collections.Generic;
using System.Text;

namespace DutWrapper.Accounts
{
    public class SchoolYear
    {
        public int Year { get; private set; }

        public int Semester { get; private set; }

        /// <summary>
        /// School year
        /// </summary>
        /// <param name="year">School year in 2-digit</param>
        /// <param name="semester">Semester in range 1-2 (3 if in summer semester)</param>
        /// <exception cref="Exception"></exception>
        public SchoolYear(int year, int semester)
        {
            if (year < 01)
            {
                throw new Exception("Year must be greater than 00!");
            }
            if (semester < 1 || semester > 3)
            {
                throw new Exception("Semester must be in range 1-3!");
            }

            Year = year;
            Semester = semester;
        }

        /// <summary>
        /// Returns string with human-readable about school year and semester.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format(
                "School year: 20{0}-20{1}, semester {2}{3}",
                Year,
                Year + 1,
                Semester > 2 ? 2 : Semester,
                Semester == 3 ? " (in summer)" : ""
                );
        }
    }
}
