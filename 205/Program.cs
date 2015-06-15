using System;

using Tools;

namespace _205
{
    class Program
    {
        public static Random rng = new Random();
        public const double EPSILON = 1e-10;

        public static void Main(string[] args)
        {
            int peteWins = 0;
            long games = 0;

            double currentProb = 0.5;
            double previousProb = 0;

            while (games < 1000000L * 1000)
            {
                for (int i = 0; i < 300; i++)
                {
                    int collinSum = DiceSum(6, 6);
                    int peteSum = DiceSum(4, 9);

                    if (peteSum > collinSum)
                    {
                        peteWins++;
                    }

                    games++;
                }

                previousProb = currentProb;
                currentProb = (double)peteWins / games;
            }

            Console.WriteLine("{0:f7}", currentProb);
        }

        public static int DiceSum(int size, int count)
        {
            int s = 0;

            for (int i = 0; i < count; i++)
            {
                s += rng.Next(1, size + 1);
            }

            return s;
        }
    }
}
