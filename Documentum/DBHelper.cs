using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documentum
{
    public class StandardExecutionResult
    {
        public int ErrCode { get; set; }

        public string ErrMessage { get; set; }
    
    }

    public class BookmarkResult
    {
        public string BookmarkName { get; set; }

        public string BookmarkValue { get; set; }

    }
}
