using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ChicAndDapper
{
    public partial class Chic
    {
        private Type FromType;

        internal Dictionary<string, string> columns = new Dictionary<string, string>();

        /// <summary>
        /// Add a column to the select statement
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="column"></param>
        public void SelectColumn<T>(string column, string alias = null)
        {
            Add<T>(column, alias);
        }
        /// <summary>
        /// Select statement for mapping db model to bll / view models
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        public void SelectColumn<T1, T2>(Expression<Func<T1, object>> p1, Expression<Func<T2, object>> p2)
        {
            var destName = GetNameFor(p2);
            var sourceName = GetNameFor(p1);

            Add<T1>(sourceName, destName);
        }

        /// <summary>
        /// Select a column for class property
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        public void SelectColumn<T>(Expression<Func<T, object>> expression)
        {
            var column = GetNameFor(expression);
            Add<T>(column);
        }

        /// <summary>
        /// Add all found columns of type to select statement
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columns" description="columns to add">ccc</param>
        public void SelectColumns<T>(List<string> columns)
        {
            foreach (var col in columns)
            {
                SelectColumn<T>(col);
            }
        }

    }
}
