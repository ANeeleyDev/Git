using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Playdate
    {
        public int playdateId { get; set; }
        public string location { get; set; }
        public DateTime meetingTime { get; set; }
    }
}
