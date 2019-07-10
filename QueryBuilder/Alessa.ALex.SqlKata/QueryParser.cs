using Irony.Parsing;
using SqlKata;

namespace Alessa.ALex.SqlKata
{
    /// <summary>
    /// The query builder 
    /// </summary>
    internal static class QueryParser
    {
        /// <summary>
        /// The entry point to convert the parse tree node to a <see cref="Query"/> object.
        /// </summary>
        /// <param name="node">Node.</param>
        /// <param name="query">The query object.</param>
        internal static void SetNode(ParseTreeNode node, Query query)
        {
            if (node.Term.Name == ALex.Common.ALexConstants.QueryStmt)
            {
                SetQueryStatement(node, query);
            }
            else if (node.Term.Name == ALex.Common.ALexConstants.UpdateStmt)
            {
                SetUpdateStatement(node, query);
            }
            else if (node.Term.Name == ALex.Common.ALexConstants.InsertStmt)
            {
                SetInsertStatement(node, query);
            }
            else if (node.Term.Name == ALex.Common.ALexConstants.DeleteStmt)
            {
                SetDeleteStatement(node, query);
            }
        }

        #region Insert

        private static void SetInsertStatement(ParseTreeNode node, Query query)
        {
            // The index 0 is fixed, hence there is no need to make a loop statement.
            SetInsertTable(node, query);
            // The index 1 is fixed as well.
            SetFilterStatement(node.ChildNodes[1], query);
        }

        private static void SetInsertTable(ParseTreeNode node, Query query)
        {
            int valuesIndex = 2;

            // The index 1 is fixed, hence there is no need to make a loop statement.
            var name = GetSchemaName(node.ChildNodes[0].ChildNodes[1]);
            query.From(name.Name + (string.IsNullOrWhiteSpace(name.Alias) ? string.Empty : " AS " + name.Alias));

            // Gets the columns list.
            var columns = GetColumns(node.ChildNodes[1]);

            // The insert statement contains a select statement for inserting records in the table.
            if (node.ChildNodes[2].Term.Name == ALex.Common.ALexConstants.QueryStmt)
            {
                var selectQuery = new Query();
                SetQueryStatement(node.ChildNodes[valuesIndex], selectQuery);
                query.AsInsert(columns, selectQuery);
            }
            else if (node.ChildNodes[valuesIndex].Term.Name == ALex.Common.ALexConstants.InsertValues)
            {
                var values = GetListValues(node.ChildNodes[valuesIndex]);

                query.AsInsert(columns, values);
            }

        }

        private static System.Collections.Generic.IEnumerable<string> GetColumns(ParseTreeNode node)
        {
            const int listIndex = 1;
            for (int index = 0; index < node.ChildNodes[listIndex].ChildNodes.Count; index++)
            {
                yield return node.ChildNodes[listIndex].ChildNodes[index].Token.ValueString;
            }
        }

        private static System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<object>> GetListValues(ParseTreeNode node)
        {
            for (int index = 0; index < node.ChildNodes.Count; index++)
            {
                yield return GetInsertValues(node.ChildNodes[index]);
            }
        }

        private static System.Collections.Generic.IEnumerable<object> GetInsertValues(ParseTreeNode node)
        {
            const int listIndex = 1;
            for (int index = 0; index < node.ChildNodes[listIndex].ChildNodes.Count; index++)
            {
                yield return GetValue(node.ChildNodes[listIndex].ChildNodes[index]).Value;
            }

        }
        #endregion

        #region Update
        private static void SetUpdateStatement(ParseTreeNode node, Query query)
        {
            // The index 0 is fixed, hence there is no need to make a loop statement.
            SetUpdateTable(node.ChildNodes[0], query);
            const int listIndex = 1;

            System.Collections.Generic.List<string> columns = new System.Collections.Generic.List<string>();
            System.Collections.Generic.List<object> values = new System.Collections.Generic.List<object>();

            for (int i = 0; i < node.ChildNodes[listIndex].ChildNodes.Count; i++)
            {
                SetUpdateTerms(node.ChildNodes[listIndex].ChildNodes[i].ChildNodes[listIndex], columns, values);

                SetFilterStatement(node.ChildNodes[listIndex].ChildNodes[i].ChildNodes[listIndex + 1], query);
            }

            query.AsUpdate(columns, values);
        }

