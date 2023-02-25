using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers
{
    public class RegisterationsController : Controller
    {
        private StudentCourseEntities db = new StudentCourseEntities();

        // GET: Registerations
        public ActionResult Index()
        {
            var registerations = db.Registerations.Include(r => r.Batch).Include(r => r.Course);
            return View(registerations.ToList());
        }

        // GET: Registerations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registeration registeration = db.Registerations.Find(id);
            if (registeration == null)
            {
                return HttpNotFound();
            }
            return View(registeration);
        }

        // GET: Registerations/Create
        public ActionResult Create()
        {
            ViewBag.Batch_Id = new SelectList(db.Batches, "Id", "Batch1");
            ViewBag.Course_Id = new SelectList(db.Courses, "Id", "Course1");
            return View();
        }

        // POST: Registerations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Nic,Course_Id,Batch_Id,Telephone")] Registeration registeration)
        {
            if (ModelState.IsValid)
            {
                db.Registerations.Add(registeration);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Batch_Id = new SelectList(db.Batches, "Id", "Batch1", registeration.Batch_Id);
            ViewBag.Course_Id = new SelectList(db.Courses, "Id", "Course1", registeration.Course_Id);
            return View(registeration);
        }

        // GET: Registerations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registeration registeration = db.Registerations.Find(id);
            if (registeration == null)
            {
                return HttpNotFound();
            }
            ViewBag.Batch_Id = new SelectList(db.Batches, "Id", "Batch1", registeration.Batch_Id);
            ViewBag.Course_Id = new SelectList(db.Courses, "Id", "Course1", registeration.Course_Id);
            return View(registeration);
        }

        // POST: Registerations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Nic,Course_Id,Batch_Id,Telephone")] Registeration registeration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registeration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Batch_Id = new SelectList(db.Batches, "Id", "Batch1", registeration.Batch_Id);
            ViewBag.Course_Id = new SelectList(db.Courses, "Id", "Course1", registeration.Course_Id);
            return View(registeration);
        }

        // GET: Registerations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registeration registeration = db.Registerations.Find(id);
            if (registeration == null)
            {
                return HttpNotFound();
            }
            return View(registeration);
        }

        // POST: Registerations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Registeration registeration = db.Registerations.Find(id);
            db.Registerations.Remove(registeration);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
