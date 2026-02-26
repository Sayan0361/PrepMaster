using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace PrepMaster.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(int id) //teacherID
        {
            return View();
        }

        [HttpPost]
        public JsonResult Create()
        {
            //return Json(new
            //{
            //    success = result.Success == 1,
            //    message = result.Message
            //});
            return Json(new { });
        }
    }
}