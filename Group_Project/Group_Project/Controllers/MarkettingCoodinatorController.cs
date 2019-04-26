using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Group_Project.Controllers
{
    public class MarkettingCoodinatorController : Controller
    {
        // GET: MarkettingCoodinator

        DBUtil db = new DBUtil();
        public ActionResult Index()
        {
            if (Session["User"] != null)
            {
                var sql = from d in db.SendContends
                          where d.contendStatus == "New"
                          select d;
                Session["New"] = sql.Count();

                var changedsql = from d in db.SendContends
                          where d.contendStatus == "Changed"
                          select d;
                Session["changed"] = changedsql.Count();

                var listAll = db.SendContends.OrderByDescending(c => c.id);
                return View(listAll.ToList());
            }
            else
            {
                return RedirectToAction("Error");
            }


        }
     
        public ActionResult Error()
        {
            return View();
        }
        public ActionResult Success()
        {
            return View();
        }
        public ActionResult Edit(int id)
        {
            var editContend = db.SendContends.SingleOrDefault(c => c.id == id);
            return View(editContend);
        }
        public ActionResult DoEdit(SendContend changeContend)
        {
            var Contend = db.SendContends.SingleOrDefault(c => c.id == changeContend.id);
            String status = Request["chooseStatus"];

            Contend.comment = changeContend.comment;
            Contend.contendStatus = status;

            db.SaveChanges();
            return View("Success");
        }
        public ActionResult postChanged()
        {
            var changed = db.SendContends.Where(c => c.contendStatus == "Changed").OrderByDescending(c => c.id);

            return View(changed);
        }
        public ActionResult NewNotice()
        {
            var newP = db.SendContends.Where(c => c.contendStatus == "New").OrderByDescending(c => c.id);

            return View(newP);
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