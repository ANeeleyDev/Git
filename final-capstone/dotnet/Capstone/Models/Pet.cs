using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Pet
    {
        public int petId { get; set; }
        public int userId { get; set; }
        public string petName { get; set; }
        public int age { get; set; }
        public string species { get; set; }
        public string breed { get; set; }
        public string otherComments { get; set; }
        
        //personalities
        public bool playful { get; set; }
        public bool nervous { get; set; }
        public bool confident { get; set; }
        public bool shy { get; set; }
        public bool mischievous { get; set; }
        public bool independent { get; set; }
    }
}
