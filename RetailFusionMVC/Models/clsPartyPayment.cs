using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RetailFusionMVC.Models
{
    public class clsPartyPayment
    {

        public string Id { get; set; }

        string partyName;

        public string PartyName
        {
            get { return partyName; }
            set { partyName = value; }
        }
        string paymentAmount;

        public string PaymentAmount
        {
            get { return paymentAmount; }
            set { paymentAmount = value; }
        }
        string paymentDate;

        public string PaymentDate
        {
            get { return paymentDate; }
            set { paymentDate = value; }
        }
    }
}
