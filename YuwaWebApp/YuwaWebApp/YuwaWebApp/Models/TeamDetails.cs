using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YuwaWebApp.Models
{
    public class TeamDetail
    {
        public string TeamName { get; set; }
        public string Captain { get; set; }
        public CoachDetail Coach { get; set; }
        public List<StudentDetail> Students { get; set; }
    }
}