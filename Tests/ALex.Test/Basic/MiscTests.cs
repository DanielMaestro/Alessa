using Xunit;

namespace ALex.Test.Basic
{
    public class MiscTests : ALexTesterBasic
    {

        [Fact]
        public void TopStatement()
        {
            var query = @"From(My.Table)
Select(My.Col1, My.Col2)
Filter(My.Col1 IS NULL)
Limit(10)
";
            var q = this.GetQuery(query);
            Assert.Equal(@"SELECT TOP (10) [My].[Col1], [My].[Col2] FROM [My].[Table] WHERE ([My].[Col1] IS NULL)", base.GetSentence(q));
        }

        [Fact]
        public void Paging()
        {
            var query = @"From(My.Table)
Select(My.Col1, My.Col2)
Filter(My.Col1 IS NOT NULL)
OrderBY(My.Col1 DESC, My.Col2 ASC)
Page(1)
Limit(10)
";
            var q = this.GetQuery(query);
            Assert.Equal(@"SELECT TOP (10) [My].[Col1], [My].[Col2] FROM [My].[Table] WHERE ([My].[Col1] IS NOT NULL) ORDER BY [My].[Col1] DESC, [My].[Col2]", base.GetSentence(q));
        }

        [Fact]
        public void AggregatesSimple()
        {
            var query = @"From(My.Table)
Select(My.Col1, MAX(My.Col2) AS Max, MIN(My.Col2) AS Min, AVG(My.Col3) AS 'The Average', SUM(My.Col4) AS Sum)
Groupby(My.Col1)
";
            var q = this.GetQuery(query);
            Assert.Equal(@"SELECT [My].[Col1], MAX(My.Col2) AS [Max], MIN(My.Col2) AS [Min], AVG(My.Col3) AS [The Average], SUM(My.Col4) AS [Sum] FROM [My].[Table] GROUP BY [My].[Col1]", base.GetSentence(q));
        }

        [Fact]
        public void AggregatesComplex()
        {
            var query = @"From(My.Table)
Select(My.Col1, MAX(My.Col2 + 1) AS Expression)
Groupby(My.Col1)
";
            var q = this.GetQuery(query);
            Assert.Equal(@"SELECT [My].[Col1], MAX([My].[Col2] + 1) AS [Expression] FROM [My].[Table] GROUP BY [My].[Col1]", base.GetSentence(q));
        }

        [Fact]
        public void TestAllTogether()
        {
            var query = @"From(MT.MyTable AS My)
Select(124 as dgseg,MT.TextColumn as MyColumn, MT.IntegerColumn, AnotherText, DateColumn as fsegtoi, JT.JoinedText, MAX(MT.DoubleColumn) as asfgrtj, JT.LeftOperator + MT.RightOperator AS sdghtj)
Distinct()
Filter(((MT.DateColumn >= '2018-01-01' AND MT.DateColumn <= '2018-01-31')AND (JT.JoinedText LIKE '%my type%' OR JT.JoinedText LIKE 'he%' OR JT.JoinedText not LIKE '%llo'))OR MT.TextColumn not LIKE '%wire%' OR MT.TextColumn like 'cable' AND MT.AnotherText in ('val1', 'val2', 'val3') OR MT.DoubleField >= 45.86 AND MT.AnyOther IS NOT NULL)
Filter(MT.Main <> OJ.cdt OR OJ.Enabled = -1)
GroupBy(MT.TextColumn + 1, MIN(MT.IntegerColumn), MT.AnotherText, MT.DateColumn, JT.JoinedText)
OrderBy(MT.IntegerColumn, MT.DateColumn DESC)
Page(1)
Limit(10)
Filter(MT.Mine <> OJ.ASDGFR OR OJ.CrteatedDate >= '2018-05-18')
";
            var q = this.GetQuery(query);
            Assert.Equal(@"SELECT DISTINCT TOP (10) 124 AS [dgseg], [MT].[TextColumn] AS [MyColumn], [MT].[IntegerColumn], [AnotherText], [DateColumn] AS [fsegtoi], [JT].[JoinedText], MAX(MT.DoubleColumn) AS [asfgrtj], [JT].[LeftOperator] +[MT].[RightOperator] AS [sdghtj] FROM [MT].[MyTable] AS [My] WHERE ((([MT].[DateColumn] >=  '2018-01-01' AND [MT].[DateColumn] <=  '2018-01-31') AND (LOWER([JT].[JoinedText]) like '%my type%' OR LOWER([JT].[JoinedText]) like 'he%' OR NOT (LOWER([JT].[JoinedText]) like '%llo'))) OR NOT (LOWER([MT].[TextColumn]) like '%wire%') OR LOWER([MT].[TextColumn]) like 'cable' AND [MT].[AnotherText] IN ('val1', 'val2', 'val3') OR [MT].[DoubleField] >=  45.86 AND [MT].[AnyOther] IS NOT NULL) AND ([MT].[Main] <> [OJ].[cdt] OR [OJ].[Enabled] =  -1) AND ([MT].[Mine] <> [OJ].[ASDGFR] OR [OJ].[CrteatedDate] >=  '2018-05-18') GROUP BY [MT].[TextColumn] + 1, MIN(MT.IntegerColumn), [MT].[AnotherText], [MT].[DateColumn], [JT].[JoinedText] ORDER BY [MT].[IntegerColumn], [MT].[DateColumn] DESC", base.GetSentence(q));
        }
    }
}
