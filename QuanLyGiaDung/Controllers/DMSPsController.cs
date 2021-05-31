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
    public class DMSPsController : Controller
    {
        private QuanLyGiaDung2Entities db = new QuanLyGiaDung2Entities();

        // GET: DMSPs
        public ActionResult Index(string searchString, int? page)
        {
            var links = from l in db.DMSPs // lấy toàn bộ liên kết
                        select l;

            if (!String.IsNullOrEmpty(searchString)) // kiểm tra chuỗi tìm kiếm có rỗng/null hay không
            {
                links = links.Where(s => s.TenDMSP.Contains(searchString)); //lọc theo chuỗi tìm kiếm
            }
            if (page == null) page = 1;
            links = links.OrderBy(x => x.TenDMSP);
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(links.ToPagedList(pageNumber, pageSize)); //trả về kết quả
        }

        // GET: DMSPs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DMSP dMSP = db.DMSPs.Find(id);
            if (dMSP == null)
            {
                return HttpNotFound();
            }
            return View(dMSP);
        }

        // GET: DMSPs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DMSPs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDMSP,TenDMSP")] DMSP dMSP)
        {
            if (ModelState.IsValid)
            {
                db.DMSPs.Add(dMSP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dMSP);
        }

        // GET: DMSPs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DMSP dMSP = db.DMSPs.Find(id);
            if (dMSP == null)
            {
                return HttpNotFound();
            }
            return View(dMSP);
        }

        // POST: DMSPs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDMSP,TenDMSP")] DMSP dMSP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dMSP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dMSP);
        }

        // GET: DMSPs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DMSP dMSP = db.DMSPs.Find(id);
            if (dMSP == null)
            {
                return HttpNotFound();
            }
            return View(dMSP);
        }

        // POST: DMSPs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DMSP dMSP = db.DMSPs.Find(id);
            db.DMSPs.Remove(dMSP);
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
