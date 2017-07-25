using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OneWebApp.SqlLibrary;
using System.Data;
using OneWebApp.Models;

namespace OneWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        private T GetValue<T>(DataRow row, string columnName, T defaultVal)
        {
                object value = row[columnName];
                return value == DBNull.Value
                    ? defaultVal
                    : (T)Convert.ChangeType(value, typeof(T));
        }

        public ActionResult About()
        {
            TargetDB db = new TargetDB("Yuwa", "anly4s85vg.database.windows.net", "Yuwa", "Welcome_1234");
            List<DataTable> result = db.ExecuteQuery("Select * from StudentDetails");
            string studentList = "";

            List <StudentDetail> students = new List<StudentDetail>();


/*
    [id] [int] NOT NULL,

    [O365ID] [uniqueidentifier] NULL,
	[Birthdate]
        [smalldatetime]
        NOT NULL,
    [Gender] [char](1) NULL,
	[Address] [nvarchar] (1000) NULL,
	[Student Pic] [nvarchar] (100) NULL,
	[Blood Group] [char](3) NULL,
	[Village] [nvarchar] (100) NULL,
	[Phone] [int] NULL,
	[DOJ_Yuwa] [smalldatetime] NULL,
	[Familyid] [int] NULL,
	[firstname] [nvarchar] (255) NULL,
	[lastname] [nvarchar] (255) NULL,
	[middlename] [nvarchar] (255) NULL,
	[is_student] [bit] NULL,
	[is_coach] [bit] NULL,
	[schoolid] [int] NULL,
	[teamid] [int] NULL,
	[classid] [int] NULL,
	[nickname] [nvarchar] (255) NULL,*/

            foreach (DataRow row in result[0].Rows)
            {
                students.Add(new StudentDetail()
                {
                    FirstName = GetValue<string>(row,"firstname", ""),
                    LastName = GetValue<string>(row, "lastname", ""),
                    Gender = GetValue<string>(row, "Gender", ""),
                    NickName = GetValue<string>(row, "nickname", ""),
                    Birthdate = (DateTime)Convert.ChangeType(row["Birthdate"], typeof(DateTime)),
                    DOJ_Yuwa = GetValue<DateTime>(row, "DOJ_Yuwa", DateTime.MaxValue),
                    Address = GetValue<string>(row, "Address", ""),
                    BloodGroup = GetValue<string>(row, "Blood Group", ""),
                    Village = GetValue<string>(row, "Village", ""),
                    Phone = GetValue<string>(row, "Phone", ""),
                    IsStudent = GetValue<bool>(row, "is_student", true),
                    IsCoach = GetValue<bool>(row, "is_coach", false),
                });
            }

            ViewBag.Message = "studentList";

            return View(students);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}