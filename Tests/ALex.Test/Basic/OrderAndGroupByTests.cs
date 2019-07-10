using Xunit;

namespace ALex.Test.Basic
{
    public class OrderAndGroupByTests : ALexTesterBasic
    {
        [Fact]
        public void OrderBy()
        {
            var query = @"From(My.Table)
Select(My.Col1, My.Col2)
Filter(My.Col1 IS NOT NULL)
OrderBY(My.Col1 DESC, My.Col2 ASC)
";
            var q = this.GetQuery(query);
            Assert.Equal(@"SELECT [My].[Col1], [My].[Col2] FROM [My].[Table] WHERE ([My].[Col1] IS NOT NULL) ORDER BY [My].[Col1] DESC, [My].[Col2]", base.GetSentence(q));
        }

        [Fact]
        public void GroupBySimple()
        {
            var query = @"From(My.Table)
Select(My.Col1, My.Col2)
Groupby(My.Col1, My.Col2)
";
            var q = this.GetQuery(query);
            Assert.Equal(@"SELECT [My].[Col1], [My].[Col2] FROM [My].[Table] GROUP BY [My].[Col1], [My].[Col2]", base.GetSentence(q));
        }

        [Fact]
        public void GroupByComplex()
        {
            var query = @"From(My.Table)
Select(My.Col1, My.Col2)
Groupby(My.Col1, MAX(My.Col2), My.Col1 + 1)
";
            var q = this.GetQuery(query);
            Assert.Equal(@"SELECT [My].[Col1], [My].[Col2] FROM [My].[Table] GROUP BY [My].[Col1], MAX(My.Col2), [My].[Col1] + 1", base.GetSentence(q));
        }
    }
}
