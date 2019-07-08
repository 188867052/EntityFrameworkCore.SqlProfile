using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.SqlProfile
{
    public static class SqlProfileExtention
    {
        public static int MaxCount = 100;
        public static IRouteBuilder MapSqlProfile(this IRouteBuilder routes)
        {
            routes.Routes.Add(new SqlProfileRouter(routes.DefaultHandler, EntityFrameworkLogger.DefaultRoute));
            return routes;
        }

        public static DbContextOptionsBuilder UseSqlProfile(this DbContextOptionsBuilder optionbuilder, int maxCount = 100)
        {
            if (maxCount > 0)
            {
                MaxCount = maxCount;
            }

            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new EntityFrameworkLoggerProvider());
            optionbuilder.UseLoggerFactory(loggerFactory).EnableSensitiveDataLogging();
            return optionbuilder;
        }
    }
}