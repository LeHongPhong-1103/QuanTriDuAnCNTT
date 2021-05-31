using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLyGiaDung.Models;
using System.IO;
using PagedList;

namespace QuanLyGiaDung.Controllers
{
    public class NhanViensController : Controller
    {
        private QuanLyGiaDung2Entities db = new QuanLyGiaDung2Entities();

        // GET: NhanViens
        public ActionResult Index(string searchString, string sortOrder, int? page)
        {


            var links = from l in db.NhanViens // lấy toàn bộ liên kết
                        select l;

            if (!String.IsNullOrEmpty(searchString)) // kiểm tra chuỗi tìm kiếm có rỗng/null hay không
            {
                links = links.Where(s => s.TenNV.Contains(searchString)); //lọc theo chuỗi tìm kiếm
            }

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            switch (sortOrder)
            {
                // 3.1 Nếu biến sortOrder sắp giảm thì sắp giảm theo LinkName
                case "name_desc":
                    links = links.OrderByDescending(s => s.TenNV);
                    break;

                // 3.2 Mặc định thì sẽ sắp tăng
                default:
                    links = links.OrderBy(s => s.TenNV);
                    break;
            }

            if (page == null) page = 1;
            links = links.OrderBy(s => s.MaNV);
            int pageSize = 6;
            int pageNumber = (page ?? 1);

            return View(links.ToPagedList(pageNumber, pageSize)); //trả về kết quả
        }


        // GET: NhanViens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // GET: NhanViens/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NhanViens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase AnhNV, NhanVien nhanVien)
        {
            if (AnhNV != null)
            {
                string p = Path.Combine(Server.MapPath("~/Anh"), Path.GetFileName(AnhNV.FileName));
                AnhNV.SaveAs(p);
                nhanVien.AnhNV = AnhNV.FileName;
            }
            if (ModelState.IsValid)
            {
                db.NhanViens.Add(nhanVien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nhanVien);
        }

        // GET: NhanViens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // POST: NhanViens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase AnhNV, NhanVien nhanVien)
        {
            if (AnhNV != null)
            {
                string path = Path.Combine(Server.MapPath("~/Anh"), Path.GetFileName(AnhNV.FileName));
                AnhNV.SaveAs(path);
                nhanVien.AnhNV = AnhNV.FileName;
            }
            if (ModelState.IsValid)
            {
                db.Entry(nhanVien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                db.NhanViens.Add(nhanVien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(nhanVien);
        }

        // GET: NhanViens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // POST: NhanViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NhanVien nhanVien = db.NhanViens.Find(id);
            db.NhanViens.Remove(nhanVien);
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
