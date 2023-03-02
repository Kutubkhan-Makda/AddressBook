using AddressBook.DAL;
using AddressBook.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;

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
            String str = this.Configuration.GetConnectionString("myConnectionString");
            LOC_DAL dalLOC = new LOC_DAL();
            DataTable dt = dalLOC.dbo_PR_LOC_City_SelectAll(str);
            return View("LOC_CityList", dt);
        }

        public ActionResult Delete(int CityID)
        {
            String str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_City_DeleteByPK";
            cmd.Parameters.AddWithValue("@CityID", CityID);
            DataTable dt = new DataTable();
            cmd.ExecuteNonQuery();
            conn.Close();

            return RedirectToAction("Index");
        }
        public IActionResult Add(int? CityID)
        {
            
            String connectionstr1 = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt2 = new DataTable();
            SqlConnection conn2 = new SqlConnection(connectionstr1);
            conn2.Open();
            SqlCommand cmd2 = conn2.CreateCommand();
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.CommandText = "PR_LOC_Country_SelectForDropDown";
            SqlDataReader objSDR2 = cmd2.ExecuteReader();
            dt2.Load(objSDR2);
            List<LOC_CountryDropDownModel> list1 = new List<LOC_CountryDropDownModel>();
            foreach (DataRow dr in dt2.Rows)
            {
                LOC_CountryDropDownModel modelLOC_CountryDropDown = new LOC_CountryDropDownModel();
                modelLOC_CountryDropDown.CountryID = (Convert.ToInt32(dr["CountryID"]));
                modelLOC_CountryDropDown.CountryName = (Convert.ToString(dr["CountryName"]));
                list1.Add(modelLOC_CountryDropDown);
            }
            ViewBag.CountryList = list1;
            conn2.Close();
            List<LOC_StateDropDownModel> list = new List<LOC_StateDropDownModel>();
            ViewBag.StateList = list;

            if (CityID != null)
            {
                String str = this.Configuration.GetConnectionString("myConnectionString");
                SqlConnection conn = new SqlConnection(str);
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PR_LOC_City_SelectByPK";
                cmd.Parameters.Add("@CityID", SqlDbType.Int).Value = CityID;
                DataTable dt = new DataTable();
                SqlDataReader objSDR = cmd.ExecuteReader();
                dt.Load(objSDR);
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
                        modelLOC_City.PhotoPath = (Convert.ToString(dr["PhotoPath"]));
                    }
                    String connectionstr = this.Configuration.GetConnectionString("myConnectionString");


                    SqlConnection conn1 = new SqlConnection(connectionstr);
                    conn1.Open();
                    SqlCommand cmd1 = conn1.CreateCommand();
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.CommandText = "PR_LOC_State_SelectForDropDownByCountryID";
                    cmd1.Parameters.AddWithValue("@CountryID", modelLOC_City.CountryID);
                    DataTable dt1 = new DataTable();
                    SqlDataReader objSDR1 = cmd1.ExecuteReader();
                    dt1.Load(objSDR1);

                    List<LOC_StateDropDownModel> list6 = new List<LOC_StateDropDownModel>();
                    foreach (DataRow dr in dt1.Rows)
                    {
                        LOC_StateDropDownModel vl = new LOC_StateDropDownModel();
                        vl.StateID = (Convert.ToInt32(dr["StateID"]));
                        vl.StateName = (Convert.ToString(dr["StateName"]));
                        list6.Add(vl);
                    }
                    ViewBag.StateList = list;

                    conn.Close();
                    return View("LOC_CityAddEdit", modelLOC_City);
                }
            }
            return View("LOC_CityAddEdit");
        }
        [HttpPost]
        public ActionResult Save(LOC_CityModel modelLOC_City)
        {

            if (modelLOC_City.File != null)
            {
                string FilePath = "wwwroot\\Upload";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                string fileNameWithPath = Path.Combine(path, modelLOC_City.File.FileName);
                modelLOC_City.PhotoPath = "~" + FilePath.Replace("wwwroot\\", "/") + "/" + modelLOC_City.File.FileName;

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))

                {
                    modelLOC_City.File.CopyTo(stream);
                }

            }


            String str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            if (modelLOC_City.CityID == null)
            {
                cmd.CommandText = "PR_LOC_City_Insert";
                cmd.Parameters.Add("@CreationDate", SqlDbType.DateTime).Value = DBNull.Value;
                cmd.Parameters.Add("@PhotoPath", SqlDbType.NVarChar).Value = modelLOC_City.PhotoPath;
            }
            else
            {
                cmd.CommandText = "PR_LOC_City_UpdateByPK";
                cmd.Parameters.Add("@CityID", SqlDbType.Int).Value = modelLOC_City.CityID;
                cmd.Parameters.Add("@PhotoPath", SqlDbType.NVarChar).Value = modelLOC_City.PhotoPath;
            }
            
            cmd.Parameters.Add("@CityName", SqlDbType.NVarChar).Value = modelLOC_City.CityName;
            cmd.Parameters.Add("@CityCode", SqlDbType.NVarChar).Value = modelLOC_City.CityCode; 
            cmd.Parameters.Add("@ModificationDate", SqlDbType.DateTime).Value = DBNull.Value;
            cmd.Parameters.Add("@StateID", SqlDbType.Int).Value = modelLOC_City.StateID;
            cmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = modelLOC_City.CountryID;
            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                if (modelLOC_City.CityID == null)
                {
                    TempData["CityInsertMsg"] = "Record Inserted Successfully!!!";
                }
                else
                {
                    TempData["CityInsertMsg"] = "Record Updated Successfully!!!";
                }
            }
            conn.Close();
            return RedirectToAction("Add");
        }
        public ActionResult DropDownByCountry(int CountryID)
        {
            String connectionstr = this.Configuration.GetConnectionString("myConnectionString");
           
           
            SqlConnection conn1 = new SqlConnection(connectionstr);
            conn1.Open();
            SqlCommand cmd1 = conn1.CreateCommand();
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.CommandText = "PR_LOC_State_SelectForDropDownByCountryID";
            cmd1.Parameters.AddWithValue("@CountryID", CountryID);
            DataTable dt1 = new DataTable();
            SqlDataReader objSDR1 = cmd1.ExecuteReader();
            dt1.Load(objSDR1);
           
            List<LOC_StateDropDownModel> list = new List<LOC_StateDropDownModel>();
            foreach (DataRow dr in dt1.Rows)
            {
                LOC_StateDropDownModel vl = new LOC_StateDropDownModel();
                vl.StateID = (Convert.ToInt32(dr["StateID"]));
                vl.StateName = (Convert.ToString(dr["StateName"]));
                list.Add(vl);
            }
           

            var vmodel = list;
            return Json(vmodel);
        }
        public ActionResult Search(int CityID)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
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
