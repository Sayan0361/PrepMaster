using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PrepMaster.Models
{
    public class UserModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [StringLength(150)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8,
            ErrorMessage = "Password must be at least 8 characters")]
        public string PasswordHash { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; }

        public int SignUp(DynamicParameters param)
        {
            try {
                DapperConn conn = new DapperConn();
                return conn.ExecuteSingle<int>("sp_SignUp", param);
                //return 201;  // statuscode for successfully created
            }catch(SqlException ex)
            {
                if (ex.Number == 50001)
                {
                    throw new Exception ("Email already Exist");
                }
                throw;
            }
            
        }

        public UserModel LogIn(DynamicParameters param)
        {
            try
            {
                DapperConn conn = new DapperConn();
                UserModel user =  (conn.ExecuteSingleRow<UserModel>("sp_GetUserByEmail", param));
                return user;
            }
            catch(SqlException ex)
            {
                throw ex;
            }
        }

    }
}
    