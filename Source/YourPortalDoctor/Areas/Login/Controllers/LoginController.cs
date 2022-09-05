using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using YourPortalDoctor.Areas.Login.Models;
using YourPortalDoctor.Areas.YourPortalDoctor.Data;
using YourPortalDoctor.Common;
using YPD_Demo.Common;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;

namespace YourPortalDoctor.Controllers
{
    [Area("Login")]
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILoginRespository _LoginRespository;

        public LoginController(IConfiguration configuration, ILoginRespository LoginRespository)
        {
            _configuration = configuration;
            _LoginRespository = LoginRespository;
        }
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [Route(URLRouting.Login.Signin)]
        public ActionResult Signin()
        {
            return View("Login");
        }
        //Get Registration Form
        [Route(URLRouting.Login.Registration)]
        public IActionResult Registration()
        {
            return View();
        }
        [Route(URLRouting.Login.Overview)]
        public IActionResult Overview()
        {
            return View();
        }
        // Get Doctor Dashboard
        [Route(URLRouting.Login.Dashboard)]
        public IActionResult Dashboard()
        {
            return View();
        }
        // Get Patient Dashboard
        [Route(URLRouting.Login.PatientDashboard)]
        public IActionResult PatientDashboard()
        {
            return View();
        }
        //Registration form 
        public IActionResult SaveRegisterDetails(EntryModel objModel)
        {
            try
            {
                _LoginRespository.SaveRegisterDetails(objModel);
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.SendErrorToText(ex);
            }
            return RedirectToAction("Login");
        }
        public IActionResult LoginIn(Loginmodel objModel)
        {
            
                //return RedirectToAction("~/addproduct");
                string connString = _configuration.GetValue<string>("ConnStrings:ConnString");
                SqlConnection con = new SqlConnection(connString);//system.Data.Sqllite 
                con.Open();

                //string query = "select RoleId="+ objModel.RoleId +" from tblUsers where Email_Id="+ objModel.UserName+ " and password="+ objModel.Password+"";
                string query = "select Roll_Id from tblRegister where UserName='" + objModel.UserName + " 'and Password='" + objModel.Password + "'";
            
                SqlCommand cmd = new SqlCommand(query, con);
                Loginmodel user = new Loginmodel();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    user.RollId = Convert.ToInt32(reader["Roll_Id"]);
                }
                string Area = "";
                string Controller = "";
                string ControllerAction = "";
            try
            {

                if (user.RollId == 1)
                {
                    HttpContext.Session.SetInt32("CurrentLayout", 1);
                    Area = "Login";
                   Controller = "Login";
                   ControllerAction = "Dashboard";

                   // return RedirectToAction("Dashboard");
                }
                else if (user.RollId == 2)
                {
                    HttpContext.Session.SetInt32("CurrentLayout", 2);
                    Area = "Login";
                    Controller = "Login";
                    ControllerAction = "PatientDashboard";
                   // return RedirectToAction("PatientDashboard");
                }

                else
                {
                    ViewBag.Message = "Invalid Username or Password";
                    return View("Login");

                }


                // string GetValues = HttpContext.Session.GetString("CurrentLayout");
                con.Close();
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.SendErrorToText(ex);
            }
            return RedirectToAction(ControllerAction, Controller, new { area = Area });
        }
    }
}