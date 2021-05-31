using QuanLyGiaDung.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace QuanLyDienThoai.Controllers
{
    public class HomeController : Controller
    {
        private QuanLyGiaDung2Entities db = new QuanLyGiaDung2Entities();
        public ActionResult Index()
        {
            if (Session["idUser"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        //GET: Register

        public ActionResult Register()
        {
            return View();
        }

        //POST: Register
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Register(TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                var check = db.TaiKhoans.FirstOrDefault(s => s.Email == taiKhoan.Email);
                if (check == null)
                {
                    taiKhoan.Password = GetMD5(taiKhoan.Password);
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.TaiKhoans.Add(taiKhoan);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Email đã tồn tại";
                    return View();
                }
                var check2 = db.TaiKhoans.FirstOrDefault(s => s.FirstName == null);
                var check3 = db.TaiKhoans.FirstOrDefault(s => s.LastName == null);
                if (check2 == null || check3 == null)
                {
                    ViewBag.error = "Chưa nhập đủ thông tin !";
                    return View();
                }
            }
            return View();


        }

        public ActionResult Login()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {


                var f_password = GetMD5(password);
                var data = db.TaiKhoans.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["FullName"] = data.FirstOrDefault().FirstName + " " + data.FirstOrDefault().LastName;
                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["idUser"] = data.FirstOrDefault().idUser;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }

        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }



        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}