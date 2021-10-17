using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Item
    {
        [Key]
        public int CartId { get; set; }
        public Laptop Laptop{ get; set; }
        public int Quantity { get; set; }
    }
}
