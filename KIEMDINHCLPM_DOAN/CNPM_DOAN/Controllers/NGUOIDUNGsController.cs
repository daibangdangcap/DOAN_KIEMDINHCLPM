using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using CNPM_DOAN.Models;

namespace CNPM_DOAN.Controllers
{
    public class NGUOIDUNGsController : Controller
    {
        private CNPM_DOANEntities db = new CNPM_DOANEntities();

        // GET: NGUOIDUNGs
        public ActionResult Index()
        {
            var nGUOIDUNGs = db.NGUOIDUNGs.Include(n => n.NGUOIDUNG2).Include(n => n.VAITRO);
            return View(nGUOIDUNGs.ToList());
        }

        // GET: NGUOIDUNGs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NGUOIDUNG nGUOIDUNG = db.NGUOIDUNGs.Find(id);
            if (nGUOIDUNG == null)
            {
                return HttpNotFound();
            }
            return View(nGUOIDUNG);
        }

        // GET: NGUOIDUNGs/Create
        public ActionResult Create()
        {
            ViewBag.IDQuanLy = new SelectList(db.NGUOIDUNGs, "IDNguoiDung", "TenNguoiDung");
            ViewBag.IDVaiTro = new SelectList(db.VAITROes, "IDVaiTro", "TenVaiTro");
            return View();
        }

        // POST: NGUOIDUNGs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDNguoiDung,TenNguoiDung,GioiTinh,NgaySinh,Email,Avatar,TenTaiKhoan,MatKhau,IDVaiTro,IDQuanLy")] NGUOIDUNG nGUOIDUNG)
        {
            if (ModelState.IsValid)
            {
                db.NGUOIDUNGs.Add(nGUOIDUNG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDQuanLy = new SelectList(db.NGUOIDUNGs, "IDNguoiDung", "TenNguoiDung", nGUOIDUNG.IDQuanLy);
            ViewBag.IDVaiTro = new SelectList(db.VAITROes, "IDVaiTro", "TenVaiTro", nGUOIDUNG.IDVaiTro);
            return View(nGUOIDUNG);
        }

        // GET: NGUOIDUNGs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NGUOIDUNG nGUOIDUNG = db.NGUOIDUNGs.Find(id);
            if (nGUOIDUNG == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDQuanLy = new SelectList(db.NGUOIDUNGs, "IDNguoiDung", "TenNguoiDung", nGUOIDUNG.IDQuanLy);
            ViewBag.IDVaiTro = new SelectList(db.VAITROes, "IDVaiTro", "TenVaiTro", nGUOIDUNG.IDVaiTro);
            return View(nGUOIDUNG);
        }

        // POST: NGUOIDUNGs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDNguoiDung,TenNguoiDung,GioiTinh,NgaySinh,Email,Avatar,TenTaiKhoan,MatKhau,IDVaiTro,IDQuanLy")] NGUOIDUNG nGUOIDUNG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nGUOIDUNG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDQuanLy = new SelectList(db.NGUOIDUNGs, "IDNguoiDung", "TenNguoiDung", nGUOIDUNG.IDQuanLy);
            ViewBag.IDVaiTro = new SelectList(db.VAITROes, "IDVaiTro", "TenVaiTro", nGUOIDUNG.IDVaiTro);
            return View(nGUOIDUNG);
        }

        // GET: NGUOIDUNGs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NGUOIDUNG nGUOIDUNG = db.NGUOIDUNGs.Find(id);
            if (nGUOIDUNG == null)
            {
                return HttpNotFound();
            }
            return View(nGUOIDUNG);
        }

