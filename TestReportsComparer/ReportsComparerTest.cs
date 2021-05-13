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
        
        [TestCase("1", "2", ExpectedResult = "+1(100%)")]
        [TestCase("2", "1", ExpectedResult = "-1(-50%)")]
        [TestCase("0", "1", ExpectedResult = "+1(0%)")]
        [TestCase("0", "0", ExpectedResult = "0(0%)")]
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
    }
}