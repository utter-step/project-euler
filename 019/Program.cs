using System;

namespace _19
{
    class Program
    {
        private const int YEAR_START = 1901;
        private const int YEAR_END = 2001;

        static void Main(string[] args)
        {
            Tools.Decorators.TimeIt(CountGoodSundaysBetween1901And2001);

            Tools.Decorators.TimeItAccurate(CountGoodSundaysBetween1901And2001, 1000);
        }

        private static int CountGoodSundaysBetween1901And2001()
        {
            return CountSundaysOnFirst(YEAR_START, YEAR_END);
        }

        private static int[] months = new int[] { 
            31, 28, 31, 
            30, 31, 30, 
            31, 31, 30, 
            31, 30, 31 };

        private static int CountSundaysOnFirst(int startingYear, int endingYear)
        {
            int startingDay = 0;
            int year = 1900;

            int count = 0;
            while (year < endingYear)
            {
                for (int i = 0; i < 12; i++)
                {
                    if (startingDay == 6 && year >= startingYear)
                    {
                        count++;
                    }

                    if (i != 1) {
                        startingDay = (startingDay + months[i]) % 7;
                    }
                    else
                    {
                        if (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0))
                        {
                            startingDay = (startingDay + 29) % 7;
                        }
                        else
                        {
                            startingDay = (startingDay + 28) % 7; 
                        }
                    }
                }

                year++;
            }

            return count;
        }
    }
}
