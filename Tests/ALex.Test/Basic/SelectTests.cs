using Xunit;

namespace ALex.Test.Basic
{
    public class SelectTests : ALexTesterBasic
    {
        [Fact]
        public void SimpleSelect()
        {
            var query = @"From(MyTable)
Select(Col1, Col2)
";
            var q = this.GetQuery(query);
            Assert.Equal(@"SELECT [Col1], [Col2] FROM [MyTable]", base.GetSentence(q));
        }

        [Fact]
        public void SimpleSelectDistinct()
        {
            var query = @"From(MyTable)
Select(Col1, Col2)
Distinct()
";
            var q = this.GetQuery(query);
            Assert.Equal(@"SELECT DISTINCT [Col1], [Col2] FROM [MyTable]", base.GetSentence(q));
        }

        [Fact]
        public void SimpleSelectWithNull()
        {
            var query = @"From(MyTable)
Select(Col1, NULL AS Col2)
";
            var q = this.GetQuery(query);
            Assert.Equal(@"SELECT [Col1], NULL AS [Col2] FROM [MyTable]", base.GetSentence(q));
        }

        [Fact]
        public void SimpleSelectWithTableAlias()
        {
            var query = @"From(My.Table AS My)
Select(My.Col1, My.Col2)
";
            var q = this.GetQuery(query);
            Assert.Equal(@"SELECT [My].[Col1], [My].[Col2] FROM [My].[Table] AS [My]", base.GetSentence(q));
        }

        [Fact]
        public void SimpleSelectWithColumnAlias()
        {
            var query = @"From(My.Table AS My)
Select(My.Col1 AS ThisColum, My.Col2 AS AnotherColumn)
";
            var q = this.GetQuery(query);
            Assert.Equal(@"SELECT [My].[Col1] AS [ThisColum], [My].[Col2] AS [AnotherColumn] FROM [My].[Table] AS [My]", base.GetSentence(q));
        }

        [Fact]
        public void SelectWithOneOperationInSelect()
        {
            var query = @"From(My.Table)
Select(MyCol1 + 1 AS OperationColumn, MyCol2)
";
            var q = this.GetQuery(query);
            Assert.Equal(@"SELECT [MyCol1] + 1 AS [OperationColumn], [MyCol2] FROM [My].[Table]", base.GetSentence(q));
        }

        [Fact]
        public void SelectWithManyOperationInSelect()
        {
            var query = @"From(MyTable)
Select(MyCol1 + 1 / (45*0.25 / (AnotherColum % 0))  AS OperationColumn, MyCol2)
";
            var q = this.GetQuery(query);
            Assert.Equal(@"SELECT [MyCol1] + 1 /( 45 * 0.25 /([AnotherColum] % 0)) AS [OperationColumn], [MyCol2] FROM [MyTable]", base.GetSentence(q));
        }

        [Fact]
        public void SelectWithOneOperationInFilter()
        {
            var query = @"From(MyTable)
Select(MyCol1 + 1 / (45*0.25 / (AnotherColum % 0))  AS OperationColumn, MyCol2)
Filter((MyCol2 * 100) = 125)
";
            var q = this.GetQuery(query);
            Assert.Equal(@"SELECT [MyCol1] + 1 /( 45 * 0.25 /([AnotherColum] % 0)) AS [OperationColumn], [MyCol2] FROM [MyTable] WHERE ([MyCol2] * 100 =  125)", base.GetSentence(q));
        }

        [Fact]
        public void SelectWithManyOperationInFilter()
        {
            var query = @"From(MyTable)
Select(MyCol1 + 1 / (45*0.25 / (AnotherColum % 0))  AS OperationColumn, MyCol2)
Filter((MyCol2 * 100) *125 = (125 / MyCol1) - 85)
";
            var q = this.GetQuery(query);
            Assert.Equal(@"SELECT [MyCol1] + 1 /( 45 * 0.25 /([AnotherColum] % 0)) AS [OperationColumn], [MyCol2] FROM [MyTable] WHERE (([MyCol2] * 100) * 125 = ( 125 /[MyCol1]) - 85)", base.GetSentence(q));
        }

        [Fact]
        public void SelectWithHarcodedValues()
        {
            var query = @"From(MyTable)
Select(125 AS Val1, 'My String test' AS Val2)
";
            var q = this.GetQuery(query);
            Assert.Equal(@"SELECT 125 AS [Val1], 'My String test' AS [Val2] FROM [MyTable]", base.GetSentence(q));
        }

        [Fact]
        public void SelectWithManyOperationInFilterAliases()
        {
            var query = @"From(My.Table AS My)
Select(My.Col1 + 1 / (45*0.25 / (My.AnotherColum % 0))  AS OperationColumn, My.Col2)
Filter((My.Col2 * 100) *125 = (125 / My.Col1) - 85)
";
            var q = this.GetQuery(query);
            Assert.Equal(@"SELECT [My].[Col1] + 1 /( 45 * 0.25 /([My].[AnotherColum] % 0)) AS [OperationColumn], [My].[Col2] FROM [My].[Table] AS [My] WHERE (([My].[Col2] * 100) * 125 = ( 125 /[My].[Col1]) - 85)", base.GetSentence(q));
        }
    }
}
