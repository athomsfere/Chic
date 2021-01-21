using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ChicAndDapper
{
    public partial class Chic
    {
        private ChicOptions chicOptions { get; set; }
        public Chic(ChicOptions chicOptions)
        {
            this.chicOptions = chicOptions;           
        }

        
        public static string GetName<T>()
        {
            return typeof(T).Name;
        }

        private void Add<T>(string columnName, string alias = null)
        {
            var defaultName = $"{typeof(T).Name}.{columnName}";
            if (!columns.ContainsKey(defaultName))
            {
                columns.Add(defaultName, alias ?? columnName);
            }

            // I might want to throw an exception for these
        }



        private static string GetNameFor<T1>(Expression<Func<T1, object>> p1)
        {
            string sourceName;
            var body = p1.Body as MemberExpression;

            if (body == null)
            {
                sourceName = ((MemberExpression)((UnaryExpression)p1.Body).Operand).Member.Name;
            }
            else
            {
                sourceName = body.Member.Name;
            }

            return sourceName;
        }

        public void From<T>()
        {
            FromType = typeof(T);
        }
    }
}
