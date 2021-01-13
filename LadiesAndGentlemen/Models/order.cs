using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LadiesAndGentlemen.Models
{
    public class Order
    {
        
        public int Id { get; set; }
        public Client Client { get; set; }
        public float Sum { get; set; }
        
        public Cart Cart { get; set; }
        public int CartId { get; set; }

        public Address Address { get; set; }
    }
}