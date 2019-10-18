using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ChicAndDapper
{    
    public enum Direction
    {
        Ascending,
        Descending
    }
    internal class OrderBy
    {
        

        internal Direction Direction { get; set; }
        internal string Column { get; set; }
    }

    public partial class Chic
    {
        internal Dictionary<Direction, string> directionMap = new Dictionary<Direction, string>()
        {
            { Direction.Ascending, "ASC" },
            { Direction.Descending, "DESC" }
        };

        List<OrderBy> orders = new List<OrderBy>();

        public void Orderby<T>(Expression<Func<T, object>> expression, Direction direction = Direction.Ascending)
        {
            var colName = GetNameFor(expression);
            if (!orders.Exists(e => e.Column == colName))
            {
                orders.Add(new OrderBy()
                {
                    Direction = direction,
                    Column = GetNameFor(expression)
                });
            }
        }

        

    }
}
