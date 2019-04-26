using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO.Compression;

namespace Group_Project.Controllers
{
    public class MarkettingManagerController : Controller
    {
        DBUtil db = new DBUtil();
        // GET: MarkettingManager
        public ActionResult Index()
        {
            if (Session["User"] != null)
            {
                var listAll = db.SendContends.OrderByDescending(c => c.id);
                return View(listAll.ToList());
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        public ActionResult viewReport()
        {
            var sqlTotal = from d in db.SendContends
                      select d;
            Session["Total"] = sqlTotal.Count();

            var sqlNew = from d in db.SendContends
                          where d.contendStatus == "new"
                          select d;
            Session["New"] = sqlNew.Count();

            var sqlDone = from d in db.SendContends
                          where d.contendStatus == "Done"
                          select d;
            Session["Accepted"] = sqlDone.Count();


            var sqlCancel = from d in db.SendContends
                          where d.contendStatus == "Canceled"
                          select d;
            Session["Cancel"] = sqlCancel.Count();

            var sqlRC = from d in db.SendContends
                          where d.contendStatus == "Require Change"
                        select d;
            Session["Rc"] = sqlRC.Count();

            var sqlChange = from d in db.SendContends
                          where d.contendStatus == "Changed"
                          select d;
            Session["Change"] = sqlChange.Count();
            Session["totalByDate"] = 0;
            Session["newByDate"] = 0;
            Session["acceptByDate"] = 0;
            Session["cancelByDate"] = 0;
            Session["rcByDate"] = 0;
            Session["changedByDate"] = 0;

            return View();
        }



        public ActionResult Search()
        {
            try
            {
                DateTime stDate = Convert.ToDateTime(Request["txtStartDate"]);
                DateTime endDate = Convert.ToDateTime(Request["txtEndDate"]);
                var query1 = from q in db.SendContends
                             where q.uploaddDate >= stDate
                             && q.uploaddDate <= endDate
                             select q;
                var query2 = from q in db.SendContends
                             where q.uploaddDate >= stDate
                             && q.uploaddDate <= endDate
                             && q.contendStatus == "new"
                             select q;
                var query3 = from q in db.SendContends
                             where q.uploaddDate >= stDate
                             && q.uploaddDate <= endDate
                             && q.contendStatus == "Done"
                             select q;
                var query4 = from q in db.SendContends
                             where q.uploaddDate >= stDate
                             && q.uploaddDate <= endDate
                             && q.contendStatus == "Canceled"
                             select q;
                var query5 = from q in db.SendContends
                             where q.uploaddDate >= stDate
                             && q.uploaddDate <= endDate
                             && q.contendStatus == "Require Change"
                             select q;
                var query6 = from q in db.SendContends
                             where q.uploaddDate >= stDate
                             && q.uploaddDate <= endDate
                             && q.contendStatus == "Changed"
                             select q;

                if (query1.Any()||query2.Any()|| query3.Any()|| query4.Any()|| query5.Any()|| query6.Any())
                {
                    Session["txtStartDate"] = stDate;
                    Session["txtEndDate"] = endDate;
                    Session["totalByDate"] = query1.Count();
                    Session["newByDate"] = query2.Count();
                    Session["acceptByDate"] = query3.Count();
                    Session["cancelByDate"] = query4.Count();
                    Session["rcByDate"] = query5.Count();
                    Session["changedByDate"] = query6.Count();
                    return View("viewReport");
                }
                else
                {
                    return View("viewReport");
                }
            }
            catch (FormatException ex)
            {
                ViewBag.Messgae = ex.Message;
                return View("viewReport");
            }
        }
        //public void ZipFiles()
        //{

        //    string zipCreatePath = System.Web.Hosting.HostingEnvironment.MapPath("/myZip.zip");

        //    using (ZipArchive archive = ZipFile.Open(zipCreatePath, ZipArchiveMode.Create))
        //    {
        //        List<string> files = new List<string>();
        //        files.Add("wallhaven-103536.jpg");
        //        files.Add("wallhaven-161383.jpg");
        //        files.Add("wallhaven-30764.jpg");

        //        foreach (string file in files)
        //        {
        //            string filePath = System.Web.Hosting.HostingEnvironment.MapPath(string.Format("/images/{0}", file));

        //            archive.CreateEntryFromFile(filePath, file);
        //        }
        //    }
        //}
    }
}