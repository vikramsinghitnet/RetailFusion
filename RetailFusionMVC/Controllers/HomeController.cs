using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RetailFusionMVC.Models;
using System.Data;
using System.Web.Security;
using RetailFusionMVC.Filters;

namespace RetailFusionMVC.Controllers
{
    [Authorize]
    [SessionExpire]
    [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]
    public class HomeController : Controller
    {

        clsDAL objDal = new clsDAL();
        int StoreId = 0;
        string user = "";
        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to Fashion Fusion World";
            return View();
        }

        public ActionResult Reports()
        {
            return View();
        }

        public ActionResult EOD()
        {
            GetStoreId();
            return View();
        }

        public ActionResult PartyMaster()
        {
            ViewData["Message"] = "Enter Party Details:";
            return View();
        }

        public ActionResult Employeedetail()
        {
            ViewData["Message"] = "Under Construction";
            return View();
        }

        public ActionResult StoreSetup()
        {
            return View();
        }

        public ActionResult PayRoll()
        {
            return View();
        }

        bool isValidSubmissionEod(string EodDate)
        {
            List<clsEODDetails> TodayEOD = objDal.GetDayBeforeEODDetail(StoreId, EodDate);
            return true;
        }

        [HttpPost]
        public string DeleteTransaction(string Type, string Id)
        {
            //if (Convert.ToInt32(PartyId) > 0 && objLedgerDal.DeleteTodayLedger(Convert.ToInt32(PartyId), 0) > 0)
            //{
            //    return "Today's ledger deleted successfully.";
            //}
            //else if (Convert.ToInt32(LedgerId) > 0 && objLedgerDal.DeleteTodayLedger(0, Convert.ToInt32(LedgerId)) > 0)
            //{
            //    return "Ledger deleted successfully.";
            //}
            //else
            //{
            //    return "No ledger deleted !";
            //}
            return "";
        }

        [HttpPost]
        public string DeleteTodayEOD(string EodDate)
        {
            GetStoreId();

            if (objDal.DeleteTodayEOD(StoreId, EodDate) >= 0)
            {
                return "1";
            }
            else
            {
                return "-1";
            }
        }

        [HttpPost]
        public string SubmitEODDetail(string TotalSale, string TotalDiscount, string TotalCardPayment, string TotalCounterCash, string EodDate)
        {
            GetStoreId();
            if (string.IsNullOrEmpty(EodDate))
            {
                EodDate = DateTime.Now.ToString();
            }
            List<clsEODDetails> dayBeforeEOD = objDal.GetDayBeforeEODDetail(StoreId);
            List<clsEODDetails> TodayEOD = objDal.GetDayBeforeEODDetail(StoreId, EodDate);
            List<clsAdvance> todayAdvance = objDal.GetAdvanceList(StoreId, EodDate);
            List<clsDeposit> todayDeposit = objDal.GetDepositList(StoreId, EodDate);
            List<clsPartyPayment> todayPartyPayment = objDal.GetPartyPaymentList(StoreId, EodDate);
            List<clsExpense> todayExpense = objDal.GeExpanseList(StoreId, EodDate,false);

            decimal vtodayExpense = 0;
            decimal vtodayAdvance = 0;
            decimal vtodayDeposit = 0;
            decimal vtodayPartyPayment = 0;
            decimal vshortageAmount = 0;

            if (TodayEOD.Count == 0)
            {
                if (todayPartyPayment.Count > 0)
                {
                    vtodayPartyPayment = Convert.ToDecimal(todayPartyPayment.Sum(x => Convert.ToDecimal(x.PaymentAmount)));
                }
                if (todayExpense.Count > 0)
                {
                    vtodayExpense = Convert.ToDecimal(todayExpense.Sum(x => Convert.ToDecimal(x.ExpenseAmount)));
                }
                if (todayAdvance.Count > 0)
                {
                    vtodayAdvance = Convert.ToDecimal(todayAdvance.Sum(x => Convert.ToDecimal(x.AdvanceAmount)));
                }
                if (todayDeposit.Count > 0)
                {
                    vtodayDeposit = Convert.ToDecimal(todayDeposit.Sum(x => Convert.ToDecimal(x.DepositAmount)));
                }
                if (dayBeforeEOD.Count > 0)
                {
                    if (!String.IsNullOrEmpty(TotalSale) && !String.IsNullOrEmpty(TotalDiscount) && !String.IsNullOrEmpty(TotalSale) && !String.IsNullOrEmpty(TotalCounterCash))
                    {
                        vshortageAmount = (dayBeforeEOD[0].CounterCash + ((Convert.ToDecimal(TotalSale) - (vtodayDeposit + vtodayExpense + vtodayAdvance + vtodayPartyPayment + Convert.ToDecimal(TotalCardPayment != "" ? TotalCardPayment : "0") + Convert.ToDecimal(TotalDiscount != "" ? TotalDiscount : "0"))))) - Convert.ToDecimal(TotalCounterCash != "" ? TotalCounterCash : "0");
                    }
                    else
                        return "-2";//"Please complete the form.";
                }
                else
                {
                    vshortageAmount = -0;
                }

                if (!String.IsNullOrEmpty(TotalSale) && !String.IsNullOrEmpty(TotalDiscount) && !String.IsNullOrEmpty(TotalSale) && !String.IsNullOrEmpty(TotalCounterCash))
                {
                    if (0 <= objDal.SaveEODDetail(float.Parse(TotalSale), float.Parse(TotalCardPayment), float.Parse(TotalDiscount), float.Parse(TotalCounterCash), float.Parse(vshortageAmount.ToString()), StoreId, EodDate, User.Identity.Name))
                    {
                        //string error = "";
                        return "1";// +objDal.CreateDataBackup();
                    }
                    else
                        return "-1";
                }
                else
                    return "-2";//"Please complete the form.";
            }
            else
                return "-3";//"Oops!! You already submitted EOD for Today .";
        }

