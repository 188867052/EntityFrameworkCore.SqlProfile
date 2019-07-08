using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SqlServerController : Controller
    {
        private readonly SqlServerDbContext dbContext;

        public SqlServerController(SqlServerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public JsonResult Insert()
        {
            Log entity = new Log
            {
                Message = "Harry Cheng",
                CreateTime = DateTime.Now,
            };

            dbContext.Set<Log>().Add(entity);
            dbContext.SaveChanges();
            return Json(entity);
        }

        [HttpGet]
        public JsonResult Select()
        {
            var data = dbContext.Set<Log>().ToList();
            if (data.Count > 0)
            {
                return Json(data);
            }

            return Json("No data");
        }

        // POST api/values
        [HttpGet]
        public JsonResult Delete()
        {
            var log = dbContext.Set<Log>().FirstOrDefault();
            if (log != null)
            {
                dbContext.Remove(log);
                dbContext.SaveChanges();

                return Json(log);
            }

            return Json("Nothing Deleted.");
        }

        [HttpGet]
        public JsonResult Update()
        {
            var log = dbContext.Set<Log>().FirstOrDefault();
            if (log != null)
            {
                log.CreateTime = DateTime.Now;
                dbContext.SaveChanges();

                return Json(log);
            }

            return Json("Nothing Updated.");
        }
    }
}
