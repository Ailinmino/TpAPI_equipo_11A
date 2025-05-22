using System.Web;
using System.Web.Mvc;

namespace Tp_API_equipo_11A
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
