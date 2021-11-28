using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using RetailFusionMVC.Models;
using System.Web.Security;
using RetailFusionMVC.Filters;

namespace RetailFusionMVC.Controllers
{
    [Authorize]
    [SessionExpire]
    [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]
    public class ReportController : Controller
    {
        //
        // GET: /Report/
        clsDAL objDal = new clsDAL();
        clsLedgerDAL objLedgerDal = new clsLedgerDAL();
        int StoreId = 0;

        public ActionResult Index()
        {
            return Content("Hi there!");
            //return View();
        }

        public PartialViewResult CreatePartialView()
        {
            string ActionType = Request.QueryString["ViewType"];
            if (ActionType == "Advance")
            {
                return PartialView("AdvanceDetail");
            }
            else if (ActionType == "Deposit")
            {
                return PartialView("Deposit");
            }
            else if (ActionType == "PartyPayment")
            {
                return PartialView("PartyPayment");
            }
            else
            {
                return PartialView("ExpenseDetail");
            }
        }
        public ActionResult Reports()
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

        public string GetPedingCustomerCredit()
        {
            GetStoreId();
            return objDal.GetBillPendingAmount(StoreId);
        }

        public string GetSessionUser()
        {
            GetStoreId();
            return Session["user"].ToString();
        }

        public JsonResult GetPedingBills()
        {

            List<string> listParty = new List<string>(); ;
            GetStoreId();
            listParty = objDal.GetPedingAmountList("Bill", StoreId);
            listParty.Sort();
            var Result = new
            {
                page = 1,
                records = listParty.Count(),
                rows = listParty,
                total = 1
            };
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string SubmitPaidAmount(int AdvanceId, decimal PartialPayment)
        {
            GetStoreId();
            if (objDal.SubmitPaidAmount(AdvanceId, "Bill", StoreId, PartialPayment:PartialPayment) > 0)
            {
                return "Paid Amount Setteled.";
            }
            else
            {
                return "Error occured/Please enter valid Credit Id !";
            }
        }

        [HttpPost]
        public string AddInvoice(string InvoiceNo, string InvoiceDate, string InvoiceAmount, string Vat, string StockQty, string RecievedDate, string PartyName, string CashDiscount, string FrieghtChgs, string TotalInvoiceAmount, string Remarks)
        {
            GetStoreId();
            try
            {
                if (!string.IsNullOrEmpty(InvoiceAmount) && !string.IsNullOrEmpty(InvoiceDate) && !string.IsNullOrEmpty(InvoiceNo)
                    && !string.IsNullOrEmpty(Vat) && !string.IsNullOrEmpty(StockQty) && !string.IsNullOrEmpty(RecievedDate) &&
                    !string.IsNullOrEmpty(PartyName) && !string.IsNullOrEmpty(CashDiscount) && !string.IsNullOrEmpty(FrieghtChgs)
                    && !string.IsNullOrEmpty(TotalInvoiceAmount))
                {
                    if (0 < objDal.AddInvoice(InvoiceNo, InvoiceDate, InvoiceAmount, Vat, StockQty, RecievedDate, PartyName, CashDiscount, FrieghtChgs, TotalInvoiceAmount, StoreId, Remarks))
                        return "Thank you " + User.Identity.Name + ". Invoice Added.";
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

        [HttpPost]
        public string SaveLedger(string PartyId, string InvoiceNo, string InvoiceAmount, string Remarks, string DrOrCr, string Date, string Brand,string Branch)
        {
            GetStoreId();
            try
            {
                if (!string.IsNullOrEmpty(InvoiceAmount)  && !string.IsNullOrEmpty(InvoiceNo)
                 && !string.IsNullOrEmpty(PartyId) && !string.IsNullOrEmpty(Date))
                {
                    if (0 < objLedgerDal.SaveLedger(PartyId, InvoiceAmount, InvoiceNo, Remarks, DrOrCr, Date, Brand,Branch))
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
        public PartialViewResult PartPaymentView()
        {
            return PartialView("PartPayment");
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

        public JsonResult GetPartPaymentDetails(string BillId)
        {
            var lstPartPayments = objDal.DLGetPartPaymentDetails(BillId);

            var Result = new
            {
                page = 1,
                records = lstPartPayments.Count(),
                rows = lstPartPayments,
                total = 1
            };

            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetLedger(string PartyId)
        {
            var legder = objLedgerDal.GetLedgerbyParty(PartyId);
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

            var parties=objDal.GetPartyList(StoreId);
            List<LedgerSummary> ledgerSummary = new List<LedgerSummary>();

            foreach (clsParty party in parties)
            {
                var ledger= objLedgerDal.GetLedgerbyParty(party.PartyId.ToString());
                
                if (ledger != null && ledger.Count() != 0)
                {
                    clsLedger objledger = ledger.Last();

                    LedgerSummary summary = new LedgerSummary();
                    summary.ClosingBalance = Convert.ToDecimal( objledger.ClosingBalance);
                    summary.PartyName = party.PartyName;
                    ledgerSummary.Add(summary);
                }
            }

            
            var Result = new
            {
                page = 1,
                records = ledgerSummary.Count(),
                rows = ledgerSummary.OrderByDescending(x=>x.ClosingBalance),
                total = 1
            };
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetInvoices(string ReportType, string partyId)
        {
            GetStoreId();

            var listInvoice = objDal.GetInvoiceList(ReportType, StoreId, partyId);
            var Result = new
            {
                page = 1,
                records = listInvoice.Count(),
                rows = listInvoice,
                total = 1
            };
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetExpenseSummary()
        {
            GetStoreId();
            var listExpenseSummary = objDal.GetExpenseSummary(StoreId);
            var Result = new
            {
                page = 1,
                records = listExpenseSummary.Count(),
                rows = listExpenseSummary,
                total = 1
            };

            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPendingAmountSummary()
        {
            GetStoreId();
            var listPendingAmountSummary = objDal.GetPendingAmountSummary(StoreId);
            var Result = new
            {
                page = 1,
                records = listPendingAmountSummary.Count(),
                rows = listPendingAmountSummary,
                total = 1
            };

            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMonthSummaryDL()
        {
            GetStoreId();
            var listEOD = objDal.GetMonthSummaryDL(StoreId, "M");
            var Result = new
            {
                page = 1,
                records = listEOD.Count(),
                rows = listEOD,
                total = 1
            };

            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEODDetail()
        {

            GetStoreId();

            var listEOD = objDal.GetEODList(StoreId + 100, "");

            //JqGridData Result = new JqGridData()
            //     {
            //         page = 1,
            //         records = 0,
            //         rows = new List<clsEODDetails>(),
            //         total = 1
            //     };

            //Result.rows = listEOD;
            //Result.records = listEOD.Count();

            var Result = new
            {
                page = 1,
                records = listEOD.Count(),
                rows = listEOD,
                total = 1
            };

            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEODMonths()
        {
            GetStoreId();
            DataSet dsMonthYear = objDal.GetEODMonths(StoreId);

            dsMonthYear = objDal.GetEODMonths(StoreId);

            var monthYear = dsMonthYear.Tables[0].AsEnumerable().
                Select(datarow => datarow.Field<string>("MonthYear")).ToList();

            var Result = new
            {
                page = 1,
                records = monthYear.Count(),
                rows = monthYear,
                total = 1
            };

            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult StockMgmt()
        {
            return View();
        }

        public ActionResult Purchase()
        {
            return View();
        }

        public ActionResult Ledger()
        {
            return View();
        }

        public string SubmitReturn(string ReturnType, string Party, string Quantity, string Value, string Remark, string CourierName, string TrackingId)
        {
            GetStoreId();
            if (objDal.SumbitReturn(ReturnType, Party, Quantity, Value, Remark, StoreId, CourierName, TrackingId) > 0)
            {
                return "Return submitted .";
            }
            else
            {
                return "Error occured !!";
            }
        }

        public JsonResult GetStockReturnList()
        {
            GetStoreId();

            var listInvoice = objDal.GetStockReturnList(StoreId);
            var Result = new
            {
                page = 1,
                records = listInvoice.Count(),
                rows = listInvoice,
                total = 1
            };
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDisptachIds()
        {
            GetStoreId();
            List<string> listParty = new List<string>();

            listParty = objDal.GetDisptachIds(StoreId);

            var Result = new
            {
                page = 1,
                records = listParty.Count(),
                rows = listParty,
                total = 1
            };
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string SubmitDispatchStatus(int DispatchId, string DispatchStatus)
        {
            GetStoreId();
            if (objDal.UpdateDispatchStatus(DispatchId, DispatchStatus, StoreId) > 0)
            {
                return "Dispatch Status Updated.";
            }
            else
            {
                return "Error occured/Please enter valid Credit Id !";
            }
        }

        public JsonResult GetMonthExpenses(string MonthYear)
        {

            GetStoreId();

            var listExpenses = objDal.GetMonthExpenses(StoreId, MonthYear);

            var Result = new
            {
                page = 1,
                records = listExpenses.Count(),
                rows = listExpenses,
                total = 1
            };

            return Json(Result, JsonRequestBehavior.AllowGet);
        }
    }
}
