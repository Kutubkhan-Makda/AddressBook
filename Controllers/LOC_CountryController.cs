using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Reflection;
using AddressBook.Models;

namespace AddressBook.Controllers
{
    public class LOC_CountryController : Controller
    {
        private IConfiguration Configuration;

        public LOC_CountryController(IConfiguration _configuration)
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
            objCmd.CommandText = "PR_LOC_Country_SelectAll";
            
            SqlDataReader objSDR = objCmd.ExecuteReader();
            dt.Load(objSDR);
            return View("LOC_CountryList",dt);
        }


        public ActionResult Delete(int CountryID)
        {
            string connectionString = this.Configuration.GetConnectionString("SQL_AddressBook");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionString); 
            
            conn.Open();
            SqlCommand objCmd = conn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_LOC_Country_DeleteByPK";
            objCmd.Parameters.AddWithValue("@CountryID",CountryID);
            objCmd.ExecuteNonQuery();
            
            conn.Close();

            return RedirectToAction("Index");
        }


        public IActionResult Add(int? CountryID)
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

            if(CountryID!=null)
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand objCmd = conn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "PR_LOC_Country_SelectByPK";
                objCmd.Parameters.Add("@CountryID",SqlDbType.Int).Value = CountryID;
                DataTable dt = new DataTable();
                SqlDataReader objSDR = objCmd.ExecuteReader();
                dt.Load(objSDR);
                LOC_CountryModel modelLoc_Country = new LOC_CountryModel();

                foreach(DataRow dr in dt.Rows)
                {
                    modelLoc_Country.CountryID = Convert.ToInt32(dr["CountryID"]);
                    modelLoc_Country.CountryName = dr["CountryName"].ToString();
                    modelLoc_Country.CountryCode = dr["CountryCode"].ToString();
                    modelLoc_Country.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);
                }
                return View("LOC_CountryAddEdit",modelLoc_Country);
            }
            return View("LOC_CountryAddEdit");
        }


        [HttpPost]
        public IActionResult Save(LOC_CountryModel modelLoc_Country)
        {
            string connectionString = this.Configuration.GetConnectionString("SQL_AddressBook");
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand objCmd = conn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            if(modelLoc_Country.CountryID == null)
            {
                objCmd.CommandText = "PR_LOC_Country_Insert";
                objCmd.Parameters.Add("@CreationDate",SqlDbType.Date).Value = DBNull.Value;
            }
            else
            {
                objCmd.CommandText = "PR_LOC_Country_UpdateByPK";
                objCmd.Parameters.Add("@CountryID",SqlDbType.Int).Value = modelLoc_Country.CountryID;
            }
            objCmd.Parameters.Add("@CountryName",SqlDbType.VarChar).Value = modelLoc_Country.CountryName;
            objCmd.Parameters.Add("@CountryCode",SqlDbType.VarChar).Value = modelLoc_Country.CountryCode;
            objCmd.Parameters.Add("@ModificationDate",SqlDbType.Date).Value = DBNull.Value;
            
             
            if(Convert.ToBoolean(objCmd.ExecuteNonQuery()))
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
            conn.Close();
            
            return View("LOC_CountryAddEdit");
        }

        public IActionResult SearchByPage(string CountryName, string CountryCode)
        {
            //TempData["SearchInput"] = CountryName + " " + CountryCode;

            string str = this.Configuration.GetConnectionString("SQL_AddressBook");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "";
            if (CountryName == null)
            {
                CountryName = "";

            }
            if (CountryCode == null)
            {
                CountryCode = "";

            }
            cmd.Parameters.AddWithValue("@CountryName", CountryName);
            cmd.Parameters.AddWithValue("@CountryCode", CountryCode);
            DataTable dt = new DataTable();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            return View("", dt);
        }
    }
}