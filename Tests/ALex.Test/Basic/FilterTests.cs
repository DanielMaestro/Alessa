using Xunit;

namespace ALex.Test.Basic
{
    public class FilterTests : ALexTesterBasic
    {
        [Fact]
        public void SimpleSelectWithOneFilter()
        {
            var query = @"From(MyTable)
Select(MyCol1, MyCol2)
Filter((MyCol1 > 0 OR MyCol2 < 1) AND (MyCol1 = 2 OR MyCol2 <> 3) AND (MyCol1 >= 4 OR MyCol2 <= 5) AND (MyCol1 != 6))
";
            var q = this.GetQuery(query);
            Assert.Equal("SELECT [MyCol1], [MyCol2] FROM [MyTable] WHERE (([MyCol1] >  0 OR [MyCol2] <  1) AND ([MyCol1] =  2 OR [MyCol2] <>  3) AND ([MyCol1] >=  4 OR [MyCol2] <=  5) AND ([MyCol1] <>  6))", base.GetSentence(q));
        }

        [Fact]
        public void SimpleSelectWithTwoFilters()
        {
            var query = @"From(MyTable)
Select(MyCol1, MyCol2)
Filter((MyCol1 > 0 OR MyCol < 1) AND (MyCol1 = 2 OR MyCol2 <> 3) AND (MyCol1 >= 4 OR MyCol2 <= 5) AND (MyCol1 != 6))
Filter(MyCol1 = 35)
";
            var q = this.GetQuery(query);
            Assert.Equal("SELECT [MyCol1], [MyCol2] FROM [MyTable] WHERE (([MyCol1] >  0 OR [MyCol] <  1) AND ([MyCol1] =  2 OR [MyCol2] <>  3) AND ([MyCol1] >=  4 OR [MyCol2] <=  5) AND ([MyCol1] <>  6)) AND ([MyCol1] =  35)", base.GetSentence(q));
        }

        [Fact]
        public void SimpleSelectWithTwoFiltersAndAlias()
        {
            var query = @"From(My.Table)
Select(MyCol1, MyCol2)
Filter((My.Col1 > 0 OR My.Col < 1) AND (My.Col1 = 2 OR MyCol2 <> 3) AND (My.Col1 >= 4 OR MyCol2 <= 5) AND (MyCol1 != 6))
Filter(My.Col1 = 35)
";
            var q = this.GetQuery(query);
            Assert.Equal("SELECT [MyCol1], [MyCol2] FROM [My].[Table] WHERE (([My].[Col1] >  0 OR [My].[Col] <  1) AND ([My].[Col1] =  2 OR [MyCol2] <>  3) AND ([My].[Col1] >=  4 OR [MyCol2] <=  5) AND ([MyCol1] <>  6)) AND ([My].[Col1] =  35)", base.GetSentence(q));
        }
        [Fact]
        public void InStatement()
        {
            var query = @"From(My.Table)
Select(My.Col1, My.Col2)
Filter(My.Col1 In ('val1', 'val2', 'val3'))
";
            var q = this.GetQuery(query);
            Assert.Equal("SELECT [My].[Col1], [My].[Col2] FROM [My].[Table] WHERE ([My].[Col1] IN ('val1', 'val2', 'val3'))", base.GetSentence(q));
        }

        [Fact]
        public void NotInStatement()
        {
            var query = @"From(My.Table)
Select(My.Col1, My.Col2)
Filter(My.Col1 Not In ('val1', 'val2', 'val3'))
";
            var q = this.GetQuery(query);
            Assert.Equal("SELECT [My].[Col1], [My].[Col2] FROM [My].[Table] WHERE ([My].[Col1] NOT IN ('val1', 'val2', 'val3'))", base.GetSentence(q));
        }

        [Fact]
        public void LikeStatement()
        {
            var query = @"From(My.Table)
Select(My.Col1, My.Col2)
Filter(My.Col1 LIKE '%term%')
";
            var q = this.GetQuery(query);
            Assert.Equal("SELECT [My].[Col1], [My].[Col2] FROM [My].[Table] WHERE (LOWER([My].[Col1]) like '%term%')", base.GetSentence(q));
        }

        [Fact]
        public void NotLikeStatement()
        {
            var query = @"From(My.Table)
Select(My.Col1, My.Col2)
Filter(My.Col1 Not LIKE '%term%')
";
            var q = this.GetQuery(query);
            Assert.Equal("SELECT [My].[Col1], [My].[Col2] FROM [My].[Table] WHERE (NOT (LOWER([My].[Col1]) like '%term%'))", base.GetSentence(q));
        }

        [Fact]
        public void IsNullStatement()
        {
            var query = @"From(My.Table)
Select(My.Col1, My.Col2)
Filter(My.Col1 IS NULL)
";
            var q = this.GetQuery(query);
            Assert.Equal("SELECT [My].[Col1], [My].[Col2] FROM [My].[Table] WHERE ([My].[Col1] IS NULL)", base.GetSentence(q));
        }

        [Fact]
        public void IsNotNullStatement()
        {
            var query = @"From(My.Table)
Select(My.Col1, My.Col2)
Filter(My.Col1 IS NOT NULL)
";
            var q = this.GetQuery(query);
            Assert.Equal("SELECT [My].[Col1], [My].[Col2] FROM [My].[Table] WHERE ([My].[Col1] IS NOT NULL)", base.GetSentence(q));
        }

        [Fact]
        public void FilterAtEnd()
        {
            var query = @"From(My.Table)
Select(My.Col1, My.Col2)
Filter(My.Col1 IS NOT NULL)
GroupBy(My.Col1)
Filter(My.Col2 Like '%h%')
";
            var q = this.GetQuery(query);
            Assert.Equal("SELECT [My].[Col1], [My].[Col2] FROM [My].[Table] WHERE ([My].[Col1] IS NOT NULL) AND (LOWER([My].[Col2]) like '%h%') GROUP BY [My].[Col1]", base.GetSentence(q));
        }

    }
}
