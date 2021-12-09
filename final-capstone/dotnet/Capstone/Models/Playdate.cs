using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Playdate
    {
        public int playdateId { get; set; }
        public int playdatePostedUserId { get; set; }
        public int playdateRequestedUserId { get; set; }
        public DateTime meetingTime { get; set; }
        public string playdateAddress { get; set; }
        public string playdateCity { get; set; }
        public string playdateState { get; set; }
        public string playdateZip { get; set; }
        public int playdateStatusId { get; set; }
    }
}
