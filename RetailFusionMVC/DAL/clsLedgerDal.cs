using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace RetailFusionMVC.Models
{
    public class clsLedgerDAL
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adptr;
        int affectedRows = 0;

        public int SaveLedger(string partyId, string amount, string invoiceNo, string remarks, string DrOrCr, string Date, string Brand,string Branch,string user)
        {
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into T_Ledger values (" + partyId + "," + amount + ",'" + DrOrCr + "','" + Convert.ToDateTime(Date) + "','" + invoiceNo +
                    "','" + remarks + "',GETDATE(),'" + Brand + "','" + Branch + "','" + user + "')";
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

        public int DeleteTodayLedger(int PartyId, int LedgerId)
        {
            int affectedRecords = 0;
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spDeleteTodayLedger";
                cmd.Parameters.Clear();
                if (PartyId > 0)
                    cmd.Parameters.Add(new SqlParameter("PartyId", PartyId));

                if (LedgerId > 0)
                    cmd.Parameters.Add(new SqlParameter("LedgerId", LedgerId));

                affectedRecords = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return affectedRecords;
        }

        public List<clsLedger> GetLedgerbyParty(string partyId,string frmDate=null,string toDate=null)
        {
            var listLedger = new List<clsLedger>();

            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spGetLedgerByParty";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("PartyId", partyId));

                if (!string.IsNullOrEmpty(frmDate))
                {
                    cmd.Parameters.Add(new SqlParameter("fromDate", frmDate));
                    cmd.Parameters.Add(new SqlParameter("toDate", toDate));
                }
                
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    listLedger.Add(new clsLedger()
                    {
                        Amount = dr["Amount"].ToString(),
                        Debit = dr["Debit"].ToString(),
                        Credit = dr["Credit"].ToString(),
                        TransactionDate = Convert.ToDateTime(dr["Transaction_Date"]).ToString("dd-MMM-yyyy"),
                        InvoiceNo = dr["Invoice_No"].ToString(),
                        Remarks = dr["Remarks"].ToString(),
                        EntryDate = dr["CreatedDate"].ToString(),
                        BrandDesc = dr["BrandDesc"].ToString(),
                        ClosingBalance = dr["ClosingBalance"].ToString(),
                        LedgerId = dr["Ledger_Id"].ToString(),
                        Branch = dr["Branch"].ToString(),
                        CreatedBy = dr["CreatedBy"].ToString()
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
            return listLedger;
        }
    }
}