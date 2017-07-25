using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YuwaWebApp.Models
{
    using System;

    public class StudentModel
    {
        public int id { get; set; }
        public Nullable<System.Guid> O365ID { get; set; }
        public System.DateTime Birthdate { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Student_Pic { get; set; }
        public string Blood_Group { get; set; }
        public string Village { get; set; }
        public Nullable<int> Phone { get; set; }
        public Nullable<System.DateTime> DOJ_Yuwa { get; set; }
        public Nullable<int> Familyid { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string middlename { get; set; }
        public Nullable<bool> is_student { get; set; }
        public Nullable<bool> is_coach { get; set; }
    }
}
