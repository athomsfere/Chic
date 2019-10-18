using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ChicAndDapper
{
    public class Read<T>
    {

        public T ForColumn()
        {
            return Activator.CreateInstance<T>();
        }

        internal MemberInfo GetMemberExpression<TProp>(Expression<Func<T, TProp>> expression)
        {
            var body = expression.Body as MemberExpression;
            if (body == null)
                throw new ArgumentException("'expression' should be a member expression");
            return body.Member;
        }


        public string Name(object member)

        {

            var propertyInfo = member.GetType().Name;

            return propertyInfo;

        }

    }
}
