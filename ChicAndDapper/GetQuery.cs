using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChicAndDapper
{
    public partial class Chic
    {

        private StringBuilder selectString = new StringBuilder();
        private StringBuilder columnsString = new StringBuilder();

        public string GetQuery()
        {
            BuildColumns();
            SetSelectString();
            SetJoinString();
            SetWhereString();
            SetOrderByString();

            return selectString.ToString();
        }

        private void BuildColumns()
        {
            for (int i = 0; i <= columns.Count - 1; i++)
            {
                var col = columns.ElementAt(i);
                columnsString.Append($" {col.Key} as {col.Value}");

                if (i != columns.Count - 1)
                {
                    columnsString.Append(",");
                }
            }
        }     


        private void SetSelectString()
        {
            selectString.Append($"Select {columnsString.ToString()} from {GetName(FromType)}");
        }

        private string GetName(Type fromType)
        {
            return fromType.Name;
        }

        private void SetJoinString()
        {
            foreach (var join in Joins)
            {
                selectString.Append($" {join.JoinType} join {join.RightTable} {join.RightTable} " +
                    $"on {join.LeftTable}.{join.Key} = {join.RightTable}.{join.RightKey ?? join.Key} ");
            }
        }

        internal void SetOrderByString()
        {
            StringBuilder orderBy = new StringBuilder();

            if (orders.Count == 0)
            {
                return;
            }

            orderBy.Append(" ORDER BY ");
            for (int i = 0; i <= orders.Count - 1; i++)
            {
                orderBy.Append($" {orders[i].Column} {(i == orders.Count - 1 ? " " : ", ")}");
            }
            orderBy.Append($" {directionMap[orders[0].Direction]} ");

            selectString.Append(orderBy);
        }
    }
}
