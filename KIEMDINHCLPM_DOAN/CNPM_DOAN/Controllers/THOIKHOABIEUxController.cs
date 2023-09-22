using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CNPM_DOAN.Models;
using Microsoft.Ajax.Utilities;

namespace CNPM_DOAN.Controllers
{
    public class THOIKHOABIEUxController : Controller
    {
        private CNPM_DOANEntities db = new CNPM_DOANEntities();

        // GET: THOIKHOABIEUx
        public ActionResult Index()
        {
            var tHOIKHOABIEUx = db.THOIKHOABIEUx.Include(t => t.NGUOIDUNG);
            return View(tHOIKHOABIEUx.ToList());
        }

        // GET: THOIKHOABIEUx/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THOIKHOABIEU tHOIKHOABIEU = db.THOIKHOABIEUx.Find(id);
            if (tHOIKHOABIEU == null)
            {
                return HttpNotFound();
            }
            return View(tHOIKHOABIEU);
        }

        // GET: THOIKHOABIEUx/Create
        public ActionResult Create()
        {
            ViewBag.IDNguoiDung = new SelectList(db.NGUOIDUNGs, "IDNguoiDung", "TenNguoiDung");
            return View();
        }

        // POST: THOIKHOABIEUx/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDTKB,TenTKB,IDNguoiDung")] THOIKHOABIEU tHOIKHOABIEU)
        {
            if (ModelState.IsValid)
            {
                db.THOIKHOABIEUx.Add(tHOIKHOABIEU);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDNguoiDung = new SelectList(db.NGUOIDUNGs, "IDNguoiDung", "TenNguoiDung", tHOIKHOABIEU.IDNguoiDung);
            return View(tHOIKHOABIEU);
        }

        // GET: THOIKHOABIEUx/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THOIKHOABIEU tHOIKHOABIEU = db.THOIKHOABIEUx.Find(id);
            if (tHOIKHOABIEU == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDNguoiDung = new SelectList(db.NGUOIDUNGs, "IDNguoiDung", "TenNguoiDung", tHOIKHOABIEU.IDNguoiDung);
            return View(tHOIKHOABIEU);
        }

        // POST: THOIKHOABIEUx/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDTKB,TenTKB,IDNguoiDung")] THOIKHOABIEU tHOIKHOABIEU)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tHOIKHOABIEU).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDNguoiDung = new SelectList(db.NGUOIDUNGs, "IDNguoiDung", "TenNguoiDung", tHOIKHOABIEU.IDNguoiDung);
            return View(tHOIKHOABIEU);
        }

        // GET: THOIKHOABIEUx/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THOIKHOABIEU tHOIKHOABIEU = db.THOIKHOABIEUx.Find(id);
            if (tHOIKHOABIEU == null)
            {
                return HttpNotFound();
            }
            return View(tHOIKHOABIEU);
        }

        // POST: THOIKHOABIEUx/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            THOIKHOABIEU tHOIKHOABIEU = db.THOIKHOABIEUx.Find(id);
            db.THOIKHOABIEUx.Remove(tHOIKHOABIEU);
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
        public ActionResult showTKB(string iduser)
        {
            if(iduser.Contains("HS"))
            {
                var data = db.THOIKHOABIEUx.Where(s => s.IDNguoiDung == iduser).ToList();
                return View(data);
            }
            else
            {
                return RedirectToAction("showUserTKB_PH", "THOIKHOABIEUx", new { idus=iduser});
            }
        }
        public ActionResult showUserTKB_PH(string idus)
        {
            var data = db.NGUOIDUNGs.Where(s => s.IDQuanLy == idus && s.IDNguoiDung != idus).ToList();
            return View(data);
        }
        public ActionResult taoTKBMoi()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult taoTKBMoi([Bind(Include = "TenTKB,IDNguoiDung")] THOIKHOABIEU thoikb,string idnguoitao)
        {
            if (ModelState.IsValid)
            {
                THOIKHOABIEU tHOIKHOABIEU = new THOIKHOABIEU();
                tHOIKHOABIEU.IDTKB = "TKB" + new RANDOMID().GenerateRandomString(2);
                tHOIKHOABIEU.TenTKB = thoikb.TenTKB;
                tHOIKHOABIEU.IDNguoiDung = idnguoitao;
                db.THOIKHOABIEUx.Add(tHOIKHOABIEU);
                db.SaveChanges();
                return RedirectToAction("showTKB", "THOIKHOABIEUx", new { iduser = idnguoitao });
            }
            else return View(thoikb);
        }
        public ActionResult deleteTKB(string idnguoitao,string idtkb)
        {
            var data = db.TIETHOCs.Where(s => s.IDTKB == idtkb).ToList();
            foreach(var item in data)
            {
                db.TIETHOCs.Remove(item);
            }
            var tkb = db.THOIKHOABIEUx.Find(idtkb);
            db.THOIKHOABIEUx.Remove(tkb);
            db.SaveChanges();
            return RedirectToAction("showTKB", "THOIKHOABIEUx", new { iduser = idnguoitao });
        }
        public ActionResult showTKB_PH(string iduser)
        {
            var data = db.THOIKHOABIEUx.Where(s => s.IDNguoiDung == iduser);
            return View(data.ToList());
        }
    }
}