        private static void SetUpdateTable(ParseTreeNode node, Query query)
        {
            // The index 1 is fixed, hence there is no need to make a loop statement.
            var name = GetSchemaName(node.ChildNodes[1]);
            query.From(name.Name + (string.IsNullOrWhiteSpace(name.Alias) ? string.Empty : " AS " + name.Alias));

        }

        private static void SetUpdateTerms(ParseTreeNode node, System.Collections.Generic.List<string> columns, System.Collections.Generic.List<object> values)
        {

            for (int index = 0; index < node.ChildNodes.Count; index++)
            {
                if (node.ChildNodes[index].Term.Name == ALex.Common.ALexConstants.SetArg)
                {
                    var column = GetValue(node.ChildNodes[index].ChildNodes[0]);
                    object value = GetValue(node.ChildNodes[index].ChildNodes[2]).Value;

                    columns.Add(column.Name);
                    values.Add(value);
                }
            }

        }
        #endregion

        #region Delete
        private static void SetDeleteStatement(ParseTreeNode node, Query query)
        {
            // The index 0 is fixed, hence there is no need to make a loop statement.
            SetDeleteTable(node.ChildNodes[1], query);

            SetFilterStatement(node.ChildNodes[2], query);

            query.AsDelete();
        }

        private static void SetDeleteTable(ParseTreeNode node, Query query)
        {
            // The index 1 is fixed, hence there is no need to make a loop statement.
            var name = GetSchemaName(node);
            query.From(name.Name + (string.IsNullOrWhiteSpace(name.Alias) ? string.Empty : " AS " + name.Alias));

        }
        #endregion

        #region Query
        private static void SetQueryStatement(ParseTreeNode node, Query query)
        {
            for (int index = 0; index < node.ChildNodes.Count; index++)
            {
                if (node.ChildNodes[index].Term.Name == ALex.Common.ALexConstants.FromStmt)
                {
                    SetFromStatement(node.ChildNodes[index], query);
                }
                else if (node.ChildNodes[index].Term.Name == ALex.Common.ALexConstants.FilterStmt)
                {
                    SetFilterStatement(node.ChildNodes[index], query);
                }
                else if (node.ChildNodes[index].Term.Name == ALex.Common.ALexConstants.SelectStmt)
                {
                    SetSelectStatement(node.ChildNodes[index], query);
                }
                else if (node.ChildNodes[index].Term.Name == ALex.Common.ALexConstants.GroupByStmt)
                {
                    SetGroupByStatement(node.ChildNodes[index], query);
                }
                else if (node.ChildNodes[index].Term.Name == ALex.Common.ALexConstants.OrderByStmt)
                {
                    SetOrderByStatement(node.ChildNodes[index], query);
                }
                else if (node.ChildNodes[index].Term.Name == ALex.Common.ALexConstants.PagingStmnt)
                {
                    SetPagingStatement(node.ChildNodes[index], query);
                }
                else if (node.ChildNodes[index].Term.Name == ALex.Common.ALexConstants.JoinStmt)
                {
                    SetJoinStatement(node.ChildNodes[index], query);
                }
            }
        }

        private static void SetFromStatement(ParseTreeNode node, Query query)
        {
            if (node.ChildNodes[0].Term.Name == ALex.Common.ALexConstants.From)
            {
                var name = GetSchemaName(node.ChildNodes[1]);
                query.From(name.Name + (string.IsNullOrWhiteSpace(name.Alias) ? string.Empty : " AS " + name.Alias));
            }
            else
            {
                throw new System.Exception("The Source statement is not correct.");
            }
        }

        private static void SetFilterStatement(ParseTreeNode node, Query query)
        {
            const int listIndex = 0;
            if (node.ChildNodes.Count > 0)
                for (var index = 0; index < node.ChildNodes[listIndex].ChildNodes.Count; index++)
                {
                    for (var i = 0; i < node.ChildNodes[listIndex].ChildNodes[index].ChildNodes.Count; i++)
                    {
                        query.Where(q =>
                        {
                            SetFieldExpression(node.ChildNodes[listIndex].ChildNodes[index].ChildNodes[i], q, null);
                            return q;
                        });
                    }
                }
        }

