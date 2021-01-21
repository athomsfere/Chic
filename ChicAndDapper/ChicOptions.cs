using System;
using System.Collections.Generic;
using System.Text;

namespace ChicAndDapper
{
    public class ChicOptions
    {
        public SqlType SqlType { get; set; }

        public ChicOptions()
        {
            this.SqlType = SqlType.SqlServer;
        }
    }
}
