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

        public ActionResult About()
        {
            TargetDB db = new TargetDB("Yuwa", "anly4s85vg.database.windows.net", "Yuwa", "Welcome_1234");
            List<DataTable> result = db.ExecuteQuery("Select firstname, Gender from StudentDetails");
            string studentList = "";

            List <StudentDetail> students = new List<StudentDetail>();

            foreach (DataRow row in result[0].Rows)
            {
                students.Add(new StudentDetail() { FirstName = row["firstname"].ToString(), Gender = row["Gender"].ToString() });
                studentList += Environment.NewLine + row["firstname"].ToString();
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