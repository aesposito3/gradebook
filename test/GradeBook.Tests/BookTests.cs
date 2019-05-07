using System;
using Xunit;


namespace GradeBook.Tests
{
    public class BookTests
    {
        [Fact]
        public void BookCaculatesAnAverageGrade()
        {
            //arrange
            var book = new Book("");
            book.AddGrade(89.1);
            book.AddGrade(90.5);
            book.AddGrade(77.3);

            //act
            var result = book.GetStattistics();


            //assert
            Assert.Equal(85.6, result.Average, 1);
            Assert.Equal(90.5 , result.High, 1);
            Assert.Equal(77.3 , result.Low, 1);
            Assert.Equal('B', result.Letter);
        }
        [Fact]
        public void AddGradeContainsDataValidation()
        {
            var book = new Book("AddGradVal");


            book.AddGrade(90);
            Assert.Throws<ArgumentException>(() => book.AddGrade(100.1));
            Assert.Throws<ArgumentException>(() => book.AddGrade(-1));


            var result = book.GetStattistics();
            Assert.Equal(90, result.Average);
            Assert.Equal(90, result.Low);
            Assert.Equal(90, result.High);
            Assert.NotEqual(-1, result.Low);
            Assert.NotEqual(100.1, result.High);


        }
    }
}
