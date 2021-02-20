using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailFusionMVC.Models
{
    public class clsCustomerCredit
    {
        string store;


        public string Store
        {
            get { return store; }
            set { store = value; }
        }
        decimal ?expenseAmount;

        public decimal ?ExpenseAmount
        {
            get { return expenseAmount; }
            set { expenseAmount = value; }
        }
        string expenseDate;

        public string ExpenseDate
        {
            get { return expenseDate; }
            set { expenseDate = value; }
        }

        string receivedDate;

        public string ReceivedDate
        {
            get { return receivedDate; }
            set { receivedDate = value; }
        }
        string remarks;

        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }

        private string expenseId;

        private string status;

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public string ExpenseId
        {
            get { return expenseId; }
            set { expenseId = value; }
        }
    }
}