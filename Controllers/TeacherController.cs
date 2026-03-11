using Dapper;
using PrepMaster.DAL;
using PrepMaster.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace PrepMaster.Controllers
{
    public class TeacherController : Controller
    {
        private readonly TeacherDAL _dal;
        private readonly TestDAL _Testdal;
        public TeacherController()
        {
            _dal = new TeacherDAL();
            _Testdal = new TestDAL();
        }
        // GET: Teacher
        public ActionResult Index(int id)
        {
            TeacherDashboardVM model = new TeacherDashboardVM();
            var testDetails = _dal.GetTestsDetailsByTeacherId(id);
            var teacherDetails = _dal.GetTeacherDashboardDetails(id);

            model.TeacherId = teacherDetails.TeacherId;
            model.TeacherName = teacherDetails.TeacherName;
            model.TotalTests = teacherDetails.TotalTests;
            model.UpcomingTests = teacherDetails.UpcomingTests;
            model.ActiveTests = teacherDetails.ActiveTests;
            model.ExpiredTests  = teacherDetails.ExpiredTests;
            model.TotalStudentsAssigned = teacherDetails.TotalStudentsAssigned;

            model.Tests = testDetails;

            //model.TestId = testDetails.TestId;
            //model.Title = testDetails.Title;
            //model.Description = testDetails.Description;   
            //model.CreatedByTeacherId = testDetails.CreatedByTeacherId;
            //model.FullName = testDetails.FullName;
            //model.TargetClassId = testDetails.TargetClassId;
            //model.ClassName = testDetails.ClassName;
            //model.SubjectId = testDetails.SubjectId;
            //model.SubjectName = testDetails.SubjectName;
            //model.StartDateTime = testDetails.StartDateTime;
            //model.EndDateTime = testDetails.EndDateTime;
            //model.TotalMarks = testDetails.TotalMarks;
            //model.TestStatus = testDetails.TestStatus;

            return View(model);
        }

        // Onboard the Teacher : Teacher must fulfill the details
        // Details : Select Subjects and Classes
        public ActionResult Onboarding(int id)
        {
            var availableSubjectsAndClasses = _dal.GetSubjectsAndClasses();
            var viewmodel = new TeacherOnboardingVM
            {
                TeacherId = id,
                AvailableSubjects = availableSubjectsAndClasses
            };

            return View(viewmodel);
        }

        // Post req : for adding TeacherSpealization
        // TeacherId and MatchIdList will come from Jquery ajax call
        // MatchIdList = "1,2,3,4"
        [HttpPost]
        public ActionResult AddSpecialization(int TeacherId, string MatchIdList)
        {
            var result = _dal.AddTeacherSpecialization(TeacherId, MatchIdList);

            return Json(new
            {
                success = result.Success == 1,
                message = result.Message
            });
        }

        public ActionResult Analytics()
        {
            return View();
        }
        
        public JsonResult AnalyticsData(int TeacherId)
        {
            var testDetails = _Testdal.GetTestResultByTeacherId(TeacherId);
            return Json(new { testDetails }, JsonRequestBehavior.AllowGet);
        }
    }
}