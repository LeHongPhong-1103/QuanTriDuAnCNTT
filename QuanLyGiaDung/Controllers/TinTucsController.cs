using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLyGiaDung.Models;
using PagedList;

namespace QuanLyGiaDung.Controllers
{
    public class TinTucsController : Controller
    {
        private QuanLyGiaDung2Entities db = new QuanLyGiaDung2Entities();

        // GET: TinTucs
        public ActionResult Index(string searchString, int? page)
        {
            var links = from l in db.TinTucs // lấy toàn bộ liên kết
                        select l;

            if (!String.IsNullOrEmpty(searchString)) // kiểm tra chuỗi tìm kiếm có rỗng/null hay không
            {
                links = links.Where(s => s.TenTinTuc.Contains(searchString)); //lọc theo chuỗi tìm kiếm
            }
            if (page == null) page = 1;
            links = links.OrderBy(x => x.TenTinTuc);
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(links.ToPagedList(pageNumber, pageSize)); //trả về kết quả
        }

        // GET: TinTucs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinTuc tinTuc = db.TinTucs.Find(id);
            if (tinTuc == null)
            {
                return HttpNotFound();
            }
            return View(tinTuc);
        }

        // GET: TinTucs/Create
        public ActionResult Create()
        {
            ViewBag.MaDMTT = new SelectList(db.DMTinTucs, "MaDMTT", "TenDMTT");
            return View();
        }

        // POST: TinTucs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(HttpPostedFileBase AnhTinTuc, TinTuc tinTuc)
        {
            if (AnhTinTuc != null)
            {
                string p = Path.Combine(Server.MapPath("~/Anh"), Path.GetFileName(AnhTinTuc.FileName));
                AnhTinTuc.SaveAs(p);
                tinTuc.AnhTinTuc = AnhTinTuc.FileName;
            }
            if (ModelState.IsValid)
            {
                db.TinTucs.Add(tinTuc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tinTuc);
        }

        // GET: TinTucs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinTuc tinTuc = db.TinTucs.Find(id);
            if (tinTuc == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDMTT = new SelectList(db.DMTinTucs, "MaDMTT", "TenDMTT", tinTuc.MaDMTT);
            return View(tinTuc);
        }

        // POST: TinTucs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(HttpPostedFileBase AnhTinTuc, TinTuc tinTuc)
        {
            if (AnhTinTuc != null)
            {
                string p = Path.Combine(Server.MapPath("~/Anh"), Path.GetFileName(AnhTinTuc.FileName));
                AnhTinTuc.SaveAs(p);
                tinTuc.AnhTinTuc = AnhTinTuc.FileName;
            }
            if (ModelState.IsValid)
            {
                db.Entry(tinTuc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                db.TinTucs.Add(tinTuc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tinTuc);
        }

        // GET: TinTucs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinTuc tinTuc = db.TinTucs.Find(id);
            if (tinTuc == null)
            {
                return HttpNotFound();
            }
            return View(tinTuc);
        }

        // POST: TinTucs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TinTuc tinTuc = db.TinTucs.Find(id);
            db.TinTucs.Remove(tinTuc);
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
