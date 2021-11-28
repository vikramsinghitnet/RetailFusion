using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailFusionMVC.Models
{
    public class clsLedger
    {
        string amount;
        public string Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        string remarks;
        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }

        string transactionDate;
        public string TransactionDate
        {
            get { return transactionDate; }
            set { transactionDate = value; }
        }

        string debit;
        public string Debit
        {
            get { return debit; }
            set { debit = value; }
        }

        string credit;
        public string Credit
        {
            get { return credit; }
            set { credit = value; }
        }

        string invoiceNo;
        public string InvoiceNo
        {
            get { return invoiceNo; }
            set { invoiceNo = value; }
        }

        string closingBalance;
        public string ClosingBalance
        {
            get { return closingBalance; }
            set { closingBalance = value; }
        }

        string brandDesc;
        public string BrandDesc
        {
            get { return brandDesc; }
            set { brandDesc = value; }
        }

        string entryDate;
        public string EntryDate
        {
            get { return entryDate; }
            set { entryDate = value; }
        }

        string ledgerId;
        public string LedgerId
        {
            get { return ledgerId; }
            set { ledgerId = value; }
        }

        string branch;
        public string Branch
        {
            get { return branch; }
            set { branch = value; }
        }
    }
}