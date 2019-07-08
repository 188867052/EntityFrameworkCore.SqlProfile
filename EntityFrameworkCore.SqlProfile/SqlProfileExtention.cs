using Microsoft.AspNetCore.Routing;

namespace EntityFrameworkCore.SqlProfile
{
    public static class SqlProfileExtention
    {
        public static IRouteBuilder MapSqlProfile(this IRouteBuilder routes)
        {
            routes.Routes.Add(new SqlProfileRouter(routes.DefaultHandler, EntityFrameworkLogger.DefaultRoute));
            return routes;
        }
    }
}