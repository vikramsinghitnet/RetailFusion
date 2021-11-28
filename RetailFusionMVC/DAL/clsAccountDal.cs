using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RetailFusionMVC.Models
{
    public class clsAccountDal
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adptr;
        int affectedRows = 0;

        public clsUser GetUserLogin(int StoreId,string UserName,string Password)
        {
            var user = new clsUser();
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandText = "select Total_Sale,Card_Payment,CONVERT(nvarchar,EOD_Date, 100) EOD_Date,Total_Discount,Counter_Cash from T_EOD";
                cmd.CommandText = "getLoginDetails";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("UserName", UserName));
                cmd.Parameters.Add(new SqlParameter("Password", Password));
                cmd.Parameters.Add(new SqlParameter("StoreId", StoreId));
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    user= new clsUser()
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        UserName = dr["UserName"].ToString(),
                        Password = dr["Password"].ToString(),
                        StoreId = Convert.ToInt32(dr["StoreId"])
                    };
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
            return user;
        }

    }
}