using Xunit;

namespace ALex.Test.Full
{
    public class JoinTests : ALexTesterFull
    {
        [Fact]
        public void JoinSimple()
        {
            var query = @"From(My.Table AS My)
Select(My.Col1, My.Col2, An.Col1, An.Col2)
Join(Left, An.OtherTable AS An, My.Col1 = An.Col1)
";
            var q = this.GetQuery(query);
            Assert.Equal(@"SELECT [My].[Col1], [My].[Col2], [An].[Col1], [An].[Col2] FROM [My].[Table] AS [My] 
LEFT JOIN [An].[OtherTable] AS [An] ON ([My].[Col1] = [An].[Col1])", base.GetSentence(q));
        }

        [Fact]
        public void JoinManyConstraints()
        {
            var query = @"From(My.Table AS My)
Select(My.Col1, My.Col2, An.Col1, An.Col2)
Join(Inner, An.OtherTable AS An, My.Col1 = An.Col1 AND (An.Col2 + 1 <= My.Col2 OR An.Col2 +1 >= My.Col2))
";
            var q = this.GetQuery(query);
            Assert.Equal(@"SELECT [My].[Col1], [My].[Col2], [An].[Col1], [An].[Col2] FROM [My].[Table] AS [My] 
INNER JOIN [An].[OtherTable] AS [An] ON ([My].[Col1] = [An].[Col1] AND ([An].[Col2] + 1 <= [My].[Col2] OR [An].[Col2] + 1 >= [My].[Col2]))", base.GetSentence(q));
        }

        [Fact]
        public void JoinWithFilters()
        {
            var query = @"From(My.Table AS My)
Select(My.Col1, My.Col2, An.Col1, An.Col2)
Join(Inner, An.OtherTable AS An, My.Col1 = An.Col1 AND (An.Col2 + 1 <= My.Col2 OR An.Col2 +1 >= My.Col2))
Filter(An.Col1 <= My.Col1 OR (My.Col2 * 1.15 >= 1000 AND An.Col2 <= 12))
Filter(My.Col3 >= 85)
";
            var q = this.GetQuery(query);
            Assert.Equal(@"SELECT [My].[Col1], [My].[Col2], [An].[Col1], [An].[Col2] FROM [My].[Table] AS [My] 
INNER JOIN [An].[OtherTable] AS [An] ON ([My].[Col1] = [An].[Col1] AND ([An].[Col2] + 1 <= [My].[Col2] OR [An].[Col2] + 1 >= [My].[Col2])) WHERE ([An].[Col1] <= [My].[Col1] OR ([My].[Col2] * 1.15 >=  1000 AND [An].[Col2] <=  12)) AND ([My].[Col3] >=  85)", base.GetSentence(q));
        }

        [Fact]
        public void JoinMultipleTables()
        {
            var query = @"From(My.Table AS My)
Select(My.Col1, A1.Col2, A2.Col1, A3.Col2)
Join(Inner, A1.OtherTable AS A1, My.Col1 = A1.Col1 AND (A1.Col2 + 1 <= My.Col2 OR A1.Col2 +1 >= My.Col2))
Join(Right, A2.OtherTable AS A2, My.Col1 = A2.Col1)
Join(Left, A3.OtherTable AS A3, My.Col1 = A3.Col1 AND A3.Col2 < My.Col2)
Join(Inner, OtherTable AS A4, My.Col1 = A1.Col1 AND (A1.Col2 + 1 <= My.Col2 OR A1.Col2 +1 >= My.Col2))
Join(Right, OtherTable AS A5, My.Col1 = A2.Col1)
Join(Left, OtherTable, My.Col1 = A3.Col1 AND A3.Col2 < My.Col2)
";
            var q = this.GetQuery(query);
            Assert.Equal(@"SELECT [My].[Col1], [A1].[Col2], [A2].[Col1], [A3].[Col2] FROM [My].[Table] AS [My] 
INNER JOIN [A1].[OtherTable] AS [A1] ON ([My].[Col1] = [A1].[Col1] AND ([A1].[Col2] + 1 <= [My].[Col2] OR [A1].[Col2] + 1 >= [My].[Col2]))
RIGHT JOIN [A2].[OtherTable] AS [A2] ON ([My].[Col1] = [A2].[Col1])
LEFT JOIN [A3].[OtherTable] AS [A3] ON ([My].[Col1] = [A3].[Col1] AND [A3].[Col2] < [My].[Col2])
INNER JOIN [OtherTable] AS [A4] ON ([My].[Col1] = [A1].[Col1] AND ([A1].[Col2] + 1 <= [My].[Col2] OR [A1].[Col2] + 1 >= [My].[Col2]))
RIGHT JOIN [OtherTable] AS [A5] ON ([My].[Col1] = [A2].[Col1])
LEFT JOIN [OtherTable] ON ([My].[Col1] = [A3].[Col1] AND [A3].[Col2] < [My].[Col2])", base.GetSentence(q));
        }

        [Fact]
        public void JoinAfterStatement()
        {
            var query = @"From(My.Table AS My)
Select(My.Col1, My.Col2, An.Col1, An.Col2)
Distinct()
Join(Inner, An.OtherTable AS An, My.Col1 = An.Col1 AND (An.Col2 + 1 <= My.Col2 OR An.Col2 +1 >= My.Col2))
Filter(An.Col1 <= My.Col1 OR (My.Col2 * 1.15 >= 1000 AND An.Col2 <= 12))
Filter(My.Col3 >= 85)
";
            query += "Join(left, Ot.OtherTable, My.Col1 = Ot.Col1) Filter(Ot.Col2 < 0)";
            var q = this.GetQuery(query);
            Assert.Equal(@"SELECT DISTINCT [My].[Col1], [My].[Col2], [An].[Col1], [An].[Col2] FROM [My].[Table] AS [My] 
INNER JOIN [An].[OtherTable] AS [An] ON ([My].[Col1] = [An].[Col1] AND ([An].[Col2] + 1 <= [My].[Col2] OR [An].[Col2] + 1 >= [My].[Col2]))
LEFT JOIN [Ot].[OtherTable] ON ([My].[Col1] = [Ot].[Col1]) WHERE ([An].[Col1] <= [My].[Col1] OR ([My].[Col2] * 1.15 >=  1000 AND [An].[Col2] <=  12)) AND ([My].[Col3] >=  85) AND ([Ot].[Col2] <  0)", base.GetSentence(q));
        }

    }
}
