using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailFusionMVC.Models
{
    public class clsExpensSummary
    {
        string monthYear;

        public string MonthYear
        {
            get { return monthYear; }
            set { monthYear = value; }
        }
        decimal vegetable;

        public decimal Vegetable
        {
            get { return vegetable; }
            set { vegetable = value; }
        }
        decimal alteration;

        public decimal Alteration
        {
            get { return alteration; }
            set { alteration = value; }
        }
        decimal petrol;

        public decimal Petrol
        {
            get { return petrol; }
            set { petrol = value; }
        }
        decimal kirana;

        public decimal Kirana
        {
            get { return kirana; }
            set { kirana = value; }
        }
        decimal electricity;

        public decimal Electricity
        {
            get { return electricity; }
            set { electricity = value; }
        }
        decimal buying;

        public decimal Buying
        {
            get { return buying; }
            set { buying = value; }
        }
        decimal stockTransport;

        public decimal StockTransport
        {
            get { return stockTransport; }
            set { stockTransport = value; }
        }
        decimal customerCredit;

        public decimal CustomerCredit
        {
            get { return customerCredit; }
            set { customerCredit = value; }
        }
        decimal shopSetup;

        public decimal ShopSetup
        {
            get { return shopSetup; }
            set { shopSetup = value; }
        }
    }

    public class clsPendingAmountDetails
    {
        string partyName;

        public string PartyName
        {
            get { return partyName; }
            set { partyName = value; }
        }
        decimal pendingAmount;

        public decimal PendingAmount
        {
            get { return pendingAmount; }
            set { pendingAmount = value; }
        }
        decimal pendingVAT;

        public decimal PendingVAT
        {
            get { return pendingVAT; }
            set { pendingVAT = value; }
        }
    }
    public class clsInvoiceDetails
    {
        int invoiceID;

        public int InvoiceID
        {
            get { return invoiceID; }
            set { invoiceID = value; }
        }

        string remarks;
        public string Remarks
        {
            get { return remarks; }
            set { remarks=value; }
        }

        string invoiceNo;

        public string InvoiceNo
        {
            get { return invoiceNo; }
            set { invoiceNo = value; }
        }

        string partyName;

        public string PartyName
        {
            get { return partyName; }
            set { partyName = value; }
        }
        string invoiceDate;

        public string InvoiceDate
        {
            get { return invoiceDate; }
            set { invoiceDate = value; }
        }
        decimal invoiceAmount;

        public decimal InvoiceAmount
        {
            get { return invoiceAmount; }
            set { invoiceAmount = value; }
        }
        decimal vat;

        public decimal Vat
        {
            get { return vat; }
            set { vat = value; }
        }
        int stockQty;

        public int StockQty
        {
            get { return stockQty; }
            set { stockQty = value; }
        }
        string recievedDate;

        public string RecievedDate
        {
            get { return recievedDate; }
            set { recievedDate = value; }
        }

        decimal paidAmount;

        public decimal PaidAmount
        {
            get { return paidAmount; }
            set { paidAmount = value; }
        }

        decimal pendingAmount;
        public decimal PendingAmount
        {
            get { return pendingAmount; }
            set { pendingAmount = value; }
        }
        string paymentDate;

        public string PaymentDate
        {
            get { return paymentDate; }
            set { paymentDate = value; }
        }
        string dewDate;

        public string DewDate
        {
            get { return dewDate; }
            set { dewDate = value; }
        }
        int daysLeft;

        public int DaysLeft
        {
            get { return daysLeft; }
            set { daysLeft = value; }
        }

        string invoiceEntryDate;

        public string InvoiceEntryDate
        {
            get { return invoiceEntryDate; }
            set { invoiceEntryDate = value; }
        }

        decimal cashDiscount;

        public decimal CashDiscount
        {
            get { return cashDiscount; }
            set { cashDiscount = value; }
        }
        decimal frieghtChgs;

        public decimal FrieghtChgs
        {
            get { return frieghtChgs; }
            set { frieghtChgs = value; }
        }
        decimal totalInvoiceAmount;

        public decimal TotalInvoiceAmount
        {
            get { return totalInvoiceAmount; }
            set { totalInvoiceAmount = value; }
        }

    }
    public class clsStockReturn
    {
        int stockReturn_ID;

        public int StockReturn_ID
        {
            get { return stockReturn_ID; }
            set { stockReturn_ID = value; }
        }

        string returnType;

        public string ReturnType
        {
            get { return returnType; }
            set { returnType = value; }
        }

        string remarks;

        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }
        string party;

        public string Party
        {
            get { return party; }
            set { party = value; }
        }
        
        int qty;

        public int Qty
        {
            get { return qty; }
            set { qty = value; }
        }
        int returnValue;

        public int ReturnValue
        {
            get { return returnValue; }
            set { returnValue = value; }
        }
        
        string returnStatus;

        public string ReturnStatus
        {
            get { return returnStatus; }
            set { returnStatus = value; }
        }

        string courierName;

        public string CourierName
        {
            get { return courierName; }
            set { courierName = value; }
        }
        string trackingId;

        public string TrackingId
        {
            get { return trackingId; }
            set { trackingId = value; }
        }
        string updatedDate;

        public string UpdatedDate
        {
            get { return updatedDate; }
            set { updatedDate = value; }
        }
    }

    public class clsExpenses
    {
        public decimal ExpenseAmount { get; set; }
        public string Remarks { get; set; }
        public string ExpName { get; set; }
        public string Date { get; set; }
    }
    public class clsEODDetails
    {

        decimal shortageAmount;

        public decimal ShortageAmount
        {
            get { return shortageAmount; }
            set { shortageAmount = value; }
        }

        decimal totalSale;

        public decimal TotalSale
        {
            get { return totalSale; }
            set { totalSale = value; }
        }

        decimal totalExpense;

        public decimal TotalExpense
        {
            get { return totalExpense; }
            set { totalExpense = value; }
        }

        decimal totalAdvance;

        public decimal TotalAdvance
        {
            get { return totalAdvance; }
            set { totalAdvance = value; }
        }

        decimal totalDeposit;

        public decimal TotalDeposit
        {
            get { return totalDeposit; }
            set { totalDeposit = value; }
        }
        decimal totapPartypayment;

        public decimal TotapPartypayment
        {
            get { return totapPartypayment; }
            set { totapPartypayment = value; }
        }

        decimal cardPayment;

        public decimal CardPayment
        {
            get { return cardPayment; }
            set { cardPayment = value; }
        }

        decimal totalDiscount;

        public decimal TotalDiscount
        {
            get { return totalDiscount; }
            set { totalDiscount = value; }
        }

        decimal counterCash;

        public decimal CounterCash
        {
            get { return counterCash; }
            set { counterCash = value; }
        }
        string eodDate;

        public string EODDate
        {
            get { return eodDate; }
            set { eodDate = value; }
        }



    }

    public class clsAdvance
    {
        public string Id { get; set; }

        decimal advanceAmount;

        public decimal AdvanceAmount
        {
            get { return advanceAmount; }
            set { advanceAmount = value; }
        }
        string empName;

        public string EmpName
        {
            get { return empName; }
            set { empName = value; }
        }

        string advanceDate;

        public string AdvanceDate
        {
            get { return advanceDate; }
            set { advanceDate = value; }
        }

        string paymentType;

        public string PaymentType
        {
            get { return paymentType; }
            set { paymentType = value; }
        }

        string remarks;

        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }

    }

    public class clsExpense
    {
        public string Id { get; set; }

        decimal expenseAmount;

        public decimal ExpenseAmount
        {
            get { return expenseAmount; }
            set { expenseAmount = value; }
        }

        string expenseType;

        public string ExpenseType
        {
            get { return expenseType; }
            set { expenseType = value; }
        }

        string remarks;

        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }

        string expenseDate;

        public string ExpenseDate
        {
            get { return expenseDate; }
            set { expenseDate = value; }
        }

    }

    public class LedgerSummary
    {
        string partyName;

        public string PartyName
        {
            get { return partyName; }
            set { partyName = value; }
        }

        decimal closingBalance;

        public decimal ClosingBalance
        {
            get { return closingBalance; }
            set { closingBalance = value; }
        }

    }

    public class clsParty
    {
        string partyName;

        public string PartyName
        {
            get { return partyName; }
            set { partyName = value; }
        }
        string partyAddress;

        public string PartyAddress
        {
            get { return partyAddress; }
            set { partyAddress = value; }
        }
        string partyContact;

        public string PartyContact
        {
            get { return partyContact; }
            set { partyContact = value; }
        }
        string partyProduct;

        public string PartyProduct
        {
            get { return partyProduct; }
            set { partyProduct = value; }
        }
        string partyBankname;

        public string PartyBankname
        {
            get { return partyBankname; }
            set { partyBankname = value; }
        }
        string partyAccount;

        public string PartyAccount
        {
            get { return partyAccount; }
            set { partyAccount = value; }
        }

        string partyId;

        public string PartyId
        {
            get { return partyId; }
            set { partyId = value; }
        }
    }

    public class clsEmployee
    {
        string empName;

        public string EmpName
        {
            get { return empName; }
            set { empName = value; }
        }

        string empId;

        public string EmpId
        {
            get { return empId; }
            set { empId = value; }
        }

        string empRole;

        public string EmpRole
        {
            get { return empRole; }
            set { empRole = value; }
        }
        string empMobile;

        public string EmpMobile
        {
            get { return empMobile; }
            set { empMobile = value; }
        }
        string empProofID;

        public string EmpProofID
        {
            get { return empProofID; }
            set { empProofID = value; }
        }
        string empProofType;

        public string EmpProofType
        {
            get { return empProofType; }
            set { empProofType = value; }
        }
    }

}