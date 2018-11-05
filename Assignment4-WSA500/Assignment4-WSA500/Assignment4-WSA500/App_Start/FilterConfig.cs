using System.Web;
using System.Web.Mvc;

namespace Assignment4_WSA500
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
