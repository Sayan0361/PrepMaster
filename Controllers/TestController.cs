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

        public JsonResult GetTeacherSpecs(int TeacherId) //teacherID
        {
            var teacherSpecs = _dal.GetTeacherSpecs(TeacherId);
            return Json(new { teacherSpecs }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            //var teacherSpecsList = _dal.GetTeacherSpecs(id);
            //return View(teacherSpecsList);
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

        public ActionResult StartTest()
        {
            return View();
        }

        public JsonResult StartTestJSONData(int TestID, int StudentId)
        {
            var result = _dal.StartTest(TestID, StudentId);

            if (result == null || result.Response == null)
            {
                return Json(new
                {
                    Success = false,
                    Message = "Something went wrong."
                }, JsonRequestBehavior.AllowGet);
            }

            if (result.Response.Success == 0)
            {
                return Json(new
                {
                    Success = false,
                    Message = result.Response.Message
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                Success = result.Response.Success == 1,
                Message = result.Response.Message,
                Test = result.Test,
                Questions = result.Questions
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SubmitTest(int TestID, int StudentId, List<AnswerTableTypeModel> Answers)
        {

            if (TestID == 0)
            {
                return Json(new
                {
                    Success = "false",
                    Message = "TestID missing",
                });
            }

            if (StudentId == 0)
            {
                return Json(new
                {
                    Success = "false",
                    Message = "StudentID missing",
                });
            }

            if(Answers == null && !Answers.Any())
            {
                return Json(new
                {
                    Success = "false",
                    Message = "No Answers were attempted.",
                });
            }

            var response = _dal.SubmitTest(
                    TestID,
                    StudentId,
                    Answers
                );

            return Json(new
            {
                Success = response.Success == 1,
                Message = response.Message
            });
        }
    }
}