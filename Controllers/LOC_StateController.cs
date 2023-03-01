using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Reflection;
using AddressBook.Models;

namespace AddressBook.Controllers
{
    public class LOC_StateController : Controller
    {
        private IConfiguration Configuration;

        public LOC_StateController(IConfiguration _configuration)
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
            objCmd.CommandText = "PR_LOC_State_SelectAll";
            
            SqlDataReader objSDR = objCmd.ExecuteReader();
            dt.Load(objSDR);
            conn.Close();

            return View("LOC_StateList",dt);
        }


        public ActionResult Delete(int StateID)
        {
            string connectionString = this.Configuration.GetConnectionString("SQL_AddressBook");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionString); 
            
            conn.Open();
            SqlCommand objCmd = conn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_LOC_State_DeleteByPK";
            objCmd.Parameters.AddWithValue("@stateID",StateID);
            objCmd.ExecuteNonQuery();
            
            conn.Close();

            return RedirectToAction("Index");
        }


        public IActionResult Add(int? StateID)
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
            conn1.Close();

            if(StateID!=null)
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand objCmd = conn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "PR_LOC_State_SelectByPK";
                objCmd.Parameters.Add("@StateID",SqlDbType.Int).Value = StateID;
                DataTable dt = new DataTable();
                SqlDataReader objSDR = objCmd.ExecuteReader();
                dt.Load(objSDR);
                LOC_StateModel modelLoc_State = new LOC_StateModel();

                foreach(DataRow dr in dt.Rows)
                {
                    modelLoc_State.StateID = Convert.ToInt32(dr["StateID"]);
                    modelLoc_State.StateName = dr["StateName"].ToString();
                    modelLoc_State.StateCode = dr["StateCode"].ToString();
                    modelLoc_State.CountryID = Convert.ToInt32(dr["CountryID"]);
                    modelLoc_State.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                    modelLoc_State.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);
                }
                return View("LOC_StateAddEdit",modelLoc_State);
            }
            return View("LOC_StateAddEdit");
        }


        [HttpPost]
        public IActionResult Save(LOC_StateModel modelLoc_State)
        {
            string connectionString = this.Configuration.GetConnectionString("SQL_AddressBook");
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand objCmd = conn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            if(modelLoc_State.StateID == null)
            {
                objCmd.CommandText = "PR_LOC_State_Insert";
                objCmd.Parameters.Add("@CreationDate",SqlDbType.Date).Value = DBNull.Value;
            }
            else
            {
                objCmd.CommandText = "PR_LOC_State_UpdateByPK";
                objCmd.Parameters.Add("@StateID",SqlDbType.Int).Value = modelLoc_State.StateID;
            }
            objCmd.Parameters.Add("@StateName",SqlDbType.VarChar).Value = modelLoc_State.StateName;
            objCmd.Parameters.Add("@StateCode",SqlDbType.VarChar).Value = modelLoc_State.StateCode;
            objCmd.Parameters.Add("@CountryID",SqlDbType.Int).Value = modelLoc_State.CountryID;
            objCmd.Parameters.Add("@ModificationDate",SqlDbType.Date).Value = DBNull.Value;
             
            if(Convert.ToBoolean(objCmd.ExecuteNonQuery()))
            {
                if(modelLoc_State.StateID == null)
                {
                    TempData["StateInsetMsg"] = "Record Inserted Successfully";
                }
                else
                {
                    TempData["StateInsetMsg"] = "Record Updated Successfully";
                }
            }
            conn.Close();
            
            return RedirectToAction("Index");
        }
    }
}