        // POST: NGUOIDUNGs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NGUOIDUNG nGUOIDUNG = db.NGUOIDUNGs.Find(id);
            db.NGUOIDUNGs.Remove(nGUOIDUNG);
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
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
                NGUOIDUNG nguoidung = new NGUOIDUNG();
                nguoidung = nguoidung.checkAccount(username, password);
                if (nguoidung != null)
                {
                    Session["IDUSER"] = nguoidung.IDNguoiDung;
                    Session["TENTAIKHOAN"] = nguoidung.TenTaiKhoan;
                    Session["MATKHAU"] = nguoidung.MatKhau;
                    Session["EMAIL"] = nguoidung.Email;
                    Session["NGAYSINH"] = nguoidung.NgaySinh;
                    Session["GIOITINH"] = nguoidung.GioiTinh;
                    Session["TENNGUOIDUNG"] = nguoidung.TenNguoiDung;
                    Session["AVATAR"] = nguoidung.Avatar;
                    Session["IDQUANLY"] = nguoidung.IDQuanLy;
                    return RedirectToAction("showBaiTap", "BAITAPs", new { iduser = nguoidung.IDNguoiDung });
                }
            }
            return RedirectToAction("Index", "Home");
        }
        [ChildActionOnly]
        public ActionResult LoginUser()
        {
            Login login = new Login();
            return PartialView("Login", login);
        }
        [ChildActionOnly]
        public ActionResult RegisterUser()
        {
            NGUOIDUNG nguoidung = new NGUOIDUNG();
            ViewBag.IDVaiTro = new SelectList(db.VAITROes, "IDVaiTro", "TenVaiTro");
            return PartialView("Register", nguoidung);
        }
        public ActionResult Register()
        {
            return View();
        }

        // POST: NGUOIDUNGs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(string name, string email, DateTime birthday, string gender, string role, string username, string password, string repassword)
        {
            if (ModelState.IsValid)
            {
                NGUOIDUNG nGUOIDUNG = new NGUOIDUNG();
                nGUOIDUNG.TenNguoiDung = name;
                nGUOIDUNG.Email = email;
                nGUOIDUNG.NgaySinh = birthday;
                nGUOIDUNG.GioiTinh = gender;
                nGUOIDUNG.Avatar = null;
                if (role == "Học sinh") nGUOIDUNG.IDVaiTro = "HS";
                else nGUOIDUNG.IDVaiTro = "PH";
                nGUOIDUNG.TenTaiKhoan = username;
                nGUOIDUNG.MatKhau = password;
                nGUOIDUNG.IDNguoiDung = nGUOIDUNG.IDVaiTro + new RANDOMID().GenerateRandomString(2);
                nGUOIDUNG.IDQuanLy = nGUOIDUNG.IDNguoiDung;
                if (nGUOIDUNG.checkRegister(nGUOIDUNG.TenTaiKhoan) == true)
                {
                    db.NGUOIDUNGs.Add(nGUOIDUNG);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Informs(string iduser)
        {
            if (iduser == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NGUOIDUNG nGUOIDUNG = db.NGUOIDUNGs.Find(iduser);
            if (nGUOIDUNG == null)
            {
                return HttpNotFound();
            }
            return View(nGUOIDUNG);
        }
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file, string iduserr)
        {
            if (file != null && file.ContentLength > 0)
            {
                byte[] data = new byte[file.ContentLength];
                file.InputStream.Read(data, 0, file.ContentLength);

                var nguoidung = db.NGUOIDUNGs.Find(iduserr);
                if (iduserr == null) return RedirectToAction("Index", "VAITROes");

                nguoidung.Avatar = data;
                NGUOIDUNG nGUOIDUNG = nguoidung;
                db.Entry(nGUOIDUNG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Informs", "NGUOIDUNGs", new { iduser = iduserr });
            }
            else
            {
                ViewBag.Message = "Please select a file";
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult GetImage(string id)
        {
            // Lấy ảnh từ cơ sở dữ liệu
            var image = db.NGUOIDUNGs.Find(id);

            if (image.Avatar != null)
            {
                // Trả về ảnh dưới dạng tập tin hình ảnh động
                return File(image.Avatar, "image/png");
            }
            else
            {
                return HttpNotFound();
            }
        }
        public ActionResult updateUserInfo(string iduser)
        {
            var data = db.NGUOIDUNGs.Find(iduser);
            return View(data);
        }
        [HttpPost]
        public ActionResult updateUserInfo(string id, [Bind(Include = "TenNguoiDung,Email,NgaySinh,GioiTinh")] NGUOIDUNG nGUOIDUNG)
        {
            if (ModelState.IsValid) { new NGUOIDUNG().ChinhSuaThongTinCaNhan(id, nGUOIDUNG); return RedirectToAction("Informs", "NGUOIDUNGs", new { iduser = id }); }
            return View(nGUOIDUNG);
        }
        public ActionResult updateMatKhau(string iduser)
        {
            var data = db.NGUOIDUNGs.Find(iduser);
            return View(data);
        }
        [HttpPost]
        public ActionResult updateMatKhau(string id, string matkhaucu, string matkhaumoi, string xacnhanmatkhaumoi)
        {
            var data = db.NGUOIDUNGs.Find(id);

            if (matkhaumoi == xacnhanmatkhaumoi && matkhaucu == data.MatKhau)
            {
                new NGUOIDUNG().ChinhSuaMatKhau(id, matkhaumoi); return RedirectToAction("Informs", "NGUOIDUNGs", new { iduser = id });
            }
            return View(data);
        }
        public ActionResult showNguoiDung(string iduser)
        {
            var data=db.NGUOIDUNGs.Where(s=>s.IDQuanLy==iduser&&s.IDNguoiDung!=iduser);
            return View(data.ToList());
        }
        public ActionResult Register_PH(string idquanly)
        {
            var gioitinh = new List<string> { "Nam", "Nữ" };
            ViewBag.GioiTinh = new SelectList(gioitinh);
            ViewBag.IDQuanLy = idquanly;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register_PH([Bind(Include = "IDNguoiDung,TenNguoiDung,GioiTinh,NgaySinh,Email,Avatar,TenTaiKhoan,MatKhau,IDVaiTro,IDQuanLy")] NGUOIDUNG nguoidung)
        {
            if (ModelState.IsValid)
            {
                NGUOIDUNG nGUOIDUNG = new NGUOIDUNG();
                nGUOIDUNG.TenNguoiDung = nguoidung.TenNguoiDung;
                nGUOIDUNG.Email = nguoidung.Email;
                nGUOIDUNG.NgaySinh = nguoidung.NgaySinh;
                nGUOIDUNG.GioiTinh = nguoidung.GioiTinh;
                nGUOIDUNG.Avatar = null;
                nGUOIDUNG.TenTaiKhoan = nguoidung.TenTaiKhoan;
                nGUOIDUNG.MatKhau = nguoidung.MatKhau;
                nGUOIDUNG.IDVaiTro = "HS";
                nGUOIDUNG.IDNguoiDung = nGUOIDUNG.IDVaiTro + new RANDOMID().GenerateRandomString(2);
                nGUOIDUNG.IDQuanLy = nguoidung.IDQuanLy;
                if (nGUOIDUNG.checkRegister(nGUOIDUNG.TenTaiKhoan) == true)
                {
                    db.NGUOIDUNGs.Add(nGUOIDUNG);
                    db.SaveChanges();
                    return RedirectToAction("showNguoiDung", "NGUOIDUNGs", new { iduser=nGUOIDUNG.IDQuanLy});
                }
            }
            return View();
        }
        public ActionResult deleteTaiKhoan(string iduser)
        {
            var baigiai=db.BAIGIAIs.Where(s=>s.IDNguoiDung == iduser).ToList();
            foreach (var item in baigiai) 
            {
                db.BAIGIAIs.Remove(item);
            }
            
            if(iduser.Contains("PH"))
            {
                var baitap=db.BAITAPs.Where(s=>s.IDNguoiTao==iduser).ToList();
                foreach(var item in baitap)
                {
                    db.BAITAPs.Remove(item);
                }
                
                var hocsinh = db.NGUOIDUNGs.Where(s => s.IDQuanLy == iduser).ToList();
                foreach (var item in hocsinh)
                {
                    item.IDQuanLy = item.IDNguoiDung;
                    db.Entry(item).State = EntityState.Modified;
                }

                var nguoidung = db.NGUOIDUNGs.Find(iduser);
                db.NGUOIDUNGs.Remove(nguoidung);
            }
            else
            {
                var baitap=db.BAITAPs.Where(s=>s.IDNguoiNhan==iduser).ToList();
                foreach( var item in baitap)
                {
                    db.BAITAPs.Remove(item);
                }
                var nguoidung = db.NGUOIDUNGs.Find(iduser);
                db.NGUOIDUNGs.Remove(nguoidung);
            }

            var todo=db.TODOes.Where(s=>s.IDNguoiDung==iduser).ToList();
            foreach (var item in todo) 
            {
                db.TODOes.Remove(item);
            }

            var muctieu=db.MUCTIEUx.Where(s=>s.IDNguoiDung==iduser).ToList();
            foreach (var item in muctieu) { db.MUCTIEUx.Remove(item); }

            var tiethoc=db.TIETHOCs.Where(s=>s.IDNGUOITAO==iduser).ToList();
            foreach (var item in tiethoc) { db.TIETHOCs.Remove(item); }

            var tkb=db.THOIKHOABIEUx.Where(s=>s.IDNguoiDung==iduser).ToList() ;
            foreach (var item in tkb) { db.THOIKHOABIEUx.Remove(item); }

            db.SaveChanges();
            Session.Clear();
            return RedirectToAction("Index","Home");
        }

        public ActionResult deleteTaiKhoan_PH(string idquanly, string iduser)
        {
            var baigiai = db.BAIGIAIs.Where(s => s.IDNguoiDung == iduser).ToList();
            foreach (var item in baigiai)
            {
                db.BAIGIAIs.Remove(item);
            }

            var baitap = db.BAITAPs.Where(s => s.IDNguoiNhan == iduser).ToList();
            foreach (var item in baitap)
            {
                db.BAITAPs.Remove(item);
            }
            var nguoidung = db.NGUOIDUNGs.Find(iduser);
            db.NGUOIDUNGs.Remove(nguoidung);

            var todo = db.TODOes.Where(s => s.IDNguoiDung == iduser).ToList();
            foreach (var item in todo)
            {
                db.TODOes.Remove(item);
            }

            var muctieu = db.MUCTIEUx.Where(s => s.IDNguoiDung == iduser).ToList();
            foreach (var item in muctieu) { db.MUCTIEUx.Remove(item); }

            var tiethoc = db.TIETHOCs.Where(s => s.IDNGUOITAO == iduser).ToList();
            foreach (var item in tiethoc) { db.TIETHOCs.Remove(item); }

            var tkb = db.THOIKHOABIEUx.Where(s => s.IDNguoiDung == iduser).ToList();
            foreach (var item in tkb) { db.THOIKHOABIEUx.Remove(item); }

            db.SaveChanges();
            return RedirectToAction("showNguoiDung", "NGUOIDUNGs", new { iduser=idquanly});
        }
        public ActionResult showTTUser(string iduser)
        {
            var data = db.NGUOIDUNGs.Find(iduser);
            var phuhuynh = db.NGUOIDUNGs.Find(data.IDQuanLy);
            if (data != null) return View(phuhuynh);
            else return RedirectToAction("Index", "VAITROes");
        }
    }
}