        [HttpPost]
        public string SubmitExpense(string ExpenseType, string ExpenseAmount, string Remarks, string EodDate)
        {
            GetStoreId();
            if (isValidSubmissionEod(EodDate))
            {               
                if (!String.IsNullOrEmpty(ExpenseType) && !String.IsNullOrEmpty(ExpenseAmount))
                {
                    if (0 < objDal.SaveExpenseDetail(ExpenseType, Convert.ToDecimal(ExpenseAmount), Remarks, StoreId, EodDate))
                        return "Thank you " + User.Identity.Name + ". Expense Saved.";
                    else
                        return "Error Occured";
                }
                else
                    return "Please complete the form.";
            }
            else
            {
                return "EOD already submitted for this date";
            }
        }
        [HttpPost]
        public string SubmitPartyPayment(string PaymentAmount, string PartyID, string EodDate)
        {
            GetStoreId();
            if (isValidSubmissionEod(EodDate))
            {
                if (!String.IsNullOrEmpty(PaymentAmount) && !String.IsNullOrEmpty(PartyID))
                {
                    if (0 < objDal.SavePartyPaymentDetail(Convert.ToDecimal(PaymentAmount), Convert.ToInt16(PartyID), StoreId, EodDate))
                        return "Thank you " + User.Identity.Name + ". Advance Saved.";
                    else
                        return "Error Occured";
                }
                else
                    return "Please complete the form.";
            }
            else
            {
                return "EOD already submitted for this date";
            }
        }

        [HttpPost]
        public string SubmitAdvance(string AdvanceAmount, string AdvanceTypeId, string EmpID, string Remarks, string EodDate)
        {
            GetStoreId();
            if (isValidSubmissionEod(EodDate))
            {
                if (!String.IsNullOrEmpty(AdvanceAmount) && !String.IsNullOrEmpty(EmpID) && !String.IsNullOrEmpty(AdvanceTypeId))
                {
                    if (0 < objDal.SaveAdvanceDetail(Convert.ToDecimal(AdvanceAmount), Convert.ToInt16(AdvanceTypeId), Convert.ToInt16(EmpID), StoreId, Remarks, EodDate))
                        return "Thank you " + User.Identity.Name + ". Advance Saved.";
                    else
                        return "Error Occured";
                }
                else
                    return "Please complete the form.";
            }
            else
            {
                return "EOD already submitted for this date";
            }
        }

