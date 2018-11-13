using LABMANAGE.UntityCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LABMANAGE.Filter
{
    public class HeadAuthorizeFilterAttribute : ActionFilterAttribute
    {
        public bool IsCheck { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            string controller = filterContext.RouteData.Values["controller"].ToString();
            string action = filterContext.RouteData.Values["action"].ToString();
            if (IsCheck)
            {
                if (filterContext.HttpContext.Session != null)
                {
                    UserInfo user = (UserInfo)filterContext.HttpContext.Session["userbase"];
                    if (user != null && !string.IsNullOrEmpty(user.ID.Trim()) && !string.IsNullOrEmpty(user.RoleId.Trim()))
                    {
                        return;
                    }
                }
                filterContext.Result = new RedirectToRouteResult("Default", new RouteValueDictionary(new { controller = "Login", action = "Login" }));
            }
        }
    }
}