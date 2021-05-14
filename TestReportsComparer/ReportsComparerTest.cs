using System.Linq;
using NUnit.Framework;
using ReportsComparer;
using ReportsComparer.ReportViewFiles;

namespace TestReportsComparer
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }
        
        [TestCase("1", "2", ExpectedResult = "+1(100%)", TestName = "int values first is greater")]
        [TestCase("2", "1", ExpectedResult = "-1(-50%)", TestName = "int values second is greater")]
        [TestCase("0", "1", ExpectedResult = "+1(0%)", TestName = "int values first is zero")]
        [TestCase("0", "0", ExpectedResult = "0(0%)", TestName = "int values both are zero")]
        [TestCase("100", "100", ExpectedResult = "0(0%)", TestName = "int values both are equal")]
        public string CompareValues(string first, string second)
        {
            return ReportsComparer.ReportsComparer.CompareValues(first, second);
        }

        [Test]
        public void GetTextsCompared_CorrectIntValues()
        {
            var baseTexts = new [] {new Text {Name = "Name1", Value = "1"}, new Text {Name = "Name2", Value = "100"}};
            
            var newTexts = new [] {new Text {Name = "Name1", Value = "10"}, new Text {Name = "Name2", Value = "10"}};

            var expected = new[]
                {
                    new Text {Name = "Name1", Value = ReportsComparer.ReportsComparer.CompareValues("1", "10")}, 
                    new Text {Name = "Name2", Value = ReportsComparer.ReportsComparer.CompareValues("100", "10")}
                }
                .Select(x => (x.Name, x.Value));

            var actual = ReportsComparer.ReportsComparer.GetTextsCompared(baseTexts, newTexts)
                .Select(x => (x.Name, x.Value));
            
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void GetTextsCompared_CorrectFloatValues()
        {
            var baseTexts = new [] {new Text {Name = "Name1", Value = "1.7"}, new Text {Name = "Name2", Value = "100.5"}};
            
            var newTexts = new [] {new Text {Name = "Name1", Value = "10.2"}, new Text {Name = "Name2", Value = "10.3"}};

            var expected = new[]
                {
                    new Text {Name = "Name1", Value = ReportsComparer.ReportsComparer.CompareValues("1.7", "10.2")}, 
                    new Text {Name = "Name2", Value = ReportsComparer.ReportsComparer.CompareValues("100.5", "10.3")}
                }
                .Select(x => (x.Name, x.Value));

            var actual = ReportsComparer.ReportsComparer.GetTextsCompared(baseTexts, newTexts)
                .Select(x => (x.Name, x.Value));
            
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void GetTextsCompared_FirstValueIsZero()
        {
            var baseTexts = new [] {new Text {Name = "Name1", Value = "0"}};
            
            var newTexts = new [] {new Text {Name = "Name1", Value = "10"}};

            var expected = new[]
                {
                    new Text {Name = "Name1", Value = ReportsComparer.ReportsComparer.CompareValues("0", "10")}
                }
                .Select(x => (x.Name, x.Value));

            var actual = ReportsComparer.ReportsComparer.GetTextsCompared(baseTexts, newTexts)
                .Select(x => (x.Name, x.Value));
            
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void GetMatricesCompared_CorrectIntValues()
        {
            var baseMatrix = new [] {new [] {"Row1", "10", "20"}, new [] {"Row2", "30", "100"}};
            
            var newMatrix = new [] {new [] {"Row1", "20", "10"}, new [] {"Row2", "10", "200"}};

            var expected = new[]
            {
                new[]
                {
                    "Row1",
                    ReportsComparer.ReportsComparer.CompareValues("10", "20"),
                    ReportsComparer.ReportsComparer.CompareValues("20", "10")
                },
                new[]
                {
                    "Row2",
                    ReportsComparer.ReportsComparer.CompareValues("30", "10"),
                    ReportsComparer.ReportsComparer.CompareValues("100", "200")
                }
            };

            var actual = ReportsComparer.ReportsComparer.GetMatricesCompared(baseMatrix, newMatrix);

            Assert.AreEqual(expected, actual);
        }
    }
}