        private static void SetSelectStatement(ParseTreeNode node, Query query)
        {
            for (int index = 1; index < node.ChildNodes.Count; index++)
            {
                if (node.ChildNodes[index].Term.Name == ALex.Common.ALexConstants.SelectList)
                {
                    SetSelectList(node.ChildNodes[index], query);
                }
                else if (node.ChildNodes[index].Term.Name == ALex.Common.ALexConstants.DISTINCT)
                {
                    query.Distinct();
                }
                else if (node.ChildNodes[index].Term.Name == ALex.Common.ALexConstants.JoinStmt)
                {
                    SetJoinStatement(node.ChildNodes[index], query);
                }
                else if (node.ChildNodes[index].Term.Name == ALex.Common.ALexConstants.FilterStmt)
                {
                    SetFilterStatement(node.ChildNodes[index], query);
                }
            }
        }

        private static void SetSelectList(ParseTreeNode node, Query query)
        {
            for (var index = 0; index < node.ChildNodes.Count; index++)
            {
                string aliasIdentifier = null;
                // This means the SelectTerm node has an "AS" statement in order to set an alias in the query.
                if (node.ChildNodes[index].ChildNodes.Count == 3)
                {
                    //if (node.ChildNodes[index].ChildNodes[2].Term.Name == ALex.Common.ALexConstants.Identifier)
                        aliasIdentifier = " AS [" + node.ChildNodes[index].ChildNodes[2].Token.ValueString + "]";
                    //else
                    //    aliasIdentifier = " AS ['" + node.ChildNodes[index].ChildNodes[2].Token.ValueString + "']";
                }

                if (node.ChildNodes[index].ChildNodes[0].Term.Name == ALex.Common.ALexConstants.AggregateExpr)
                {
                    SetSelectAggregate(node.ChildNodes[index].ChildNodes[0], aliasIdentifier, query);
                }
                else if (node.ChildNodes[index].ChildNodes[0].Term.Name == ALex.Common.ALexConstants.BinExpr)
                {
                    var exp = GetBinExpr(node.ChildNodes[index].ChildNodes[0]);
                    query.SelectRaw($"{exp.Expression}{aliasIdentifier}", exp.Parameters);
                }
                else if (node.ChildNodes[index].ChildNodes[0].Term.Name == ALex.Common.ALexConstants.RawTerm)
                {
                    query.SelectRaw($"{node.ChildNodes[index].ChildNodes[0].ChildNodes[1].Token.ValueString}{aliasIdentifier}");
                }
                else if (node.ChildNodes[index].ChildNodes[0].Term.Name == ALex.Common.ALexConstants.FilterStmt)
                {
                    SetFilterStatement(node.ChildNodes[index].ChildNodes[0], query);
                }
                else
                {
                    var name = GetValue(node.ChildNodes[index].ChildNodes[0]);
                    if (name.ValueType == ALex.Common.ALexConstants.StringType)
                        query.SelectRaw($"'{name.Value}'{aliasIdentifier}");
                    else
                        query.SelectRaw($"{name.Value}{aliasIdentifier}");
                }
            }
        }

        private static void SetGroupByStatement(ParseTreeNode node, Query query)
        {
            for (int index = 1; index < node.ChildNodes.Count; index++)
            {
                if (node.ChildNodes[index].Term.Name == ALex.Common.ALexConstants.GroupByList)
                {
                    SetGroupByList(node.ChildNodes[index], query);
                }
                else if (node.ChildNodes[index].Term.Name == ALex.Common.ALexConstants.JoinStmt)
                {
                    SetJoinStatement(node.ChildNodes[index], query);
                }
                else if (node.ChildNodes[index].Term.Name == ALex.Common.ALexConstants.FilterStmt)
                {
                    SetFilterStatement(node.ChildNodes[index], query);
                }
            }
        }

        private static void SetGroupByList(ParseTreeNode node, Query query)
        {
            for (var index = 0; index < node.ChildNodes.Count; index++)
            {
                if (node.ChildNodes[index].Term.Name == ALex.Common.ALexConstants.AggregateExpr)
                {
                    SetGroupAggregate(node.ChildNodes[index], query);
                }
                else if (node.ChildNodes[index].Term.Name == ALex.Common.ALexConstants.BinExpr)
                {
                    var exp = GetBinExpr(node.ChildNodes[index]);
                    query.GroupByRaw(exp.Expression, exp.Parameters);
                }
                else
                {
                    var name = GetValue(node.ChildNodes[index]);
                    query.GroupBy(name.RawValue);
                }
            }
        }

