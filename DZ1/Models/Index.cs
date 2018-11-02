using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DZ1.Models
{
    public class Index
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "Индекс")]
        [Required(ErrorMessage = "Введите индекс")]
        [RegularExpression(@"\-?\d+(\d{0,})?", ErrorMessage = "Неверный вид индекса")]
        [DataType(DataType.Text)]
        public string Name_Index { get; set; }
    }
}