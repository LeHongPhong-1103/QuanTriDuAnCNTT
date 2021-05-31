using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLyGiaDung.Models;
using PagedList;

namespace QuanLyGiaDung.Controllers
{
    public class DMTinTucsController : Controller
    {
        private QuanLyGiaDung2Entities db = new QuanLyGiaDung2Entities();

        // GET: DMTinTucs
        public ActionResult Index(string searchString, int? page)
        {
            var links = from l in db.DMTinTucs // lấy toàn bộ liên kết
                        select l;

            if (!String.IsNullOrEmpty(searchString)) // kiểm tra chuỗi tìm kiếm có rỗng/null hay không
            {
                links = links.Where(s => s.TenDMTT.Contains(searchString)); //lọc theo chuỗi tìm kiếm
            }
            if (page == null) page = 1;
            links = links.OrderBy(x => x.TenDMTT);
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(links.ToPagedList(pageNumber, pageSize)); //trả về kết quả
        }

        // GET: DMTinTucs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DMTinTuc dMTinTuc = db.DMTinTucs.Find(id);
            if (dMTinTuc == null)
            {
                return HttpNotFound();
            }
            return View(dMTinTuc);
        }

        // GET: DMTinTucs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DMTinTucs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDMTT,TenDMTT")] DMTinTuc dMTinTuc)
        {
            if (ModelState.IsValid)
            {
                db.DMTinTucs.Add(dMTinTuc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dMTinTuc);
        }

        // GET: DMTinTucs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DMTinTuc dMTinTuc = db.DMTinTucs.Find(id);
            if (dMTinTuc == null)
            {
                return HttpNotFound();
            }
            return View(dMTinTuc);
        }

        // POST: DMTinTucs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDMTT,TenDMTT")] DMTinTuc dMTinTuc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dMTinTuc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dMTinTuc);
        }

        // GET: DMTinTucs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DMTinTuc dMTinTuc = db.DMTinTucs.Find(id);
            if (dMTinTuc == null)
            {
                return HttpNotFound();
            }
            return View(dMTinTuc);
        }

        // POST: DMTinTucs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DMTinTuc dMTinTuc = db.DMTinTucs.Find(id);
            db.DMTinTucs.Remove(dMTinTuc);
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
