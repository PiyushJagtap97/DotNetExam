using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DotNetExam.Models
{
    public class Product
    {
       
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Error")]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        public string ProductName { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Enter Rate")]
        public decimal Rate { get; set; }

        [Required(ErrorMessage = "Error")]
        [MaxLength(200)]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Select Category")]

        public int CategoryId { get; set; }
    }
}