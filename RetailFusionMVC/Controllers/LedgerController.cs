using RetailFusionMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RetailFusionMVC.Controllers
{
    public class LedgerController : Controller
    {
        //
        // GET: /Ledger/
        clsLedgerDAL objLedgerDal = new clsLedgerDAL();

        public ActionResult Index()
        {
            return View();
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

    }
}