        private static void SetOrderByStatement(ParseTreeNode node, Query query)
        {
            for (int index = 1; index < node.ChildNodes.Count; index++)
            {
                if (node.ChildNodes[index].Term.Name == ALex.Common.ALexConstants.OrderByList)
                {
                    for (int i = 0; i < node.ChildNodes[index].ChildNodes.Count; i++)
                        SetOrderByList(node.ChildNodes[index].ChildNodes[i], query);
                }
                else if (node.ChildNodes[index].Term.Name == ALex.Common.ALexConstants.JoinStmt)
                {
                    SetJoinStatement(node.ChildNodes[index], query);
                }
                else if (node.ChildNodes[index].Term.Name == ALex.Common.ALexConstants.FilterStmt)
                {
                    SetFilterStatement(node.ChildNodes[index], query);
                }
            }
        }

        private static void SetOrderByList(ParseTreeNode node, Query query)
        {
            string dir;
            var name = GetValue(node.ChildNodes[0]);
            if (node.ChildNodes.Count == 2)
            {
                dir = node.ChildNodes[1].ChildNodes[0].Token.ValueString.ToUpper();
            }
            else
            {
                dir = ALex.Common.ALexConstants.ASC;
            }

            if (dir == ALex.Common.ALexConstants.DESC)
            {
                query.OrderByDesc(name.RawValue);
            }
            else
            {
                query.OrderBy(name.RawValue);
            }
        }

        private static void SetPagingStatement(ParseTreeNode node, Query query)
        {
            int? rowCount = null;
            int? pageNumber = null;
            for (int index = 0; index < node.ChildNodes.Count; index++)
            {
                if (node.ChildNodes[index].Term.Name == ALex.Common.ALexConstants.PageExpr)
                {
                    pageNumber = int.Parse(node.ChildNodes[index].ChildNodes[1].Token.ValueString);
                }
                else if (node.ChildNodes[index].Term.Name == ALex.Common.ALexConstants.Limit)
                {
                    rowCount = int.Parse(node.ChildNodes[index].ChildNodes[1].Token.ValueString);
                }
                else if (node.ChildNodes[index].Term.Name == ALex.Common.ALexConstants.JoinStmt)
                {
                    SetJoinStatement(node.ChildNodes[index], query);
                }
                else if (node.ChildNodes[index].Term.Name == ALex.Common.ALexConstants.FilterStmt)
                {
                    SetFilterStatement(node.ChildNodes[index], query);
                }
            }

            if (rowCount.HasValue)
            {
                query.Limit(rowCount.Value);
            }

            if (pageNumber.HasValue)
            {
                query.Offset((pageNumber.Value - 1) * rowCount.Value);
            }
        }

        private static void SetJoinStatement(ParseTreeNode node, Query query)
        {
            for (int index = 0; index < node.ChildNodes.Count; index++)
            {
                if (node.ChildNodes[index].Term.Name == ALex.Common.ALexConstants.JoinOperator)
                {
                    for (int i = 0; i < node.ChildNodes[index].ChildNodes.Count; i++)
                        SetJoinList(node.ChildNodes[index].ChildNodes[i], query);
                }
                else if (node.ChildNodes[index].Term.Name == ALex.Common.ALexConstants.FilterStmt)
                {
                    SetFilterStatement(node.ChildNodes[index], query);
                }
            }
        }

