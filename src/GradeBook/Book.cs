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
            var stat = new Statistics();
            using(var data = File.OpenText($"{Name}.txt"))
            {
                var line = data.ReadLine();
                while (line!= null)
                {
                    var number = double.Parse(line);
                    stat.Add(number);
                    line = data.ReadLine();
                }
            }

            //var grades = Convert.ToDouble(sgrades);


            return stat;

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


            for(var index = 0; index < grades.Count; index += 1)
            {
                stat.Add(grades[index]);
            } 


            return stat;
        }
        private List<double> grades;
        public const string CATEGORY = "Science";
    }
}