using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using AddressBook.Models;
using AddressBook.DAL;

namespace AddressBook.Controllers
{
    public class LOC_StateController : Controller
    {
        private IConfiguration Configuration;
        public LOC_StateController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        LOC_DAL dalLOC = new LOC_DAL();
        public IActionResult Index()
        {
            String str = this.Configuration.GetConnectionString("SQL_AddressBook");
            DataTable dt1 = dalLOC.PR_LOC_State_SelectAll(str);
            return View("LOC_StateList", dt1);
            
        }
        public ActionResult Delete(int StateID)
        {
            String str = this.Configuration.GetConnectionString("SQL_AddressBook");
            if (Convert.ToBoolean(dalLOC.PR_LOC_State_Delete(str, StateID)))
                return RedirectToAction("Index");
            return View("Index");
        }
        public IActionResult Add(int? StateID)
         {

            String connectionstr = this.Configuration.GetConnectionString("SQL_AddressBook");
            DataTable dt1 = dalLOC.PR_LOC_State_SelectByDropdownList(connectionstr);
            
            List<LOC_CountryDropDownModel> list=new List<LOC_CountryDropDownModel>();
            foreach(DataRow dr in dt1.Rows)
            {
                LOC_CountryDropDownModel modelLOC_CountryDropDown = new LOC_CountryDropDownModel();
                modelLOC_CountryDropDown.CountryID = (Convert.ToInt32(dr["CountryID"]));
                modelLOC_CountryDropDown.CountryName = (Convert.ToString(dr["CountryName"]));
                list.Add(modelLOC_CountryDropDown);
            }
            ViewBag.CountryList=list;
            if (StateID != null)
            {
                String str = this.Configuration.GetConnectionString("SQL_AddressBook");
                SqlConnection conn = new SqlConnection(str);
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PR_LOC_State_SelectByPK";
                cmd.Parameters.Add("@StateID", SqlDbType.Int).Value = StateID;
                DataTable dt = new DataTable();
                SqlDataReader objSDR = cmd.ExecuteReader();
                dt.Load(objSDR);
                if(dt.Rows.Count > 0)
                {
                    LOC_StateModel modelLOC_State = new LOC_StateModel();
                    foreach (DataRow dr in dt.Rows)
                    {
                        modelLOC_State.StateID = (Convert.ToInt32(dr["StateID"]));
                        modelLOC_State.StateName = (Convert.ToString(dr["StateName"]));
                        modelLOC_State.StateCode = (Convert.ToString(dr["StateCode"]));
                        modelLOC_State.CreationDate = (Convert.ToDateTime(dr["CreationDate"]));
                        modelLOC_State.ModificationDate = (Convert.ToDateTime(dr["ModificationDate"]));
                        modelLOC_State.CountryID = (Convert.ToInt32(dr["CountryID"]));
                    }
                    conn.Close();
                    return View("LOC_StateAddEdit", modelLOC_State);
                } 
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
        public ActionResult Search(int StateID)
        {
            string str = this.Configuration.GetConnectionString("SQL_AddressBook");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_State_Search";
            cmd.Parameters.AddWithValue("@StateName", Convert.ToString(HttpContext.Request.Form["StateName"]));
            cmd.Parameters.AddWithValue("@StateCode", Convert.ToString(HttpContext.Request.Form["StateCode"]));
            /*cmd.Parameters.AddWithValue("@CountryName", Convert.ToString(HttpContext.Request.Form["CountryName"]));
            cmd.Parameters.Add("@CountryName", SqlDbType.VarChar).Value = CountryName;
            cmd.Parameters.Add("@CountryCode", SqlDbType.VarChar).Value = CountryCode;*/
            DataTable dt = new DataTable();
            SqlDataReader objSDR = cmd.ExecuteReader();
            dt.Load(objSDR);

            return View("LOC_StateList", dt);
        }
    }
}
