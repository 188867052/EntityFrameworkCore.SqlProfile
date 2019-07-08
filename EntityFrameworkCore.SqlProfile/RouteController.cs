using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Text;

namespace EntityFrameworkCore.SqlProfile
{
    public class RouteController : Controller
    {
        [HttpGet]
        [Route(EntityFrameworkLogger.DefaultRoute)]
        public IActionResult ShowAllSql()
        {
            var html = EntityFrameworkLogger.GetHtml(EntityFrameworkLogger.SqlInfoCache);
            return this.Content(html, "text/html", Encoding.UTF8);
        }
    }
}