using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Group_Project.Controllers
{
    public class HomeController : Controller
    {
        DBUtil db = new DBUtil();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Client()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult DoLogin()
        {
            String userName = Request["username"];
            String Pass = Request["password"];
            var query = from q in db.Accounts
                        where q.userName == userName
                        && q.password == Pass
                        select q;
            Session["User"] = userName;

            if (query.Any())
            {
                var a = query.First();

                if (a.role == "student")
                {
                    Session["User"] = userName;
                    return RedirectToAction("Index", "Student");
                }
                else if (a.role == "Coodinator")
                {
                    Session["User"] = userName;
                    return RedirectToAction("Index", "MarkettingCoodinator");
                }
                else
                {
                    Session["User"] = userName;
                    return RedirectToAction("Index", "MarkettingManager");
                }
            }
            else
            {
                return RedirectToAction("LoginFailed");
            }
        }
        public ActionResult LoginFailed()
        {
            return View();
        }

    }
}