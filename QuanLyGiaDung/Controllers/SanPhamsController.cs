using System;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLyGiaDung.Models;
using PagedList;
using System.Linq.Dynamic; // nhúng vào tập tin 
namespace QuanLyGiaDung.Controllers
{
    public class SanPhamsController : Controller
    {
        private QuanLyGiaDung2Entities db = new QuanLyGiaDung2Entities();

        // GET: SanPhams
        public ActionResult Index(string searchString, string sortOrder,  int? page, int MaDMSP = 0)
        {

            var links = from l in db.SanPhams // lấy toàn bộ liên kết
                        select l;

            var categories = from c in db.DMSPs select c;
            ViewBag.MaDMSP = new SelectList(categories, "MaDMSP", "TenDMSP"); // danh sách Category
            links = links = db.SanPhams.Include(l => l.DMSP);
            //4. Tìm kiếm theo CategoryID
            if (MaDMSP != 0)
            {
                links = links.Where(x => x.MaDMSP == MaDMSP);
            }

            if (!String.IsNullOrEmpty(searchString)) // kiểm tra chuỗi tìm kiếm có rỗng/null hay không
            {
                links = links.Where(s => s.TenSP.Contains(searchString)); //lọc theo chuỗi tìm kiếm
            }

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            switch (sortOrder)
            {
                // 3.1 Nếu biến sortOrder sắp giảm thì sắp giảm theo LinkName
                case "name_desc":
                    links = links.OrderByDescending(s => s.TenSP);
                    break;

                // 3.2 Mặc định thì sẽ sắp tăng
                default:
                    links = links.OrderBy(s => s.TenSP);
                    break;
            }


            if (page == null) page = 1;
            links = links.OrderBy(x => x.MaSP);
            int pageSize = 6;
            int pageNumber = (page ?? 1);

            return View(links.ToPagedList(pageNumber, pageSize)); //trả về kết quả
        }

        // GET: SanPhams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.MaDMSP = new SelectList(db.DMSPs, "MaDMSP", "TenDMSP");
            return View();
        }

        // POST: SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(HttpPostedFileBase AnhSP, SanPham sanpham)
        {
            if (AnhSP != null)
            {
                string p = Path.Combine(Server.MapPath("~/Anh"), Path.GetFileName(AnhSP.FileName));
                AnhSP.SaveAs(p);
                sanpham.AnhSP = AnhSP.FileName;
            }
            if (ModelState.IsValid)
            {
                db.SanPhams.Add(sanpham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sanpham);
        }

        // GET: SanPhams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDMSP = new SelectList(db.DMSPs, "MaDMSP", "TenDMSP", sanPham.MaDMSP);
            return View(sanPham);
        }

        // POST: SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(HttpPostedFileBase AnhSP, SanPham sanPham)
        {
            if (AnhSP != null)
            {
                string path = Path.Combine(Server.MapPath("~/Anh"), Path.GetFileName(AnhSP.FileName));
                AnhSP.SaveAs(path);
                sanPham.AnhSP = AnhSP.FileName;
            }
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                db.SanPhams.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaDMSP = new SelectList(db.DMSPs, "MaDMSP", "TenDMSP", sanPham.MaDMSP);
            return View(sanPham);
        }

        // GET: SanPhams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
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
