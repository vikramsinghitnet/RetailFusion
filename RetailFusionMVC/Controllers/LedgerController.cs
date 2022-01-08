using RetailFusionMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RetailFusionMVC.Controllers
{
    public class LedgerController : Controller
    {
        //
        // GET: /Ledger/
        clsLedgerDAL objLedgerDal = new clsLedgerDAL();
        clsDAL objDal = new clsDAL();
        int StoreId = 0;
        string user = "";

        public ActionResult Index()
        {
            return View();
        }

        void GetStoreId()
        {
            if (Session["store"] != null)
            {
                StoreId = Convert.ToInt32(Session["store"]);
                user = Session["user"].ToString();
            }
            else
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
                RedirectToAction("About", "Public");
            }
        }

        [HttpPost]
        public string DeleteTodayLedger(string PartyId, string LedgerId)
        {
            if (Convert.ToInt32(PartyId) > 0 && objLedgerDal.DeleteTodayLedger(Convert.ToInt32(PartyId), 0) > 0)
            {
                return "Today's ledger deleted successfully.";
            }
            else if (Convert.ToInt32(LedgerId) > 0 && objLedgerDal.DeleteTodayLedger(0, Convert.ToInt32(LedgerId)) > 0)
            {
                return "Ledger deleted successfully.";
            }
            else
            {
                return "No ledger deleted !";
            }
        }

        [HttpGet]
        public JsonResult GetLedger(string PartyId,string frmDate,string toDate)
        {
            var legder = objLedgerDal.GetLedgerbyParty(PartyId,frmDate,toDate);
            var Result = new
            {
                page = 1,
                records = legder.Count(),
                rows = legder,
                total = 1
            };
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetLedgerSummery()
        {
            GetStoreId();

            var parties = objDal.GetPartyList(StoreId);
            List<LedgerSummary> ledgerSummary = new List<LedgerSummary>();

            foreach (clsParty party in parties)
            {
                var ledger = objLedgerDal.GetLedgerbyParty(party.PartyId.ToString());

                if (ledger != null && ledger.Count() != 0)
                {
                    clsLedger objledger = ledger.Last();

                    LedgerSummary summary = new LedgerSummary();
                    summary.ClosingBalance = Convert.ToDecimal(objledger.ClosingBalance);
                    summary.PartyName = party.PartyName;
                    ledgerSummary.Add(summary);
                }
            }


            var Result = new
            {
                page = 1,
                records = ledgerSummary.Count(),
                rows = ledgerSummary.OrderByDescending(x => x.ClosingBalance),
                total = 1
            };
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string SaveLedger(string PartyId, string InvoiceNo, string InvoiceAmount, string Remarks, string DrOrCr, string Date, string Brand, string Branch)
        {
            GetStoreId();
            try
            {
                if (!string.IsNullOrEmpty(InvoiceAmount) && !string.IsNullOrEmpty(InvoiceNo)
                 && !string.IsNullOrEmpty(PartyId) && !string.IsNullOrEmpty(Date))
                {
                    if (0 < objLedgerDal.SaveLedger(PartyId, InvoiceAmount, InvoiceNo, Remarks, DrOrCr, Date, Brand, Branch, user))
                        return "Thank you " + User.Identity.Name + ". Legder saved.";
                    else
                        return "Error Occured";
                }
                else
                {
                    return "Please complete the form.";
                }
            }
            catch (Exception ex)
            {
                return "Error Occurred !!  " + ex.Message;
            }
        }

    }
}
