using Dapper;
using PrepMaster.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BCrypt.Net;


namespace PrepMaster.Controllers
{
    public class AuthController : Controller
    {
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SignUp(string FullName,string Email, string Password,string Role)
        {
            DynamicParameters param = new DynamicParameters();
            string PasswordHash = BCrypt.Net.BCrypt.HashPassword(Password);
            param.Add("FullName", FullName);
            param.Add("Email", Email);
            param.Add("PasswordHash", PasswordHash);
            param.Add("Role", Role);

            UserModel md = new UserModel();
            try
            {
                int createdUserID = md.SignUp(param);
                return Json(
                        new
                        {
                            success = true,
                            StatusCode = 201,
                            Message = "User Created Successfully",
                            Data = new { id = createdUserID },
                            Error = "",
                        }
                );
            }
            catch(Exception ex)
            {
                return Json(
                    new
                    {
                        success = false,
                        StatusCode = 409,
                        Message = ex.Message,
                        Data = new {},
                        Error = "",
                    }
                );
            }          
            
        }

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public JsonResult LogIn(string Email, string Password, string Role)
        {
            DynamicParameters param = new DynamicParameters();
            //string PasswordHash = BCrypt.Net.BCrypt.HashPassword(Password);
            param.Add("Email", Email);
            
            
            try
            {
                UserModel md = new UserModel();
                UserModel user = md.LogIn(param);

                string userHashedPassword = user.PasswordHash;
                bool isValidLogin = BCrypt.Net.BCrypt.Verify(Password, userHashedPassword);

                string roleFetchedFromDB = user.Role;

                if(isValidLogin && roleFetchedFromDB == Role)
                {
                    return Json(
                         new
                         {
                             success = true,
                             StatusCode = 200,
                             Message = "User logged In suceesfully",
                             Data = new { id = user.UserId, UserName = user.FullName },
                             Error = "",
                         }
                    );
                }
                else if(roleFetchedFromDB != Role)
                {
                    return Json(
                         new
                         {
                             StatusCode = 403,
                             Message = "Please verify your credentials.",
                             Data = new { },
                             Error = "",
                         }
                    );
                }
                else
                {
                    return Json(
                         new
                         {
                             StatusCode = 403,
                             Message = "Incorrect Password",
                             Data = new {  },
                             Error = "",
                         }
                    );
                }
                
            }
            catch (SqlException ex)
            {
                if(ex.Number == 50001)
                {
                    return Json(
                        new
                        {
                            StatusCode = 404,
                            Message = "Email does not exist",
                            Data = new {},
                            Error = "",
                        }
                    );
                }
                else
                {
                    return Json(
                        new
                        {
                            StatusCode = 500,
                            Message = "Unexpected Error",
                            Data = new { },
                            Error = "",
                        }
                    );
                }

            }

        }

    }
}