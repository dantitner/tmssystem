using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayersDLL.DTO
{
    public class CompanyDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual UserDTO Creator { get; set; }
        public string EnteringPassword { get; set; }

        public virtual List<UserDTO> Users { get; set; }

        public CompanyDTO()
        {
            Users = new List<UserDTO>();
        }
    }
}
