using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using YuwaWebApp.Models;

namespace YuwaWebApp.Controllers
{
    public class StudentController : Controller
    {
        // POST: /Account/AddNewStudent
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewStudent(StudentModel student)
        {
            return RedirectToAction("Index", "Home");
        }
    }

}