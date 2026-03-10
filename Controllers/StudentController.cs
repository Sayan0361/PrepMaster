using PrepMaster.DAL;
using PrepMaster.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrepMaster.Controllers
{
    public class StudentController : Controller
    {
        private readonly TestDAL testDAL;
        private readonly UserDAL studentDAL;
        private readonly TeacherDAL teacherDAL;
        public StudentController()
        {
           testDAL = new TestDAL();
           studentDAL = new UserDAL();
           teacherDAL = new TeacherDAL();
        }
        // GET: Student/id
        public ActionResult Index(int id)
        {
            List<TestModel> testList = testDAL.GetTestsForStudent(id) ?? new List<TestModel>();
            return View(testList);
        }

        public ActionResult Results()
        {
            return View();
        }

        // GET : Student/Onboard/{id}
        public ActionResult Onboard(int id)
        {
            var availableSubjectsAndClasses = teacherDAL.GetSubjectsAndClasses();
            return View(availableSubjectsAndClasses); 
        }

        [HttpPost]
        public ActionResult SaveOnboard(int UserId,int StudentClassId)
        {
            var result = studentDAL.OnboardStudent(
                        UserId, 
                        StudentClassId
            );
            return Json(new
            {
                Success = result.Success == 1,
                Message = result.Message
            });
        }
    }
}