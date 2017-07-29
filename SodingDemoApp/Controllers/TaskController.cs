using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SodingDemoApp;

namespace SodingDemoApp.Controllers
{
    public class TaskController : Controller
    {
        private SodingDBEntities db = new SodingDBEntities();

        // GET: /Task/
        public ActionResult Index()
        {
            return View(db.Table_UserInfo.ToList());
        }

        // GET: /Task/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table_UserInfo table_userinfo = db.Table_UserInfo.Find(id);
            if (table_userinfo == null)
            {
                return HttpNotFound();
            }
            return View(table_userinfo);
        }

        // GET: /Task/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Task/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Name,Description,Email")] Table_UserInfo table_userinfo)
        {
            if (ModelState.IsValid)
            {
                table_userinfo.DateCreated = DateTime.Now.Date;
                db.Table_UserInfo.Add(table_userinfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(table_userinfo);
        }

        // GET: /Task/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table_UserInfo table_userinfo = db.Table_UserInfo.Find(id);
            if (table_userinfo == null)
            {
                return HttpNotFound();
            }
            return View(table_userinfo);
        }

        // POST: /Task/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name,Description,Email")] Table_UserInfo table_userinfo)
        {
            if (ModelState.IsValid)
            {
                var getOldData = db.Table_UserInfo.Find(table_userinfo.Id);
                getOldData.Id = table_userinfo.Id;
                getOldData.Name = table_userinfo.Name;
                getOldData.Description = table_userinfo.Description;
                getOldData.Email = table_userinfo.Email;
                getOldData.DateUpdated = DateTime.Now.Date;
                //table_userinfo.DateCreated = getOldData.DateCreated;
                //table_userinfo.DateUpdated = DateTime.Now.Date;
                //db.Table_UserInfo.Add(table_userinfo);
                //db.Entry(table_userinfo).CurrentValues.SetValues(table_userinfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(table_userinfo);
        }

        // GET: /Task/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table_UserInfo table_userinfo = db.Table_UserInfo.Find(id);
            if (table_userinfo == null)
            {
                return HttpNotFound();
            }
            return View(table_userinfo);
        }

        // POST: /Task/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Table_UserInfo table_userinfo = db.Table_UserInfo.Find(id);
            db.Table_UserInfo.Remove(table_userinfo);
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
