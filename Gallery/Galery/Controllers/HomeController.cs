using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Galery.Models;
using System.Data.Entity;

namespace Galery.Controllers
{
    public class HomeController : Controller
    {
        PictureContent db = new PictureContent();
        Wrapper wr;
        static string RegSucess;       
        protected override void Dispose(bool disposing)

        {
          
            db.Dispose();
            base.Dispose(false);
        }
        [HttpGet]
        public ActionResult Index(int Id=0)
       {

            ViewBag.success = RegSucess;
            wr = new Wrapper { Pictures = db.Pictures.ToList(), Users = db.Users.ToList(), IdUser = Id };
            return View(wr);
        }
        [HttpGet]
        public ActionResult PictureInform(int Id)
        {
            try
            {
                wr = new Wrapper { Pictures = db.Pictures.ToList(), Users = db.Users.ToList(), IdPict = Id };
                return View(wr);
            }
            catch
            {
                return HttpNotFound();
            }
        }
        [HttpPost]
        public ActionResult AddPicture(Picture picture,HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                picture.Data = new byte[image.ContentLength];
                picture.FileName = image.FileName;
                image.InputStream.Read(picture.Data, 0, image.ContentLength);
                int id = Convert.ToInt32(Session["Id"]);
                picture.User = db.Users.FirstOrDefault(p => p.Id == id);
                db.Pictures.Add(picture);
                db.SaveChanges();
              
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Registration()
        {
            wr = wr = new Wrapper { Pictures = db.Pictures.ToList(), Users = db.Users.ToList()};
            return View(wr);
        }
        [HttpPost]
        public ActionResult AddUser(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();

                Session["Id"] = user.Id;
                Session["Login"] = user.Login;
                Session["Password"] = user.Password;
                Session["Name"] = user.Name;
                Session["Surname"] = user.Surname;
                RegSucess = null;
              
                return RedirectToAction("Index");//View(user);
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult PartialRegistration()
        {           
            return PartialView();
        }
        [HttpPost]
        public ActionResult EnterAsUser(User user)
        {
            User us = db.Users.FirstOrDefault(u => u.Login == user.Login && u.Password == user.Password);
            if (us != null) {
                Session["Id"] = us.Id;
                Session["Login"] = user.Login;
                Session["Password"] = user.Password;
                Session["Name"] = us.Name;
                Session["Surname"] = us.Surname;
                RegSucess = null;

                RegSucess = null;
            }
            else
            {            
                RegSucess= "You don't registrated!";
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult ExitAsUser()
        {
            Session["Id"] = null;
            Session["Login"] = null;
            Session["Password"] = null;
            Session["Name"] = null;
            Session["Surname"] = null;
            RegSucess = null;
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult FormForPicture()
        {
            wr = new Wrapper { Pictures = db.Pictures.ToList(), Users = db.Users.ToList() };

            return View(wr);
        }             
        [HttpPost]
        public ActionResult DeletePicture(int Id)
        {
            Picture pc= db.Pictures.FirstOrDefault(p => p.Id == Id);
            if (pc != null)
            {
                db.Pictures.Remove(pc);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult FormForEditPicture(int Id)
        {
            Picture picture = db.Pictures.FirstOrDefault(p => p.Id == Id);
            wr = new Wrapper { Pictures = db.Pictures.ToList(), Users = db.Users.ToList(), Picture = picture };
            return View(wr);
        }
        [HttpPost]
        public ActionResult EditPicture(Picture newPict)
        {           
           
            var pict = db.Pictures.FirstOrDefault(p => p.Id == newPict.Id);
            pict.Price = newPict.Price;
            pict.Title = newPict.Title;
            pict.Direction = newPict.Direction;
            db.Entry(pict).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult SearchPicture(string words)
        {
            ViewBag.request = words;
            wr = new Wrapper { Pictures = db.Pictures.ToList(), Users = db.Users.ToList(), Words=words };
            return View(wr);
        }
        public ActionResult PicturesOfAutor(int Id)
        {
            wr = new Wrapper { Pictures = db.Pictures.ToList(), Users = db.Users.ToList()};
            return View(wr);
        }
        public ActionResult PartialListOfPicture()
        {
            return PartialView();
        }
        public ActionResult Toolbar()
        {
            wr = new Wrapper { Pictures = db.Pictures.ToList(), Users = db.Users.ToList(), IdUser = 0};
            return PartialView(wr);
        }
    }

}