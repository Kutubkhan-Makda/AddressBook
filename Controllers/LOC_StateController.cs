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
        public IActionResult Index()
        {
            String str = this.Configuration.GetConnectionString("myConnectionString");
            LOC_DAL dalLOC= new LOC_DAL();
            DataTable dt1 = dalLOC.dbo_PR_LOC_State_SelectAll(str);
            return View("LOC_StateList", dt1);
            



        }
        public ActionResult Delete(int StateID)
        {
            String str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_State_DeleteByPK";
            cmd.Parameters.AddWithValue("@StateID", StateID);
            DataTable dt = new DataTable();
            cmd.ExecuteNonQuery();
            conn.Close();


            return RedirectToAction("Index");
        }
        public IActionResult Add(int? StateID)
         {

            String connectionstr = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt1 = new DataTable();
            SqlConnection conn1 = new SqlConnection(connectionstr);
            conn1.Open();
            SqlCommand cmd1 = conn1.CreateCommand();
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.CommandText = "PR_LOC_Country_SelectForDropDown";
            SqlDataReader objSDR1 = cmd1.ExecuteReader();
            dt1.Load(objSDR1);
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
                String str = this.Configuration.GetConnectionString("myConnectionString");
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
                        modelLOC_State.PhotoPath = (Convert.ToString(dr["PhotoPath"]));
                    }
                    conn.Close();
                    return View("LOC_StateAddEdit", modelLOC_State);
                } 
            }
            return View("LOC_StateAddEdit");
        }
        [HttpPost]
        public ActionResult Save(LOC_StateModel modelLOC_State)
        {
            if (modelLOC_State.File != null)
            {
                string FilePath = "wwwroot\\Upload";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                string fileNameWithPath = Path.Combine(path, modelLOC_State.File.FileName);
                modelLOC_State.PhotoPath = "~" + FilePath.Replace("wwwroot\\", "/") + "/" + modelLOC_State.File.FileName;

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))

                {
                    modelLOC_State.File.CopyTo(stream);
                }

            }


            String str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            if (modelLOC_State.StateID == null)
            {
                cmd.CommandText = "PR_LOC_State_Insert";
                cmd.Parameters.Add("@CreationDate", SqlDbType.DateTime).Value = DBNull.Value;
                cmd.Parameters.Add("@PhotoPath", SqlDbType.NVarChar).Value = modelLOC_State.PhotoPath;
            }
            else
            {
                cmd.CommandText = "PR_LOC_state_UpdateByPK";
                cmd.Parameters.Add("@StateID", SqlDbType.Int).Value = modelLOC_State.StateID;
                cmd.Parameters.Add("@PhotoPath", SqlDbType.NVarChar).Value = modelLOC_State.PhotoPath;
            }
            cmd.Parameters.Add("@StateName", SqlDbType.NVarChar).Value = modelLOC_State.StateName;
            cmd.Parameters.Add("@StateCode", SqlDbType.NVarChar).Value = modelLOC_State.StateCode;
            cmd.Parameters.Add("@ModificationDate", SqlDbType.DateTime).Value = DBNull.Value;
            cmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = modelLOC_State.CountryID;

            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                if (modelLOC_State.StateID == null)
                {
                    TempData["StateInsertMsg"] = "Record Inserted Successfully!!!";
                }
                else
                {
                    TempData["StateInsertMsg"] = "Record Updated Successfully!!!";
                }
            }
            
            conn.Close();
            return RedirectToAction("Add");
;        }
        public ActionResult Search(int StateID)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
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