        [HttpPost]
        public string SubmitDeposit(string DepositAmount, string BankName, string EodDate)
        {
            GetStoreId();
            if (isValidSubmissionEod(EodDate))
            {
                if (!String.IsNullOrEmpty(DepositAmount) && !String.IsNullOrEmpty(BankName))
                {
                    if (0 < objDal.SaveDepositDetail(Convert.ToDecimal(DepositAmount), BankName, StoreId, EodDate))
                        return "Thank you " + User.Identity.Name + ". Deposit Saved.";
                    else
                        return "Error Occured";
                }
                else
                    return "Please complete the form.";
            }
            else
            {
                return "EOD already submitted for this date";
            }
        }
        [HttpPost]
        public string SubmitStoreExpense(string ExpenseDesc, string ExpenseAmount)
        {
            if (User.Identity.Name == "GAURAV" || User.Identity.Name == "ANUP")
            {
                if (!String.IsNullOrEmpty(ExpenseDesc) && !String.IsNullOrEmpty(ExpenseAmount) && !String.IsNullOrEmpty(User.Identity.Name))
                {
                    if (0 < objDal.SaveStoreSetupExpenseDetail(ExpenseDesc, Convert.ToDecimal(ExpenseAmount), User.Identity.Name))
                        return "Thank you " + User.Identity.Name + ".Store Setup Expense Saved.";
                    else
                        return "Error Occured";
                }
                else
                    return "Please complete the form.";
            }
            else
            {
                return "You are not authorize to submit expense";
            }
        }

        [HttpPost]
        public string AddParty(string PartyName, string PartyAddress, string PartyContacts, string PartyBankDetails, string Brands)
        {
            GetStoreId();
            if (!String.IsNullOrEmpty(PartyName) && !String.IsNullOrEmpty(PartyAddress) && !string.IsNullOrEmpty(PartyContacts) && !string.IsNullOrEmpty(PartyBankDetails))
            {
                if (0 < objDal.SaveParty(PartyName, PartyAddress, PartyContacts, PartyBankDetails, Brands, StoreId))
                    return "Thank you " + User.Identity.Name + ". Party Added.";
                else
                    return "Error Occured";
            }
            else
                return "Please complete the Party form.";
        }

        [HttpPost]
        public string AddEmployee(string EmpName, string EmployeeAddress, string EmployeeProofType, string EmployeeProofId, string EmployeeMobile)
        {
            GetStoreId();
            if (!String.IsNullOrEmpty(EmpName) && !String.IsNullOrEmpty(EmployeeAddress) && !string.IsNullOrEmpty(EmployeeProofType) && !string.IsNullOrEmpty(EmployeeProofId))
            {
                if (0 < objDal.SaveEmployee(EmpName, EmployeeAddress, EmployeeProofType, EmployeeMobile, EmployeeProofId, StoreId))
                    return "Thank you " + User.Identity.Name + ". Employee Added.";
                else
                    return "Error Occured";
            }
            else
                return "Please complete the Party form.";
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

        int GetStoreId()
        {
            if (Session["store"] != null)
            {
                StoreId = Convert.ToInt32(Session["store"]);
                user = Session["store"].ToString();
            }
            else
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
                RedirectToAction("About", "Public");
            }
            return StoreId;
        }

