using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Layers.Models
{
    /// <summary>
    /// Модель данных для вступления в компанию
    /// </summary>
    public class EnterCompanyVM
    {
        [Required]
        [Display(Name = "Список компаний")]
        public SelectList Companies { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль вступления в компанию")]
        public string EnteringPassword { get; set; }

        public bool AlreadyEmployed { get; set; }
    }
}