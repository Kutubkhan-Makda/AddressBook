using AddressBook.DAL;
using AddressBook.Areas.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace AddressBook.Areas.SEC_User.Controllers
{
    [Area("SCE_User")]
    [Route("SCE_User/[Controller]/[action]")]

    public class SEC_User : Controller
    {
        private IConfiguration Configuration;
        public SEC_User(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(SEC_UserModel modelSEC_User)
        {
            String connstr = this.Configuration.GetConnectionString("SQL_AddressBook");
            String error = null;

            if(modelSEC_User.UserName == null )
            {
                error  += "User Name is Required";
            }
            if(modelSEC_User.Password == null )
            {
                error  += "<br/>Password is Required";
            }

            if(error != null)
            {
                TempData["Error"] = error;
                return RedirectToAction("Index");
            }
            else{
                SEC_DAL dal = new SEC_DAL();
                DataTable dt = dal.PR_User_SelectByIDPass(connstr,modelSEC_User.UserName,modelSEC_User.Password);
                if(dt.Rows.Count > 0)
                {
                    foreach(DataRow dr in dt.Rows)
                    {
                        HttpContext.Session.SetString("UserName",dr["UserName"].ToString());
                        HttpContext.Session.SetString("Password",dr["Password"].ToString());
                        break;
                    }
                }
                else
                {
                    TempData["Error"] = "User Name and Password is Incorect";
                    return RedirectToAction("Index");
                }
                if(HttpContext.Session.GetString("UserName") != null && HttpContext.Session.GetString("Password") != null)
                {
                    return RedirectToAction("Index","Home");
                }
            }
            return RedirectToAction("Index");
        }
    }
}