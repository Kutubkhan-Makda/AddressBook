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
            string connstr = this.Configuration.GetConnectionString("SQL_AddressBook");
            string error = null;

            if(modelSEC_User.UserName == null )
            {
                error  += 
            }
        }
    }
}