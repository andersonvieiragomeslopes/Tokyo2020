using System.Web;
using System.Web.Mvc;

namespace Tokyo2020.AdministrativePanel
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
