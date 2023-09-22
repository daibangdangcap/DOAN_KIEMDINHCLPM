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
    public class MUCTIEUxController : Controller
    {
        private CNPM_DOANEntities db = new CNPM_DOANEntities();

        // GET: MUCTIEUx
        public ActionResult Index()
        {
            var mUCTIEUx = db.MUCTIEUx.Include(m => m.NGUOIDUNG);
            return View(mUCTIEUx.ToList());
        }

        // GET: MUCTIEUx/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MUCTIEU mUCTIEU = db.MUCTIEUx.Find(id);
            if (mUCTIEU == null)
            {
                return HttpNotFound();
            }
            return View(mUCTIEU);
        }

        // GET: MUCTIEUx/Create
        public ActionResult Create()
        {
            ViewBag.IDNguoiDung = new SelectList(db.NGUOIDUNGs, "IDNguoiDung", "TenNguoiDung");
            return View();
        }

        // POST: MUCTIEUx/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDMucTieu,TenMucTieu,IDNguoiDung")] MUCTIEU mUCTIEU)
        {
            if (ModelState.IsValid)
            {
                db.MUCTIEUx.Add(mUCTIEU);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDNguoiDung = new SelectList(db.NGUOIDUNGs, "IDNguoiDung", "TenNguoiDung", mUCTIEU.IDNguoiDung);
            return View(mUCTIEU);
        }

        // GET: MUCTIEUx/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MUCTIEU mUCTIEU = db.MUCTIEUx.Find(id);
            if (mUCTIEU == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDNguoiDung = new SelectList(db.NGUOIDUNGs, "IDNguoiDung", "TenNguoiDung", mUCTIEU.IDNguoiDung);
            return View(mUCTIEU);
        }

        // POST: MUCTIEUx/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDMucTieu,TenMucTieu,IDNguoiDung")] MUCTIEU mUCTIEU)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mUCTIEU).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDNguoiDung = new SelectList(db.NGUOIDUNGs, "IDNguoiDung", "TenNguoiDung", mUCTIEU.IDNguoiDung);
            return View(mUCTIEU);
        }

        // GET: MUCTIEUx/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MUCTIEU mUCTIEU = db.MUCTIEUx.Find(id);
            if (mUCTIEU == null)
            {
                return HttpNotFound();
            }
            return View(mUCTIEU);
        }

        // POST: MUCTIEUx/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            MUCTIEU mUCTIEU = db.MUCTIEUx.Find(id);
            db.MUCTIEUx.Remove(mUCTIEU);
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
        public ActionResult showMucTieu(string idus)
        {
            if(idus.Contains("PH"))
            {
                return RedirectToAction("showUserMucTieu_PH", "MUCTIEUx", new { iduser = idus });
            }
            else
            {
                var data = db.MUCTIEUx.Where(s => s.IDNguoiDung == idus).ToList();
                return View(data);
            }
        }
        public ActionResult showUserMucTieu_PH(string iduser)
        {
            var data=db.NGUOIDUNGs.Where(s=>s.IDQuanLy==iduser&&s.IDNguoiDung!=iduser).ToList();
            return View(data);
        }
        [HttpPost]
        public ActionResult ThemMucTieu(string id,string ndmuctieu)
        {
            if(ndmuctieu==null)
            {
                return RedirectToAction("showMucTieu", "MUCTIEUx", new { idus = id });
            }
            if(ModelState.IsValid)
            {
                MUCTIEU mUCTIEU = new MUCTIEU();
                mUCTIEU.IDMucTieu = id + "MT" + new RANDOMID().GenerateRandomString(2);
                mUCTIEU.IDNguoiDung = id;
                mUCTIEU.TenMucTieu = ndmuctieu;
                db.MUCTIEUx.Add(mUCTIEU);
                db.SaveChanges();
                return RedirectToAction("showMucTieu", "MUCTIEUx", new { idus = id });
            }
            return View();
        }
        
        public ActionResult deleteMucTieu(string idMT, string id)
        {
            var data = db.MUCTIEUx.Find(idMT);
            db.MUCTIEUx.Remove(data);
            db.SaveChanges();
            return RedirectToAction("showMucTieu", "MUCTIEUx", new {idus=id});
        }
        public ActionResult showMucTieu_PH(string iduser) 
        {
            var data=db.MUCTIEUx.Where(s=>s.IDNguoiDung==iduser);
            return View(data.ToList());
        }
    }
}
