﻿using System.Web;
using System.Web.Mvc;

namespace WSA500_Assignment3
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
