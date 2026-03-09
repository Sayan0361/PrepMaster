using Dapper;
using PrepMaster.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PrepMaster.Models
{
    public class TestModel
    {
        public int TestId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TargetClassId { get; set; }
        public string ClassName { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string TestStatus { get; set; }
        public string AttemptStatus { get; set; }
        public int ScoreObtained { get; set; }
        public int TotalMarks { get; set; }
        public string ResultStatus { get; set; }
    }
    public class QuestionTableType
    {
        public string Questiontext { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public char CorrectOption { get; set; }
        public int Marks { get; set; }
    }
    public class CreateNewTestModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int CreatedByTeacherId { get; set; }
        public int TargetClassId { get; set; }
        public int SubjectId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int TotalMarks { get; set; }
        public int IsActive { get; set; }
        public List<QuestionTableType> Questions { get; set; }
    }
    public class TeacherSpecializationModel
    {
        public int SpecId { get; set; }
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
    }
    public class TestStartModel
    {
        public int TestId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int TotalMarks { get; set; }
        public string SubjectName { get; set; }
    }
    public class QuestionStartModel
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public int Marks { get; set; }
    }
    public class StartTestResponseModel
    {
        public TestStartModel Test { get; set; }
        public List<QuestionStartModel> Questions { get; set; }
        public DbResponse Response { get; set; }
    }
    public class AnswerTableTypeModel
    {
        public int QuestionId { get; set; }
        public char AttemptedOption { get; set; }
    }
    public class SubmitTestModel
    {
        public int TestID { get; set; }
        public int StudentId { get; set; }
        public List<AnswerTableTypeModel> Answers { get; set; }
    }
}

namespace PrepMaster.DAL
{
    public class TestDAL
    {
        private readonly DapperConn _conn;
        public TestDAL()
        {
            _conn = new DapperConn();
        }

         public List<TestModel> GetTestsForStudent(int StudentId)
         {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@StudentId", StudentId);

                var sp = "sp_GetTestsDetailsByStudentId";
                return _conn.ExecuteMultipleRow<TestModel>(
                        sp, 
                        param
                    );
            }
            catch (SqlException ex)
            {
                throw ex;
            }
         }

        public DbResponse CreateNewTest(
            string Title,
            string Description,
            int CreatedByTeacherId,
            int TargetClassId,
            int SubjectId,
            DateTime StartDateTime,
            DateTime EndDateTime,
            int TotalMarks,
            List<QuestionTableType> Questions,
            int IsActive = 1
        )
        {
            var proc = "sp_CreateNewTest"; 
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Title",Title);
            parameters.Add("@Description", Description);
            parameters.Add("@CreatedByTeacherId", CreatedByTeacherId);
            parameters.Add("@TargetClassId", TargetClassId);
            parameters.Add("@SubjectId", SubjectId);
            parameters.Add("@StartDateTime", StartDateTime);
            parameters.Add("@EndDateTime", EndDateTime);
            parameters.Add("@TotalMarks", TotalMarks);
            parameters.Add("@IsActive", IsActive);
            var table = new DataTable();
            table.Columns.Add("QuestionText", typeof(string));
            table.Columns.Add("OptionA", typeof(string));
            table.Columns.Add("OptionB", typeof(string));
            table.Columns.Add("OptionC", typeof(string));
            table.Columns.Add("OptionD", typeof(string));
            table.Columns.Add("CorrectOption", typeof(string));
            table.Columns.Add("Marks", typeof(int));

            foreach (var q in Questions)
            {
                table.Rows.Add(
                    q.Questiontext,
                    q.OptionA,
                    q.OptionB,
                    q.OptionC,
                    q.OptionD,
                    q.CorrectOption.ToString(),
                    q.Marks
                );
            }

            parameters.Add(
                "@Questions",
                table.AsTableValuedParameter("dbo.QuestionTableType")
            );
            return _conn.ExecuteSingleRow<DbResponse>(
                proc,
                parameters
            );
        }

        public List<TeacherSpecializationModel> GetTeacherSpecs(int TeacherId)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@TeacherId", TeacherId);

                var sp = "sp_GetTeacherSpecialization";
                return _conn.ExecuteMultipleRow<TeacherSpecializationModel>(
                        sp,
                        param
                    );
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public StartTestResponseModel StartTest(int TestID, int StudentId)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@TestID", TestID);
                parameters.Add("@StudentId", StudentId);
                var proc = "sp_StartTest";

                var result = _conn.ExecuteMultipleResult<TestStartModel, QuestionStartModel, DbResponse>(
                    proc,
                    parameters
                );

                return new StartTestResponseModel
                {
                    Test = result.Item1,
                    Questions = result.Item2 ?? new List<QuestionStartModel>(),
                    Response = result.Item3 ?? new DbResponse
                    {
                        Success = 0,
                        Message = "Unknown error."
                    }
                };
            }
            catch (SqlException)
            {
                throw;
            }
        }
        
        public DbResponse SubmitTest(
            int TestID, 
            int StudentId,
            List<AnswerTableTypeModel> Answers
        )
        {
            try
            {
                var proc = "sp_SubmitTest";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@TestID", TestID);
                parameters.Add("@StudentId", StudentId);

                var table = new DataTable();
                table.Columns.Add("QuestionId", typeof(int));
                table.Columns.Add("AttemptedOption", typeof(char));
                if (Answers != null) 
                {
                    foreach (var answer in Answers)
                    {
                        table.Rows.Add(
                            answer.QuestionId,
                            answer.AttemptedOption
                        );
                    }
                }
                if (Answers == null)
                {
                    Answers = new List<AnswerTableTypeModel>();
                }
                    
                parameters.Add(
                    "@Answers",
                    table.AsTableValuedParameter("dbo.AnswerTableType")
                );

                return _conn.ExecuteSingleRow<DbResponse>(
                    proc,
                    parameters
                );

            }
            catch(SqlException sqlex)
            {
                throw new Exception("Error submitting test", sqlex); 
            }
        }
    }
}