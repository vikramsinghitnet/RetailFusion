using RetailFusionMVC.Models;
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
        clsAccountDal objDal = new clsAccountDal();
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
            clsUser user = objDal.GetUserLogin(Convert.ToInt32(Store), UserName, Password);
            if (user.Id !=0)
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