        public JsonResult GetEODDetail(string MonthYear)
        {

            GetStoreId();

            var listEOD = objDal.GetEODList(StoreId, MonthYear);
            
            decimal lastDayClosingBalance;

            foreach (clsEODDetails clseodDetails in listEOD)
            {
                lastDayClosingBalance = clseodDetails.CounterCash;
            }

            var Result = new
            {
                page = 1,
                records = listEOD.Count(),
                rows = listEOD,
                total = 1
            };

            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMonthSummaryDL()
        {
            GetStoreId();
            var listEOD = objDal.GetMonthSummaryDL(StoreId);
            var Result = new
            {
                page = 1,
                records = listEOD.Count(),
                rows = listEOD,
                total = 1
            };

            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string DeleteTodayTransaction(string Id, string Type)
        {
            if (Convert.ToInt32(Id) > 0 && Type != "")
            {
                objDal.DeleteTodayTransaction(Convert.ToInt32(Id), Type);
                return Type + " deleted successfully.";
            }
            else
            {
               return "Not Deleted";
            }
        }

        public JsonResult GetAdvanceDetail(string EODDate, string EmpName)
        {
            List<clsAdvance> listAdvance = new List<clsAdvance>(); ;
            GetStoreId();

            if (string.IsNullOrEmpty(EmpName))
            {
                if (!string.IsNullOrEmpty(EODDate))
                {
                    listAdvance = objDal.GetAdvanceList(StoreId, EODDate.Replace("%20", " "));
                }
                else
                    listAdvance = objDal.GetAdvanceList(StoreId);
            }
            else
            {
                listAdvance = objDal.GeMonthWisePaymentReportDl(StoreId, EmpName.Replace("%20", " "), EODDate.Replace("%20", " "));
            }

            var Result = new
            {
                page = 1,
                records = listAdvance.Count(),
                rows = listAdvance,
                total = 1
            };

            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPartyList()
        {
            GetStoreId();
            List<clsParty> listParty = new List<clsParty>(); ;

            listParty = objDal.GetPartyList(StoreId);

            var Result = new
            {
                page = 1,
                records = listParty.Count(),
                rows = listParty,
                total = 1
            };
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEmployeeList()
        {
            List<clsEmployee> listEmployee = new List<clsEmployee>(); ;
            GetStoreId();
            listEmployee = objDal.GetEmployeeList(StoreId);

            var Result = new
            {
                page = 1,
                records = listEmployee.Count(),
                rows = listEmployee,
                total = 1
            };
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPartyPaymentList(string EODDate)
        {
            List<clsPartyPayment> listPartyPayment = new List<clsPartyPayment>(); ;
            GetStoreId();
            if (!string.IsNullOrEmpty(EODDate))
            {
                listPartyPayment = objDal.GetPartyPaymentList(StoreId, EODDate.Replace("%20", " "));
            }
            else
            {
                listPartyPayment = objDal.GetPartyPaymentList(StoreId);
            }
            var Result = new
            {
                page = 1,
                records = listPartyPayment.Count(),
                rows = listPartyPayment,
                total = 1
            };
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDepositList(string EODDate)
        {
            List<clsDeposit> listDeposit = new List<clsDeposit>(); ;
            GetStoreId();
            if (!string.IsNullOrEmpty(EODDate))
            {
                listDeposit = objDal.GetDepositList(StoreId, EODDate.Replace("%20", " "));
            }
            else
            {
                listDeposit = objDal.GetDepositList(StoreId);
            }

            var Result = new
            {
                page = 1,
                records = listDeposit.Count(),
                rows = listDeposit,
                total = 1
            };
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetExpanseDetail(string EODDate)
        {
            List<clsExpense> listExpense = new List<clsExpense>(); ;
            GetStoreId();
            if (!string.IsNullOrEmpty(EODDate))
            {
                listExpense = objDal.GeExpanseList(StoreId, EODDate.Replace("%20", " "),false);
            }
            else
                listExpense = objDal.GeExpanseList(StoreId);

            var Result = new
            {
                page = 1,
                records = listExpense.Count(),
                rows = listExpense,
                total = 1
            };

            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStoreSetupExpanseDetail()
        {
            List<clsStoreSetup> listStoreExpense = new List<clsStoreSetup>(); ;

            listStoreExpense = objDal.GeStoreSetupExpanseList();

            var Result = new
            {
                page = 1,
                records = listStoreExpense.Count(),
                rows = listStoreExpense,
                total = 1
            };

            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStoreSetupSummary()
        {
            List<clsStoreSetup> listStoreExpense = new List<clsStoreSetup>(); ;

            listStoreExpense = objDal.GetStoreSetupSummary();

            var Result = new
            {
                page = 1,
                records = listStoreExpense.Count(),
                rows = listStoreExpense,
                total = 1
            };

            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMonthWiseEmployeePaymentDetail()
        {
            List<clsAdvance> listAdvance = new List<clsAdvance>(); ;
            GetStoreId();
            listAdvance = objDal.GeMonthWisePaymentReportDl(StoreId);

            var Result = new
            {
                page = 1,
                records = listAdvance.Count(),
                rows = listAdvance,
                total = 1
            };

            return Json(Result, JsonRequestBehavior.AllowGet);
        }

    }
}
