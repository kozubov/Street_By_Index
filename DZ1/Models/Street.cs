using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DZ1.Models
{
    public class Street
    {
        [Display(Name = "Название районов")]
        public string Name { get; set; }
    }
}