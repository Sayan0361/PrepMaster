using Dapper;
using PrepMaster.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace PrepMaster.Models
{
    public class Teacher
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8)]
        public string PasswordHash { get; set; }

        [Required]
        public int Role { get; set; }
    }
    public class GetSubjectsAndClassesVM
    {
        public int MatchId { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
    }
    public class TeacherOnboardingVM
    {
        public int TeacherId { get; set; }
        public List<GetSubjectsAndClassesVM> AvailableSubjects { get; set; }
    }
    public class DbResponse
    {
        public int Success { get; set; }
        public string Message { get; set; }
    }

    public class TeacherTestVM
    {
        public int TestId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public int CreatedByTeacherId { get; set; }
        public string FullName { get; set; }
        public int TargetClassId { get; set; }
        public string ClassName { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int TotalMarks { get; set; }
        public string TestStatus { get; set; }
    }

    public class TeacherDashboardVM
    {
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public int TotalTests{ get; set; }

        public int UpcomingTests { get; set; }
        public int ActiveTests { get; set; }
        public int ExpiredTests { get; set; }
        public int TotalStudentsAssigned { get; set; } 
        
        public List<TeacherTestVM> Tests { get; set; }
    }

}

namespace PrepMaster.DAL
{
    public class TeacherDAL
    {
        private readonly DapperConn _conn;
        public TeacherDAL()
        {
            _conn = new DapperConn();
        }

        public List<GetSubjectsAndClassesVM> GetSubjectsAndClasses()
        {
            var proc = "sp_GetSubjectsAndClasses";
            return _conn.ExecuteMultipleRow<GetSubjectsAndClassesVM>(proc, null);
        }

        public DbResponse AddTeacherSpecialization(int TeacherId, string MatchIdList)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@TeacherId", TeacherId);
            parameters.Add("@MatchIdList", MatchIdList);

            var proc = "sp_AddTeacherSpecialization";

            return _conn.ExecuteSingleRow<DbResponse>(
                proc,
                parameters
            );
        }

        public TeacherDashboardVM GetTeacherDashboardDetails(int TeacherId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@TeacherId", TeacherId);

            var proc = "sp_getTeacherDashboardDetails";
            return _conn.ExecuteSingleRow<TeacherDashboardVM>(
                proc,
                parameters
                );
        }

        public List<TeacherTestVM> GetTestsDetailsByTeacherId(int TeacherId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@TeacherId", TeacherId);

            var proc = "sp_GetTestsDetailsByTeacherId";
            return _conn.ExecuteMultipleRow<TeacherTestVM>(
                proc,
                parameters
                );
        }
    }
}