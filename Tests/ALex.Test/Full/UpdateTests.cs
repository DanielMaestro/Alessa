using Xunit;

namespace ALex.Test.Full
{
    public class UpdateTests : ALexTesterFull
    {
        [Fact]
        public void SimpleUpdate()
        {
            var query = @"Update(My.Table AS My)
Set(My.Col1 = 1, My.Col2 = 'Test')
";
            var q = this.GetQuery(query);
            Assert.Equal(@"UPDATE [My].[Table] AS [My] SET [Col1] = 1, [Col2] = 'Test'", base.GetSentence(q));
        }

        [Fact]
        public void UpdateWithFilter()
        {
            var query = @"Update(My.Table AS ret)
Set(My.Col1 = 1, My.Col2 = 'Test')
Filter(My.Col3 IS NULL)
";
            var q = this.GetQuery(query);
            Assert.Equal(@"UPDATE [My].[Table] AS [ret] SET [Col1] = 1, [Col2] = 'Test' WHERE ([My].[Col3] IS NULL)", base.GetSentence(q));
        }

        [Fact]
        public void UpdateMultipleSets()
        {
            var query = @"Update(My.Table AS ret)
Set(My.Col1 = 1, My.Col2 = 'Test')
Filter(My.Col3 IS NULL)
Set(My.Col3 = 2, My.Col4 = 'Hello')
Set(My.Col5 = 3, My.Col6 = 'There')
Filter(My.Col4 < '2020-12-31')
Filter(My.Col4 > '2020-01-01')
";
            var q = this.GetQuery(query);
            Assert.Equal(@"UPDATE [My].[Table] AS [ret] SET [Col1] = 1, [Col2] = 'Test', [Col3] = 2, [Col4] = 'Hello', [Col5] = 3, [Col6] = 'There' WHERE ([My].[Col3] IS NULL) AND ([My].[Col4] <  '2020-12-31') AND ([My].[Col4] >  '2020-01-01')", base.GetSentence(q));
        }


        /******************** There is not support for UPDATE with joins yet *****************************/
        //        [Fact]
        //        public void UpdateWithLimit()
        //        {
        //            var query = @"Update(My.Table AS ret)
        //Set(My.Col1 = 1, My.Col2 = 'Test')
        //Filter(My.Col3 IS NULL)
        //Limit(15)
        //";
        //            var q = this.GetQuery(query);
        //            Assert.Equal(@"", base.GetSentence(q));
        //        }

        //        [Fact]
        //        public void UpdateWithSubquery()
        //        {
        //            var query = @"Update(My.Table AS ret)
        //Set(My.Col1 = 1, My.Col2 = 'Test')
        //Join(left, A.Table AS A, My.Col1 = A.Col1 OR My.Col2 = A.Col2)
        //Filter(A.Col3 IS NOT NULL)
        //";
        //            var q = this.GetQuery(query);
        //            Assert.Equal(@"", base.GetSentence(q));
        //        }
    }
}
