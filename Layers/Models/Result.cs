using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Layers.Models
{
    /// <summary>
    /// Класс для передачи данных во вьюшку отчетов
    /// </summary>
    public class Result
    {
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}