        private static void SetJoinList(ParseTreeNode node, Query query)
        {
            string joinType = null;
            string tableName = null;

            for (var index = 1; index < node.ChildNodes.Count; index++)
            {
                if (node.ChildNodes[index].Term.Name == ALex.Common.ALexConstants.JoinType)
                {
                    joinType = node.ChildNodes[index].ChildNodes[0].Token.ValueString.ToUpper();
                }
                else if (node.ChildNodes[index].Term.Name == ALex.Common.ALexConstants.SchemaName)
                {
                    var val = GetSchemaName(node.ChildNodes[index]);
                    tableName = val.Name + (!string.IsNullOrWhiteSpace(val.Alias) ? " AS " + val.Alias : string.Empty);
                }
                if (node.ChildNodes[index].Term.Name == ALex.Common.ALexConstants.FieldExpr || node.ChildNodes[index].Term.Name == ALex.Common.ALexConstants.FieldDef)
                {
                    if (joinType == ALex.Common.ALexConstants.LEFT)
                        query.LeftJoin(tableName, e =>
                        {
                            SetFieldExpression(node.ChildNodes[index], e, null);
                            return e;
                        });
                    else if (joinType == ALex.Common.ALexConstants.RIGHT)
                        query.RightJoin(tableName, e =>
                        {
                            SetFieldExpression(node.ChildNodes[index], e, null);
                            return e;
                        });
                    else
                        query.Join(tableName, e =>
                        {
                            SetFieldExpression(node.ChildNodes[index], e, null);
                            return e;
                        });
                }
            }
        }

        private static void SetGroupAggregate(ParseTreeNode node, Query query)
        {
            string column = null;
            string aggregateFunc = null;
            object[] parameters = null;
            for (var index = 0; index < node.ChildNodes.Count; index++)
            {
                if (node.ChildNodes[index].Term.Name == ALex.Common.ALexConstants.AggregateTerm)
                {
                    aggregateFunc = node.ChildNodes[index].ChildNodes[0].Token.ValueString;
                }
                else if (node.ChildNodes[index].Term.Name == ALex.Common.ALexConstants.BinExpr)
                {
                    var exp = GetBinExpr(node.ChildNodes[index].ChildNodes[0]);
                    column = exp.Expression;
                    parameters = exp.Parameters;
                }
                else
                {
                    var name = GetValue(node.ChildNodes[index]);
                    column = name.RawValue;
                }
            }

            query.GroupByRaw($"{aggregateFunc}({column})", parameters ?? new string[0]);
        }

        private static void SetSelectAggregate(ParseTreeNode node, string alias, Query query)
        {
            string column = null;
            string aggregateFunc = null;
            bool useRaw = !string.IsNullOrWhiteSpace(alias);
            object[] parameters = null;
            for (var index = 0; index < node.ChildNodes.Count; index++)
            {
                if (node.ChildNodes[index].Term.Name == ALex.Common.ALexConstants.AggregateTerm)
                {
                    aggregateFunc = node.ChildNodes[index].ChildNodes[0].Token.ValueString;
                }
                else if (node.ChildNodes[index].Term.Name == ALex.Common.ALexConstants.BinExpr)
                {
                    useRaw = true;
                    var exp = GetBinExpr(node.ChildNodes[index]);
                    column = exp.Expression;
                    parameters = exp.Parameters;
                }
                else
                {
                    var name = GetValue(node.ChildNodes[index]);
                    column = name.RawValue;
                }
            }
            if (useRaw)
            {
                query.SelectRaw($"{aggregateFunc}({column}){alias}", parameters ?? new string[0]);
            }
            else
            {
                if (aggregateFunc == ALex.Common.ALexConstants.MAX)
                {
                    query.AsMax(column);
                }
                else if (aggregateFunc == ALex.Common.ALexConstants.MIN)
                {
                    query.AsMin(column);
                }
                else if (aggregateFunc == ALex.Common.ALexConstants.COUNT)
                {
                    query.AsCount(column);
                }
                else if (aggregateFunc == ALex.Common.ALexConstants.SUM)
                {
                    query.AsSum(column);
                }
                else if (aggregateFunc == ALex.Common.ALexConstants.AVG)
                {
                    query.AsAvg(column);
                }
            }
        }

