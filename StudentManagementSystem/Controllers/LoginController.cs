using StudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentManagementSystem.Controllers
{
    public class LoginController : Controller
    {
        private StudentCourseEntities db;
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Student std)
        {
            if (ModelState.IsValid)
            {
                using(StudentCourseEntities db= new StudentCourseEntities())
                {

                    var student = db.Students.Where(a => a.UserName.Equals(std.UserName) && a.Password.Equals(std.Password)).FirstOrDefault();
                    if (student != null)
                    {
                        Session["UserId"] = student.Id.ToString();
                        Session["UserName"] = student.UserName.ToString();

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "UserName Or Password Is Incorret");
                    }   
                }
                
            }
            return View(std);
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Login");
    }
    }
 
}