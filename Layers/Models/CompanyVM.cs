using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Layers.Models
{
    public class CompanyVM
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Название компании")]
        public string Name { get; set; }
        public virtual UserVM Creator { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль вступления в компанию")]
        public string EnteringPassword { get; set; }

        public virtual List<UserVM> Users { get; set; }

        public CompanyVM()
        {
            Users = new List<UserVM>();
        }
    }
}