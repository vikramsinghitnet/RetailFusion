﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RetailFusionMVC.Controllers
{
    public class PublicController : Controller
    {
        //
        // GET: /Public/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

    }
}
