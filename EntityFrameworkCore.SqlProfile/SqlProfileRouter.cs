﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;

namespace EntityFrameworkCore.SqlProfile
{
    internal class SqlProfileRouter : IRouter
    {
        private readonly IRouter defaultRouter;
        private readonly string routePath;

        internal SqlProfileRouter(IRouter defaultRouter, string routePath)
        {
            this.defaultRouter = defaultRouter;
            this.routePath = routePath;
        }

        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            return null;
        }

        public async Task RouteAsync(RouteContext context)
        {
            if (context.HttpContext.Request.Path == this.routePath)
            {
                var routeData = new RouteData(context.RouteData);
                routeData.Routers.Add(this.defaultRouter);
                routeData.Values["controller"] = nameof(RouteController).Replace("Controller", "");
                routeData.Values["action"] = nameof(RouteController.ShowAllSql);
                context.RouteData = routeData;
                await this.defaultRouter.RouteAsync(context);
            }
        }
    }
}