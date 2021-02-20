using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RetailFusionMVC.Filters
{
    public class SessionExpireAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            // check  sessions here
            if (HttpContext.Current.Session["store"] == null || HttpContext.Current.Request.IsAuthenticated==false)
            {
                filterContext.Result = new RedirectResult("~/Account/Index");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}