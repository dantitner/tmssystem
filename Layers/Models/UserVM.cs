using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Layers.Models
{
    public class UserVM
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Login { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        public string Email { get; set; }
    }
}