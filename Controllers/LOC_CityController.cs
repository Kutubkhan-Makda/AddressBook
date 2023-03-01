using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Reflection;
using AddressBook.Models;

namespace AddressBook.Controllers
{
    public class LOC_CityController : Controller
    {
        private IConfiguration Configuration;

        public LOC_CityController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        public ActionResult Index()
        {
            string connectionString = this.Configuration.GetConnectionString("SQL_AddressBook");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionString); 
            
            conn.Open();
            SqlCommand objCmd = conn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_LOC_City_SelectAll";
            
            SqlDataReader objSDR = objCmd.ExecuteReader();
            dt.Load(objSDR);
            conn.Close();

            return View("LOC_CityList",dt);
        }


        public ActionResult Delete(int CityID)
        {
            string connectionString = this.Configuration.GetConnectionString("SQL_AddressBook");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionString); 
            
            conn.Open();
            SqlCommand objCmd = conn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_LOC_City_DeleteByPK";
            objCmd.Parameters.AddWithValue("@CityID",CityID);
            objCmd.ExecuteNonQuery();
            
            conn.Close();

            return RedirectToAction("Index");
        }


        public IActionResult Add(int? CityID)
        {
            string connectionString = this.Configuration.GetConnectionString("SQL_AddressBook");
            SqlConnection conn1 = new SqlConnection(connectionString);
            conn1.Open();
            SqlCommand objCmd1 = conn1.CreateCommand();
            objCmd1.CommandType = CommandType.StoredProcedure;
            objCmd1.CommandText = "PR_LOC_Country_SelectForDropDown";
            DataTable dt1 = new DataTable();
            SqlDataReader objSDR1 = objCmd1.ExecuteReader();
            dt1.Load(objSDR1);

            List<LOC_CountryDropDownModel> list = new List<LOC_CountryDropDownModel>();
            foreach(DataRow dr in dt1.Rows)
            {
                LOC_CountryDropDownModel vlst = new LOC_CountryDropDownModel();
                vlst.CountryID = Convert.ToInt32(dr["CountryID"]);
                vlst.CountryName = dr["CountryName"].ToString();
                list.Add(vlst);
            }
            ViewBag.CountryList = list;

            List<LOC_CountryDropDownModel> LOC_StateDropdown_List = new List<LOC_CountryDropDownModel>();
            ViewBag.StateList = LOC_StateDropdown_List;

            if(CityID!=null)
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand objCmd = conn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "PR_LOC_City_SelectByPK";
                objCmd.Parameters.Add("@CityID",SqlDbType.Int).Value = CityID;
                DataTable dt = new DataTable();
                SqlDataReader objSDR = objCmd.ExecuteReader();
                dt.Load(objSDR);
                LOC_CityModel modelLoc_City = new LOC_CityModel();

                foreach(DataRow dr in dt.Rows)
                {
                    modelLoc_City.CityID = Convert.ToInt32(dr["CityID"]);
                    modelLoc_City.CityName = dr["CityName"].ToString();
                    modelLoc_City.CityCode = dr["CityCode"].ToString();
                    modelLoc_City.StateID = Convert.ToInt32(dr["StateID"]);
                    modelLoc_City.CountryID = Convert.ToInt32(dr["CountryID"]);
                    modelLoc_City.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                    modelLoc_City.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);
                }
                return View("LOC_CityAddEdit",modelLoc_City);
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

        [HttpPost]
        public IActionResult DropdownByCountryID(int CountryID)
        {
            string connectionString = this.Configuration.GetConnectionString("SQL_AddressBook");
            SqlConnection conn = new SqlConnection(connectionString);
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand objCmd = conn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_LOC_Country_SelectForDropDownByCountryID";
            objCmd.Parameters.AddWithValue("@CountryID",CountryID);
            SqlDataReader objSDR = objCmd.ExecuteReader();
            dt.Load(objSDR);
            conn.Close();

            List<LOC_StateDropDownModel> list = new List<LOC_StateDropDownModel>();
            foreach(DataRow dr in dt.Rows)
            {
                LOC_StateDropDownModel vlst = new LOC_StateDropDownModel();
                vlst.StateID = Convert.ToInt32(dr["StateID"]);
                vlst.StateName = dr["StateName"].ToString();
                list.Add(vlst);
            }
            var vModel = list;
            return Json(vModel);
        }

    }
}