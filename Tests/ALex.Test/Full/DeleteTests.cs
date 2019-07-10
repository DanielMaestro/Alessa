using Xunit;

namespace ALex.Test.Full
{
    public class DeleteTests : ALexTesterFull
    {
        [Fact]
        public void SimpleDelete()
        {
            var query = @"Delete(My.Table AS ret)
";
            var q = this.GetQuery(query);
            Assert.Equal(@"DELETE FROM [My].[Table] AS [ret]", base.GetSentence(q));
        }

        [Fact]
        public void DeleteWithFilter()
        {
            var query = @"Delete(My.Table AS ret)
Filter(A > 1 AND A < 25)
";
            var q = this.GetQuery(query);
            Assert.Equal(@"DELETE FROM [My].[Table] AS [ret] WHERE ([A] >  1 AND [A] <  25)", base.GetSentence(q));
        }
    }
}
