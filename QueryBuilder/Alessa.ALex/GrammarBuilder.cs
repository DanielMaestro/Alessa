using Irony.Parsing;

namespace Alessa.ALex
{
    /// <summary>
    /// This is a class for helping to build a common builder.
    /// </summary>
    internal static class GrammarBuilder
    {
        /// <summary>
        /// Builds the grammar.
        /// </summary>
        /// <param name="grammar">Grammar intance.</param>
        /// <param name="handleAsInternal">If is true it allows join statements and raw queries.</param>
        /// <returns></returns>
        internal static NonTerminal GetRoot(Grammar grammar, bool handleAsInternal)
        {
            // Root
            var Program = new NonTerminal("Program");

            #region Statements declaration
            var FromStmt = new NonTerminal(Common.ALexConstants.FromStmt);
            var SelectStmt = new NonTerminal(Common.ALexConstants.SelectStmt);
            var JoinStmt = new NonTerminal(Common.ALexConstants.JoinStmt);
            var FilterStmt = new NonTerminal(Common.ALexConstants.FilterStmt);
            var OrderByStmt = new NonTerminal(Common.ALexConstants.OrderByStmt);
            var GroupByStmt = new NonTerminal(Common.ALexConstants.GroupByStmt);
            var PagingStmnt = new NonTerminal(Common.ALexConstants.PagingStmnt);
            var QueryStmt = new NonTerminal(Common.ALexConstants.QueryStmt);

            var UpdateStmt = new NonTerminal(Common.ALexConstants.UpdateStmt);

            var InsertStmt = new NonTerminal(Common.ALexConstants.InsertStmt);

            var DeleteStmt = new NonTerminal(Common.ALexConstants.DeleteStmt);
            #endregion

            if (handleAsInternal)
                Program.Rule = QueryStmt | UpdateStmt | InsertStmt | DeleteStmt;
            else
                Program.Rule = QueryStmt;

            #region Common

            // Keywords
            var IN = grammar.ToTerm(Common.ALexConstants.IN);
            var AND = grammar.ToTerm(Common.ALexConstants.AND);
            var OR = grammar.ToTerm(Common.ALexConstants.OR);
            var Number = new NumberLiteral(Common.ALexConstants.Number, NumberOptions.AllowSign);
            var LIKE = grammar.ToTerm(Common.ALexConstants.LIKE);
            var NOT = grammar.ToTerm(Common.ALexConstants.NOT);
            var IS = grammar.ToTerm(Common.ALexConstants.IS);
            var NULL = grammar.ToTerm(Common.ALexConstants.NULL);
            var AS = grammar.ToTerm(Common.ALexConstants.AS);

            // Punctuation
            var comma = grammar.ToTerm(",");
            var dot = grammar.ToTerm(".");
            var openParenthesis = grammar.ToTerm("(");
            var closeParenthesis = grammar.ToTerm(")");
            grammar.MarkPunctuation(comma, dot, openParenthesis, closeParenthesis);

            // Strings
            var identifier = new IdentifierTerminal(Common.ALexConstants.Identifier);
            var nameIdentifier = new NonTerminal(Common.ALexConstants.NameIdentifier);
            var stringLit = new StringLiteral(Common.ALexConstants.StringType, "'", StringOptions.AllowsDoubledQuote);

            // Expressions.
            var Expr = new NonTerminal(Common.ALexConstants.Expr);
            var Term = new NonTerminal(Common.ALexConstants.Term);
            var BinExpr = new NonTerminal(Common.ALexConstants.BinExpr);
            var ParExpr = new NonTerminal(Common.ALexConstants.ParExpr);
            var BinOp = new NonTerminal(Common.ALexConstants.BinOp, "operator");
            Expr.Rule = Term | BinExpr;
            Term.Rule = Number | ParExpr | nameIdentifier | stringLit;
            ParExpr.Rule = "(" + Expr + ")";
            BinExpr.Rule = Expr + BinOp + Expr;
            BinOp.Rule = grammar.ToTerm("+") | "-" | "*" | "/" | "%";

            // Field expressions
            var FieldUnion = new NonTerminal(Common.ALexConstants.FieldUnion);
            var CompareOp = new NonTerminal(Common.ALexConstants.CompareOp);
            var LikeOp = new NonTerminal(Common.ALexConstants.LikeOp);
            var LikeTerm = new NonTerminal(Common.ALexConstants.LikeTerm);
            var InOp = new NonTerminal(Common.ALexConstants.InOp);
            var InTerm = new NonTerminal(Common.ALexConstants.InTerm);
            var TermList = new NonTerminal(Common.ALexConstants.TermList);
            var TermListDef = new NonTerminal(Common.ALexConstants.TermListDef);

            // Aggregates
            var AggregateExpr = new NonTerminal(Common.ALexConstants.AggregateExpr);
            var AggregateTerm = new NonTerminal(Common.ALexConstants.AggregateTerm);
            AggregateExpr.Rule = AggregateTerm + openParenthesis + Expr + closeParenthesis;
            //AggregateExpr.Rule = AggregateTerm + openParenthesis + FieldTerm + closeParenthesis;
            AggregateTerm.Rule = grammar.ToTerm(Common.ALexConstants.MAX) | Common.ALexConstants.MIN | Common.ALexConstants.COUNT | Common.ALexConstants.SUM | Common.ALexConstants.AVG;

            // Raw
            var RawTerm = new NonTerminal(Common.ALexConstants.RawTerm);

            // Is statements
            var IsStmt = new NonTerminal(Common.ALexConstants.IsStmt);
            var IsTerm = new NonTerminal(Common.ALexConstants.IsTerm);

            // Boolean operators definitions.

            CompareOp.Rule = grammar.ToTerm("!=") | "<" | ">" | "<=" | ">=" | "=" | "<>";
            LikeOp.Rule = LIKE | NOT + LIKE;
            LikeTerm.Rule = nameIdentifier + LikeOp + stringLit;
            InOp.Rule = IN | NOT + IN;
            InTerm.Rule = nameIdentifier + InOp + openParenthesis + TermList + closeParenthesis;
            TermList.Rule = grammar.MakePlusRule(TermList, comma, TermListDef);
            TermListDef.Rule = Number | stringLit;


            IsStmt.Rule = nameIdentifier + IsTerm;
            IsTerm.Rule = IS + NULL | IS + NOT + NULL;

            var aliasName = new NonTerminal(Common.ALexConstants.AliasName);
            nameIdentifier.Rule = identifier | aliasName;
            aliasName.Rule = identifier + dot + identifier;

            #endregion

            #region Query definition
            if (handleAsInternal)
                QueryStmt.Rule = FromStmt + SelectStmt + JoinStmt + GroupByStmt + OrderByStmt + PagingStmnt;
            else
                QueryStmt.Rule = FromStmt + SelectStmt + GroupByStmt + OrderByStmt + PagingStmnt;
            #endregion

            #region Source definition
            var From = grammar.ToTerm(Common.ALexConstants.From);
            var SchemaName = new NonTerminal(Common.ALexConstants.SchemaName);

            FromStmt.Rule = From + openParenthesis + SchemaName + closeParenthesis;
            SchemaName.Rule = nameIdentifier | nameIdentifier + AS + stringLit | nameIdentifier + AS + identifier;
            #endregion

            #region Select definition
            var Select = grammar.ToTerm(Common.ALexConstants.Select);
            var SelectList = new NonTerminal(Common.ALexConstants.SelectList);
            var SelectTerm = new NonTerminal(Common.ALexConstants.SelectTerm);
            var DistinctTerm = grammar.ToTerm(Common.ALexConstants.DISTINCT);

            SelectStmt.Rule = Select + openParenthesis + SelectList + closeParenthesis + FilterStmt + JoinStmt
                | Select + openParenthesis + SelectList + closeParenthesis + DistinctTerm + openParenthesis + closeParenthesis + FilterStmt + JoinStmt;
            if (handleAsInternal)
            {
                SelectTerm.Rule = nameIdentifier
                    | NULL + AS + stringLit | NULL + AS + identifier
                    | AggregateExpr + AS + identifier | AggregateExpr + AS + stringLit
                    | RawTerm + AS + identifier | RawTerm + AS + stringLit
                    | Expr + AS + identifier | Expr + AS + stringLit;
            }
            else
            {
                SelectTerm.Rule = nameIdentifier
                    | NULL + AS + identifier | NULL + AS + stringLit
                    | AggregateExpr + AS + identifier | AggregateExpr + AS + stringLit
                    | Expr + AS + identifier | Expr + AS + stringLit;
            }

            SelectList.Rule = grammar.MakePlusRule(SelectList, comma, SelectTerm);
            #endregion

            #region Join definition
            var JoinType = new NonTerminal(Common.ALexConstants.JoinType);
            var JoinTerm = new NonTerminal(Common.ALexConstants.JoinTerm);
            var Join = grammar.ToTerm(Common.ALexConstants.Join);
            var joinOperator = new NonTerminal(Common.ALexConstants.JoinOperator);

            JoinStmt.Rule = grammar.Empty
                | joinOperator + FilterStmt;

            joinOperator.Rule = grammar.MakePlusRule(joinOperator, JoinTerm); ;
            JoinTerm.Rule = Join + openParenthesis + JoinType + comma + SchemaName + comma + FieldUnion + closeParenthesis;
            JoinType.Rule = grammar.ToTerm(Common.ALexConstants.LEFT) | grammar.ToTerm(Common.ALexConstants.RIGHT) | grammar.ToTerm(Common.ALexConstants.INNER);
            #endregion

            #region OrderBy definition
            var OrderBy = grammar.ToTerm(Common.ALexConstants.OrderBy);
            var OrderByList = new NonTerminal(Common.ALexConstants.OrderByList);
            var OrderDirection = new NonTerminal("OrderDirection");
            var OrderByTerm = new NonTerminal("OrderByTerm");
            OrderByStmt.Rule = grammar.Empty
                | OrderBy + openParenthesis + OrderByList + closeParenthesis + FilterStmt + JoinStmt;
            OrderByList.Rule = grammar.MakePlusRule(OrderByList, comma, OrderByTerm);
            OrderByTerm.Rule = nameIdentifier + OrderDirection | nameIdentifier;
            OrderDirection.Rule = grammar.ToTerm(Common.ALexConstants.ASC) | Common.ALexConstants.DESC;
            #endregion

            #region Group by definition
            var GroupBy = grammar.ToTerm(Common.ALexConstants.GroupBy);
            var GroupByList = new NonTerminal(Common.ALexConstants.GroupByList);
            var GroupByTerm = new NonTerminal(Common.ALexConstants.GroupByTerm);
            GroupByStmt.Rule = grammar.Empty
                | GroupBy + openParenthesis + GroupByList + closeParenthesis + FilterStmt + JoinStmt;

            GroupByTerm.Rule = Expr | AggregateExpr;
            GroupByList.Rule = grammar.MakePlusRule(GroupByList, comma, GroupByTerm);
            #endregion

            #region Paging definition
            var PageExpr = new NonTerminal(Common.ALexConstants.PageExpr);
            var uNumber = new NumberLiteral(Common.ALexConstants.Number, NumberOptions.IntOnly);
            var Limit = new NonTerminal(Common.ALexConstants.Limit);

            PagingStmnt.Rule = grammar.Empty
                | Limit + FilterStmt + JoinStmt
                | Limit + PageExpr + FilterStmt + JoinStmt
                | PageExpr + Limit + FilterStmt + JoinStmt;

            PageExpr.Rule = grammar.ToTerm("Page") + openParenthesis + uNumber + closeParenthesis;
            Limit.Rule = grammar.ToTerm(Common.ALexConstants.Limit) + openParenthesis + uNumber + closeParenthesis;
            #endregion

            #region Filter definition
            var Filter = grammar.ToTerm(Common.ALexConstants.Filter);
            var FilterList = new NonTerminal(Common.ALexConstants.FilterList);
            var FilterTerm = new NonTerminal(Common.ALexConstants.FilterTerm);
            var FieldDef = new NonTerminal(Common.ALexConstants.FieldDef);
            var ParField = new NonTerminal(Common.ALexConstants.ParField);
            var FieldExpr = new NonTerminal(Common.ALexConstants.FieldExpr);
            if (handleAsInternal)
            {
                FieldUnion.Rule = FieldDef | ParField | FieldExpr | LikeTerm | InTerm | IsStmt | RawTerm;
                RawTerm.Rule = grammar.ToTerm(Common.ALexConstants.RAW) + openParenthesis + stringLit + closeParenthesis;
            }
            else
            {
                FieldUnion.Rule = FieldDef | ParField | FieldExpr | LikeTerm | InTerm | IsStmt;
            }

            var BooleanOperator = new NonTerminal(Common.ALexConstants.BooleanOperator);
            FieldDef.Rule = Expr + BooleanOperator + Expr;
            BooleanOperator.Rule = CompareOp | LikeOp | InOp;
            ParField.Rule = openParenthesis + FieldUnion + closeParenthesis;

            var FieldOp = new NonTerminal(Common.ALexConstants.FieldOp);
            FieldExpr.Rule = FieldUnion + FieldOp + FieldUnion;
            FieldOp.Rule = AND | OR;

            FilterStmt.Rule = grammar.Empty | FilterList;
            FilterTerm.Rule = Filter + openParenthesis + FieldUnion + closeParenthesis;
            FilterList.Rule = grammar.MakePlusRule(FilterList, FilterTerm);
            #endregion

            #region Updated definition
            // Actually there is no support for Update with join statemens hence, I'll let it only with the simple update.
            var Update = grammar.ToTerm(Common.ALexConstants.Update);
            var Set = grammar.ToTerm(Common.ALexConstants.Set);
            var SetTerm = new NonTerminal(Common.ALexConstants.SetTerm);
            var SetList = new NonTerminal(Common.ALexConstants.SetList);
            var SetArg = new NonTerminal(Common.ALexConstants.SetArg);
            var UpdateList = new NonTerminal(Common.ALexConstants.UpdateList);
            var UpdateTerm = new NonTerminal(Common.ALexConstants.UpdateTerm);


            // If you wish to add the Update with join support then UNCOMMENT the following line:
            //UpdateStmt.Rule = UpdateTerm + FilterStmt + JoinStmt + PagingStmnt;

            // If you wish to add the Update with join support then COMMENT the following line:
            UpdateStmt.Rule = UpdateTerm + SetList;

            UpdateTerm.Rule = Update + openParenthesis + SchemaName + closeParenthesis;

            SetList.Rule =  grammar.MakePlusRule(SetList, SetTerm);
            SetTerm.Rule = Set + openParenthesis + UpdateList + closeParenthesis + FilterStmt;
            UpdateList.Rule = grammar.MakePlusRule(UpdateList, comma, SetArg);

            // Actually there is no support for columns as update term.
            SetArg.Rule = nameIdentifier + grammar.ToTerm("=") + TermListDef;
            #endregion

            #region Insert definition
            var Insert = grammar.ToTerm(Common.ALexConstants.Insert);
            var InsertTerm = new NonTerminal(Common.ALexConstants.InsertTerm);
            var ColumnDef = new NonTerminal(Common.ALexConstants.ColumnDef);
            var ColumnsList = new NonTerminal(Common.ALexConstants.ColumnList);


            InsertTerm.Rule = Insert + openParenthesis + SchemaName + closeParenthesis;
            ColumnDef.Rule = grammar.Empty | grammar.ToTerm(Common.ALexConstants.Columns) + openParenthesis + ColumnsList + closeParenthesis;
            ColumnsList.Rule = grammar.MakePlusRule(ColumnsList, comma, identifier);

            //var Values = new NonTerminal(Common.ALexConstants.Values);
            var ValuesDef = new NonTerminal(Common.ALexConstants.ValuesDef);
            var ValueList = new NonTerminal(Common.ALexConstants.ValueList);
            var InsertValues = new NonTerminal(Common.ALexConstants.InsertValues);
            var values = new NonTerminal("val");

            ValuesDef.Rule = grammar.ToTerm(Common.ALexConstants.Values) + openParenthesis + ValueList + closeParenthesis;
            ValueList.Rule = grammar.MakePlusRule(ValueList, comma, values);
            values.Rule = nameIdentifier | stringLit | Number;
            InsertValues.Rule = grammar.MakePlusRule(InsertValues, ValuesDef);

            InsertStmt.Rule = InsertTerm + ColumnDef + InsertValues | InsertTerm + ColumnDef + QueryStmt;
            #endregion

            #region Delete
            DeleteStmt.Rule = grammar.ToTerm(Common.ALexConstants.Delete) + openParenthesis + SchemaName + closeParenthesis + FilterStmt;
            #endregion

            grammar.RegisterOperators(10, "*", "/", "%");
            grammar.RegisterOperators(8, "+", "-");
            grammar.RegisterOperators(7, "=", ">", "<", ">=", "<=", "<>", "!=");
            grammar.RegisterOperators(6, NOT, LIKE, IN, IS);
            grammar.RegisterOperators(5, AND, OR);
            grammar.RegisterOperators(4, Join);
            grammar.MarkTransient(Term, CompareOp, nameIdentifier, TermListDef, FieldUnion, GroupByTerm, Expr, values);

            return Program;
        }
    }
}
