using Xunit;

namespace ALex.Test.Full
{
    public class MiscTests : ALexTesterFull
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
Page(2)
Limit(10)
";
            var q = this.GetQuery(query);
            Assert.Equal(@"SELECT * FROM (SELECT [My].[Col1], [My].[Col2], ROW_NUMBER() OVER (ORDER BY [My].[Col1] DESC, [My].[Col2]) AS [row_num] FROM [My].[Table] WHERE ([My].[Col1] IS NOT NULL)) AS [results_wrapper] WHERE [row_num] BETWEEN 11 AND 20", base.GetSentence(q));
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
GroupBy(My.Col1)
";
            var q = this.GetQuery(query);
            Assert.Equal(@"SELECT [My].[Col1], MAX([My].[Col2] + 1) AS [Expression] FROM [My].[Table] GROUP BY [My].[Col1]", base.GetSentence(q));
        }

        [Fact]
        public void RawInSelect()
        {
            var query = @"From(MyTable)
Select(RAW('CASE WHEN A < B THEN 1 WHEN A = B THEN 2 ELSE 3 END') AS Result)
";
            var q = this.GetQuery(query);
            Assert.Equal(@"SELECT CASE WHEN A < B THEN 1 WHEN A = B THEN 2 ELSE 3 END AS [Result] FROM [MyTable]", base.GetSentence(q));
        }

        [Fact]
        public void RawInFilter()
        {
            var query = @"From(MyTable)
Select(A,B,C)
FILTER(RAW('CASE WHEN A < B THEN 1 WHEN A = B THEN 2 ELSE 3 END IS NOT NULL'))
";
            var q = this.GetQuery(query);
            Assert.Equal(@"SELECT [A], [B], [C] FROM [MyTable] WHERE (CASE WHEN A < B THEN 1 WHEN A = B THEN 2 ELSE 3 END IS NOT NULL)", base.GetSentence(q));
        }


        [Fact]
        public void TestAllTogether()
        {
            var query = @"From(MT.MyTable AS My)
Select(RAW('CASE WHEN A <> B THEN B WHEN C <= A THEN D END') AS 'asfg', 124 as dgseg,MT.TextColumn as MyColumn, MT.IntegerColumn, AnotherText, DateColumn as fsegtoi, JT.JoinedText, MAX(MT.DoubleColumn) as asfgrtj, JT.LeftOperator + MT.RightOperator AS sdghtj)
Distinct()
Join(Left,JT.JoinTable AS Jt, MT.LeftColumn = JT.RightColum AND (MT.BooleanColumn = 1 OR JT.DoubleColumn <= 25.354))
Join(RIGHT,OJ.OtherJoin AS OJ, OJ.LeftColumn = JT.RightColum AND (OJ.BooleanColumn != 1))
Filter(((MT.DateColumn >= '2018-01-01' AND MT.DateColumn <= '2018-01-31')AND (JT.JoinedText LIKE '%my type%' OR JT.JoinedText LIKE 'he%' OR JT.JoinedText not LIKE '%llo'))OR MT.TextColumn not LIKE '%wire%' OR MT.TextColumn like 'cable' AND MT.AnotherText in ('val1', 'val2', 'val3') OR MT.DoubleField >= 45.86 AND MT.AnyOther IS NOT NULL)
Filter(RAW('CratedDate < ''2018-01-01''') AND MT.Main <> OJ.cdt OR OJ.Enabled = -1)
GroupBy(MT.TextColumn + 1, MIN(MT.IntegerColumn), MT.AnotherText, MT.DateColumn, JT.JoinedText)
OrderBy(MT.IntegerColumn, MT.DateColumn DESC)
Page(2)
Limit(10)
Filter(MT.Mine <> OJ.ASDGFR OR OJ.CrteatedDate >= '2018-05-18')
";
            var q = this.GetQuery(query);
            Assert.Equal(@"SELECT * FROM (SELECT DISTINCT CASE WHEN A <> B THEN B WHEN C <= A THEN D END AS [asfg], 124 AS [dgseg], [MT].[TextColumn] AS [MyColumn], [MT].[IntegerColumn], [AnotherText], [DateColumn] AS [fsegtoi], [JT].[JoinedText], MAX(MT.DoubleColumn) AS [asfgrtj], [JT].[LeftOperator] +[MT].[RightOperator] AS [sdghtj], ROW_NUMBER() OVER (ORDER BY [MT].[IntegerColumn], [MT].[DateColumn] DESC) AS [row_num] FROM [MT].[MyTable] AS [My] 
LEFT JOIN [JT].[JoinTable] AS [Jt] ON ([MT].[LeftColumn] = [JT].[RightColum] AND ([MT].[BooleanColumn] =  1 OR [JT].[DoubleColumn] <=  25.354))
RIGHT JOIN [OJ].[OtherJoin] AS [OJ] ON ([OJ].[LeftColumn] = [JT].[RightColum] AND ([OJ].[BooleanColumn] <>  1)) WHERE ((([MT].[DateColumn] >=  '2018-01-01' AND [MT].[DateColumn] <=  '2018-01-31') AND (LOWER([JT].[JoinedText]) like '%my type%' OR LOWER([JT].[JoinedText]) like 'he%' OR NOT (LOWER([JT].[JoinedText]) like '%llo'))) OR NOT (LOWER([MT].[TextColumn]) like '%wire%') OR LOWER([MT].[TextColumn]) like 'cable' AND [MT].[AnotherText] IN ('val1', 'val2', 'val3') OR [MT].[DoubleField] >=  45.86 AND [MT].[AnyOther] IS NOT NULL) AND (CratedDate < '2018-01-01' AND [MT].[Main] <> [OJ].[cdt] OR [OJ].[Enabled] =  -1) AND ([MT].[Mine] <> [OJ].[ASDGFR] OR [OJ].[CrteatedDate] >=  '2018-05-18') GROUP BY [MT].[TextColumn] + 1, MIN(MT.IntegerColumn), [MT].[AnotherText], [MT].[DateColumn], [JT].[JoinedText]) AS [results_wrapper] WHERE [row_num] BETWEEN 11 AND 20", base.GetSentence(q));
        }
    }
}
