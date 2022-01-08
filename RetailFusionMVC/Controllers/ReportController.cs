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
        int StoreId = 0;
        string user = "";
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
                user = Session["user"].ToString();
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
            if (objDal.SubmitPaidAmount(AdvanceId, "Bill", StoreId, PartialPayment: PartialPayment) > 0)
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

        public PartialViewResult PartPaymentView()
        {
            return PartialView("PartPayment");
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

        public JsonResult GetMonthExpenses(string MonthYear, string FromDate, string ToDate)
        {

            GetStoreId();

            var listExpenses = objDal.GetMonthExpenses(StoreId, MonthYear, FromDate, ToDate);

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