        private static (string Name, string Alias, string ValueType, object Value, string RawValue) GetValue(ParseTreeNode node)
        {
            if (node.Term.Name == ALex.Common.ALexConstants.AliasName)
            {
                return (Name: node.ChildNodes[1].Token.ValueString,
                    Alias: node.ChildNodes[0].Token.ValueString,
                    ValueType: node.Term.Name,
                    Value: "[" + node.ChildNodes[0].Token.ValueString + "].[" + node.ChildNodes[1].Token.ValueString + "]",
                    RawValue: node.ChildNodes[0].Token.ValueString + "." + node.ChildNodes[1].Token.ValueString);
            }
            else if (node.Term.Name == ALex.Common.ALexConstants.NameIdentifier)
            {
                return (Name: node.ChildNodes[1].Token.ValueString,
                    Alias: null,
                    ValueType: node.Term.Name,
                    Value: "[" + node.ChildNodes[1].Token.ValueString + "]",
                    RawValue: node.ChildNodes[1].Token.ValueString);
            }
            else if (node.Term.Name == ALex.Common.ALexConstants.Identifier)
            {
                return (Name: node.Token.ValueString,
                    Alias: null,
                    ValueType: node.Term.Name,
                    Value: "[" + node.Token.ValueString + "]",
                    RawValue: node.Token.ValueString);
            }
            else if (node.Term.Name == ALex.Common.ALexConstants.StringType || node.Term.Name == ALex.Common.ALexConstants.Number)
            {
                return (Name: node.Token.ValueString,
                    Alias: null,
                    ValueType: node.Term.Name,
                    Value: node.Token.Value,
                    RawValue: node.Token.ValueString);
            }
            else if (node.Term.Name == ALex.Common.ALexConstants.NULL)
            {
                return (Name: node.Token.ValueString,
                    Alias: null,
                    ValueType: node.Term.Name,
                    Value: node.Token.Value,
                    RawValue: node.Token.ValueString);
            }

            throw new System.Exception("Name not valid");
        }

        private static (string Name, string Alias, string ValueType, object Value, string RawValue) GetSchemaName(ParseTreeNode node)
        {
            (string Name, string Alias, string ValueType, object Value, string RawValue) result;
            var name = GetValue(node.ChildNodes[0]);
            result = (Name: name.RawValue,
               Alias: null,
               ValueType: node.Term.Name,
               Value: name.Value,
               RawValue: name.RawValue);

            if (node.ChildNodes.Count == 3)
            {
                name = GetValue(node.ChildNodes[2]);

                if (node.ChildNodes[2].Term.Name == ALex.Common.ALexConstants.StringType)
                    result.Alias = $"'{name.RawValue}'";
                else
                    result.Alias = name.RawValue;
            }

            return result;
        }

        private static bool? SetFieldExpression<Q>(ParseTreeNode node, Q query, bool? isAnd)
            where Q : BaseQuery<Q>
        {
            if (node.Term.Name == ALex.Common.ALexConstants.FieldExpr)
            {
                for (var index = 0; index < node.ChildNodes.Count; index++)
                {
                    isAnd = SetFieldExpression(node.ChildNodes[index], query, isAnd);
                }
            }
            else if (node.Term.Name == ALex.Common.ALexConstants.ParField)
            {
                System.Func<Q, Q> expression = e =>
                {
                    for (var i = 0; i < node.ChildNodes.Count; i++)
                    {
                        isAnd = SetFieldExpression(node.ChildNodes[i], e, isAnd);
                    }
                    return e;
                };

                if (isAnd == true)
                {
                    query.Where(expression);
                }
                else
                {
                    query.OrWhere(expression);
                }
            }
            else if (node.Term.Name == ALex.Common.ALexConstants.FieldOp)
            {
                isAnd = node.ChildNodes[0].Term.Name == ALex.Common.ALexConstants.AND;
            }
            else if (node.Term?.Name == ALex.Common.ALexConstants.FieldDef)
            {
                SetFieldDefAsFilter(node, isAnd, query);
            }
            else if (node.Term?.Name == ALex.Common.ALexConstants.LikeTerm)
            {
                SetLikeTerm(node, isAnd, query);
            }
            else if (node.Term?.Name == ALex.Common.ALexConstants.InTerm)
            {
                SetInTerm(node, isAnd, query);
            }
            else if (node.Term?.Name == ALex.Common.ALexConstants.IsStmt)
            {
                SetIsTerm(node, isAnd, query);
            }
            else if (node.Term?.Name == ALex.Common.ALexConstants.RawTerm)
            {
                SetRawFilter(node, isAnd, query);
            }

            return isAnd;
        }

        private static void SetRawFilter<Q>(ParseTreeNode node, bool? isAnd, Q query)
            where Q : BaseQuery<Q>
        {
            if (isAnd == false)
            {
                query.OrWhereRaw(node.ChildNodes[1].Token.ValueString);
            }
            else
            {
                query.WhereRaw(node.ChildNodes[1].Token.ValueString);
            }
        }

