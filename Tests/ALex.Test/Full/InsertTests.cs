using Xunit;

namespace ALex.Test.Full
{
    public class InsertTests : ALexTesterFull
    {
        [Fact]
        public void SimpleInsert()
        {
            var query = @"Insert(My.Table AS ret)
Columns(Col1, Col2, Col3, Col4)
Values(1,Jonh,'adsfag', My.Col4)
";
            var q = this.GetQuery(query);
            Assert.Equal(@"INSERT INTO [My].[Table] AS [ret] ([Col1], [Col2], [Col3], [Col4]) VALUES (1, '[Jonh]', 'adsfag', '[My].[Col4]')", base.GetSentence(q));
        }

        [Fact]
        public void InsertWithManyValues()
        {
            var query = @"Insert(My.Table AS ret)
Columns(Col1, Col2, Col3, Col4)
Values(1,Jonh,'adsfag', My.Col4)
Values(2,doe,'lkjhg', My.Col3)
";
            var q = this.GetQuery(query);
            Assert.Equal(@"INSERT INTO [My].[Table] AS [ret] ([Col1], [Col2], [Col3], [Col4]) VALUES (1, '[Jonh]', 'adsfag', '[My].[Col4]'), (2, '[doe]', 'lkjhg', '[My].[Col3]')", base.GetSentence(q));
        }

        [Fact]
        public void InsertWithQuery()
        {
            var query = @"Insert(My.Table AS ret)
Columns(Col1, Col2, Col3, Col4)
From(My.Table)
Select(My.Col1, My.Col2, My.Col3, My.Col4)
Filter(My.Col1 IS NOT NULL)
GroupBy(My.Col1)
Filter(My.Col2 Like '%h%')
";
            var q = this.GetQuery(query);
            Assert.Equal(@"INSERT INTO [My].[Table] AS [ret] ([Col1], [Col2], [Col3], [Col4]) SELECT [My].[Col1], [My].[Col2], [My].[Col3], [My].[Col4] FROM [My].[Table] WHERE ([My].[Col1] IS NOT NULL) AND (LOWER([My].[Col2]) like '%h%') GROUP BY [My].[Col1]", base.GetSentence(q));
        }
    }
}
