using Multi_AddressBook.DAL;
using Multi_AddressBook.BAL;
using Multi_AddressBook.Areas.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace Multi_AddressBook.Areas.LOC_City.Controllers
{
    [CheckAccess]
    [Area("LOC_City")]
    [Route("LOC_City/[Controller]/[action]")]
    public class LOC_CityController : Controller
    {
        private IConfiguration Configuration;
        public LOC_CityController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        public ActionResult Index()
        {
            LOC_DAL dalLOC = new LOC_DAL();
            DataTable dt = dalLOC.PR_LOC_City_SelectAll();
            return View("LOC_CityList", dt);
        }

        public ActionResult Delete(int CityID)
        {
            LOC_DAL dalLOC = new LOC_DAL();
            if (Convert.ToBoolean(dalLOC.PR_LOC_City_Delete(CityID)))
                return RedirectToAction("Index");
            return View("Index");
        }
        public IActionResult Add(int? CityID)
        {
            LOC_DAL dalLOC = new LOC_DAL();
            DataTable dtDropdown = dalLOC.PR_LOC_State_SelectByDropdownList();
            
            List<Areas.Models.LOC_CountryDropDownModel> listCountry = new List<Areas.Models.LOC_CountryDropDownModel>();
            foreach (DataRow dr in dtDropdown.Rows)
            {
                Areas.Models.LOC_CountryDropDownModel modelLOC_CountryDropDown = new Areas.Models.LOC_CountryDropDownModel();
                modelLOC_CountryDropDown.CountryID = (Convert.ToInt32(dr["CountryID"]));
                modelLOC_CountryDropDown.CountryName = (Convert.ToString(dr["CountryName"]));
                listCountry.Add(modelLOC_CountryDropDown);
            }
            ViewBag.CountryList = listCountry;
            
            List<Areas.Models.LOC_StateDropDownModel> listState = new List<Areas.Models.LOC_StateDropDownModel>();
            ViewBag.StateList = listState;

            if (CityID != null)
            {
                DataTable dt = dalLOC.PR_LOC_City_SelectByPK(CityID);

                if (dt.Rows.Count > 0)
                {
                    LOC_CityModel modelLOC_City = new LOC_CityModel();
                    foreach (DataRow dr in dt.Rows)
                    {
                        modelLOC_City.CityID = (Convert.ToInt32(dr["CityID"]));
                        modelLOC_City.CityName = (Convert.ToString(dr["CityName"]));
                        modelLOC_City.CityCode = (Convert.ToString(dr["CityCode"]));
                        modelLOC_City.CreationDate = (Convert.ToDateTime(dr["CreationDate"]));
                        modelLOC_City.ModificationDate = (Convert.ToDateTime(dr["ModificationDate"]));
                        modelLOC_City.StateID = (Convert.ToInt32(dr["StateID"]));
                        modelLOC_City.CountryID = (Convert.ToInt32(dr["CountryID"]));
                    }

                    String connectionstr = this.Configuration.GetConnectionString("SQL_AddressBook");
                    SqlConnection conn1 = new SqlConnection(connectionstr);
                    conn1.Open();
                    SqlCommand cmd1 = conn1.CreateCommand();
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.CommandText = "PR_LOC_State_SelectForDropDownByCountryID";
                    cmd1.Parameters.AddWithValue("@CountryID", modelLOC_City.CountryID);
                    DataTable dt1 = new DataTable();
                    SqlDataReader objSDR1 = cmd1.ExecuteReader();
                    dt1.Load(objSDR1);

                    List<Areas.Models.LOC_StateDropDownModel> listState1 = new List<Areas.Models.LOC_StateDropDownModel>();
                    foreach (DataRow dr in dt1.Rows)
                    {
                        Areas.Models.LOC_StateDropDownModel vl = new Areas.Models.LOC_StateDropDownModel();
                        vl.StateID = (Convert.ToInt32(dr["StateID"]));
                        vl.StateName = (Convert.ToString(dr["StateName"]));
                        listState1.Add(vl);
                    }
                    ViewBag.StateList = listState;

                    return View("LOC_CityAddEdit", modelLOC_City);
                }
            }
            return View("LOC_CityAddEdit");
        }
        [HttpPost]
        public IActionResult Save(LOC_CityModel modelLoc_City)
        {
            string connectionString = this.Configuration.GetConnectionString("SQL_AddressBook");
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand objCmd = conn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            if(modelLoc_City.CityID == null)
            {
                objCmd.CommandText = "PR_LOC_City_Insert";
                objCmd.Parameters.Add("@CreationDate",SqlDbType.Date).Value = DBNull.Value;
            }
            else
            {
                objCmd.CommandText = "PR_LOC_City_UpdateByPK";
                objCmd.Parameters.Add("@CityID",SqlDbType.Int).Value = modelLoc_City.CityID;
            }
            objCmd.Parameters.Add("@CityName",SqlDbType.VarChar).Value = modelLoc_City.CityName;
            objCmd.Parameters.Add("@CityCode",SqlDbType.VarChar).Value = modelLoc_City.CityCode;
            objCmd.Parameters.Add("@StateID",SqlDbType.Int).Value = modelLoc_City.StateID;
            objCmd.Parameters.Add("@CountryID",SqlDbType.Int).Value = modelLoc_City.CountryID;
            objCmd.Parameters.Add("@ModificationDate",SqlDbType.Date).Value = DBNull.Value;
             
            if(Convert.ToBoolean(objCmd.ExecuteNonQuery()))
            {
                if(modelLoc_City.CityID == null)
                {
                    TempData["CityInsetMsg"] = "Record Inserted Successfully";
                }
                else
                {
                    TempData["CityInsetMsg"] = "Record Updated Successfully";
                }
            }
            conn.Close();
            
            return RedirectToAction("Index");
        }
        public ActionResult DropDownByCountry(int CountryID)
        {
            String connectionstr = this.Configuration.GetConnectionString("SQL_AddressBook");
           
            SqlConnection conn1 = new SqlConnection(connectionstr);
            conn1.Open();
            SqlCommand cmd1 = conn1.CreateCommand();
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.CommandText = "PR_LOC_State_SelectForDropDownByCountryID";
            cmd1.Parameters.AddWithValue("@CountryID", CountryID);
            DataTable dt1 = new DataTable();
            SqlDataReader objSDR1 = cmd1.ExecuteReader();
            dt1.Load(objSDR1);
           
            List<Models.LOC_StateDropDownModel> list = new List<Models.LOC_StateDropDownModel>();
            foreach (DataRow dr in dt1.Rows)
            {
                Models.LOC_StateDropDownModel vl = new Models.LOC_StateDropDownModel();
                vl.StateID = (Convert.ToInt32(dr["StateID"]));
                vl.StateName = (Convert.ToString(dr["StateName"]));
                list.Add(vl);
            }
           
            var vmodel = list;
            return Json(vmodel);
        }
        public ActionResult Search(int CityID)
        {
            string str = this.Configuration.GetConnectionString("SQL_AddressBook");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_City_Search";
            cmd.Parameters.AddWithValue("@CityName", Convert.ToString(HttpContext.Request.Form["CityName"]));
            cmd.Parameters.AddWithValue("@CityCode", Convert.ToString(HttpContext.Request.Form["CityCode"]));
            /*cmd.Parameters.AddWithValue("@CountryName", Convert.ToString(HttpContext.Request.Form["CountryName"]));
            cmd.Parameters.Add("@CountryName", SqlDbType.VarChar).Value = CountryName;
            cmd.Parameters.Add("@CountryCode", SqlDbType.VarChar).Value = CountryCode;*/
            DataTable dt = new DataTable();
            SqlDataReader objSDR = cmd.ExecuteReader();
            dt.Load(objSDR);

            return View("LOC_CityList", dt);
        }



    }
}
