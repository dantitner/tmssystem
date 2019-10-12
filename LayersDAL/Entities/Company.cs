using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayersDAL.Entities
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual User Creator { get; set; }
        public string EnteringPassword { get; set; }

        public virtual List<User> Users { get; set; }

        public Company()
        {
            Users = new List<User>();
        }
    }
}
