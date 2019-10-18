using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ChicAndDapper
{
    public partial class For<T>
    {
        public static string EntityName()
        {
            return typeof(T).Name;
        }

        public static string Column<TProp>(Expression<Func<T, TProp>> expression)
        {
            var body = expression.Body as MemberExpression;
            if (body == null)
                throw new ArgumentException("'expression' should be a member expression");
            return body.Member.Name;
        }

        internal static List<string> Columns<TProp>(params Expression<Func<T, TProp>>[] expressions)
        {
            List<string> names = new List<string>();
            foreach (var expression in expressions)
            {
                names.Add(Column(expression));
            }

            return names;
        }

        internal static List<string> As<TProp>(Expression<Func<T, TProp>>[] expressions)
        {
            List<string> names = new List<string>();
            foreach (var expression in expressions)
            {
                names.Add(Column(expression));
            }

            return names;
        }
    }
}
