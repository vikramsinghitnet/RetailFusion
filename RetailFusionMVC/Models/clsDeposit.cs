using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RetailFusionMVC.Models
{
    public class clsDeposit
    {
        public string Id { get; set; }

        string depositAmount;

        public string DepositAmount
        {
            get { return depositAmount; }
            set { depositAmount = value; }
        }
        string depositBank;

        public string DepositBank
        {
            get { return depositBank; }
            set { depositBank = value; }
        }
        string depositDate;

        public string DepositDate
        {
            get { return depositDate; }
            set { depositDate = value; }
        }
    }
}
