using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using YuwaWebApp.SqlLibrary;
using YuwaWebApp.Models;

namespace YuwaWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private T GetValue<T>(DataRow row, string columnName, T defaultVal)
        {
            object value = row[columnName];
            return value == DBNull.Value
                ? defaultVal
                : (T)Convert.ChangeType(value, typeof(T));
        }

        public ActionResult Students()
        {
            TargetDB db = new TargetDB("Yuwa", "anly4s85vg.database.windows.net", "Yuwa", "Welcome_1234");
            List<DataTable> result = db.ExecuteQuery("Select * from StudentDetails");
            List<StudentDetail> students = new List<StudentDetail>();

            foreach (DataRow row in result[0].Rows)
            {
                students.Add(new StudentDetail()
                {
                    FirstName = GetValue<string>(row, "firstname", ""),
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

        public ActionResult Coaches()
        {
            TargetDB db = new TargetDB("Yuwa", "anly4s85vg.database.windows.net", "Yuwa", "Welcome_1234");
            List<DataTable> result = db.ExecuteQuery("Select * from CoachDetails");
            List<CoachDetail> coaches = new List<CoachDetail>();

            foreach (DataRow row in result[0].Rows)
            {
                coaches.Add(new CoachDetail()
                {
                    CoachName = GetValue<string>(row, "coachname", "")
                });
            }

            ViewBag.Message = "coachList";

            return View(coaches);
        }
        
        public ActionResult AddStudent()
        {

            return View();
        }


        public ActionResult AddCoach()
        {

            return View();
        }

        private void writeToDB(AddStudentViewModel student)
        {
            //throw new NotImplementedException();
        }

        private void writeToDB(CoachDetail coach)
        {
            //throw new NotImplementedException();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult AddStudent(AddStudentViewModel student)
        {
            writeToDB(student);
            return RedirectToAction("Students", "Home");
        }
        public ActionResult SubmitNewStudent(StudentDetail student)
        {
            return RedirectToAction("Students", "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult AddCoach(CoachDetail coach)
        {
            writeToDB(coach);
            return RedirectToAction("Coaches", "Home");
        }

    }
}