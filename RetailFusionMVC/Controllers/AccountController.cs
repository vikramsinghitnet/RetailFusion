using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RetailFusionMVC.Controllers
{
    public class AccountController : Controller
    {        

        public ActionResult Index()
        {
            return View();
        }
        //Test
        //spUpdatedRents 35000,2,2020
        // Store 2 - FF Men
        // Store 4 - FF Women
        // Store 1 - FF Singrauli
        // Store 3 - Upolis

        public string Logon(string UserName, string Password, string Store)
        {
            if (UserName.ToLower() == "abhishek" && Password.ToLower() == "abhishek1" && (Store == "2" || Store == "4"))
            {
                FormsAuthentication.SetAuthCookie(UserName.ToUpper(), false);
                //return RedirectToAction("EOD", "Home");
            }
            else if (UserName.ToLower() == "gaurav" && Password.ToLower() == "gaurav1" && (Store == "2" || Store == "4"))
            {
                FormsAuthentication.SetAuthCookie(UserName.ToUpper(), false);
                //return RedirectToAction("EOD", "Home");
            }
            else if (UserName.ToLower() == "anup" && Password.ToLower() == "anup1" && Store == "2")
            {
                FormsAuthentication.SetAuthCookie(UserName.ToUpper(), false);
                //return RedirectToAction("EOD", "Home");
            }
            else if (UserName.ToLower() == "ff2" && Password.ToLower() == "ffsgrl" && Store == "1")
            {
                FormsAuthentication.SetAuthCookie(UserName.ToUpper(), false);
                //return RedirectToAction("EOD", "Home");
            }
            else if (UserName.ToLower() == "sarovar" && Password.ToLower() == "sarovar1" && Store == "3")
            {
                FormsAuthentication.SetAuthCookie(UserName.ToUpper(), false);
                //return RedirectToAction("EOD", "Home");
            }
            else if (UserName.ToLower() == "test" && Password.ToLower() == "test1" && Store == "5")
            {
                FormsAuthentication.SetAuthCookie(UserName.ToUpper(), false);
                //return RedirectToAction("EOD", "Home");
            }
            else if (UserName.ToLower() == "admin" && Password.ToLower() == "admin")
            {
                FormsAuthentication.SetAuthCookie(UserName.ToUpper(), false);
            }
            else
            {
                return "false";
            }

            Session["store"] = Store;
            Session["user"] = UserName;
            return "true";
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("About", "Public");
        }

    }
}
