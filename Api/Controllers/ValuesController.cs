using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : Controller
    {
        private readonly SqliteDBContext dbContext;

        public ValuesController(SqliteDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public JsonResult Insert()
        {
            Category entity = new Category
            {
                Name = "Harry Cheng",
                Time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                Guid = Guid.NewGuid().ToString("N"),
            };

            dbContext.Set<Category>().Add(entity);
            dbContext.SaveChanges();
            return Json(entity);
        }

        [HttpGet]
        public JsonResult Select()
        {
            var data = dbContext.Set<Category>().ToList();
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
            var category = dbContext.Set<Category>().FirstOrDefault();
            if (category != null)
            {
                dbContext.Remove(category);
                dbContext.SaveChanges();

                return Json(category);
            }

            return Json("Nothing Deleted.");
        }

        [HttpGet]
        public JsonResult Update()
        {
            var category = dbContext.Set<Category>().FirstOrDefault();
            if (category != null)
            {
                category.Time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                dbContext.SaveChanges();

                return Json(category);
            }

            return Json("Nothing Updated.");
        }
    }
}
