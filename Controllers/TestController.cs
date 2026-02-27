using PrepMaster.DAL;
using PrepMaster.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace PrepMaster.Controllers
{
    public class TestController : Controller
    {
        private readonly TestDAL _dal;
        public TestController()
        {
            _dal = new TestDAL();
        }

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
        public ActionResult Create(CreateNewTestModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    Success = false,
                    Message = "Invalid data submitted."
                });
            }
            if (model.Questions == null || !model.Questions.Any())
            {
                return Json(new { 
                    Success = false,
                    Message = "At least one question required." 
                });
            }
            var result = _dal.CreateNewTest(
                model.Title,
                model.Description,
                model.CreatedByTeacherId,
                model.TargetClassId,
                model.SubjectId,
                model.StartDateTime,
                model.EndDateTime,
                model.TotalMarks,
                model.Questions,
                model.IsActive
            );
            return Json(new { 
                Success = result.Success == 1,
                Message = result.Message
            });
        }
    }
}