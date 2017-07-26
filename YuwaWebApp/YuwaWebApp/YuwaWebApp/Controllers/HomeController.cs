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
        private Lazy<TargetDB> targetDB = new Lazy<TargetDB>(new Func<TargetDB>(() => new TargetDB("Yuwa", "anly4s85vg.database.windows.net", "Yuwa", "Welcome_1234")));

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
            List<DataTable> result = targetDB.Value.ExecuteQuery("Select * from StudentDetails");
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
            List<DataTable> result = targetDB.Value.ExecuteQuery("Select * from CoachDetails");
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

        public ActionResult Teams()
        {
            List<DataTable> result = targetDB.Value.ExecuteQuery("Select * from TeamDetails");
            List<TeamDetail> teams = new List<TeamDetail>();

            foreach (DataRow row in result[0].Rows)
            {
                teams.Add(new TeamDetail()
                {
                    TeamName = GetValue<string>(row, "teamname", ""),
                    Captain = GetValue<string>(row, "captain", ""),
                    CoachId = GetValue<int>(row, "coachid", 0)
                });
            }

            ViewBag.Message = "teamList";

            return View(teams);
        }

        public ActionResult AddStudent()
        {

            return View();
        }

        private void writeToDB(StudentDetail student)
        {
            TargetDB db = new TargetDB("Yuwa", "anly4s85vg.database.windows.net", "Yuwa", "Welcome_1234");
            var queryString = string.Format("INSERT INTO StudentDetails(FirstName, LastName, Gender) VALUES ('{0}','{1}','{2}')", student.FirstName, student.LastName, student.Gender);
            db.ExecuteNonQuery(queryString);
        }

        public ActionResult AddCoach()
        {

            return View();
        }
        
        
        private void writeToDB(CoachDetail coach)
        {
            string insertQry = string.Format("INSERT INTO CoachDetails(CoachName, Comments) VALUES('{0}', '{1}')", coach.CoachName, "Comments about " + coach.CoachName);
            targetDB.Value.ExecuteNonQuery(insertQry);  
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult AddStudent(StudentDetail student)
        {
            writeToDB(student);
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