using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ChicAndDapper
{
    public partial class Chic
    {
        private Dictionary<ExpressionType, string> ExpressionTypes = new Dictionary<ExpressionType, string>()
        {
            { ExpressionType.Equal, "=" },
            { ExpressionType.NotEqual , "!=" },
            {ExpressionType.GreaterThan,  ">" },
            {ExpressionType.GreaterThanOrEqual,  ">=" },
            {ExpressionType.LessThan,  "<" },
            {ExpressionType.LessThanOrEqual,  "<=" },
            { ExpressionType.OrElse, "OR" },
            { ExpressionType.AndAlso, "AND" },
            { ExpressionType.Default, "AND" }
        };

        List<WhereBuilder> whereClauses = new List<WhereBuilder>();

        private StringBuilder WhereString = new StringBuilder();

        /// <summary>
        /// Used to build where clauses
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        public void Where<T>(Expression<Func<T, bool>> expression)
        {
            string sourceName;
            var body = expression.Body as MemberExpression;
            if (body == null)
            {
                var compare = (BinaryExpression)expression.Body;
                //var oneof = compare.Method;

                AddExpression<T>(compare);
            }
        }

        private void AddExpression<T>(BinaryExpression compare, ExpressionType parentOperand = ExpressionType.Default)
        {
            ExpressionType operand = compare.NodeType;

            if (operand == ExpressionType.OrElse || operand == ExpressionType.AndAlso)
            {
                var leftCompare = (BinaryExpression)compare.Left;

                AddExpression<T>(leftCompare, operand);

                //whereClauses.Add(AddComparer<T>(leftCompare, compare.Left.NodeType, operand));

                var rightExpression = (BinaryExpression)compare.Right;
                AddExpression<T>(rightExpression, operand);
            }
            else
            {
                WhereBuilder clause = AddComparer<T>(compare, operand, parentOperand);
                whereClauses.Add(clause);
            }

        }

        private static ExpressionType GetOperand<T>(Expression<Func<T, bool>> expression)
        {
            return ((BinaryExpression)expression.Body).NodeType;
        }

        private static WhereBuilder AddComparer<T>(BinaryExpression compare, ExpressionType operand, ExpressionType parentOperand)
        {
            string propertyName = compare.Left.ToString().Split('.')[1];
            var right = compare.Right.ToString();

            if (operand == ExpressionType.LessThan || 
                operand == ExpressionType.LessThanOrEqual ||
                operand == ExpressionType.GreaterThan ||
                operand == ExpressionType.GreaterThanOrEqual)
            {
                right = $"convert({(compare.Right).NodeType.ToString()},{compare.Right.Type} )";
            }

            var name = typeof(T).Name;

            var clause = new WhereBuilder()
            {
                Left = propertyName,
                Right = right.ToString().Replace("\"", ""),
                Operand = operand,
                ClassName = typeof(T).Name,
                ParentOperand = parentOperand
            };
            return clause;
        }

        private void SetWhereString()
        {
            if (whereClauses.Count > 0)
            {
                WhereString.Append("WHERE");
            }
            foreach (var builder in whereClauses)
            {
                if (whereClauses.IndexOf(builder) > 0)
                {
                    WhereString.Append(ExpressionTypes[builder.ParentOperand] );
                }
                WhereString.Append($" {builder.ClassName}.{builder.Left} {ExpressionTypes[builder.Operand]} '{builder.Right}'");
            }

            selectString.Append(WhereString.ToString());
        }
    }
}
