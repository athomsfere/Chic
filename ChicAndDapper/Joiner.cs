using System;
using System.Collections.Generic;
using System.Text;

namespace ChicAndDapper
{
    public class Joiner
    {
        internal JoinType JoinType { get; set; }
        internal string LeftTable { get; set; }
        internal string RightTable { get; set; }
        internal string Key { get; set; }
        internal string RightKey { get; set; }
    }
}
