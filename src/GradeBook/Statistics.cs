using System;

namespace GradeBook
{
    public class Statistics
    {
        public double Average;
        public double High;
        public double Low;
        public char Letter;

        public Statistics()
        {
            Average = 0.0;
            High = double.MinValue;
            Low = double.MaxValue;

        }
        public double CalculateAverage(double sum, double count)
        {
            return Average = sum/ count;
            
        }

        public double GetHighest(double grade)
        {
            High = Math.Max(grade, High);
            return High;
        }
        public double GetLowest(double grade)
        {
            Low = Math.Min(grade, Low);
            return Low;
        }
        public char GetLetter(double grade)
        {
            switch (grade)
            {
                case var d when  d >= 90.0:
                    Letter = 'A';
                    break;

                case var d when  d >= 80.0:
                    Letter = 'B';
                    break;

                case var d when  d >= 70.0:
                    Letter = 'C';
                    break;

                case var d when  d >= 60.0:
                    Letter = 'D';
                    break;

                default:
                    Letter = 'F';
                    break;
            }
            return Letter;
        }

    }
}