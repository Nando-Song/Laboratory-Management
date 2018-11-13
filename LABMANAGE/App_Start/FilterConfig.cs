﻿using LABMANAGE.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LABMANAGE.App_Start
{
    public static class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HeadAuthorizeFilterAttribute() { IsCheck = true });//全局过滤器
        }
    }
}