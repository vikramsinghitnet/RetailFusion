﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailFusionMVC.Models
{
    public class clsUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int StoreId { get; set; }
    }
}