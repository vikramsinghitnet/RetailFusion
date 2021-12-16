using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Text;
using System.IO;

namespace RetailFusionMVC.Models
{
    public class clsDAL
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adptr;
        int affectedRows = 0;

        public int SaveEODDetail(float totalSale, float cardPayment, float totalDiscount, float counterCash, float shortageAmout, int StoreId,string EodDate,string user)
        {
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
                con.Open();

                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                if(string.IsNullOrEmpty(EodDate))
                    cmd.CommandText = "Insert into dbo.T_EOD(Total_Sale,Card_Payment,Total_Discount,Counter_Cash,Shortage,StoreId,Eod_SubmittedBy) Values(" + totalSale.ToString() + "," + cardPayment.ToString() + "," + totalDiscount.ToString() + "," + counterCash.ToString() + "," + shortageAmout + "," + StoreId + ",'"+user+"')";
                else
                    cmd.CommandText = "Insert into dbo.T_EOD(Total_Sale,Card_Payment,Total_Discount,Counter_Cash,Shortage,StoreId,EOD_Date,Eod_SubmittedBy) Values(" + totalSale.ToString() + "," + cardPayment.ToString() + "," + totalDiscount.ToString() + "," + counterCash.ToString() + "," + shortageAmout + "," + StoreId + ",'" + EodDate + "','" + user + "')";
                affectedRows = cmd.ExecuteNonQuery();
                con.Close();
                return affectedRows;
            }
            catch (Exception ex)
            {
                return -1;
            }
            finally
            {
                con.Close();
            }
            return 0;
        }

        public int SaveParty(string PartyName, string PartyAddress, string PartyContacts, string PartyBankDetails, string Brands, int StoreId)
        {
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
                con.Open();

                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Insert into dbo.T_Party (Party_Name, Party_Address, Party_Contact,  Party_Bankname,Party_Product,StoreId)" +
                "Values('" + PartyName + "','" + PartyAddress + "','" + PartyContacts + "','" + PartyBankDetails + "','" + Brands + "'," + StoreId + ")";
                affectedRows = cmd.ExecuteNonQuery();
                con.Close();
                return affectedRows;
            }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                con.Close();
            }
            return 0;
        }

        public int SaveEmployee(string EmpName, string EmployeeAddress, string EmployeeProofType, string MobileNo, string EmployeeProofId, int StoreId)
        {
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
                con.Open();

                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Insert into dbo.T_Employee ( Emp_Name, Emp_Address, Emp_ProofType, Emp_ProofID,Emp_Mobile,StoreId)" +
                "Values('" + EmpName + "','" + EmployeeAddress + "','" + EmployeeProofType + "','" + EmployeeProofId + "','" + MobileNo + "'," + StoreId + ")";
                affectedRows = cmd.ExecuteNonQuery();
                con.Close();
                return affectedRows;
            }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                con.Close();
            }
            return 0;
        }

        public int SaveExpenseDetail(string ExpenseType, decimal ExpenseAmount, string Remarks, int StoreId, string EodDate)
        {
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
                con.Open();
                if (string.IsNullOrEmpty(EodDate))
                {
                    EodDate = DateTime.Now.ToString();
                }
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Insert into dbo.T_Expense(Expense_Type,Expense_Amount,Remarks,StoreId,Expense_Date) Values('" + ExpenseType + "'," + ExpenseAmount.ToString() + ",'" + Remarks + "'," + StoreId + ",'"+EodDate+"')";
                affectedRows = cmd.ExecuteNonQuery();
                con.Close();
                return affectedRows;
            }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                con.Close();
            }
            return 0;
        }

        public int SaveStoreSetupExpenseDetail(string ExpenseDesc, decimal ExpenseAmount, string UserName)
        {
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
                con.Open();

                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Insert into dbo.T_StoreSetupExp(StoreSetupExpDesc,Expense_Amount,Logged_UserName) Values('" + ExpenseDesc + "'," + ExpenseAmount.ToString() + ",'" + UserName + "')";
                affectedRows = cmd.ExecuteNonQuery();
                con.Close();
                return affectedRows;
            }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                con.Close();
            }
            return 0;
        }

        public int AddInvoice(string InvoiceNo, string InvoiceDate, string InvoiceAmount, string Vat, string StockQty, string RecievedDate, string PartyName, string CashDiscount, string FrieghtChgs, string TotalInvoiceAmount, int StoreId, string Remarks)
        {
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
                con.Open();

                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Insert into dbo.T_Purchase(Party_Name,Invoice_No,Invoice_Amount,Invoice_Date,Vat,Recieved_Date,Stock_Qty,Cash_Discount,Frieght_Chgs,Total_InvoiceAmount,StoreId,Remarks,Store) Values('" + PartyName + "','" + InvoiceNo + "'," + InvoiceAmount + ",'" + Convert.ToDateTime(InvoiceDate) + "'," + Vat + ",'" + Convert.ToDateTime(RecievedDate) + "'," + StockQty + "," + CashDiscount + "," + FrieghtChgs + "," + TotalInvoiceAmount + "," + StoreId + ",'" + Remarks + "')";
                affectedRows = cmd.ExecuteNonQuery();
                con.Close();
                return affectedRows;
            }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                con.Close();
            }
            return 0;
        }

        public int SaveDepositDetail(decimal DepositAmount, string BankName, int StoreId, string EodDate)
        {
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
                con.Open();
                if (string.IsNullOrEmpty(EodDate))
                {
                    EodDate = DateTime.Now.ToString();
                }
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Insert into dbo.T_Deposit (Deposit_Amount,Deposit_Bank,StoreId,Deposit_Date) Values(" + DepositAmount.ToString() + ",'" + BankName + "'," + StoreId +",'"+EodDate+ "')";
                affectedRows = cmd.ExecuteNonQuery();
                con.Close();
                return affectedRows;
            }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                con.Close();
            }
            return 0;
        }

        public int SavePartyPaymentDetail(decimal PaymentAmount, int PartyId, int StoreId, string EodDate)
        {
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
                if (string.IsNullOrEmpty(EodDate))
                {
                    EodDate = DateTime.Now.ToString();
                }
                con.Open();

                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Insert into dbo.T_PartyPayment (PartyPayment_Amount,Party_Id,StoreId,PartyPayment_Date) Values(" + PaymentAmount.ToString() + "," + PartyId.ToString() + "," + StoreId +",'"+EodDate+ "')";
                affectedRows = cmd.ExecuteNonQuery();
                con.Close();
                return affectedRows;
            }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                con.Close();
            }
            return 0;
        }

        public int SaveAdvanceDetail(decimal AdvanceAmount, int AdvanceTypeId, int EmpID, int StoreId, string Remarks, string EodDate)
        {
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
                con.Open();
                if (string.IsNullOrEmpty(EodDate))
                {
                    EodDate = DateTime.Now.ToString();
                }
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Insert into dbo.T_Advance(Emp_ID,Advance_Amount,Advance_Type_Id,StoreId,Remarks,Advance_Date) Values(" + EmpID.ToString() + "," + AdvanceAmount.ToString() + "," + AdvanceTypeId + "," + StoreId + ",'" + Remarks +"','"+EodDate+ "')";
                affectedRows = cmd.ExecuteNonQuery();
                con.Close();
                return affectedRows;
            }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                con.Close();
            }
            return 0;
        }

        public int DeleteTodayEOD(int StoreId,string EodDate)
        {
            int affectedRecords = 0;
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandText = "select Total_Sale,Card_Payment,CONVERT(nvarchar,EOD_Date, 100) EOD_Date,Total_Discount,Counter_Cash from T_EOD";
                cmd.CommandText = "spDeleteTodayEOD";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("StoreId", StoreId));
                cmd.Parameters.Add(new SqlParameter("EodDate", EodDate));
                affectedRecords = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                return -1;
            }
            finally
            {
                con.Close();
            }
            return affectedRecords;
        }

        public int UpdateDispatchStatus(int dispatchId, string Status, int StoreId)
        {
            int affectedRecords = 0;
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
                con.Open();
                cmd.Connection = con;
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spUpdateDispatchStatus";
                cmd.Parameters.Add(new SqlParameter("DispatchId", dispatchId));
                cmd.Parameters.Add(new SqlParameter("StoreId", StoreId));
                cmd.Parameters.Add(new SqlParameter("Status", Status));

                affectedRecords = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            return affectedRecords;
        }


        public int SubmitPaidAmount(int AdvanceId, string PaymentType, int StoreId,string creditDate=null, decimal PartialPayment = 0)
        {
            int affectedRecords = 0;
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
                con.Open();
                cmd.Connection = con;

                //cmd.CommandText = "select Total_Sale,Card_Payment,CONVERT(nvarchar,EOD_Date, 100) EOD_Date,Total_Discount,Counter_Cash from T_EOD";
                cmd.Parameters.Clear();
                if (PaymentType == "Customer")
                {
                    DateTime cDate;
                    DateTime.TryParse(creditDate, out cDate);

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO T_CustomerCreditPayment (Expense_ID,StoreId,CreatedDate) values (" + AdvanceId + "," + StoreId + ",'"+cDate.ToString("yyyy-MM-dd")+"')";
                }
                else
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "spSubmitPaidAmount";
                    cmd.Parameters.Add(new SqlParameter("AdvanceId", AdvanceId));
                    cmd.Parameters.Add(new SqlParameter("StoreId", StoreId));
                    if (PartialPayment > 0)
                    {
                        cmd.Parameters.Add(new SqlParameter("PartialPayment", PartialPayment));
                    }
                }
                affectedRecords = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            return affectedRecords;
        }

        public int SumbitReturn(string returnType, string party, string quantity, string value, string remark, int storeid, string courierName, string trackingId)
        {
            int affectedRecords = 0;
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
                con.Open();
                cmd.Connection = con;
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO [dbo].[T_StockReturn] values ('" + returnType + "','" + remark + "'," + party + "," + quantity + "," + value + ",'Dispatched'," + storeid + ",'" + courierName + "','" + trackingId + "',getdate())";
                affectedRecords = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            return affectedRecords;
        }

        public List<clsEODDetails> GetEODList(int StoreId, string MonthYear)
        {
            var listEOD = new List<clsEODDetails>();
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandText = "select Total_Sale,Card_Payment,CONVERT(nvarchar,EOD_Date, 100) EOD_Date,Total_Discount,Counter_Cash from T_EOD";
                cmd.CommandText = "spGetEODDetails";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("StoreId", StoreId));
                if (!string.IsNullOrEmpty(MonthYear))
                {
                    cmd.Parameters.Add(new SqlParameter("MonthYear", MonthYear));
                }
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    listEOD.Add(new clsEODDetails()
                    {
                        CardPayment = Convert.ToDecimal(dr["card_payment"]),
                        CounterCash = Convert.ToDecimal(dr["Counter_Cash"]),
                        EODDate = dr["EOD_Date"].ToString(),
                        TotalDiscount = Convert.ToDecimal(dr["Total_Discount"]),
                        TotalSale = Convert.ToDecimal(dr["Total_Sale"]),
                        TotalAdvance = Convert.ToDecimal(dr["Total_Advance"]),
                        TotalExpense = Convert.ToDecimal(dr["Total_Expense"]),
                        TotapPartypayment = Convert.ToDecimal(dr["Total_PartyPayment"]),
                        TotalDeposit = Convert.ToDecimal(dr["Total_Deposit"]),
                        ShortageAmount = Convert.ToDecimal(dr["Shortage"]),
                        SubmittedBy = dr["SubmittedBy"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            return listEOD;
        }


        public List<clsExpenses> GetMonthExpenses(int StoreId, string MonthYear)
        {
            var listMonthExpenses = new List<clsExpenses>();
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spGetMonthWiseExpenseDetails";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("StoreId", StoreId));
                if (!string.IsNullOrEmpty(MonthYear))
                {
                    cmd.Parameters.Add(new SqlParameter("MonthYear", MonthYear));
                }
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    listMonthExpenses.Add(new clsExpenses()
                    {
                        ExpenseAmount = Convert.ToDecimal(dr["Expense_Amount"]),
                        Remarks = dr["Remarks"].ToString(),
                        ExpName = dr["Expense_Type"].ToString(),
                        Date = dr["Expense_Date"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            return listMonthExpenses;
        }


        public List<clsInvoiceDetails> GetInvoiceList(string reportType, int StoreId, string PartyId)
        {
            var listInvoice = new List<clsInvoiceDetails>();
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandText = "select Total_Sale,Card_Payment,CONVERT(nvarchar,EOD_Date, 100) EOD_Date,Total_Discount,Counter_Cash from T_EOD";
                cmd.CommandText = "spGetInvoiceDetails";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("StoreId", StoreId));
                cmd.Parameters.Add(new SqlParameter("ReportType", reportType));
                if (!string.IsNullOrEmpty(PartyId))
                {
                    cmd.Parameters.Add(new SqlParameter("PartyId", PartyId.Trim()));
                }
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    listInvoice.Add(new clsInvoiceDetails()
                    {
                        InvoiceID = Convert.ToInt32(dr["Purchase_ID"]),
                        InvoiceNo = dr["Invoice_No"].ToString(),
                        PartyName = dr["Party_Name"].ToString(),
                        InvoiceDate = dr["Invoice_Date"].ToString(),
                        InvoiceAmount = Convert.ToDecimal(dr["Invoice_Amount"]),
                        Vat = Convert.ToDecimal(dr["Vat"]),
                        StockQty = Convert.ToInt32(dr["Stock_Qty"]),
                        RecievedDate = dr["Recieved_Date"].ToString(),
                        PaidAmount = Convert.ToDecimal(dr["Paid_Amount"]),
                        PendingAmount = Convert.ToDecimal(dr["PendingAmount"]),
                        PaymentDate = dr["Payment_Date"].ToString(),
                        DewDate = dr["Dew_Date"].ToString(),
                        DaysLeft = Convert.ToInt32(dr["Days_Left"]),
                        InvoiceEntryDate = dr["Create_Date"].ToString(),
                        CashDiscount = Convert.ToDecimal(dr["Cash_Discount"]),
                        FrieghtChgs = Convert.ToDecimal(dr["Frieght_Chgs"]),
                        Remarks = dr["Remarks"].ToString(),
                        TotalInvoiceAmount = Convert.ToDecimal(dr["Total_InvoiceAmount"])
                    });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            return listInvoice;
        }

        public List<clsStockReturn> DLGetPartPaymentDetails(string BillId)
        {
            var listInvoice = new List<clsStockReturn>();
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from [T_PartPayment] where Purchase_ID=" + BillId;
                cmd.Parameters.Clear();

                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    listInvoice.Add(new clsStockReturn()
                    {
                        StockReturn_ID = Convert.ToInt32(dr["Purchase_ID"]),
                        UpdatedDate = dr["PaymentDate"].ToString(),
                        ReturnValue = Convert.ToInt32(dr["PartialAmount"])
                    });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            return listInvoice;
        }


        public List<clsStockReturn> GetStockReturnList(int StoreId)
        {
            var listInvoice = new List<clsStockReturn>();
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandText = "select Total_Sale,Card_Payment,CONVERT(nvarchar,EOD_Date, 100) EOD_Date,Total_Discount,Counter_Cash from T_EOD";
                cmd.CommandText = "spGetStockReturnList";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("StoreId", StoreId));

                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    listInvoice.Add(new clsStockReturn()
                    {
                        StockReturn_ID = Convert.ToInt32(dr["StockReturn_ID"]),
                        ReturnType = dr["ReturnType"].ToString(),
                        Remarks = dr["Remarks"].ToString(),
                        Party = dr["Party"].ToString(),
                        CourierName = dr["CourierName"].ToString(),
                        TrackingId = dr["CourierTrackingId"].ToString(),
                        UpdatedDate = dr["UpdatedDate"].ToString(),
                        Qty = Convert.ToInt32(dr["Quantity"]),
                        ReturnValue = Convert.ToInt32(dr["Value"]),
                        ReturnStatus = dr["ReturnStatus"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            return listInvoice.OrderByDescending(x => x.StockReturn_ID).ToList();
        }

        public List<clsStoreSetup> GetStoreSetupSummary()
        {
            var listStoreSetupSummary = new List<clsStoreSetup>();
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandText = "select Total_Sale,Card_Payment,CONVERT(nvarchar,EOD_Date, 100) EOD_Date,Total_Discount,Counter_Cash from T_EOD";
                cmd.CommandText = "GetStoreSetupSummery";
                cmd.Parameters.Clear();
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    listStoreSetupSummary.Add(new clsStoreSetup()
                    {
                        ExpenseAmount = Convert.ToDecimal(dr["Expense_Amount"]),
                        UserName = dr["Logged_UserName"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            return listStoreSetupSummary;
        }

        public List<clsEODDetails> GetDayBeforeEODDetail(int StoreId,string EODDate=null)
        {
            var listEOD = new List<clsEODDetails>();
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandText = "select Total_Sale,Card_Payment,CONVERT(nvarchar,EOD_Date, 100) EOD_Date,Total_Discount,Counter_Cash from T_EOD";
                cmd.CommandText = "spGetEODDetails";

                cmd.Parameters.Clear();
                if(!string.IsNullOrEmpty(EODDate))
                    cmd.Parameters.Add(new SqlParameter("forDate", EODDate));
                else
                    cmd.Parameters.Add(new SqlParameter("GetLastDayEOD", true));
                
                cmd.Parameters.Add(new SqlParameter("StoreId", StoreId));
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    listEOD.Add(new clsEODDetails()
                    {
                        CardPayment = Convert.ToDecimal(dr["card_payment"]),
                        CounterCash = Convert.ToDecimal(dr["Counter_Cash"]),
                        EODDate = dr["EOD_Date"].ToString(),
                        TotalDiscount = Convert.ToDecimal(dr["Total_Discount"]),
                        TotalSale = Convert.ToDecimal(dr["Total_Sale"]),
                        TotalAdvance = Convert.ToDecimal(dr["Total_Advance"]),
                        TotalExpense = Convert.ToDecimal(dr["Total_Expense"]),
                        TotapPartypayment = Convert.ToDecimal(dr["Total_PartyPayment"]),
                        TotalDeposit = Convert.ToDecimal(dr["Total_Deposit"])
                    });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            return listEOD;
        }

        public List<clsDeposit> GetDepositList(int StoreId, string EODDate = null)
        {
            var listDepositDetails = new List<clsDeposit>();
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spGetDepositDetails";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("StoreId", StoreId));
                cmd.Parameters.Add(new SqlParameter("EODDate", EODDate));
                SqlDataReader dr;
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    listDepositDetails.Add(new clsDeposit()
                    {
                        Id= dr["Deposit_Id"].ToString(),
                        DepositAmount = dr["Deposit_Amount"].ToString(),
                        DepositBank = dr["Deposit_Bank"].ToString(),
                        DepositDate = dr["Deposit_Date"].ToString()
                    });
                }

                return listDepositDetails;
            }
            catch (Exception ex)
            {
                return listDepositDetails;
            }
            finally
            {
                con.Close();
            }
            return listDepositDetails;
        }

        public List<clsPartyPayment> GetPartyPaymentList(int StoreId, string EODDate = null)
        {
            var listPaymentDetails = new List<clsPartyPayment>();
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandText = "select Total_Sale,Card_Payment,CONVERT(nvarchar,EOD_Date, 100) EOD_Date,Total_Discount,Counter_Cash from T_EOD";
                cmd.CommandText = "spGetPartyPaymentDetails";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("EODDate", EODDate));
                cmd.Parameters.Add(new SqlParameter("StoreId", StoreId));
                SqlDataReader dr;
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    listPaymentDetails.Add(new clsPartyPayment()
                    {
                        Id=dr["PartyPayment_Id"].ToString(),
                        PartyName = (dr["Party_Name"]).ToString(),
                        PaymentAmount = (dr["PartyPayment_Amount"]).ToString(),
                        PaymentDate = dr["PartyPayment_Date"].ToString()
                    });
                }

                return listPaymentDetails;
            }
            catch (Exception ex)
            {
                return listPaymentDetails;
            }
            finally
            {
                con.Close();
            }
            return listPaymentDetails;
        }

        public List<clsEmployee> GetEmployeeList(int StoreId)
        {
            var listEmployee = new List<clsEmployee>();
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandText = "select Total_Sale,Card_Payment,CONVERT(nvarchar,EOD_Date, 100) EOD_Date,Total_Discount,Counter_Cash from T_EOD";
                cmd.CommandText = "spGetEmployeeDetail";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("StoreId", StoreId));
                SqlDataReader dr;
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    listEmployee.Add(new clsEmployee()
                    {
                        EmpId = dr["Emp_ID"].ToString(),
                        EmpName = (dr["Emp_Name"]).ToString(),
                        EmpRole = (dr["Emp_Role"]).ToString(),
                        EmpMobile = dr["Emp_Mobile"].ToString(),
                        EmpProofID = dr["Emp_ProofID"].ToString(),
                        EmpProofType = dr["Emp_ProofType"].ToString()
                    });
                }

                return listEmployee;
            }
            catch (Exception ex)
            {
                return listEmployee;
            }
            finally
            {
                con.Close();
            }
            return listEmployee;
        }

        public List<clsParty> GetPartyList(int StoreId)
        {
            var listParty = new List<clsParty>();
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = " select * from dbo.T_Party where StoreId=" + StoreId + " order by 2";
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    listParty.Add(new clsParty()
                    {
                        PartyId = (dr["Party_Id"]).ToString(),
                        PartyName = (dr["Party_Name"]).ToString().ToUpper(),
                        PartyAddress = (dr["Party_Address"]).ToString(),
                        PartyContact = dr["Party_Contact"].ToString(),
                        PartyProduct = dr["Party_Product"].ToString(),
                        PartyBankname = dr["Party_Bankname"].ToString()
                    });
                }

                return listParty;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return listParty;
        }

        public List<string> GetDisptachIds(int StoreId)
        {
            var listParty = new List<string>();
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = " select stockreturn_id from T_StockReturn where storeid=" + StoreId + " order by 1 desc";
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    listParty.Add(dr[0].ToString());
                }

                return listParty;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return listParty;
        }

        public List<string> GetPedingAmountList(string PendingAmountType, int StoreId)
        {
            var listParty = new List<string>();
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                if (PendingAmountType == "Customer")
                {
                    cmd.CommandText = " select a.expense_id from dbo.T_Expense a where Expense_Type=11 and IsActive=1 and storeid=" + StoreId + " and a.expense_id not in (select expense_id from T_CustomerCreditPayment where storeid=" + StoreId + ") order by 1 desc";
                }
                else
                {
                    cmd.CommandText = "SELECT Purchase_ID FROM [T_Purchase] WHERE Payment_Date IS NULL and storeid=" + StoreId+ " order by 1 desc";
                }

                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    listParty.Add(dr[0].ToString());
                }

                return listParty;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return listParty;
        }

        public List<clsPendingAmountDetails> GetPendingAmountSummary(int StoreId)
        {
            var listPendingDetails = new List<clsPendingAmountDetails>();
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spGetPartyPendingAmount";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("StoreId", StoreId));

                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    listPendingDetails.Add(new clsPendingAmountDetails()
                    {
                        PartyName = dr["Party_Name"].ToString(),
                        PendingAmount = Convert.ToDecimal(dr["Pending_Amount"]),
                        PendingVAT = Convert.ToDecimal(dr["VAT"])

                    });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            return listPendingDetails;
        }

        public List<clsExpensSummary> GetExpenseSummary(int StoreId)
        {
            var listExpensSummary = new List<clsExpensSummary>();
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spGetExpenseSummary";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("StoreId", StoreId));

                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    listExpensSummary.Add(new clsExpensSummary()
                    {
                        MonthYear = dr["MonthYear"].ToString(),
                        Vegetable = Convert.ToDecimal(dr["Vegetable"]),
                        Alteration = Convert.ToDecimal(dr["Alteration"]),
                        Petrol = Convert.ToDecimal(dr["Petrol"]),
                        Kirana = Convert.ToDecimal(dr["Kirana"]),
                        Electricity = Convert.ToDecimal(dr["Electricity_Bill"]),
                        Buying = Convert.ToDecimal(dr["Buying"]),
                        StockTransport = Convert.ToDecimal(dr["Stock_Transport"]),
                        CustomerCredit = Convert.ToDecimal(dr["Customer_Credit"]),
                        ShopSetup = Convert.ToDecimal(dr["Shop_2_Setup"])
                    });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            return listExpensSummary;
        }

        public List<clsExpense> GeExpanseList(int StoreId, string EODDate = null,bool excludeCredit=true)
        {
            var listExpanse = new List<clsExpense>();
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spGetExpenseDetails";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("StoreId", StoreId));
                cmd.Parameters.Add(new SqlParameter("ExcludeCredit", excludeCredit));
                if (!string.IsNullOrEmpty(EODDate))
                {
                    cmd.Parameters.Add(new SqlParameter("EODDate", EODDate));
                }

                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    listExpanse.Add(new clsExpense() {Id=dr["Expense_Id"].ToString(), ExpenseAmount = Convert.ToDecimal(dr["Expense_Amount"]), ExpenseType = dr["Expense_Type"].ToString(), ExpenseDate = dr["Expense_Date"].ToString(), Remarks = dr["Remarks"].ToString() });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            return listExpanse;

        }

        public DataSet GetEODMonths(int StoreId)
        {
            DataSet eodMonths = new DataSet();
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
                //con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spGetEODMonths";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("StoreId", StoreId));
                adptr = new SqlDataAdapter(cmd);
                adptr.Fill(eodMonths);

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }
            return eodMonths;
        }

        public List<clsAdvance> GeMonthWisePaymentReportDl(int StoreId, string EmpName = null, string Month = null)
        {
            var listAdvance = new List<clsAdvance>();
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spGetMonthWiseEmployeePaymentDetail";
                if (!string.IsNullOrEmpty(Month))
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new SqlParameter("EmpName", EmpName));
                    cmd.Parameters.Add(new SqlParameter("Month", Month));
                }
                cmd.Parameters.Add(new SqlParameter("StoreId", StoreId));

                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (!string.IsNullOrEmpty(Month))
                    {
                        listAdvance.Add(new clsAdvance() { Id= dr["Advance_ID"].ToString(), AdvanceAmount = Convert.ToDecimal(dr["PaidAmount"]), EmpName = dr["Emp_Name"].ToString(), PaymentType = dr["Advance_Type"].ToString(), AdvanceDate = dr["Advance_Date"].ToString(), Remarks = dr["Remarks"].ToString() });
                    }
                    else
                    {
                        listAdvance.Add(new clsAdvance() { AdvanceAmount = Convert.ToDecimal(dr["PaidAmount"]), EmpName = dr["Emp_Name"].ToString(), PaymentType = dr["Advance_Type"].ToString(), AdvanceDate = dr["Advance_Date"].ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            return listAdvance;

        }

        public List<clsStoreSetup> GeStoreSetupExpanseList()
        {
            var listExpanse = new List<clsStoreSetup>();
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select Expense_Amount,Expense_Date,StoreSetupExpDesc,Logged_UserName from dbo.T_StoreSetupExp order by Expense_Date desc";
                cmd.Parameters.Clear();

                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    listExpanse.Add(new clsStoreSetup() { ExpenseAmount = Convert.ToDecimal(dr["Expense_Amount"]), ExpenseDesc = dr["StoreSetupExpDesc"].ToString(), ExpenseDate = dr["Expense_Date"].ToString(), UserName = dr["Logged_UserName"].ToString() });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            return listExpanse;

        }

        public string GetBillPendingAmount(int StoreId)
        {
            string BillPendingAmount = "0";
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandText = "select Total_Sale,Card_Payment,CONVERT(nvarchar,EOD_Date, 100) EOD_Date,Total_Discount,Counter_Cash from T_EOD";
                cmd.CommandText = "spGetBillPendingAmount";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("StoreId", StoreId));
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    BillPendingAmount = dr[0].ToString();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            return BillPendingAmount;
        }

        public string GetCustomerPendingAmount(int StoreId)
        {
            string customerPendingAmount = "0";
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandText = "select Total_Sale,Card_Payment,CONVERT(nvarchar,EOD_Date, 100) EOD_Date,Total_Discount,Counter_Cash from T_EOD";
                cmd.CommandText = "spGetCustomerPendingAmount";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("StoreId", StoreId));
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    customerPendingAmount = dr[0].ToString();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            return customerPendingAmount;
        }

        public List<clsCustomerCredit> GetCustomerCreditList(int StoreId)
        {
            var listCustomerCredit = new List<clsCustomerCredit>();
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandText = "select Total_Sale,Card_Payment,CONVERT(nvarchar,EOD_Date, 100) EOD_Date,Total_Discount,Counter_Cash from T_EOD";
                cmd.CommandText = "spGetCustomerCredit";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("StoreId", StoreId));
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    listCustomerCredit.Add(new clsCustomerCredit()
                    {
                        ExpenseAmount = Convert.ToDecimal(dr["Expense_Amount"]),
                        ExpenseDate = dr["Expense_Date"].ToString(),
                        ExpenseId = dr["Expense_ID"].ToString(),
                        Remarks = dr["Remarks"].ToString(),
                        Store = dr["Store"].ToString(),
                        Status = dr["PaymentStatus"].ToString(),
                        ReceivedDate = dr["ReceivedDate"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            return listCustomerCredit;
        }

        public List<clsEODDetails> GetMonthSummaryDL(int StoreId, string ReportType = null)
        {
            var listEOD = new List<clsEODDetails>();
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandText = "select Total_Sale,Card_Payment,CONVERT(nvarchar,EOD_Date, 100) EOD_Date,Total_Discount,Counter_Cash from T_EOD";
                cmd.CommandText = "spGetMonthSummary";
                cmd.Parameters.Clear();
                if (!string.IsNullOrEmpty(ReportType))
                {
                    cmd.Parameters.Add(new SqlParameter("SummaryType", "M"));
                }
                cmd.Parameters.Add(new SqlParameter("StoreId", StoreId));
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (!string.IsNullOrEmpty(ReportType))
                    {
                        listEOD.Add(new clsEODDetails()
                        {
                            TotalDiscount = Convert.ToDecimal(dr["Total_Discount"]),
                            TotalSale = Convert.ToDecimal(dr["Total_Sale"]),
                            TotalAdvance = Convert.ToDecimal(dr["Total_EmployeePayment"]),
                            TotalExpense = Convert.ToDecimal(dr["Total_Expense"]),
                            TotapPartypayment = Convert.ToDecimal(dr["ALL_Expense"]),
                            ShortageAmount = Convert.ToDecimal(dr["Shortage"]),
                            CounterCash = Convert.ToDecimal(dr["profit"]),
                            EODDate = dr["MonthYear"].ToString()
                        });
                    }
                    else
                    {
                        listEOD.Add(new clsEODDetails()
                        {
                            TotalDiscount = Convert.ToDecimal(dr["Total_Discount"]),
                            TotalSale = Convert.ToDecimal(dr["Total_Sale"]),
                            TotalAdvance = Convert.ToDecimal(dr["Total_EmployeePayment"]),
                            TotalExpense = Convert.ToDecimal(dr["Total_Expense"]),
                            TotapPartypayment = Convert.ToDecimal(dr["ALL_Expense"]),
                            ShortageAmount = Convert.ToDecimal(dr["Shortage"]),
                            CounterCash = Convert.ToDecimal(dr["profit"]),
                            EODDate = dr["Store"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            return listEOD;

        }


        public int DeleteTodayTransaction(int Id, string Type)
        {
            int affectedRecords = 0;
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spDeleteTodayTransaction";
                cmd.Parameters.Clear();
                if (Id > 0)
                {
                    cmd.Parameters.Add(new SqlParameter("Id", Id));

                    cmd.Parameters.Add(new SqlParameter("Type", Type));

                    affectedRecords = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return affectedRecords;
            return 0;
        }


        public List<clsAdvance> GetAdvanceList(int StoreId, string EODDate = null)
        {
            var listAdvance = new List<clsAdvance>();
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                if (!string.IsNullOrEmpty(EODDate))
                {
                    cmd.CommandText = "select a.Advance_ID,(select top 1 UPPER(emp_name) from dbo.T_Employee  where emp_id=a.emp_id) Emp_Name, Advance_Amount,CONVERT(nvarchar,Advance_Date, 100) Advance_Date,(select top 1 Advance_Name from dbo.T_Advance_Type  where Advance_Type_Id=a.Advance_Type_Id) Advance_Type,Remarks from T_Advance a where storeid=" + StoreId + " AND CAST(Advance_Date AS DATE)=CAST('" + EODDate + "' AS DATE)";
                }
                else
                    cmd.CommandText = "select a.Advance_ID,(select top 1 UPPER(emp_name) from dbo.T_Employee  where emp_id=a.emp_id) Emp_Name, Advance_Amount,CONVERT(nvarchar,Advance_Date, 100) Advance_Date,(select top 1 Advance_Name from dbo.T_Advance_Type  where Advance_Type_Id=a.Advance_Type_Id) Advance_Type,Remarks from T_Advance a where storeid=" + StoreId + " AND CAST(CreatedDate AS DATE)=CAST(GETDATE() AS DATE)";
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    listAdvance.Add(new clsAdvance() {Id=dr["Advance_ID"].ToString(), AdvanceAmount = Convert.ToDecimal(dr["Advance_Amount"]), EmpName = dr["Emp_Name"].ToString(), AdvanceDate = dr["Advance_Date"].ToString(), PaymentType = dr["Advance_Type"].ToString(), Remarks = dr["Remarks"].ToString() });

                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            return listAdvance;
        }

        public string CreateDataBackup()
        {

            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
                cmd = new SqlCommand("spGetAllDataForBackup", con);
                cmd.CommandType = CommandType.StoredProcedure;
                DataSet ds = new DataSet();
                string[] lstDir = Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory + "\\DB_Backup\\");
                foreach (string dir in lstDir)
                {

                    DateTime dtCreation = Directory.GetCreationTime(dir);
                    if ((DateTime.Now - dtCreation).TotalDays > 30)
                    {
                        Directory.Delete(dir, true);
                    }
                }
                SqlDataAdapter adptr = new SqlDataAdapter(cmd);
                adptr.Fill(ds);
                string name = DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year;
                string s = AppDomain.CurrentDomain.BaseDirectory + "\\DB_Backup\\" + name;
                if (!Directory.Exists(s))
                {
                    Directory.CreateDirectory(s);
                    foreach (DataTable dt in ds.Tables)
                    {
                        DataTableToCSV(dt, s + "\\" + name + "_" + dt.TableName + ".csv");
                    }
                }
            }
            catch (Exception ex)
            {
                return "Error details : " + ex.Message + "," + ex.StackTrace;
            }
            return "";
        }

        public void DataTableToCSV(DataTable datatable, string path)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < datatable.Columns.Count; i++)
            {
                sb.Append(datatable.Columns[i]);
                if (i < datatable.Columns.Count - 1)
                    sb.Append(",");
            }
            sb.AppendLine();
            foreach (DataRow dr in datatable.Rows)
            {
                for (int i = 0; i < datatable.Columns.Count; i++)
                {
                    sb.Append(dr[i].ToString());

                    if (i < datatable.Columns.Count - 1)
                        sb.Append(",");
                }
                sb.AppendLine();
            }
            sb.ToString();

            File.WriteAllText(path, sb.ToString());
        }

    }
}