        private static void SetFieldDefAsFilter<Q>(ParseTreeNode node, bool? isAnd, Q query)
            where Q : BaseQuery<Q>
        {
            var fieldDef = GetFieldDef(node);

            if (isAnd == false)
            {
                var array = fieldDef.Left.Parameters;
                CopyNewValuesInArray(ref array, fieldDef.Right.Parameters);
                query.OrWhereRaw($"{fieldDef.Left.Expression} {fieldDef.BooleanOperator} {fieldDef.Right.Expression}", array);
            }
            else
            {
                var array = fieldDef.Left.Parameters;
                CopyNewValuesInArray(ref array, fieldDef.Right.Parameters);
                query.WhereRaw($"{fieldDef.Left.Expression} {fieldDef.BooleanOperator} {fieldDef.Right.Expression}", array);
            }

        }

        private static void SetLikeTerm<Q>(ParseTreeNode node, bool? isAnd, Q query)
            where Q : BaseQuery<Q>
        {
            bool? isLike = null;
            string column = null;
            string str = null;
            for (var index = 0; index < node.ChildNodes.Count; index++)
            {
                if (node.ChildNodes[index].Term.Name == ALex.Common.ALexConstants.LikeOp)
                {
                    if (node.ChildNodes[index].ChildNodes[0].Term.Name == ALex.Common.ALexConstants.NOT)
                    {
                        isLike = false;
                    }
                }
                else
                {
                    var name = GetValue(node.ChildNodes[index]);
                    if (index == 0)
                    {
                        column = name.RawValue;
                    }
                    else
                    {
                        str = name.RawValue;
                    }
                }
            }

            if (isLike == false)
            {
                if (isAnd == false)
                    query.OrWhereNotLike(column, str);
                else
                    query.WhereNotLike(column, str);
            }
            else
            {
                if (isAnd == false)
                    query.OrWhereLike(column, str);
                else
                    query.WhereLike(column, str);
            }
        }

        private static void SetInTerm<Q>(ParseTreeNode node, bool? isAnd, Q query)
            where Q : BaseQuery<Q>
        {
            bool? isLike = null;
            string column = null;
            System.Collections.Generic.List<object> terms = new System.Collections.Generic.List<object>();
            for (var index = 0; index < node.ChildNodes.Count; index++)
            {
                if (node.ChildNodes[index].Term.Name == ALex.Common.ALexConstants.InOp)
                {
                    if (node.ChildNodes[index].ChildNodes[0].Term.Name == ALex.Common.ALexConstants.NOT)
                    {
                        isLike = false;
                    }
                }
                else if (node.ChildNodes[index].Term.Name == ALex.Common.ALexConstants.TermList)
                {
                    for (var i = 0; i < node.ChildNodes[index].ChildNodes.Count; i++)
                    {
                        terms.Add(node.ChildNodes[index].ChildNodes[i].Token.Value);
                    }
                }
                else
                {
                    var name = GetValue(node.ChildNodes[index]);
                    column = name.RawValue;
                }
            }

            if (isLike == false)
            {
                if (isAnd == false)
                    query.OrWhereNotIn(column, terms);
                else
                    query.WhereNotIn(column, terms);
            }
            else
            {
                if (isAnd == false)
                    query.OrWhereIn(column, terms);
                else
                    query.WhereIn(column, terms);
            }
        }

        private static void SetIsTerm<Q>(ParseTreeNode node, bool? isAnd, Q query)
            where Q : BaseQuery<Q>
        {
            bool? isNot = null;
            string column = null;
            for (var index = 0; index < node.ChildNodes.Count; index++)
            {
                if (node.ChildNodes[index].Term.Name == ALex.Common.ALexConstants.IsTerm)
                {
                    if (node.ChildNodes[index].ChildNodes[1].Term.Name == ALex.Common.ALexConstants.NOT)
                    {
                        isNot = true;
                    }
                }
                else
                {
                    var name = GetValue(node.ChildNodes[index]);
                    column = name.RawValue;
                }
            }

            if (isAnd == true)
            {
                if (isNot == true)
                    query.WhereNotNull(column);
                else
                    query.WhereNull(column);
            }
            else
            {
                if (isNot == true)
                    query.OrWhereNotNull(column);
                else
                    query.OrWhereNull(column);
            }
        }

