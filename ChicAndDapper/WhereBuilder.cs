using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ChicAndDapper
{
    internal class WhereBuilder
    {
        public WhereBuilder()
        {
            ParentOperand = ExpressionType.Default;
        }
        internal string Left { get; set; }
        internal string Right { get; set; }
        internal ExpressionType Operand { get; set; }
        internal ExpressionType ParentOperand { get; set; }
        internal string ClassName { get; set; }
    }
}
