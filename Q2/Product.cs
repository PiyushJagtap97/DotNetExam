using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppExam.Models
{
    public class Product { 
     public int ProductId { get; set; }

    public String ProductName { get; set; }
    public Boolean Rate { get; set; }
    public String Description { get; set; }
    public int Catogorytype { get; set; }
}
}