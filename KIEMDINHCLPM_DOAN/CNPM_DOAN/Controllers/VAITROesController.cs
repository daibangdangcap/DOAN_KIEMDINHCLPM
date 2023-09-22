using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CNPM_DOAN.Models;

namespace CNPM_DOAN.Controllers
{
    public class VAITROesController : Controller
    {
        private CNPM_DOANEntities db = new CNPM_DOANEntities();

        // GET: VAITROes
        public ActionResult Index()
        {
            return View(db.VAITROes.ToList());
        }

        // GET: VAITROes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VAITRO vAITRO = db.VAITROes.Find(id);
            if (vAITRO == null)
            {
                return HttpNotFound();
            }
            return View(vAITRO);
        }

        // GET: VAITROes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VAITROes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDVaiTro,TenVaiTro")] VAITRO vAITRO)
        {
            if (ModelState.IsValid)
            {
                db.VAITROes.Add(vAITRO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vAITRO);
        }

        // GET: VAITROes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VAITRO vAITRO = db.VAITROes.Find(id);
            if (vAITRO == null)
            {
                return HttpNotFound();
            }
            return View(vAITRO);
        }

        // POST: VAITROes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDVaiTro,TenVaiTro")] VAITRO vAITRO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vAITRO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vAITRO);
        }

        // GET: VAITROes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VAITRO vAITRO = db.VAITROes.Find(id);
            if (vAITRO == null)
            {
                return HttpNotFound();
            }
            return View(vAITRO);
        }

        // POST: VAITROes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            VAITRO vAITRO = db.VAITROes.Find(id);
            db.VAITROes.Remove(vAITRO);
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
