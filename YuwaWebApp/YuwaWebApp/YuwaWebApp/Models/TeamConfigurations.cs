using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YuwaWebApp.Models
{
    public class TeamConfigurations
    {
        public List<StudentDetail> AllStudents { get; set; }
        public List<StudentDetail> UnAllocatedSudents { get; set; }
        public List<TeamDetail> Teams { get; set; }
    }
}