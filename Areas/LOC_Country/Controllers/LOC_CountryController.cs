using Multi_AddressBook.DAL;
using Multi_AddressBook.BAL;
using Multi_AddressBook.Areas.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace Multi_AddressBook.Areas.LOC_Country.Controllers
{
    [CheckAccess]
    [Area("LOC_Country")]
    [Route("LOC_Country/[Controller]/[action]")]
    public class LOC_CountryController : Controller
    {
        private IConfiguration Configuration;
        public LOC_CountryController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        #region SelectAll
        public ActionResult Index()
        {
            LOC_DAL dalLOC = new LOC_DAL();
            DataTable dt = dalLOC.PR_LOC_Country_SelectAll();
            return View("LOC_CountryList", dt);
        }
        #endregion

        #region Delete
        public ActionResult Delete(int CountryID)
        {
            LOC_DAL dalLOC = new LOC_DAL();
            if (Convert.ToBoolean(dalLOC.PR_LOC_Country_Delete(CountryID)))
                return RedirectToAction("Index");
            return View("Index");
        }
        #endregion


        public IActionResult Add(int? CountryID)
        {
            LOC_DAL dalLOC = new LOC_DAL();
            if (CountryID != null)
            {
                DataTable dt = dalLOC.PR_LOC_Country_SelectByPK(CountryID);

                LOC_CountryModel modelLOC_Country = new LOC_CountryModel();
                foreach (DataRow dr in dt.Rows)
                {
                    modelLOC_Country.CountryID = (Convert.ToInt32(dr["CountryID"]));
                    modelLOC_Country.CountryName = (Convert.ToString (dr["CountryName"]));
                    modelLOC_Country.CountryCode = (Convert.ToString (dr["CountryCode"]));
                    modelLOC_Country.CreationDate = (Convert.ToDateTime(dr["CreationDate"]));
                    modelLOC_Country.ModificationDate = (Convert.ToDateTime(dr["ModificationDate"]));
                }
                return View("LOC_CountryAddEdit", modelLOC_Country);
            }
            return View("LOC_CountryAddEdit");
        }
        [HttpPost]
        public IActionResult Save(LOC_CountryModel modelLoc_Country)
        {
            LOC_DAL dalLOC = new LOC_DAL();
             
            if(Convert.ToBoolean(dalLOC.PR_LOC_Save_Country(modelLoc_Country.CountryID,modelLoc_Country.CountryName,modelLoc_Country.CountryCode)))
            {
                if(modelLoc_Country.CountryID == null)
                {
                    TempData["CountryInsetMsg"] = "Record Inserted Successfully";
                }
                else
                {
                    TempData["CountryInsetMsg"] = "Record Updated Successfully";
                }
            }
            
            return RedirectToAction("Index");
        }
        public ActionResult Search(int CountryID)
        {
            string str = this.Configuration.GetConnectionString("SQL_AddressBook");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_Country_Search";
            cmd.Parameters.AddWithValue("@CountryName", Convert.ToString(HttpContext.Request.Form["CountryName"]));
            DataTable dt = new DataTable();
            SqlDataReader objSDR = cmd.ExecuteReader();
            dt.Load(objSDR);

            return View("LOC_CountryList", dt);
        }
    }
}
