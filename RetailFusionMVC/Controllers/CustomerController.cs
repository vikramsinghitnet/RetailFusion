using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RetailFusionMVC.Models;
using RetailFusionMVC.Filters;
using System.Web.Security;

namespace RetailFusionMVC.Controllers
{
    [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]
    [Authorize]
    [SessionExpire]
    public class CustomerController : Controller
    {

        int StoreId = 0;
        clsDAL objDal = new clsDAL();
        // GET: /Customer/

        public ActionResult Index()
        {
            return View();
        }
        void GetStoreId()
        {
            if (Session["store"] != null)
            {
                StoreId = Convert.ToInt32(Session["store"]);
            }
            else
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
                RedirectToAction("About", "Public");
            }
        }
        public JsonResult CustomerCreditDetails(string filterType)
        {
            GetStoreId();
            List<clsCustomerCredit> listCustomerCredit = new List<clsCustomerCredit>();
            listCustomerCredit = objDal.GetCustomerCreditList(StoreId);
            
            if (filterType=="Pending")
            {
                listCustomerCredit = listCustomerCredit.Where(x => x.Status.Trim()== "Payment Pending").ToList();
            }

            var Result = new
            {
                page = 1,
                records = listCustomerCredit.Count(),
                rows = listCustomerCredit,
                total = 1
            };
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPedingCustomerCredit()
        {
            GetStoreId();
            List<string> listParty = new List<string>();

            listParty = objDal.GetPedingAmountList("Customer", StoreId);

            var Result = new
            {
                page = 1,
                records = listParty.Count(),
                rows = listParty,
                total = 1
            };
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public string GetCustomerPendingAmount()
        {
            GetStoreId();
            return objDal.GetCustomerPendingAmount(StoreId);
        }

        [HttpPost]
        public string SubmitPaidAmount(int AdvanceId, string CreditDate)
        {
            GetStoreId();
           
            if (objDal.SubmitPaidAmount(AdvanceId, "Customer", StoreId, creditDate:CreditDate) > 0)
            {
                return "Paid Amount Setteled.";
            }
            else
            {
                return "Error occured/Please enter valid Credit Id !";
            }
        }

    }
}