        private static ((string Expression, object[] Parameters) Left, string BooleanOperator, (string Expression, object[] Parameters) Right) GetFieldDef(ParseTreeNode node)
        {
            (string Expression, object[] Parameters) left = default((string Expression, object[] Parameters));
            (string Expression, object[] Parameters) right = default((string Expression, object[] Parameters));
            string op = null;

            for (var index = 0; index <= 2; index++)
            {
                if (node.ChildNodes[index].Term?.Name == ALex.Common.ALexConstants.BinExpr || node.ChildNodes[index].Term?.Name == ALex.Common.ALexConstants.ParExpr)
                {
                    var r = GetBinExpr(node.ChildNodes[index]);
                    if (left.Expression == null)
                        left = r;
                    else
                        right = r;
                }
                else if (node.ChildNodes[index].Term?.Name == ALex.Common.ALexConstants.BooleanOperator)
                {
                    // Due to this operator is not valid for all the database engines I'll change it to the 
                    // universal format.
                    if (node.ChildNodes[index].ChildNodes[0].Token.ValueString == "!=")
                        op = "<>";
                    else
                        op = node.ChildNodes[index].ChildNodes[0].Token.ValueString;
                }
                else
                {
                    var r = GetFieldTerm(node.ChildNodes[index]);
                    if (left.Expression == null)
                        left = r;
                    else
                        right = r;
                }
            }
            return (Left: left, BooleanOperator: op, Right: right);
        }

        private static (string Expression, object[] Parameters) GetBinExpr(ParseTreeNode node)
        {
            var expression = string.Empty;
            var parameters = new object[0];

            for (var index = 0; index < node.ChildNodes.Count; index++)
            {
                if (node.ChildNodes[index].Term?.Name == ALex.Common.ALexConstants.BinOp)
                {
                    expression += " " + node.ChildNodes[index].ChildNodes[0].Token.ValueString;
                }
                else if (node.ChildNodes[index].Term?.Name == ALex.Common.ALexConstants.BinExpr)
                {
                    var exp = GetBinExpr(node.ChildNodes[index]);
                    expression += exp.Expression;
                    CopyNewValuesInArray(ref parameters, exp.Parameters);

                }
                else if (node.ChildNodes[index].Term?.Name == ALex.Common.ALexConstants.ParExpr)
                {
                    for (var i2 = 0; i2 < node.ChildNodes[index].ChildNodes.Count; i2++)
                    {
                        var exp = GetBinExpr(node.ChildNodes[index].ChildNodes[i2]);
                        expression += "(" + exp.Expression + ")";
                        CopyNewValuesInArray(ref parameters, exp.Parameters);
                    }
                }
                else if (node.ChildNodes[index].Term?.Name == ALex.Common.ALexConstants.Identifier || node.ChildNodes[index].Term?.Name == ALex.Common.ALexConstants.AliasName)
                {
                    var name = GetValue(node.ChildNodes[index]);
                    expression += name.Value;
                }
                else if (node.ChildNodes[index].Term?.Name == ALex.Common.ALexConstants.Number || node.ChildNodes[index].Term?.Name == ALex.Common.ALexConstants.StringType)
                {
                    var name = GetValue(node.ChildNodes[index]);
                    expression += " ?";
                    CopyNewValuesInArray(ref parameters, new object[] { name.Value });
                }
                else
                {
                    var res = GetValue(node.ChildNodes[index]);
                    expression += " " + res.Value;
                }

            }

            return (Expression: expression, Parameters: parameters);
        }

        private static (string Expression, object[] Parameters) GetFieldTerm(ParseTreeNode node)
        {
            var res = GetValue(node);
            if (res.ValueType == ALex.Common.ALexConstants.Number || res.ValueType == ALex.Common.ALexConstants.StringType)
                return (Expression: " ?", Parameters: new object[] { res.Value });
            else
                return (Expression: res.Value.ToString(), Parameters: new string[0]);
        }

        private static void CopyNewValuesInArray(ref object[] original, object[] newArray)
        {
            var actualIndex = original.Length;
            System.Array.Resize(ref original, original.Length + newArray.Length);

            for (var i = 0; i < newArray.Length; i++)
            {
                original[actualIndex] = newArray[i];
                actualIndex++;
            }
        }
        #endregion
    }
}
