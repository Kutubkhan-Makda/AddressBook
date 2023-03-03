using AddressBook.DAL;
using AddressBook.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace AddressBook.Controllers
{
    public class LOC_CountryController : Controller
    {
        private IConfiguration Configuration;
        public LOC_CountryController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        LOC_DAL dalLOC = new LOC_DAL();
        #region SelectAll
        public ActionResult Index()
        {
            String str = this.Configuration.GetConnectionString("SQL_AddressBook");
            DataTable dt = dalLOC.PR_LOC_Country_SelectAll(str);
            return View("LOC_CountryList", dt);
        }
        #endregion

        #region Delete
        public ActionResult Delete(int CountryID)
        {
            String str = this.Configuration.GetConnectionString("SQL_AddressBook");
            if (Convert.ToBoolean(dalLOC.PR_LOC_Country_Delete(str, CountryID)))
                return RedirectToAction("Index");
            return View("Index");
        }
        #endregion


        public IActionResult Add(int? CountryID)
        {
            if (CountryID != null)
            {
                String str = this.Configuration.GetConnectionString("SQL_AddressBook");
                SqlConnection conn = new SqlConnection(str);
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PR_LOC_Country_SelectByPK";
                cmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = CountryID;
                DataTable dt = new DataTable();
                SqlDataReader objSDR = cmd.ExecuteReader();
                dt.Load(objSDR);
                LOC_CountryModel modelLOC_Country = new LOC_CountryModel();
                foreach (DataRow dr in dt.Rows)
                {
                    modelLOC_Country.CountryID = (Convert.ToInt32(dr["CountryID"]));
                    modelLOC_Country.CountryName = (Convert.ToString (dr["CountryName"]));
                    modelLOC_Country.CountryCode = (Convert.ToString (dr["CountryCode"]));
                    modelLOC_Country.CreationDate = (Convert.ToDateTime(dr["CreationDate"]));
                    modelLOC_Country.ModificationDate = (Convert.ToDateTime(dr["ModificationDate"]));
                    //modelLOC_Country.PhotoPath = (Convert.ToString(dr["PhotoPath"]));
                }
                return View("LOC_CountryAddEdit", modelLOC_Country);
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
        public ActionResult Search(int CountryID)
        {
            string str = this.Configuration.GetConnectionString("SQL_AddressBook");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_Country_Search";
            cmd.Parameters.AddWithValue("@CountryName", Convert.ToString(HttpContext.Request.Form["CountryName"]));
            cmd.Parameters.AddWithValue("@CountryCode", Convert.ToString(HttpContext.Request.Form["CountryCode"]));
            /*cmd.Parameters.Add("@CountryName", SqlDbType.VarChar).Value = CountryName;
            cmd.Parameters.Add("@CountryCode", SqlDbType.VarChar).Value = CountryCode;*/
            DataTable dt = new DataTable();
            SqlDataReader objSDR = cmd.ExecuteReader();
            dt.Load(objSDR);

            return View("LOC_CountryList", dt);
        }
    }
}
