using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Layers.Models
{
    public class SessionVM
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int UserId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Commentary { get; set; }

        public string CompanyName { get; set; }
        public string UserName { get; set; }
        public TimeSpan? WorkTime { get; set; }
    }
}