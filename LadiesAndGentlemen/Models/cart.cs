using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LadiesAndGentlemen.Models
{
    public class Cart
    {
        
        public int Id { get; set; }
        public ICollection<Product> Products { get; set; }
        public int OrderId { get; set; }
    }
}