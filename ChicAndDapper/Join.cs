using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ChicAndDapper
{
    public partial class Chic 
    {
        private List<Joiner> Joins = new List<Joiner>();

        /// <summary>
        /// If keys match, only one key name required.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <param name="leftKey">Name for key on left table </param>
        /// <param name="rightKey">Name for optional keyname on right table</param>
        public void LeftJoin<T, R>(string leftKey, string rightKey = null)
        {
            var leftPropertyName = GetName<T>();
            var rightPropertyName = GetName<R>();

            Joins.Add(new Joiner()
            {
                JoinType = JoinType.Left,
                LeftTable = typeof(T).Name,
                RightTable = typeof(R).Name,
                Key = leftKey,
                RightKey = rightKey ?? leftKey
            });

        }



        /// <summary>
        /// If keys match, only one key name required.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <param name="leftKey">Name for key on left table </param>
        /// <param name="rightKey">Name for optional keyname on right table</param>
        public void Join<T, R>(string leftKey, string rightKey = null)
        {
            var leftPropertyName = GetName<T>();
            var rightPropertyName = GetName<R>();

            Joins.Add(new Joiner()
            {
                JoinType = JoinType.Inner,
                LeftTable = typeof(T).Name,
                RightTable = typeof(R).Name,
                Key = leftKey,
                RightKey = rightKey ?? leftKey
            });

        }

        /// <summary>
        /// Join for when keys are matching
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <param name="JoinOn"></param>
        /// <param name="joinType"></param>
        public void Join<T, R>(Expression<Func<T, object>> JoinOn, JoinType joinType = JoinType.Inner)
        {
            var keyAndFkey = GetNameFor(JoinOn);           

            Joins.Add(new Joiner()
            {
                JoinType = joinType,
                LeftTable = typeof(T).Name,
                RightTable = typeof(R).Name,
                Key = keyAndFkey,
                RightKey = keyAndFkey
            });
        }

        /// <summary>
        /// Join for not matching keynames
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="joinType"></param>
        public void Join<T, R>(Expression<Func<T, object>> p1, Expression<Func<R, object>> p2, JoinType joinType = JoinType.Inner)
        {            
            var leftKey = GetNameFor(p1);
            var rightKey = GetNameFor(p2);

            Joins.Add(new Joiner()
            {
                JoinType = joinType,
                LeftTable = typeof(T).Name,
                RightTable = typeof(R).Name,
                Key = leftKey,
                RightKey = rightKey
            });
        }

    }
}
