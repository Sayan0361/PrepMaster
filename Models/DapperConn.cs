using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;

namespace PrepMaster.Models
{
    public class DapperConn
    {
        public readonly string connectionString = null;
        public DapperConn()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        }
        // RETURN NO VALUE.
        public void ExecuteWithoutReturn(string procName, DynamicParameters param = null)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();
                conn.Execute(procName, param, commandType: CommandType.StoredProcedure);
            }
        }
        // RETURN SINGLE ROW.
        public T ExecuteSingleRow<T>(string procName, DynamicParameters param = null)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();
                return conn.QueryFirstOrDefault<T>(procName, param, commandType: CommandType.StoredProcedure);
            }
        }
        // RETURN MULTIPLE ROW AS LIST.
        public List<T> ExecuteMultipleRow<T>(string procName, DynamicParameters param)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();
                return conn.Query<T>(procName, param, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        // RETURN SINGLE VALUE LIKE ID, COUNT ETC.
        public T ExecuteSingle<T>(string procName, DynamicParameters param)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                return conn.ExecuteScalar<T>(procName, param, commandType: CommandType.StoredProcedure);
            }
        }

        // Get Multiple Model Results 
        public (T1, List<T2>, T3) ExecuteMultipleResult<T1, T2, T3>(
    string procName,
    DynamicParameters param)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (var multi = conn.QueryMultiple(procName, param, commandType: CommandType.StoredProcedure))
                {
                    var tests = multi.Read<T1>().FirstOrDefault();
                    var questions = multi.Read<T2>().ToList();
                    var response = multi.Read<T3>().FirstOrDefault();

                    return (tests, questions, response);
                }
            }
        }

        public (List<T1>, List<T2>, List<T3>) ExecuteThreeLists<T1, T2, T3>(string procName,
    DynamicParameters param)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (var multi = conn.QueryMultiple(procName, param, commandType: CommandType.StoredProcedure))
                {
                    
                    var Item1 = multi.Read<T1>().ToList();
                    var Item2 = multi.Read<T2>().ToList();
                    var Item3 = multi.Read<T3>().ToList();

                    return (Item1, Item2, Item3);
                }
            }

        }
    }
}