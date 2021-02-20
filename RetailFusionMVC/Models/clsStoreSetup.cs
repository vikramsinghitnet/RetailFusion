using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailFusionMVC.Models
{
    public class clsStoreSetup
    {
        decimal expenseAmount;

        public decimal ExpenseAmount
        {
            get { return expenseAmount; }
            set { expenseAmount = value; }
        }

        string expenseDesc;

        public string ExpenseDesc
        {
            get { return expenseDesc; }
            set { expenseDesc = value; }
        }

        string expenseDate;

        public string ExpenseDate
        {
            get { return expenseDate; }
            set { expenseDate = value; }
        }

        string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
    }
}