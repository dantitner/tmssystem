using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayersDLL.BLL
{
    public class SessionTime
    {
        private DateTime start;
        private DateTime end;
        public SessionTime(DateTime start, DateTime end)
        {
            this.start = start;
            this.end = end;
        }

        public TimeSpan GetSessionTime()
        {
            return end - start;
        }
    }
}

