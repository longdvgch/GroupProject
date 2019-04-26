using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Group_Project.Controllers
{
    public class StudentController : Controller
    {
        DBUtil db = new DBUtil();
        // GET: Student
        public ActionResult Index()
        {

            if (Session["User"] != null)
            {
                string name = (String)Session["User"];
                var getId = db.Accounts.SingleOrDefault(ac => ac.userName == name);
                int id = getId.userID;
                var sql = from d in db.SendContends
                          where d.contendStatus == "Canceled" && d.userID==id
                          select d;
                Session["canceled"] = sql.Count();

                var changedsql = from d in db.SendContends
                                 where d.contendStatus == "Require Change" && d.userID==id
                                 select d;
                Session["rchange"] = changedsql.Count();

                var acPostsql = from d in db.SendContends
                                 where d.contendStatus == "Done" && d.userID == id
                                 select d;
                Session["acPost"] = acPostsql.Count();
                return View();
            }
            else
            {
                return RedirectToAction("Error");
            }
        }


        public ActionResult UploadFile(HttpPostedFileBase file)
        {

            try
            {
                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/ImageFile"), _FileName);
                    file.SaveAs(_path);
                    Session["fileUpload"] = _FileName;
                }
                ViewBag.Message = "File Uploaded Successfully!!";

                return View("Index");
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View("Index");
            }
        }
        public ActionResult UploadWord(HttpPostedFileBase file)
        {

            try
            {
                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);

                    string _path = Path.Combine(Server.MapPath("~/docx"), _FileName);
                    file.SaveAs(_path);
                    Session["WordFileUpload"] = _FileName;
                }
                ViewBag.Message2 = "Word File Uploaded Successfully!!";

                return View("Index");
            }
            catch
            {
                ViewBag.Message2 = "File upload failed!!";
                return View("Index");
            }
        }
        public ActionResult sendContent(SendContend send)
        {
            String fileName = (String)Session["fileUpload"];
            String WordFileName = (String)Session["WordFileUpload"];
            String userName = (String)Session["User"];

            DateTime uploadDate = DateTime.Today;
            uploadDate.ToString("d");
            send.uploaddDate = uploadDate;

            var query = from q in db.Accounts
                        where q.userName == userName
                        select q.userID;

            send.userID = query.First();
            send.comment = "";
            send.contendStatus = "new";
            send.wordContend = "~/docx/" + WordFileName;
            send.imageContend = "~/ImageFile/" + fileName;
            db.SendContends.Add(send);
            db.SaveChanges();
            ViewBag.Success = "One Post has been added";
            return View("Success");
        }
        public ActionResult Success()
        {
            return View();
        }
        public ActionResult history()
        {
            string name = (String)Session["User"];
            var getId = db.Accounts.SingleOrDefault(ac => ac.userName == name);
            int id = getId.userID;
            var myUpload = db.SendContends.Where(c => c.userID == id).OrderByDescending(c => c.id);
            return View(myUpload);
        }
        public ActionResult Edit(int id)
        {
            var editContend = db.SendContends.SingleOrDefault(c => c.id == id);
            return View(editContend);
        }
        public ActionResult DoEdit(SendContend changeContend)
        {
            var Contend = db.SendContends.SingleOrDefault(c => c.id == changeContend.id);
            Contend.contendStatus = "Changed";
            HttpPostedFileBase file = Request.Files["file"];
            HttpPostedFileBase file1 = Request.Files["file1"];
            if (file.ContentLength > 0 && file1.ContentLength > 0)
            {
                string _FileName = Path.GetFileName(file.FileName);
                string _path = Path.Combine(Server.MapPath("~/ImageFile"), _FileName);
                file.SaveAs(_path);
                Contend.imageContend = "~/ImageFile/" + _FileName;

                string _FileName1 = Path.GetFileName(file1.FileName);
                string _path1 = Path.Combine(Server.MapPath("~/docx"), _FileName1);
                file1.SaveAs(_path1);
                Contend.wordContend = "~/docx/" + _FileName1;
                db.SaveChanges();
            }

            else if (file.ContentLength > 0)
            {
                string _FileName = Path.GetFileName(file.FileName);
                string _path = Path.Combine(Server.MapPath("~/ImageFile"), _FileName);
                file.SaveAs(_path);
                Contend.imageContend = "~/ImageFile/" + _FileName;
                db.SaveChanges();
            }
            else if ( file1.ContentLength > 0)
            {
                string _FileName1 = Path.GetFileName(file1.FileName);
                string _path1 = Path.Combine(Server.MapPath("~/docx"), _FileName1);
                file1.SaveAs(_path1);
                Contend.wordContend = "~/docx/" + _FileName1;
                db.SaveChanges();
            }
            else
            {
                db.SaveChanges();
            }

            return View("Success");
        }
        public ActionResult postRequireChange()
        {
            string name = (String)Session["User"];
            var getId = db.Accounts.SingleOrDefault(ac => ac.userName == name);
            int id = getId.userID;
            var rChange = db.SendContends.Where(c => c.userID == id && c.contendStatus=="Require Change").OrderByDescending(c => c.id);

            return View(rChange);
        }
        public ActionResult postCanceled()
        {
            string name = (String)Session["User"];
            var getId = db.Accounts.SingleOrDefault(ac => ac.userName == name);
            int id = getId.userID;
            var canceled = db.SendContends.Where(c => c.userID == id && c.contendStatus == "Canceled").OrderByDescending(c => c.id);

            return View(canceled);
        }
        public ActionResult acPost()
        {
            string name = (String)Session["User"];
            var getId = db.Accounts.SingleOrDefault(ac => ac.userName == name);
            int id = getId.userID;
            var acPost = db.SendContends.Where(c => c.userID == id && c.contendStatus == "Done").OrderByDescending(c => c.id);

            return View(acPost);
        }
        public ActionResult downloadFile(string Name)
        {
                string path = Server.MapPath("~/docx");
                string filename = Path.GetFileName(Name);
                string fullpath = Path.Combine(path, filename);
                return File(fullpath, path);                
        }
          
        
    }

}
