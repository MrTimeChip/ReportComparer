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
        public string Test_CompareValues(string first, string second)
        {
            return ReportsComparer.ReportsComparer.CompareValues(first, second);
        }

        [Test]
        public void Test_GetTextsCompared_CorrectIntValues()
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
        public void Test_GetTextsCompared_CorrectFloatValues()
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
        public void Test_GetTextsCompared_FirstValueIsZero()
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
        public void Test_GetMatricesCompared_CorrectIntValues()
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
        
        [Test]
        public void Test_GetMatricesCompared_CorrectFloatValues()
        {
            var baseMatrix = new [] {new [] {"Row1", "10.2", "20.2"}, new [] {"Row2", "30.77", "100.2"}};
            
            var newMatrix = new [] {new [] {"Row1", "20.4", "10.1"}, new [] {"Row2", "10.1", "200.1"}};

            var expected = new[]
            {
                new[]
                {
                    "Row1",
                    ReportsComparer.ReportsComparer.CompareValues("10.2", "20.4"),
                    ReportsComparer.ReportsComparer.CompareValues("20.2", "10.1")
                },
                new[]
                {
                    "Row2",
                    ReportsComparer.ReportsComparer.CompareValues("30.77", "10.1"),
                    ReportsComparer.ReportsComparer.CompareValues("100.2", "200.1")
                }
            };

            var actual = ReportsComparer.ReportsComparer.GetMatricesCompared(baseMatrix, newMatrix);

            Assert.AreEqual(expected, actual);
        }

        [Test] public void Test_GetTablesCompared_CorrectValues_SingleTable()
        {
            var baseMatrix = new [] {new [] {"Row1", "10.2", "20.2"}, new [] {"Row2", "30.77", "100.2"}};
            
            var newMatrix = new [] {new [] {"Row1", "20.4", "10.1"}, new [] {"Row2", "10.1", "200.1"}};

            var baseTable = new Table
            {
                Columns = new[] {"Column1", "Column2", "Column3"}, 
                Matrix = baseMatrix, 
                Name = "Name1", 
                Rows = null
            };
            
            var newTable = new Table
            {
                Columns = new[] {"Column1", "Column2", "Column3"}, 
                Matrix = newMatrix, 
                Name = "Name1", 
                Rows = null
            };

            var expectedMatrix = new[]
            {
                new[]
                {
                    "Row1",
                    ReportsComparer.ReportsComparer.CompareValues("10.2", "20.4"),
                    ReportsComparer.ReportsComparer.CompareValues("20.2", "10.1")
                },
                new[]
                {
                    "Row2",
                    ReportsComparer.ReportsComparer.CompareValues("30.77", "10.1"),
                    ReportsComparer.ReportsComparer.CompareValues("100.2", "200.1")
                }
            };

            var expectedTable = new Table
            {
                Columns = new[] {"Column1", "Column2", "Column3"},
                Matrix = expectedMatrix,
                Name = "Name1",
                Rows = null
            };

            var actual = ReportsComparer.ReportsComparer.GetTablesCompared(new[] {baseTable}, new[] {newTable});
            var actualTable = actual[0];

            Assert.AreEqual(expectedTable.Matrix, actualTable.Matrix);
            Assert.AreEqual(expectedTable.Columns, actualTable.Columns);
            Assert.AreEqual(expectedTable.Name, actualTable.Name);
            Assert.AreEqual(expectedTable.Rows, actualTable.Rows);
        }
        
        [Test] public void Test_GetTablesCompared_CorrectValues_TwoTables()
        {
            var baseMatrixFirst = new [] {new [] {"Row1", "10.2", "20.2"}, new [] {"Row2", "30.77", "100.2"}};
            var baseMatrixSecond = new [] {new [] {"Row1", "10", "20"}, new [] {"Row2", "30", "100"}};
            
            var newMatrixFirst = new [] {new [] {"Row1", "20.4", "10.1"}, new [] {"Row2", "10.1", "200.1"}};
            var newMatrixSecond = new [] {new [] {"Row1", "20", "10"}, new [] {"Row2", "10", "200"}};

            var baseTableFirst = new Table
            {
                Columns = new[] {"Column1", "Column2", "Column3"}, 
                Matrix = baseMatrixFirst, 
                Name = "Name1", 
                Rows = null
            };
            
            var baseTableSecond = new Table
            {
                Columns = new[] {"Column1", "Column2", "Column3"}, 
                Matrix = baseMatrixSecond, 
                Name = "Name2", 
                Rows = null
            };
            
            var newTableFirst = new Table
            {
                Columns = new[] {"Column1", "Column2", "Column3"}, 
                Matrix = newMatrixFirst, 
                Name = "Name1", 
                Rows = null
            };
            
            var newTableSecond = new Table
            {
                Columns = new[] {"Column1", "Column2", "Column3"}, 
                Matrix = newMatrixSecond, 
                Name = "Name2", 
                Rows = null
            };


            var expectedMatrixFirst = new[]
            {
                new[]
                {
                    "Row1",
                    ReportsComparer.ReportsComparer.CompareValues("10.2", "20.4"),
                    ReportsComparer.ReportsComparer.CompareValues("20.2", "10.1")
                },
                new[]
                {
                    "Row2",
                    ReportsComparer.ReportsComparer.CompareValues("30.77", "10.1"),
                    ReportsComparer.ReportsComparer.CompareValues("100.2", "200.1")
                }
            };
            
            var expectedMatrixSecond = new[]
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

            var expectedTableFirst = new Table
            {
                Columns = new[] {"Column1", "Column2", "Column3"},
                Matrix = expectedMatrixFirst,
                Name = "Name1",
                Rows = null
            };
            
            var expectedTableSecond = new Table
            {
                Columns = new[] {"Column1", "Column2", "Column3"},
                Matrix = expectedMatrixSecond,
                Name = "Name2",
                Rows = null
            };

            var actual = ReportsComparer.ReportsComparer.GetTablesCompared(
                new[] {baseTableFirst, baseTableSecond}, 
                new[] {newTableFirst, newTableSecond});

            var actualTableFirst = actual[0];
            var actualTableSecond = actual[1];

            Assert.AreEqual(actualTableFirst.Matrix, expectedTableFirst.Matrix);
            Assert.AreEqual(actualTableFirst.Columns, expectedTableFirst.Columns);
            Assert.AreEqual(actualTableFirst.Name, expectedTableFirst.Name);
            Assert.AreEqual(actualTableFirst.Rows, expectedTableFirst.Rows);
            
            Assert.AreEqual(actualTableSecond.Matrix, expectedTableSecond.Matrix);
            Assert.AreEqual(actualTableSecond.Columns, expectedTableSecond.Columns);
            Assert.AreEqual(actualTableSecond.Name, expectedTableSecond.Name);
            Assert.AreEqual(actualTableSecond.Rows, expectedTableSecond.Rows);
        }
    }
}