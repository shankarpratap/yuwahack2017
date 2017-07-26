using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YuwaWebApp.Models
{
    public class StudentDetail
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string Gender { get; set; }
        public DateTime Birthdate { get; set; }
        public DateTime DOJ_Yuwa { get; set; }
        public string Address { get; set; }
        public string BloodGroup { get; set; }
        public string Village { get; set; }
        public string Phone { get; set; }
        public bool IsStudent { get; set; }
        public bool IsCoach { get; set; }
        public string Name
        {
            get
            {
                return this.FirstName + " " + this.LastName;
            }
            set { }
        }
        public string Age
        {
            get
            {
                return (Math.Ceiling((DateTime.Now - this.Birthdate).TotalDays / 365)).ToString();
            }
            set { }
        }
    }
    public class AddStudentViewModel
    {
        public string StudentName { get; set; }

        public string Village { get; set; }

    }
}