

using System;
using System.Collections.Generic;
using System.IO;

namespace GradeBook
{

    public delegate void GradeAddedDelegate(object sender, EventArgs args);

    public class NamedObject 
    {
        public NamedObject(string name)
        {
            Name = name;
        }

        public string Name
        {
            get;
            set;
        }
    }

    public interface IBook
    {
        void AddGrade(double grade);
        Statistics GetStattistics();
        string Name {get;}
        event GradeAddedDelegate GradeAdded;
    }


    public abstract class Book : NamedObject, IBook
    {
        public Book(string name) : base(name)
        {
        }

        public abstract event GradeAddedDelegate GradeAdded;
        public abstract void AddGrade(double grade);
        public abstract Statistics GetStattistics();
    }

    public class DiskBook : Book
    {
        public DiskBook(string name) : base(name)
        {

        }

        public override event GradeAddedDelegate GradeAdded;

        public override void AddGrade(double grade)
        {
            if(grade <= 100 && grade >= 0)
            {
                using(var writer = File.AppendText($"{Name}.txt"))
                {
                    writer.WriteLine(grade);
                    if(GradeAdded != null)
                    {
                        GradeAdded(this, new EventArgs());
                    }
                }


            }
        }

        public override Statistics GetStattistics()
        {
            throw new NotImplementedException();
        }
    }
    public class InMemoryBook : Book
    {
        public InMemoryBook(string name) : base(name)
        {
            
            grades = new List<double>();
            Name = name;

        }
        public void AddGrade(char letter)
        {
            switch(letter)
            {
                case 'A':
                    AddGrade(90);
                    break;

                case 'B':
                    AddGrade(80);
                    break;
                case 'C':
                    AddGrade(70);
                    break;

                default:
                    AddGrade(0);
                    break;
            }

        }
        public override void AddGrade(double grade)
        {

            if(grade <= 100 && grade >= 0)
            {
                grades.Add(grade);
                if(GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }

            }
            else
            {
                throw new ArgumentException($"Invalid {nameof(grade)}");
                //Console.WriteLine("Invalid Value");
            }

        }

        public override event GradeAddedDelegate GradeAdded;

        public override Statistics GetStattistics()
        {
            var stat = new Statistics();
/*             result.Average = 0.0;
            result.High=   double.MinValue;
            result.Low    =   double.MaxValue; */
            double sumOfGrades = 0;

            for(var index = 0; index < grades.Count; index += 1)
            {

                //result.High = Math.Max(grades[index], result.High);
                stat.GetHighest(grades[index]);
                //result.Low= Math.Min(grades[index], result.Low);
                stat.GetLowest(grades[index]);
                //stat.Average += grades[index];
                sumOfGrades += grades[index];
            } 

            stat.CalculateAverage(sumOfGrades, grades.Count);
            stat.GetLetter(stat.Average);
            //result.Average /= grades.Count;
/*             switch (stat.Average)
            {
                case var d when  d >= 90.0:
                    stat.Letter = 'A';
                    break;

                case var d when  d >= 80.0:
                    stat.Letter = 'B';
                    break;

                case var d when  d >= 70.0:
                    stat.Letter = 'C';
                    break;

                case var d when  d >= 60.0:
                    stat.Letter = 'D';
                    break;

                default:
                    stat.Letter = 'F';
                    break;
            } */
            return stat;
        }
        private List<double> grades;


        public const string CATEGORY = "Science";



    }
}